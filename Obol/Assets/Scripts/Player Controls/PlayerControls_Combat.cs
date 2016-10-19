using UnityEngine;
using System.Collections;

public class PlayerControls_Combat : MonoBehaviour {

	public NavMeshAgent _agent;
	public float _range;
	public Animator _anim;
	public Vector3 _objectPos;
	public Shooting _shooting;
	public LayerMask _layerMask;
	public bool _moving;
	public bool _firing;
	public string _target;
	public Combat_UI _ui;
	public Transform _textSpawn;
	public GameObject _indicator;
	public float _healTimer = 0.1f;

	// Use this for initialization
	void Start () {		
		Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		DetectInput();
	}

	void Spawn(){
		_indicator = GameObject.Find("Indicator");
		_textSpawn = transform.Find("TextSpawn");
		_ui = GameObject.Find("UI").GetComponent<Combat_UI>();
		_anim = gameObject.GetComponentInChildren<Animator>();
		_shooting = transform.FindChild("Launcher").GetComponent<Shooting>();
		_agent = gameObject.GetComponent<NavMeshAgent>();
		_agent.enabled = true;
	}

	void DetectInput(){
		DetectMove();
		DetectAim();
		Heal();		
	}

	void DetectMove(){
		if (Input.GetMouseButton(0)){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100f, _layerMask) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()){
				if (hit.collider.tag == "Ground"){
					float dist = Vector3.Distance(hit.point, transform.position);
					if (dist > 1.0f){
						_agent.SetDestination(hit.point);
						_anim.SetBool("Aim", false);
						_anim.SetBool("Running", true);
						_moving = true;
					}
				}		
			}
		}
		if (_moving){
			float dist = Vector3.Distance(transform.position, _agent.destination);
			if (dist <= 0.3f){
				_agent.SetDestination(transform.position);
				_anim.SetBool("Running", false);
				_moving = false;
			}
		}
	}

	void DetectAim(){
		if (Input.GetMouseButton(1)){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100f, _layerMask)){
				if (hit.collider.tag == "Ground" || hit.collider.tag == "Resource" || hit.collider.tag == "Enemy" || hit.collider.tag == "Destructible"){
					if (hit.collider.tag != "Resource" && hit.collider.tag != "Destructible"){
						_indicator.SetActive(true);
						_indicator.transform.position = hit.point;	
					}
					else{
						_indicator.SetActive(false);
					}						
					_agent.SetDestination(transform.position);
					_anim.SetBool("Running", false);
					_anim.SetBool("Aim", true);
					_moving = false;
					Quaternion newRotation = Quaternion.LookRotation(hit.point - transform.position);
					newRotation.x = 0f;
       				newRotation.z = 0f;
        			transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);
        			if (Input.GetMouseButton(0) && !_firing){
        				Shoot(hit.collider.gameObject, hit.point);
        			}
        			if (Input.GetMouseButtonDown(0) && !_firing){
        				Shoot(hit.collider.gameObject, hit.point);
        			}
				}					
			}			
		}		
		if (Input.GetMouseButtonUp(1)){
			_anim.SetBool("Aim", false);
			_indicator.SetActive(false);
		}	
	}

	void Shoot(GameObject go, Vector3 target){
		if (go.tag == "Ground"){
        	_shooting.CalcVelocity(target);
        	StartCoroutine(FireRate());       					
        }
        else if (go.tag == "Resource" || go.tag == "Destructible"){
       		var h = 3 + go.transform.position.y;
       		var _aimTarget = new Vector3(go.transform.position.x, h, go.transform.position.z);
       		_shooting.CalcVelocity(_aimTarget);
       		StartCoroutine(FireRate());
       	}
        else if (go.tag == "Enemy"){
        	if (go.name == "Warden_Parent"){
        		_shooting.CalcVelocity(go.transform.GetChild(1).position);
        	}
        	else{
        		_shooting.CalcVelocity(go.transform.parent.position);
        	}        	
        	StartCoroutine(FireRate());
        }		
	}

	public IEnumerator FireRate(){
		_firing = true;
		yield return new WaitForSeconds(_CombatManager._equipRanged._fireRate);
		_firing = false;
	}

	public void BeenHit(int damage){
		_CombatManager._currentHealth -= damage;
		_ui.DamageText(_textSpawn, damage, true);
		_ui.UpdateUI();
		if (_CombatManager._currentHealth <= 0){
			_agent.Stop();
			_anim.SetBool("Dead", true);
			_ui.GameOver(false);
		}
	}

	void Heal(){
		if (!_moving && !_firing){
			if (_CombatManager._currentHealth < _CombatManager._maxHealth){
				_healTimer -= Time.deltaTime;
				if (_healTimer <= 0){
					_CombatManager._currentHealth ++;
					_healTimer = 0.1f;
					_ui.UpdateUI();
				}
			}			
		}		
	}
}
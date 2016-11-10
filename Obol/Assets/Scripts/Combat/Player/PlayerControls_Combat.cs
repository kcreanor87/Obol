using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	public bool _moveToNPC;
	public List <Transform> _startPos = new List <Transform>();

	public float _armour;

	public List <GameObject> _weaponGOs = new List <GameObject>();
	public List <GameObject> _helmGOs = new List <GameObject>();
	public List <GameObject> _chestGOs = new List <GameObject>();
	public List <GameObject> _legGOs = new List <GameObject>();

	// Use this for initialization
	void Start () {		
		Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		DetectInput();
	}

	void Spawn(){
		UpdateMesh();
		transform.position = _startPos[_manager._portal].position;
		_indicator = GameObject.Find("AimIndicator");
		_textSpawn = transform.Find("TextSpawn");
		_ui = GameObject.Find("UI").GetComponent<Combat_UI>();
		_anim = gameObject.GetComponentInChildren<Animator>();
		_shooting = transform.FindChild("Launcher").GetComponent<Shooting>();
		_agent = gameObject.GetComponent<NavMeshAgent>();
		_agent.enabled = true;
		_agent.speed = (_CombatManager._speed / 10.0f);
		_anim.SetFloat("Speed", (_CombatManager._speed / 10.0f));
		_armour = ((1000 - _CombatManager._armourRating) / 1000.0f);
	}

	public void UpdateMesh(){
		for (int i = 0; i < _weaponGOs.Count; i++){
			_weaponGOs[i].SetActive(_CombatManager._itemsEquipped[0] == i);
		}
		for (int i = 0; i < _helmGOs.Count; i++){
			_helmGOs[i].SetActive(_CombatManager._itemsEquipped[1] == (i + 3));
		}
		for (int i = 0; i < _chestGOs.Count; i++){
			_chestGOs[i].SetActive(_CombatManager._itemsEquipped[2] == (i + 7));
		}
		for (int i = 0; i < _legGOs.Count; i++){
			_legGOs[i].SetActive(_CombatManager._itemsEquipped[3] == (i + 11));
		}
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
				_ui.OpenCanvas(1);
				_anim.speed = 1.0f;
				if (hit.collider.tag == "Ground"){
					float dist = Vector3.Distance(hit.point, transform.position);
					if (dist > 1.0f){
						_agent.SetDestination(hit.point);
						_anim.SetBool("Running", true);
						_moving = true;
						_moveToNPC = false;
					}
				}
				else if (hit.collider.tag == "NPC"){
					_agent.SetDestination(hit.transform.FindChild("PlayerPos").position);
					_moveToNPC = true;
					_anim.SetBool("Running", true);
					_moving = true;
				}	
			}
		}
		if (_moving){
			float dist = Vector3.Distance(transform.position, _agent.destination);
			if (dist <= 0.3f){
				_agent.SetDestination(transform.position);
				_anim.SetBool("Running", false);
				_moving = false;
				if (_moveToNPC){
					_ui.OpenCanvas(0);
					Time.timeScale = 0.0f;
					_moveToNPC = false;
				}
			}
		}
	}

	void DetectAim(){
		if (Input.GetMouseButton(1)){
			_moveToNPC = false;
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100f, _layerMask)){
				if (hit.collider.tag == "Ground" || hit.collider.tag == "Resource" || hit.collider.tag == "Enemy" || hit.collider.tag == "Destructible"){
					if (hit.collider.tag == "Ground" || hit.collider.tag == "Enemy"){
						_indicator.SetActive(hit.collider.name != "Warden_Parent");	
						_indicator.transform.position = hit.point;					
					}
					else{
						_indicator.SetActive(false);							
					}						
					_agent.SetDestination(transform.position);
					_anim.SetBool("Running", false);
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
			_anim.SetBool("Attack", false);
			_indicator.SetActive(false);
		}	
	}

	void Shoot(GameObject go, Vector3 target){
		var dist = Vector3.Distance(transform.position, target);	
		if (go.tag == "Ground"){
			if (dist <= 6.0f){
				_shooting.ShootStraight(target);
			}
			else{
        		_shooting.CalcVelocity(target);
        	}
        	StartCoroutine(FireRate());       					
        }
        else if (go.tag == "Resource" || go.tag == "Destructible"){
       		var h = 1.0f + go.transform.position.y;
       		var _aimTarget = new Vector3(go.transform.position.x, h, go.transform.position.z);
       		_shooting.CalcVelocity(_aimTarget);
       		StartCoroutine(FireRate());
       	}
        else if (go.tag == "Enemy"){
        	if (go.name == "Warden_Parent"){
        		if (dist <= 10.0f){
        			_shooting.ShootStraight(go.transform.GetChild(1).position);
        		}
        		else{
        			_shooting.CalcVelocity(go.transform.GetChild(1).position);
        		}
        		
        	}
        	else if (dist <= 7.0f){
        		_shooting.ShootStraight(go.transform.parent.position);
        	}
        	else{
        		_shooting.CalcVelocity(go.transform.parent.position);
        	}        	
        	StartCoroutine(FireRate());
        }	
        _anim.SetBool("Attack", true);	
	}

	public IEnumerator FireRate(){
		_firing = true;
		switch(_CombatManager._equipRanged._id){
			case 200:
			_anim.speed = 1.0f;
			break;
			case 201:
			_anim.speed = 0.17f;
			break;
			case 202:
			_anim.speed = 2.5f;
			break;
			case 203:
			_anim.speed = .71f;
			break;
		}
		yield return new WaitForSeconds(_CombatManager._equipRanged._fireRate);
		_anim.speed = 1.0f;
		_firing = false;
		_anim.SetBool("Attack", false);
	}

	public void BeenHit(int damage){
		var dam = Mathf.FloorToInt((float) damage * _armour);
		_CombatManager._currentHealth -= dam;
		_ui.DamageText(_textSpawn, dam, true);
		_ui.UpdateUI();
		if (_CombatManager._currentHealth <= 0){
			_agent.Stop();
			_anim.SetBool("Dead", true);
			_ui.GameOver();
		}
	}

	void Heal(){
		if (_CombatManager._currentHealth < _CombatManager._maxHealth){
			_healTimer -= Time.deltaTime;
			if (_healTimer <= 0){
				_CombatManager._currentHealth += (!_moving && !_firing) ? 2 : 1;
				if (_CombatManager._currentHealth > _CombatManager._maxHealth) _CombatManager._currentHealth = _CombatManager._maxHealth;
				_healTimer = 0.1f;
				_ui.UpdateUI();
			}			
		}		
	}
}
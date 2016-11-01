using UnityEngine;
using System.Collections;

public class PlayerControls_NonCombat : MonoBehaviour {

	public NavMeshAgent _agent;
	public float _range;
	public Animator _anim;
	public Vector3 _objectPos;
	public Shooting _shooting;
	public LayerMask _layerMask;
	public bool _moving;
	public bool _firing;
	public string _target;
	public NonCombat_UI _ui;
	public Transform _textSpawn;
	public GameObject _indicator;
	public float _healTimer = 0.1f;
	public Transform _posA, _posB;
	public SaveGame _saveGame;
	public bool _moveToNPC;
	public int _npcIndex;
	public Smith _smith;

	// Use this for initialization
	void Awake () {		
		Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		DetectInput();
	}

	void Spawn(){
		_smith = GameObject.Find("SmithScreen").GetComponent<Smith>();
		transform.position = NewGame._newGame ? _posA.position : _posB.position;
		_indicator = GameObject.Find("Indicator");
		_textSpawn = transform.Find("TextSpawn");
		_ui = GameObject.Find("Non-Combat UI").GetComponent<NonCombat_UI>();
		_anim = gameObject.GetComponentInChildren<Animator>();
		_shooting = transform.FindChild("Launcher").GetComponent<Shooting>();
		_agent = gameObject.GetComponent<NavMeshAgent>();
		_agent.enabled = true;
		_saveGame = GameObject.Find("Loader").GetComponent<SaveGame>();
		_saveGame.Save();
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
				_agent.speed = _CombatManager._speed;
				if (hit.collider.tag == "Ground"){
					float dist = Vector3.Distance(hit.point, transform.position);
					if (dist > 1.0f){
						_agent.SetDestination(hit.point);
						_anim.SetBool("Running", true);
						_moving = true;
						_moveToNPC = false;
						_ui.CloseCanvas(_npcIndex);
					}
				}
				else if (hit.collider.tag == "NPC"){
					_agent.SetDestination(hit.transform.FindChild("PlayerPos").position);
					_moveToNPC = true;
					_anim.SetBool("Running", true);
					_moving = true;
					_ui.CloseCanvas(_npcIndex);
					switch (hit.collider.name){
						case "Merchant":
						_npcIndex = 1;
						break;
						case "Smith":
						_npcIndex = 2;
						break;
						case "Priest":
						_npcIndex = 3;
						break;
						case "Librarian":
						_npcIndex = 4;
						break;
						case "Thief":
						_npcIndex = 5;
						break;
						case "Portal":
						_npcIndex = 6;
						break;
					}
				}		
			}
		}
		if (_moving){
			float dist = Vector3.Distance(transform.position, _agent.destination);
			if (dist <= 0.3f){
				Stop();
				if (_moveToNPC){
					_ui.OpenCanvas(_npcIndex);
					_moveToNPC = false;
				}
			}
		}
	}

	void Stop(){
		_agent.SetDestination(transform.position);
		_anim.SetBool("Running", false);
		_moving = false;
	}

	void DetectAim(){
		if (Input.GetMouseButton(1)){
			Stop();
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100f, _layerMask)){
				if (hit.collider.tag == "Ground" || hit.collider.tag == "Enemy" || hit.collider.tag == "NPC"){
					if (hit.collider.tag == "Ground"){
						_indicator.SetActive(true);
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
			_indicator.SetActive(false);

		}	
	}

	void Shoot(GameObject go, Vector3 target){
		var dist = Vector3.Distance(transform.position, target);	
		if (go.tag == "Ground"){
			if (dist <= 4.0f){
				_shooting.ShootStraight(target);
			}
			else{
        		_shooting.CalcVelocity(target);
        	}
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
        		if (dist <= 10.0f){
        			_shooting.ShootStraight(go.transform.GetChild(1).position);
        		}
        		else{
        			_shooting.CalcVelocity(go.transform.GetChild(1).position);
        		}
        		
        	}
        	else if (dist <= 5.0f){
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
		yield return new WaitForSeconds(_CombatManager._equipRanged._fireRate);
		_firing = false;
		_anim.SetBool("Attack", false);
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
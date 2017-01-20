using UnityEngine;
using System.Collections;

public class PlayerControls_NonCombat : MonoBehaviour {

	public NavMeshAgent _agent;
	public Animator _animBody;
	public Animator _animArms;
	public Vector3 _objectPos;
	public Shooting _shooting;
	public LayerMask _layerMask;
	public NonCombat_UI _ui;
	public Transform _textSpawn;
	public Transform _posA, _posB;
	public GameObject _indicator;	
	public SaveGame _saveGame;

	public string _target;
	public int _npcIndex;
	public float _range;
	public float _healTimer = 0.1f;
	public bool _moving;
	public bool _firing;	
	public bool _moveToNPC;	

	public bool _placing;
	public int _aimTimer;

	public Transform _body;

	public bool _aiming;

	void Awake () {		
		Spawn();
	}
	
	void Update () {
		if (!_ui._paused && !_placing) DetectInput();
	}

	void Spawn(){
		_indicator = GameObject.Find("Indicator");		
		_saveGame = GameObject.Find("Loader").GetComponent<SaveGame>();
		_ui = GameObject.Find("Non-Combat UI").GetComponent<NonCombat_UI>();
		_animBody = GameObject.Find("Player_Body").GetComponent<Animator>();
		_agent = gameObject.GetComponent<NavMeshAgent>();
		_textSpawn = transform.Find("TextSpawn");
		_body = transform.FindChild("BodyParent");
		_animArms = _body.FindChild("Player_Arms").GetComponent<Animator>();
		_shooting = _body.FindChild("Launcher").GetComponent<Shooting>();		
		_agent.enabled = true;	

	}

	void DetectInput(){
		if (!_ui._inChat) DetectMoveController();
		if (_ui._inChat){
			if (Input.GetButton("Cancel")){
				_moving = false;
				_ui.OpenCanvas(_npcIndex);
			}	
		}
		DetectControllerAim();
		Heal();		
	}

	void DetectMoveController(){
		float spdX = Mathf.Abs(Input.GetAxis("Horizontal"));
  		float spdY = Mathf.Abs(Input.GetAxis("Vertical"));
		if (spdX >= 0.1f || spdY >= 0.1f){
			var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			Quaternion newRotation = Quaternion.LookRotation(dir);
			newRotation.x = 0f;
  		   	newRotation.z = 0f;
  		    transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10.0f);
  		    _agent.SetDestination(transform.position + transform.forward);  		    
  		    _agent.speed = _CombatManager._speed * Mathf.Max(spdX, spdY) / 10.0f;
  		    _moving = true;
  		    if (!_aiming){
  		    	Quaternion bodyRotation = Quaternion.LookRotation((transform.position + transform.forward) - _body.position);
				bodyRotation.x = 0f;
       			bodyRotation.z = 0f;
        		_body.rotation = Quaternion.Slerp(_body.rotation, bodyRotation, Time.deltaTime * 15.0f);        		
  		    }
		}	
		else{
			Stop();
		}
		AnimateRun(spdX, spdY);
	}

	void Stop(){
		_agent.SetDestination(transform.position);
		_animBody.SetFloat("DirectionX", 0.0f);
		_animBody.SetFloat("DirectionY", 0.0f);
		_moving = false;
	}
	void DetectControllerAim(){
		float dirX = Input.GetAxis("HorizontalRight");
		float dirY = Input.GetAxis("VerticalRight");
		if (Mathf.Abs(dirX) >= 0.1f || Mathf.Abs(dirY) >= 0.1f){
			_aiming = true;
			var posX = Screen.width * dirX / 2 + Screen.width/2;
			var posY = Screen.height * dirY / 2 + Screen.height/2;
			var pos = new Vector2(posX, posY);
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(pos);
			if (Physics.Raycast(ray, out hit, 100f, _layerMask)){
				if (hit.collider.tag == "Ground" || hit.collider.tag == "Enemy" || hit.collider.tag == "NPC"){
					if (hit.collider.tag == "Ground"){
						_indicator.SetActive(true);
      					_indicator.transform.position = hit.point;
					}
					else{
						_indicator.SetActive(false);
					}
					Quaternion newRotation = Quaternion.LookRotation(hit.point - _body.position);
					newRotation.x = 0f;
       				newRotation.z = 0f;
        			_body.rotation = Quaternion.Slerp(_body.rotation, newRotation, Time.deltaTime * 10);
        			if (Input.GetAxisRaw("Fire1") < 0.0f && !_firing){
        				Shoot(hit.collider.gameObject, hit.point);
        			}
        			if (Input.GetAxisRaw("Fire1") < 0.0f && !_firing){
        				Shoot(hit.collider.gameObject, hit.point);
        			}
				}			
			}			
		}
		else{
			_indicator.SetActive(false);
			_aiming = false;
		}
	}

	void AnimateRun(float x, float y){		
		_animArms.SetBool("Running", (Mathf.Max(x, y) > 0.1f));
		if (!_aiming){
			_animBody.SetFloat("DirectionY", Mathf.Max(x, y));
			_animBody.SetFloat("DirectionX", 0.0f);
			_animBody.speed = Mathf.Max(x, y);			
		}
		else{
			Vector3 targetDir = transform.position - _indicator.transform.position;
			float angle = Vector3.Angle(transform.forward, targetDir);
			float speed = angle/180;
			float baseSpd = _agent.speed * speed * 0.3f;
			_agent.speed = _agent.speed * 0.7f + baseSpd;
			if (angle < 20.0f){
				_animBody.SetFloat("DirectionY",  -1 * Mathf.Max(x, y));
			}
			else if (angle > 160.0f){
				_animBody.SetFloat("DirectionY", Mathf.Max(x, y));
			}
			else if (angle < 90.0f){
				angle = Vector3.Angle(transform.right, targetDir);
				speed = (angle - 90.0f) / 90.0f;
				float dir = 1.0f - (Mathf.Abs(speed));
				_animBody.SetFloat("DirectionX", speed);
				_animBody.SetFloat("DirectionY", dir);
			}
			else{
				angle = Vector3.Angle(transform.right, targetDir);
				speed = (angle - 90.0f) / 90.0f;
				float dir = ( 1.0f - (Mathf.Abs(speed))) * -1;
				_animBody.SetFloat("DirectionX", speed);
				_animBody.SetFloat("DirectionY", dir);
			}
			_animBody.speed = Mathf.Max(x, y);
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
	}

	public IEnumerator FireRate(){
		_firing = true;		
		_animArms.SetBool("Shooting", true);
		yield return new WaitForSeconds(_CombatManager._equipRanged._fireRate);
		_firing = false;

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
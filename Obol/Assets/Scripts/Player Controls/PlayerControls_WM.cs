using UnityEngine;
using System.Collections;

public class PlayerControls_WM : MonoBehaviour {

	public NavMeshAgent _agent;
	public bool _movingToTown;
	public bool _movingToHarbour;
	public float _range;
	public static bool _inMenu;
	public Animator _anim;
	public Vector3 _objectPos;
	public TownCanvas _townCanvas;
	public Shooting _shooting;
	public LayerMask _layerMask;
	public bool _moving;
	public string _target;
	public bool _firing;

	// Use this for initialization
	void Start () {		
		Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		if (!_inMenu){
			DetectInput();
			MoveToObject();
		}
	}

	void Spawn(){
		_townCanvas = GameObject.Find("TownCanvas").GetComponent<TownCanvas>();
		_anim = gameObject.GetComponentInChildren<Animator>();
		_shooting = transform.FindChild("Launcher").GetComponent<Shooting>();
		_agent = gameObject.GetComponent<NavMeshAgent>();
		_agent.enabled = true;
		_agent.Stop();
		_inMenu = false;	
	}

	void DetectInput(){
		DetectMove();
		DetectAim();		
	}

	void DetectMove(){
		if (Input.GetMouseButton(0)){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100f, _layerMask) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()){
				if (hit.collider.tag == "Ground"){
					float dist = Vector3.Distance(hit.point, transform.position);
					if (dist > 1.0f){
						_agent.Resume();
						_agent.SetDestination(hit.point);
						_anim.SetBool("Aim", false);
						_anim.SetBool("Running", true);
						_moving = true;
					}
				}		
			}
		}
		if (Input.GetMouseButtonDown(0)){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100f, _layerMask) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()){
				if (hit.collider.tag == "Town"){
					_objectPos = hit.transform.FindChild("Entrance").position;
					_agent.SetDestination(_objectPos);
					_agent.Resume();
					_movingToTown = true;
					_movingToHarbour = false;
					_range = 1.0f;
					_target = hit.collider.name;
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
			if (Physics.Raycast(ray, out hit, 100f, _layerMask) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()){
				if (hit.collider.tag == "Ground" || hit.collider.tag == "Resource" || hit.collider.tag == "Enemy"){
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
		}		
	}

	void Shoot(GameObject go, Vector3 target){
		if (go.tag == "Ground"){
        	var dist = Vector3.Distance(target, transform.position);
        	if (dist > 2.2f){
        		_shooting.CalcVelocity(target);
        		StartCoroutine(FireRate());
        	}        					
        }
        else if (go.tag == "Resource"){
       		var h = 5 + go.transform.position.y;
       		var _aimTarget = new Vector3(go.transform.position.x, h, go.transform.position.z);
       		_shooting.CalcVelocity(_aimTarget);
       		StartCoroutine(FireRate());
       	}
        else if (tag == "Enemy"){
        	_shooting.CalcVelocity(go.transform.position);
        	StartCoroutine(FireRate());
        }		
	}

	public IEnumerator FireRate(){
		_firing = true;
		yield return new WaitForSeconds(_CombatManager._equipRanged._fireRate);
		_firing = false;
	}

	void MoveToObject(){
		if (_movingToTown){
			float distance = Vector3.Distance(transform.position, _objectPos);
			if (distance <= _range){
				_townCanvas.OpenCanvas(_target);
				_agent.SetDestination(transform.position);
				_agent.Stop();
				_movingToTown = false;
				_agent.ResetPath();
			}
		}
	}
}
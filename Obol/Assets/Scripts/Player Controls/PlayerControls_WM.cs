using UnityEngine;

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
        			if (Input.GetMouseButtonDown(0)){
        				if (hit.collider.tag == "Ground"){
        					var dist = Vector3.Distance(hit.point, transform.position);
        					if (dist > 2.2f){
        						Shoot(hit.point);
        					}        					
        				}
        				else if (hit.collider.tag == "Resource"){
        					var h = 5 + hit.collider.transform.position.y;
        					var target = new Vector3(hit.collider.transform.position.x, h, hit.collider.transform.position.z);
        					Shoot(target);
        				}
        				else if (hit.collider.tag == "Enemy"){
        					Shoot(hit.collider.transform.position);
        				}
        			}
				}					
			}			
		}
		if (Input.GetMouseButtonUp(1)){
			_anim.SetBool("Aim", false);
		}	
	}

	void Shoot(Vector3 target){
		_shooting.CalcVelocity(target);
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
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyWM : MonoBehaviour {

	public int _health = 100;

	public NavMeshAgent _agent;
	public PlayerControls_WM _player;
	public PlayerSpotted _playerSpot;
	public SpawnPoint _thisSpawn;	
	public bool _spotted;
	public bool _returning;
	public SaveGame _saveGame;
	public float _chaseDistance = 70.0f;	

	void Start(){	
		_playerSpot = gameObject.GetComponentInChildren<PlayerSpotted>();
		_saveGame = GameObject.Find("Loader").GetComponent<SaveGame>();
		_agent = gameObject.GetComponent<NavMeshAgent>();
		_player = GameObject.Find("Player").GetComponent<PlayerControls_WM>();

	}

	public void ChasePlayer(){
		print("Player Spotted");
		_spotted = true;
		_agent.enabled = true;
		StartCoroutine(ChaseLoop(0.5f));
	}

	public IEnumerator ChaseLoop(float looptime){
		while (_spotted){
			_agent.SetDestination(_player.transform.position);
			var distance = Vector3.Distance(transform.position, _thisSpawn.transform.position);
			_spotted = (distance < _chaseDistance);
			_returning = (distance > _chaseDistance);
			yield return new WaitForSeconds(looptime);
		}		
		if (_returning){			
			_agent.SetDestination(_thisSpawn.transform.position);
			StartCoroutine(Returning());
		} 		
	}

	public void AtPlayer(){
		print("Combat!");
	}

	public IEnumerator Returning(){
		while (_returning){
			var distance = Vector3.Distance(transform.position, _thisSpawn.transform.position);
			_returning = (distance >= 2.0f);
			yield return new WaitForSeconds(1.0f);
		}
		_playerSpot._chasing = false;
	}

	public void BeenHit(int damage){
		_health -= damage;
		if (_health <= 0){
			Destroy(gameObject);
		}
	}
}

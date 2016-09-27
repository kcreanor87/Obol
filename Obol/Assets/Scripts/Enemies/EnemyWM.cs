using UnityEngine;
using System.Collections;

public class EnemyWM : MonoBehaviour {

	public int _health = 100;

	public NavMeshAgent _agent;
	public PlayerControls_Combat _player;
	public CombatCounters _counter;
	public PlayerSpotted _playerSpot;
	public SpawnPoint _thisSpawn;	
	public bool _spotted;
	public bool _returning;
	public float _chaseDistance = 70.0f;	

	void Start(){	
		_playerSpot = gameObject.GetComponentInChildren<PlayerSpotted>();
		_agent = gameObject.GetComponent<NavMeshAgent>();
		_player = GameObject.Find("Player").GetComponent<PlayerControls_Combat>();
		_counter = GameObject.Find("Counters").GetComponent<CombatCounters>();
		_agent.enabled = true;
		ChasePlayer();		
	}

	public void ChasePlayer(){
		StartCoroutine(ChaseLoop(0.5f));
	}

	public IEnumerator ChaseLoop(float looptime){
		_agent.SetDestination(_player.transform.position);
		yield return new WaitForSeconds(looptime);
		ChasePlayer();
	}

	public void AtPlayer(){
		print("Combat!");
	}

	public void BeenHit(int damage){
		_health -= damage;
		if (_health <= 0){
			_counter._enemiesKilled++;
			Destroy(gameObject);
		}
	}
}

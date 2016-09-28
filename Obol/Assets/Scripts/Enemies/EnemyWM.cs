using UnityEngine;
using System.Collections;

public class EnemyWM : MonoBehaviour {

	public int _health = 100;

	public NavMeshAgent _agent;
	public PlayerControls_Combat _player;
	public CombatCounters _counter;
	public Combat_UI _ui;

	void Start(){
		_agent = gameObject.GetComponent<NavMeshAgent>();
		_player = GameObject.Find("Player").GetComponent<PlayerControls_Combat>();
		_counter = GameObject.Find("Counters").GetComponent<CombatCounters>();
		_ui = GameObject.Find("UI").GetComponent<Combat_UI>();
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
			_ui.UpdateUI();
			Destroy(gameObject);
		}
	}
}

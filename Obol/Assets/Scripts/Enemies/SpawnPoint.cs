using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	public List <GameObject> _enemyDatabase = new List <GameObject>();
	public int _spawnChance = 50;
	public bool _spawnOnScreen;
	public EnemyWM _spawnScript;
	public CombatCounters _counterScript;

	void Start(){
		_counterScript = GameObject.Find("Counters").GetComponent<CombatCounters>();
		transform.FindChild("Indicator").gameObject.SetActive(false);
		CheckCurrentSpawn();
	}
	//Only run spawn RNG if the current enemy isn't currently chasing player, nor if the spawn is on screen
	void CheckCurrentSpawn(){
		//Is the spawn on screen? 
		var ScreenPos = Camera.main.WorldToScreenPoint(transform.position);
		_spawnOnScreen = ((ScreenPos.x <= Screen.width && ScreenPos.x >= 0) && (ScreenPos.y <= Screen.height && ScreenPos.y >= 0));
		//Did this spawn create an enemy recently?
		if (!_spawnOnScreen){			
			CheckRNG();
		}
		else{
			StartCoroutine(Timer(3.0f));
		}		
	}
	//Function to control the RNG that determines whether an enemy spawns or not
	void CheckRNG(){
		var chance = Random.Range(0, 101);
		if (chance <= _spawnChance){
			SpawnEnemy();
		}
		else{
			StartCoroutine(Timer(5.0f));
		}
	}
	//Spawn a random prefab from the editor-populated list, assign it as the actice script and set _generated bool
	void SpawnEnemy(){
		if (_counterScript._enemiesSpawned < _counterScript._totalEnemies){
			var enemyType = Random.Range(0, _enemyDatabase.Count);
			Instantiate(_enemyDatabase[enemyType], transform.position, Quaternion.identity);
			_counterScript._enemiesSpawned++;
			StartCoroutine(Timer(5.0f));
		}		
	}

	public IEnumerator Timer(float timer){
		yield return new WaitForSeconds(timer);
		CheckCurrentSpawn();
	}
}

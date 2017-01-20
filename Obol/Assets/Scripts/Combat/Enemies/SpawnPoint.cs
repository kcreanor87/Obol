using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	public List <GameObject> _enemyDatabase = new List <GameObject>();
	public int _spawnChance = 100;
	public bool _spawnOnScreen;
	public CombatCounters _counterScript;
	public int _spawned;
	public float _timer;
	public int _maxWave = 12;
	public int _level = 1;

	void Awake(){
		_timer = 10.0f;
		_counterScript = GameObject.Find("Counters").GetComponent<CombatCounters>();
		transform.FindChild("Indicator").gameObject.SetActive(false);
		CheckCurrentSpawn();
	}
	public void CheckCurrentSpawn(){
		CheckRNG();
	}
	//Function to control the RNG that determines whether an enemy spawns or not
	void CheckRNG(){
		var chance = Random.Range(0, 101);
		if (chance <= _spawnChance){
			_counterScript._enemiesSpawned++;
			SpawnEnemy();
		}
		else{
			StartCoroutine(Timer(_timer));
		}
	}
	//Spawn a random prefab from the editor-populated list
	void SpawnEnemy(){		
		var enemyType = Random.Range(0, _enemyDatabase.Count);
		var enemy = (GameObject) Instantiate(_enemyDatabase[enemyType], transform.position, Quaternion.identity);
		var enemyScript = enemy.GetComponent<EnemyAI>();
		enemyScript._level = _level;
		_spawned++;
		if (_spawned >= _maxWave){
			_timer = Random.Range(8.0f, 12.0f);
			_spawned = 0;
		}
		else{
			_timer = Random.Range(0.1f, 1.0f);
		}		
		StartCoroutine(Timer(_timer));		
	}

	public IEnumerator Timer(float timer){
		yield return new WaitForSeconds(_timer);
		CheckCurrentSpawn();
	}
	public void Stop(){
		StopAllCoroutines();
		_spawnChance = 0;
	}
}

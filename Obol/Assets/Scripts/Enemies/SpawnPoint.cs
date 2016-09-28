using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	public List <GameObject> _enemyDatabase = new List <GameObject>();
	public int _spawnChance = 100;
	public bool _spawnOnScreen;
	public EnemyWM _spawnScript;
	public CombatCounters _counterScript;

	void Start(){
		_counterScript = GameObject.Find("Counters").GetComponent<CombatCounters>();
		transform.FindChild("Indicator").gameObject.SetActive(false);
		CheckCurrentSpawn();
	}
	void CheckCurrentSpawn(){
		//Is the spawn on screen? 
		var ScreenPos = Camera.main.WorldToScreenPoint(transform.position);
		_spawnOnScreen = ((ScreenPos.x <= Screen.width && ScreenPos.x >= 0) && (ScreenPos.y <= Screen.height && ScreenPos.y >= 0));
		//Has the spawn limit been reached?
		if (!(_counterScript._enemiesSpawned >= _counterScript._totalEnemies)){
			if (!_spawnOnScreen){			
				CheckRNG();
			}
			else{
				var timer = Random.Range(4.0f, 5.0f);
				StartCoroutine(Timer(timer));
			}	
		}
	}
	//Function to control the RNG that determines whether an enemy spawns or not
	void CheckRNG(){
		var chance = Random.Range(0, 101);
		if (chance <= _spawnChance){
			_counterScript._enemiesSpawned++;
			SpawnEnemy();
		}
		else{
			var timer = Random.Range(4.0f, 5.0f);
			StartCoroutine(Timer(timer));
		}
	}
	//Spawn a random prefab from the editor-populated list
	void SpawnEnemy(){		
		var enemyType = Random.Range(0, _enemyDatabase.Count);
		Instantiate(_enemyDatabase[enemyType], transform.position, Quaternion.identity);			
		var timer = Random.Range(4.0f, 5.0f);
		StartCoroutine(Timer(timer));		
	}

	public IEnumerator Timer(float timer){
		yield return new WaitForSeconds(timer);
		CheckCurrentSpawn();
	}
}

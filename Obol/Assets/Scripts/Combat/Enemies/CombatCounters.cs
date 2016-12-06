using UnityEngine;
using System.Collections.Generic;

public class CombatCounters : MonoBehaviour {

	public int _enemiesSpawned;
	public int _enemiesKilled;
	public int _totalEnemies;
	public int _spawnPoints;

	void Start(){
		_spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point").Length;
		print (_spawnPoints);
	}
}

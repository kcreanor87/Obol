using UnityEngine;

public class CombatCounters : MonoBehaviour {

	public int _enemiesSpawned;
	public int _enemiesKilled;
	public int _totalEnemies;
	public int _spawnPoints;
	public int _totalSpawns;
	public int _obolsCollected;
	public int _xpGained;

	void Start(){
		_spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point").Length;
		_totalSpawns = _spawnPoints;
	}
}

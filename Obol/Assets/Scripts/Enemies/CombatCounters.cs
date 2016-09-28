using UnityEngine;
using System.Collections.Generic;

public class CombatCounters : MonoBehaviour {

	public int _resourcesAvailable;
	public int _resourcesCollected;
	public int _enemiesSpawned;
	public int _enemiesKilled;
	public int _totalEnemies = 100;

	public List <int> _resources = new List <int>();

	// Use this for initialization
	void Start () {
		_resourcesAvailable = GameObject.FindGameObjectsWithTag("Resource").Length * 10;
		_resources.Clear();
		_resources.Add(0);
		_resources.Add(0);
		_resources.Add(0);
		_resources.Add(0);
	}
}

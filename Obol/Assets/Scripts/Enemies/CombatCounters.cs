using UnityEngine;
using System.Collections;

public class CombatCounters : MonoBehaviour {

	public int _resourcesAvailable;
	public int _resourcesCollected;
	public int _enemiesSpawned;
	public int _enemiesKilled;
	public int _totalEnemies = 100;

	// Use this for initialization
	void Start () {
		_resourcesAvailable = GameObject.FindGameObjectsWithTag("Resource").Length * 30;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

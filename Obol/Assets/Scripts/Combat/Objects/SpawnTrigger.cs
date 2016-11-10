using UnityEngine;
using System.Collections.Generic;

public class SpawnTrigger : MonoBehaviour {

	public GameObject _spawns;
	public bool _triggered;
	public bool _exit;
	public SpawnTrigger _parentTrigger;
	public List<SpawnPoint> _spawnScripts = new List<SpawnPoint>();


	// Use this for initialization
	void Start () {	
		_spawns = (_exit) ? transform.parent.FindChild("Spawns").gameObject : transform.FindChild("Spawns").gameObject;
		if (_exit) _parentTrigger = transform.parent.GetComponent<SpawnTrigger>();
		_spawnScripts.AddRange(_spawns.GetComponentsInChildren<SpawnPoint>());
		_spawns.SetActive(false);
	}
	
	void OnTriggerEnter(Collider col){
		if (col.tag == "Player"){
			if (!_exit){
				if (!_triggered){
				_triggered = true;
				_spawns.SetActive(true);
				ActivateSpawns();
				}
			}
			else{
				_parentTrigger._triggered = false;
				_spawns.SetActive(false);
			}
		}		
	}

	void ActivateSpawns(){
		for (int i = 0; i < _spawnScripts.Count; i++){
			_spawnScripts[i].CheckCurrentSpawn();
		}
	}
}

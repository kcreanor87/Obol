using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretControls : MonoBehaviour {

	public List <GameObject> _enemiesInRange = new List <GameObject>();

	void OnTriggerEnter(Collider col){
		if (col.tag == "Enemy"){
			_enemiesInRange.Add(col.gameObject);
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Enemy"){
			_enemiesInRange.Remove(col.gameObject);
		}
	}
	
}

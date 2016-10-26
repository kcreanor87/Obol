using UnityEngine;
using System.Collections;

public class ManagerSpawn : MonoBehaviour {

	public GameObject _managerPrefab;
	public bool _newGame;

	// Use this for initialization
	void Awake () {

		if (GameObject.Find("_manager") == null){
			var _mPrefab =  (GameObject) Instantiate(_managerPrefab, transform.position, Quaternion.identity);
			_mPrefab.name = "_manager";
		}
		if (_newGame) NewGame._newGame = true;
	}
}

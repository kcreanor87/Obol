﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class PortalControls : MonoBehaviour {

	public GameObject _levels;	
	public GameObject _portals;

	public Button _travelButton;

	public List <GameObject> _activeLevels = new List <GameObject>();
	public List <GameObject> _activePortals = new List <GameObject>();

	public int _level;

	// Use this for initialization
	void Awake () {

		_levels = GameObject.Find("LevelSelect");
		_portals = GameObject.Find("PortalSelect");
		_travelButton = GameObject.Find("Travel").GetComponent<Button>();
		ResetPortals();
		for (int i = 0; i < _activeLevels.Count; i++){
			_activeLevels[i].SetActive(false);
		}
		for (int i = 0; i < _manager._activeLevels.Count; i++){
			_activeLevels[i].SetActive(_manager._activeLevels[i]);
		}
		_portals.SetActive(false);	
		_travelButton.interactable = false;
	}

	public void LevelSelect(int level){
		for (int i = 0; i <= _manager._activePortals[level]; i++){
			_activePortals[i].SetActive(true);
		}
		_levels.SetActive(false);
		_portals.SetActive(true);
		_level = level + 3;
	}

	public void Back(){
		_portals.SetActive(false);
		_levels.SetActive(true);
		_travelButton.interactable = false;
		ResetPortals();
	}
	
	public void PortalSelect(int portal){
		_manager._portal = portal;
		_travelButton.interactable = true;
	}

	public void Travel(){
		SaveResources();
		SaveLocations();
		SceneManager.LoadScene(_level);

	}

	void ResetPortals(){
		for (int i = 0; i < _activePortals.Count; i++){
			_activePortals[i].SetActive(false);
		}
	}

	void SaveResources(){
		for (int i = 0; i < _manager._resources.Count; i++){
			PlayerPrefs.SetInt("Resources" + i, _manager._resources[i]);
		}
		PlayerPrefs.SetInt("Obols", _manager._obols);
	}

	void SaveLocations(){
		for (int i = 2; i < _manager._activeLevels.Count; i++){
			PlayerPrefs.SetInt("Level" + i, _manager._activeLevels[i] ? 1 : 0);
		}
		for (int i = 0; i < _manager._activePortals.Count; i++){
			PlayerPrefs.SetInt("Level Portals" + i, _manager._activePortals[i]);
		}
	}
}
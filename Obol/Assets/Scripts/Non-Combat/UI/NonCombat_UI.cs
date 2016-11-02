﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NonCombat_UI : MonoBehaviour {

	public Text _boneTxt, _ironTxt, _silverTxt, _crystalTxt, _obols;
	public RectTransform _hpBar;
	public int _hpMax = 580;
	public GameObject _damageText;
	public Text _currentHP;
	public Text _maxHP;
	public SaveGame _saveGame;

	public List <GameObject> _canvases = new List <GameObject>();

	public Camera _smithCamera;
	public Smith _smithScript;

	public GameObject _pauseMenu;
	public bool _paused;

	// Use this for initialization
	void Start () {	
		_pauseMenu = GameObject.Find("PauseMenu");
		_pauseMenu.SetActive(false);
		_smithCamera = GameObject.Find("SmithCamera").GetComponent<Camera>();
		_smithCamera.enabled = false;
		_smithScript = GameObject.Find("SmithScreen").GetComponent<Smith>();
		_obols = GameObject.Find("CurrentObols").GetComponent<Text>();		
		_currentHP = GameObject.Find("CurrentHP").GetComponent<Text>();
		_maxHP = GameObject.Find("MaxHP").GetComponent<Text>();
		_hpBar = GameObject.Find("HP").GetComponent<RectTransform>();
		_boneTxt = GameObject.Find("BoneTxt").GetComponent<Text>();
		_ironTxt = GameObject.Find("IronTxt").GetComponent<Text>();
		_silverTxt = GameObject.Find("SilverTxt").GetComponent<Text>();
		_crystalTxt = GameObject.Find("CrystalTxt").GetComponent<Text>();
		_saveGame = GameObject.Find("Loader").GetComponent<SaveGame>();
		UpdateUI();
		for (int i = 0; i < _canvases.Count; i++){
			_canvases[i].SetActive(false);
		}
		_canvases[0].SetActive(true);
	}

	void Update(){
		PauseDetect();
	}

	void PauseDetect(){
		if (Input.GetKeyDown(KeyCode.Escape)){
			if (!_paused){
				PauseMenu(true);
			}
			else{
				PauseMenu(false);
			}			
		}
	}

	public void PauseMenu(bool paused){
		if (paused){
			_pauseMenu.SetActive(true);
			Time.timeScale = 0.0f;
			_paused = true;
		}
		else{
			_pauseMenu.SetActive(false);
			Time.timeScale = 1.0f;
			_paused = false;
		}
	}

	public void UpdateUI(){
		_obols.text = _manager._obols.ToString();
		_boneTxt.text = _manager._resources[0].ToString();
		_ironTxt.text = _manager._resources[1].ToString();
		_silverTxt.text = _manager._resources[2].ToString();
		_crystalTxt.text = _manager._resources[3].ToString();
		_currentHP.text = _CombatManager._currentHealth.ToString();
		_maxHP.text = _CombatManager._maxHealth.ToString();
	}

	public void OpenCanvas(int index){
		_canvases[0].SetActive(false);
		_canvases[index].SetActive(true);
		_smithCamera.enabled = (index == 2);
		if (index == 2) _smithScript.ToggleScreens(0);
	}

	public void CloseCanvas(int index){
		_smithCamera.enabled = false;
		_canvases[index].SetActive(false);
		_canvases[0].SetActive(true);
		_saveGame.Save();
		UpdateUI();
	}

	public void ExitGame(){
		Application.Quit();
	}
}

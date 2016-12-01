using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NonCombat_UI : MonoBehaviour {

	public Text _obols;
	public RectTransform _hpBar;
	public int _hpMax = 580;
	public GameObject _damageText;
	public Text _currentHP;
	public Text _maxHP;
	public SaveGame _saveGame;

	public Camera _smithCamera;

	public GameObject _pauseMenu;
	public bool _paused;
	public bool _uiOpen;

	public GameObject _npcChatGO;
	public Text _npcChatText;
	public bool _inChat;
	public NPCChat _npcChat;

	public Smith _smith;
	public Stats _stats;

	// Use this for initialization
	void Start () {
		_smith = gameObject.GetComponent<Smith>();
		_stats = gameObject.GetComponent<Stats>();
		_npcChatGO = GameObject.Find("TextBox");
		_npcChat = gameObject.GetComponent<NPCChat>();
		_npcChatText = GameObject.Find("NPCtext").GetComponent<Text>();
		_npcChatGO.SetActive(false);
		_pauseMenu = GameObject.Find("PauseMenu");
		_pauseMenu.SetActive(false);
		_smithCamera = GameObject.Find("SmithCamera").GetComponent<Camera>();
		_smithCamera.enabled = false;
		_obols = GameObject.Find("CurrentObols").GetComponent<Text>();		
		_currentHP = GameObject.Find("CurrentHP").GetComponent<Text>();
		_maxHP = GameObject.Find("MaxHP").GetComponent<Text>();
		_hpBar = GameObject.Find("HP").GetComponent<RectTransform>();
		_saveGame = GameObject.Find("Loader").GetComponent<SaveGame>();
		UpdateUI();	
		CloseAllCanvases();
	}

	void CloseAllCanvases(){
		CloseCanvas(1);
		CloseCanvas(2);
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
		_currentHP.text = _CombatManager._currentHealth.ToString();
		_maxHP.text = _CombatManager._maxHealth.ToString();
	}

	public void OpenCanvas(int index){
		switch (index){
			case 1:
			_smith.OpenCanvas();
			break;
			case 2:
			_stats.OpenCanvas();
			break;
		}
		
	}

	public void CloseCanvas(int index){
		switch (index){
			case 1:
			_smith.CloseCanvas();
			break;
			case 2:
			_stats.CloseCanvas();
			break;
		}
	}

	public void ExitGame(){
		Application.Quit();
	}
}

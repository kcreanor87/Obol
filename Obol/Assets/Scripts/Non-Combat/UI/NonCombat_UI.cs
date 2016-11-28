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

	public List <GameObject> _canvases = new List <GameObject>();

	public Camera _smithCamera;
	public Smith _smithScript;

	public GameObject _pauseMenu;
	public bool _paused;
	public bool _uiOpen;

	public GameObject _npcChatGO;
	public Text _npcChatText;
	public bool _inChat;
	public NPCChat _npcChat;

	// Use this for initialization
	void Start () {
		_npcChatGO = GameObject.Find("TextBox");
		_npcChat = gameObject.GetComponent<NPCChat>();
		_npcChatText = GameObject.Find("NPCtext").GetComponent<Text>();
		_npcChatGO.SetActive(false);
		_pauseMenu = GameObject.Find("PauseMenu");
		_pauseMenu.SetActive(false);
		_smithCamera = GameObject.Find("SmithCamera").GetComponent<Camera>();
		_smithCamera.enabled = false;
		_smithScript = GameObject.Find("SmithScreen").GetComponent<Smith>();
		_obols = GameObject.Find("CurrentObols").GetComponent<Text>();		
		_currentHP = GameObject.Find("CurrentHP").GetComponent<Text>();
		_maxHP = GameObject.Find("MaxHP").GetComponent<Text>();
		_hpBar = GameObject.Find("HP").GetComponent<RectTransform>();
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
		_currentHP.text = _CombatManager._currentHealth.ToString();
		_maxHP.text = _CombatManager._maxHealth.ToString();
	}

	public void OpenCanvas(int index){
		if (!_manager._npcChat[index]){
			_canvases[0].SetActive(false);
			_canvases[index].SetActive(true);
			_smithCamera.enabled = (index == 2);
			if (index == 2) _smithScript.ToggleScreens(0);
		}
		else{
			_npcChat.UpdateText(index);
			_npcChatText.text = _npcChat._text;
			_npcChatGO.SetActive(_manager._npcChat[index]);
			_inChat = _manager._npcChat[index];
		}
		_uiOpen = true;
	}

	public void CloseCanvas(int index){
		_npcChatGO.SetActive(false);
		_smithCamera.enabled = false;
		_canvases[index].SetActive(false);
		_canvases[0].SetActive(true);
		_saveGame.Save();
		UpdateUI();
		_uiOpen = false;
	}

	public void ExitGame(){
		Application.Quit();
	}
}

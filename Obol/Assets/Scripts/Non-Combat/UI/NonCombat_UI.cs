using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NonCombat_UI : MonoBehaviour {

	public Text _obols;
	public RectTransform _hpBar;
	public RectTransform _xpBar;
	public int _hpMax = 580;
	public GameObject _damageText;
	public Text _currentHP;
	public Text _maxHP;
	public SaveGame _saveGame;

	public GameObject _pauseMenu;
	public GameObject _playerHUD;
	public bool _paused;
	public bool _uiOpen;

	public GameObject _npcChatGO;
	public Text _npcChatText;
	public bool _inChat;
	public NPCChat _npcChat;

	public Smith _smith;
	public Stats _stats;
	public PortalControls _portal;

	public GameObject _levelUpPrompt;
	public GameObject _levelUpText;
	public GameObject _statsPrompt;

	//Buttons to select on menu open
	public Button _resumeButton;

	public int _npcIndex;
	public bool _promptActive;
	public GameObject _promptGO;
	public Text _promptText;
	public bool _inMenu;

	// Use this for initialization
	void Start () {
		_levelUpPrompt = GameObject.Find("LevelUpPrompt");
		_levelUpText = GameObject.Find("LevelUpText");
		_statsPrompt = GameObject.Find("StatsPrompt");
		_smith = gameObject.GetComponent<Smith>();
		_stats = gameObject.GetComponent<Stats>();
		_portal = gameObject.GetComponent<PortalControls>();
		_npcChatGO = GameObject.Find("TextBox");
		_playerHUD = GameObject.Find("BaseHUD");
		_npcChat = gameObject.GetComponent<NPCChat>();
		_npcChatText = GameObject.Find("NPCtext").GetComponent<Text>();
		_npcChatGO.SetActive(false);
		_pauseMenu = GameObject.Find("PauseMenu");
		_resumeButton = _pauseMenu.transform.FindChild("Resume").GetComponent<Button>();
		_pauseMenu.SetActive(false);
		_obols = GameObject.Find("CurrentObols").GetComponent<Text>();		
		_currentHP = GameObject.Find("CurrentHP").GetComponent<Text>();
		_promptGO = GameObject.Find("NPCPrompt");
		_promptText = _promptGO.GetComponentInChildren<Text>();
		_promptGO.SetActive(false);
		_maxHP = GameObject.Find("MaxHP").GetComponent<Text>();
		_hpBar = GameObject.Find("HP").GetComponent<RectTransform>();
		_xpBar = GameObject.Find("XP").GetComponent<RectTransform>();
		_saveGame = GameObject.Find("Loader").GetComponent<SaveGame>();
		UpdateUI();
		OpenCanvas(0);
	}

	public void CloseAllCanvases(){
		CloseCanvas(1);
		CloseCanvas(2);
		CloseCanvas(3);
	}

	void Update(){
		PauseDetect();
		CancelDetect();
		if (_promptActive && !_paused) NPCDetect();
	}

	void PauseDetect(){
		if (Input.GetButtonDown("Pause")){
			if (!_paused){
				PauseMenu(true);
			}
			else{
				PauseMenu(false);
			}			
		}
	}

	void CancelDetect(){
		if (Input.GetButtonDown("Cancel")){
			CloseAllCanvases();
			PauseMenu(false);
		}
	}

	void NPCDetect(){
		if (Input.GetButtonDown("Submit")){
			OpenCanvas(_npcIndex);
			ClosePrompt();
		}
	}

	public void PauseMenu(bool paused){
		if (paused){
			CloseAllCanvases();
			_pauseMenu.SetActive(true);
			_playerHUD.SetActive(false);
			_levelUpText.SetActive(_manager._availableRanks > 0);
			_statsPrompt.SetActive(_manager._availableRanks == 0);
			_paused = true;		
			_resumeButton.Select();
		}
		else{
			CloseAllCanvases();
			_pauseMenu.SetActive(false);
			_playerHUD.SetActive(true);
			_paused = false;
			EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
		}
	}

	public void UpdateUI(){
		_obols.text = _manager._obols.ToString();
		_currentHP.text = _CombatManager._currentHealth.ToString();
		_maxHP.text = _CombatManager._maxHealth.ToString();
		//UpdateHP
		var HPwidth = (float) ((float)_CombatManager._currentHealth / _CombatManager._maxHealth) * _hpMax;
		_hpBar.sizeDelta = new Vector2(HPwidth, 130);
		//UpdateXP
		var XPwidth = (float) ((float)(_manager._currentXP - _manager._prevXP) /( _manager._nextLvlXP - _manager._prevXP)) * 571;
		_xpBar.sizeDelta = new Vector2(XPwidth, 14);
		_levelUpPrompt.SetActive(_manager._availableRanks > 0);
	}

	public void OpenCanvas(int index){
		CloseAllCanvases();
		_playerHUD.SetActive(false);
		switch (index){
			case 0:
			_playerHUD.SetActive(true);
			break;
			case 1:
			_smith.OpenCanvas();
			break;
			case 2:
			_stats.OpenCanvas();
			_pauseMenu.SetActive(false);
			break;
			case 3:
			_portal.OpenCanvas();
			break;
		}
		_inMenu = true;
	}

	public void CloseCanvas(int index){
		switch (index){
			case 1:
			_smith.CloseCanvas();
			break;
			case 2:
			_stats.CloseCanvas();
			break;
			case 3:
			_portal.CloseCanvas();
			break;
		}
		EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
		_playerHUD.SetActive(true);
		_saveGame.Save();
		_inMenu = false;
	}

	public void ExitGame(){
		Application.Quit();
	}

	public void OpenPrompt(int i){
		_npcIndex = i;
		_promptGO.SetActive(true);		
		_promptText.text = (i == 3) ? "Travel" : "Talk";
		_promptActive = true;
	}

	public void ClosePrompt(){
		_promptGO.SetActive(false);
		_promptActive = false;
	}
}

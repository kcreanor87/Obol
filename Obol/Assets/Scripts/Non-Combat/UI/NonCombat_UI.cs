using UnityEngine;
using UnityEngine.UI;

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

	// Use this for initialization
	void Start () {
		_smith = gameObject.GetComponent<Smith>();
		_stats = gameObject.GetComponent<Stats>();
		_portal = gameObject.GetComponent<PortalControls>();
		_npcChatGO = GameObject.Find("TextBox");
		_playerHUD = GameObject.Find("BaseHUD");
		_npcChat = gameObject.GetComponent<NPCChat>();
		_npcChatText = GameObject.Find("NPCtext").GetComponent<Text>();
		_npcChatGO.SetActive(false);
		_pauseMenu = GameObject.Find("PauseMenu");
		_pauseMenu.SetActive(false);
		_obols = GameObject.Find("CurrentObols").GetComponent<Text>();		
		_currentHP = GameObject.Find("CurrentHP").GetComponent<Text>();
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
			_playerHUD.SetActive(false);
			Time.timeScale = 0.0f;
			_paused = true;
		}
		else{
			_pauseMenu.SetActive(false);
			_playerHUD.SetActive(true);
			Time.timeScale = 1.0f;
			_paused = false;
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
			break;
			case 3:
			_portal.OpenCanvas();
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
			case 3:
			_portal.CloseCanvas();
			break;
		}
		_playerHUD.SetActive(true);
		_saveGame.Save();
	}

	public void ExitGame(){
		Application.Quit();
	}
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Combat_UI : MonoBehaviour {

	public Text _obols;
	public RectTransform _hpBar;
	public RectTransform _xpBar;
	public int _hpMax = 580;
	
	public Text _currentHP;
	public Text _maxHP;
	public SaveGame _saveGame;

	public GameObject _pauseMenu;
	public GameObject _playerHUD;
	public bool _paused;
	public bool _uiOpen;
	public Stats _stats;

	public GameObject _damageText;
	public GameObject _goldText;
	public GameObject _xpText;

	public GameObject _statsText;
	public GameObject _levelUpText;
	public GameObject _levelUpPrompt;

	// Use this for initialization
	void Start () {
		_levelUpPrompt = GameObject.Find("LevelUpPrompt");
		_levelUpText = GameObject.Find("LevelUpText");
		_statsText = GameObject.Find("StatsText");
		_stats = gameObject.GetComponent<Stats>();
		_pauseMenu = GameObject.Find("PauseMenu");
		_playerHUD = GameObject.Find("BaseHUD");
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

	void Update(){
		PauseDetect();
	}

	void PauseDetect(){
		if (Input.GetKeyDown(KeyCode.Escape)){
			if (!_paused){
				OpenCanvas(1);
			}
			else{
				OpenCanvas(0);
			}			
		}
	}

	public void CloseAllCanvases(){
		CloseCanvas(1);
	}	

	public void OpenCanvas(int index){
		switch (index){
			case 0:
			_playerHUD.SetActive(true);
			CloseCanvas(1);
			CloseCanvas(2);
			_paused = false;
			Time.timeScale = 1.0f;
			break;
			case 1:
			_pauseMenu.SetActive(true);
			_paused = true;
			Time.timeScale = 0.0f;
			CloseCanvas(0);
			CloseCanvas(2);
			break;
			case 2:
			CloseCanvas(1);
			_stats.OpenCanvas();
			break;
		}			
	}	

	public void CloseCanvas(int index){
		switch (index){
			case 0:
			_playerHUD.SetActive(false);
			break;
			case 1:
			_pauseMenu.SetActive(false);
			break;
			case 2:
			_stats.CloseCanvas();
			break;
		}
		UpdateUI();		
	}

	public void Concede(){
		Time.timeScale = 1.0f;
		SceneManager.LoadScene(1);
	}

	public void ExitGame(){
		Application.Quit();
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
		_levelUpText.SetActive(_manager._availableRanks > 0);
		_statsText.SetActive(_manager._availableRanks == 0);

	}

	public void DamageText(Transform target, int damage, bool playerHit){
		var pos = Camera.main.WorldToScreenPoint(target.position);
		var textGO = (GameObject) Instantiate(_damageText, transform);
		var txt = textGO.GetComponent<Text>();
		var script = textGO.GetComponent<DamageText>();
		if (playerHit) script._playerHit = true;
		script._target = target;
		txt.text = damage.ToString();
		textGO.GetComponent<RectTransform>().anchoredPosition = pos;
	}

	public void GoldText(int gold){
		var textGO = (GameObject) Instantiate(_goldText, transform);
		var txt = textGO.GetComponent<Text>();
		txt.text = "+ " + gold;
	}

	public void ExpText(int exp){
		var textGO = (GameObject) Instantiate(_xpText, transform);
		var txt = textGO.GetComponent<Text>();
		txt.text = "+ " + exp + " xp";
	}

	/*
	public void PopUpBox(int value, string text){
		_popUpText.text = "+ " + value + " " + text;
		_popUp.SetActive(true);
		StartCoroutine(FadePopUp());
	}

	public IEnumerator FadePopUp(){
		yield return new WaitForSeconds(2.0f);
		_popUp.SetActive(false);
	}*/
}

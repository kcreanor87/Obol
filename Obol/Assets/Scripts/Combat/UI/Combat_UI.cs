using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Combat_UI : MonoBehaviour {

	public Text _obols;
	public RectTransform _hpBar;
	public RectTransform _xpBar;
	public int _hpMax = 580;
	
	public Text _currentHP;
	public Text _maxHP;
	public SaveGame _saveGame;

	public Text _gateText;
	public Text _gateRemaining;
	public Text _enemiesKilled;

	public GameObject _pauseMenu;
	public GameObject _playerHUD;
	public bool _paused;
	public bool _uiOpen;
	public Stats _stats;
	public CombatCounters _counters;

	public GameObject _damageText;
	public GameObject _goldText;
	public GameObject _xpText;

	public GameObject _statsText;
	public GameObject _levelUpText;
	public GameObject _levelUpPrompt;

	public GameObject _fadeOut;
	public GameObject _endCanvas;
	public Image _gameOverImage;
	public Sprite _winSprite;
	public Sprite _loseSprite;

	public Text _gatesDestroyedTxt, _enemiesTxt, _goldEarnedTxt, _goldBonus, _xpGainedTxt, _xpBonus;
	public GameObject _areaUnlocked;
	public bool _winBonus;
	public int _tempGold;
	public int _tempXP;

	public bool _gameOver;

	public Button _continueButton;

	public Camera _miniMap;

	public TurretControls _turret;

	public bool _end;

	// Use this for initialization
	void Start () {
		_miniMap = GameObject.Find("Minimap").GetComponent<Camera>();
		_areaUnlocked = GameObject.Find("AreaUnlocked");
		_areaUnlocked.SetActive(false);
		_fadeOut = GameObject.Find("FadeOut");
		_fadeOut.SetActive(false);
		_continueButton = GameObject.Find("Continue").GetComponent<Button>();
		_continueButton.interactable = false;
		_gameOverImage = GameObject.Find("GameOverImage").GetComponent<Image>();
		_gatesDestroyedTxt = GameObject.Find("GatesDestroyed").GetComponent<Text>();
		_enemiesTxt = GameObject.Find("EnemiesKilled").GetComponent<Text>();
		_goldEarnedTxt = GameObject.Find("GoldEarned").GetComponent<Text>();
		_goldBonus = GameObject.Find("GoldBonus").GetComponent<Text>();
		_xpGainedTxt = GameObject.Find("XPGained").GetComponent<Text>();
		_xpBonus = GameObject.Find("XPBonus").GetComponent<Text>();
		_endCanvas = transform.FindChild("GameOverScreen").gameObject;
		_endCanvas.SetActive(false);
		_counters = GameObject.Find("Counters").GetComponent<CombatCounters>();
		_gateText = GameObject.Find("GateText").GetComponent<Text>();
		_gateRemaining = GameObject.Find("GateRemainingText").GetComponent<Text>();
		_enemiesKilled = GameObject.Find("EnemiesKilledText").GetComponent<Text>();
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
		_hpBar = GameObject.Find("HPbar").GetComponent<RectTransform>();
		_xpBar = GameObject.Find("XP").GetComponent<RectTransform>();
		_saveGame = GameObject.Find("Loader").GetComponent<SaveGame>();
		UpdateUI();	
		OpenCanvas(0);
	}

	void Update(){
		PauseDetect();
		if (_winBonus) AddBonus();
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
		_miniMap.enabled = false;
		switch (index){
			case 0:
			_playerHUD.SetActive(true);
			_miniMap.enabled = true;
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
		CloseAllCanvases();
		_endCanvas.SetActive(false);
		Time.timeScale = 1.0f;	
		StartCoroutine(FadeOut());
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

		_gateText.text = (_counters._spawnPoints > 0) ? "Gates Left:" : "Enemies Remaining";
		_gateRemaining.text = (_counters._spawnPoints > 0) ? _counters._spawnPoints.ToString() : (_counters._enemiesSpawned - _counters._enemiesKilled).ToString();
		_enemiesKilled.text = _counters._enemiesKilled.ToString();
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

	public void LevelEnd(bool victory){
		_miniMap.enabled = false;
		Time.timeScale = 1.0f;
		_gameOver = true;
		//Close all other canvases
		CloseCanvas(0);
		CloseCanvas(1);
		CloseCanvas(2);
		//Add all gold in level
		int spareGold = 0;
		var array = GameObject.FindGameObjectsWithTag("Shard");
		for (int i = 0; i < array.Length; i++){
			spareGold += array[i].GetComponent<Coin>()._value;
			Destroy(array[i]);
		}
		_manager._obols += spareGold;
		_counters._obolsCollected += spareGold;
		//Open canvas
		_gameOverImage.sprite = victory ? _winSprite : _loseSprite;
		_endCanvas.SetActive(true);
		//Show gold, xp, enemies killed and gates.
		EndText();
		if (victory) {
			CalcBonus();
			var nextLevel = (SceneManager.GetActiveScene().buildIndex - 1);
			_areaUnlocked.SetActive(!_manager._activeLevels[nextLevel]);
			_manager._activeLevels[nextLevel] = true;
		}
		else{
			_continueButton.interactable = true;
			Time.timeScale = 0.2f;
		}
		//once added, enable game over button
		//enabled GameOVer button
	}

	void EndText(){
		_gatesDestroyedTxt.text = "" + (_counters._totalSpawns - _counters._spawnPoints) + " / " + _counters._totalSpawns;
		_goldEarnedTxt.text = _counters._obolsCollected.ToString();
		_xpGainedTxt.text = _counters._xpGained.ToString();
		_enemiesTxt.text = _counters._enemiesKilled.ToString();
	}

	void CalcBonus(){
		_tempGold = Mathf.FloorToInt(_counters._obolsCollected * 0.2f);
		_tempXP = Mathf.FloorToInt(_counters._xpGained * 0.2f);
		_goldBonus.text = _tempGold.ToString();
		_xpBonus.text = _tempXP.ToString();
		_manager._obols += _tempGold;
		_manager._currentXP += _tempXP;
		StartCoroutine(Pause());
	}

	void AddBonus(){
		if (_tempGold >= 2){
			_tempGold -= 2;
			_counters._obolsCollected += 2;
		}
		else{
			_counters._obolsCollected += _tempGold;
			_tempGold = 0;
		}
		if (_tempXP >= 8){
			_tempXP -= 8;
			_counters._xpGained += 8;
		}
		else{
			_counters._xpGained += _tempXP;
			_tempXP = 0;
		}
		if (_tempGold == 0 && _tempXP == 0){
			_winBonus = false;
			_continueButton.interactable = true;
			Time.timeScale = 0.0f;
			UpdateUI();
		}
		_xpBonus.text = _tempXP.ToString();
		_xpGainedTxt.text = _counters._xpGained.ToString();
		_goldEarnedTxt.text = _counters._obolsCollected.ToString();
		_goldBonus.text = _tempGold.ToString();
	}

	public IEnumerator Pause(){
		yield return new WaitForSeconds(1.5f);
		_winBonus = true;
	}

	public IEnumerator FadeOut(){
		_fadeOut.SetActive(true);
		Time.timeScale = 1.0f;
		_endCanvas.SetActive(false);
		yield return new WaitForSeconds(1.2f);		
		SceneManager.LoadScene("Crypt");
	}
}

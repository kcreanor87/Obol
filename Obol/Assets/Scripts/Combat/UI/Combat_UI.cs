using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Combat_UI : MonoBehaviour {

	public Text _boneTxt, _ironTxt, _sulphurTxt, _crystalTxt, _enemiesTxt;
	public RectTransform _hpBar;
	public int _hpMax = 580; 
	public CombatCounters _counter;
	public GameObject _damageText;
	public bool _gameOver;
	public Image _gameOverImage;
	public GameObject _startGO;
	public GameObject _endGO;
	public GameObject _activeGO;
	public bool _victory;
	public Sprite _winSprite;
	public Sprite _lossSprite;
	public float _size = 50.0f;
	public float _timer = 3.0f;
	public bool _switch;
	public bool _imageActive = true;
	public GameObject _startText;
	public GameObject _portalCanvas;
	public GameObject _mainUI;
	public GameObject _blackout;
	public int _resIndex;
	public bool _ticker;
	public Text _currentHP;
	public Text _maxHP;
	public bool _tutorial;
	public GameObject _fadeOut;
	public GameObject _pauseMenu;
	public bool _paused;

	// Use this for initialization
	void Start () {	
		_fadeOut = GameObject.Find("FadeOut");
		_fadeOut.SetActive(false);
		_pauseMenu = GameObject.Find("PauseMenu");
		_pauseMenu.SetActive(false);
		_blackout = GameObject.Find("Blackout");
		_blackout.SetActive(false);
		_portalCanvas = GameObject.Find("PortalScreen");
		_portalCanvas.SetActive(false);
		_mainUI = GameObject.Find("MainUI");
		_currentHP = GameObject.Find("CurrentHP").GetComponent<Text>();
		_maxHP = GameObject.Find("MaxHP").GetComponent<Text>();
		_activeGO = _startGO;		
		_gameOverImage = _endGO.GetComponent<Image>();
		_endGO.SetActive(false);
		_startText = GameObject.Find("Start Text");
		_startText.SetActive(false);
		_counter = GameObject.Find("Counters").GetComponent<CombatCounters>();
		_boneTxt = GameObject.Find("BoneTxt").GetComponent<Text>();
		_ironTxt = GameObject.Find("Iron").GetComponent<Text>();
		_sulphurTxt = GameObject.Find("Sulphur").GetComponent<Text>();
		_crystalTxt = GameObject.Find("Crystal").GetComponent<Text>();
		_enemiesTxt = GameObject.Find("EnemiesKilled").GetComponent<Text>();
		_hpBar = GameObject.Find("HP").GetComponent<RectTransform>();
		_activeGO.SetActive(false);
		UpdateUI();
	}

	void Update (){
		if (_tutorial && _imageActive){
			EnlargeSprite();
		}
		if (!_imageActive){
			PauseDetect();
		}
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
		_boneTxt.text = "" + _manager._resources[0];
		_ironTxt.text = "" + _manager._resources[1];
		_sulphurTxt.text = "" + _manager._resources[2];
		_crystalTxt.text = "" + _manager._resources[3];
		if (_CombatManager._currentHealth > 0){
			_currentHP.text = _CombatManager._currentHealth.ToString();
			_maxHP.text = _CombatManager._maxHealth.ToString();
		}
		else{
			_currentHP.text = "0";
		}		
		_enemiesTxt.text = _counter._totalEnemies.ToString();
		var HPwidth = (float) ((float)_CombatManager._currentHealth / _CombatManager._maxHealth) * _hpMax;
		_hpBar.sizeDelta = new Vector2(HPwidth, 130);
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

	public void GameOver(){
		_fadeOut.SetActive(true);		
		StartCoroutine(Restart());
	}

	public IEnumerator Restart(){
		yield return new WaitForSeconds(2.0f);
		_CombatManager.CalculateStats();
		for (int i = 0; i < _manager._resources.Count; i++){
			_manager._resources[i] = 0;
			PlayerPrefs.SetInt("Resources" + i, 0);
		}
		SceneManager.LoadScene(_tutorial ? "Level0" : "Crypt");
	}

	void EnlargeSprite(){
		if (!_switch){
			_activeGO.SetActive(true);
			if (_size <= 150.0f){
				_size += 10.0f;
				_activeGO.GetComponent<RectTransform>().sizeDelta = new Vector2(350, _size);
			}
			else{
				_switch = true;
				_startText.SetActive(true);
			}
		}
		_timer -= Time.deltaTime;
		if (_gameOver){
			_blackout.SetActive(true);
		}
		if (_timer <= 0){
			if (!_gameOver){			
				_startText.SetActive(false);
				if (_size >= 50.0f){
					_size -= 5.0f;
					_activeGO.GetComponent<RectTransform>().sizeDelta = new Vector2(350, _size);
				}
				else{
					_timer = 4.0f;
					_switch = false;
					_activeGO.SetActive(false);
					_activeGO = _endGO;
					_imageActive = false;
				}				
			}
			else{				
				_imageActive = false;
				Time.timeScale = 0.0f;		
			}
		}			
	}
	public void OpenCanvas(int index){
		if (!_paused){
			switch (index){
			case 0:
			_portalCanvas.SetActive(true);
			_mainUI.SetActive(false);
			break;
			case 1:
			_portalCanvas.SetActive(false);
			_mainUI.SetActive(true);
			Time.timeScale = 1.0f;
			break;
			}
		}		
	}

	public void ExitGame(){
		Application.Quit();
	}
}

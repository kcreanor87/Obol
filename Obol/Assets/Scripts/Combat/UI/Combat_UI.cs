﻿using UnityEngine;
using UnityEngine.UI;

public class Combat_UI : MonoBehaviour {

	public Text _boneTxt, _ironTxt, _sulphurTxt, _crystalTxt, _enemiesTxt, _resTxt;
	public Text _boneEndTxt, _ironEndTxt, _sulphurEndTxt, _crystalEndTxt;
	public Text _bonePrice, _ironPrice, _sulphurPrice, _crystalPrice, _obols;
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

	// Use this for initialization
	void Start () {	
		_boneEndTxt = GameObject.Find("BoneCollected").GetComponent<Text>();
		_ironEndTxt = GameObject.Find("IronCollected").GetComponent<Text>();
		_sulphurEndTxt = GameObject.Find("SulphurCollected").GetComponent<Text>();
		_crystalEndTxt = GameObject.Find("CrystalCollected").GetComponent<Text>();
		_obols = GameObject.Find("CurrentObols").GetComponent<Text>();
		_obols.text = _manager._obols.ToString();
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
		_resTxt = GameObject.Find("ResCollected").GetComponent<Text>();
		_hpBar = GameObject.Find("HP").GetComponent<RectTransform>();
		_activeGO.SetActive(false);
		UpdateUI();
	}

	void Update (){
		if (_tutorial && _imageActive){
			EnlargeSprite();
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
		_resTxt.text = (_counter._resourcesAvailable - _counter._resourcesCollected).ToString();
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

	public void GameOver(bool win){
		_victory = win;
		_gameOver = true;
		_imageActive = true;
		_gameOverImage.sprite = (_victory) ? _winSprite : _lossSprite;		
		_activeGO.SetActive(true);
	}

	void EnlargeSprite(){
		if (!_switch){
			_activeGO.SetActive(true);
			if (_size <= 260.0f){
				_size += 10.0f;
				_activeGO.GetComponent<RectTransform>().sizeDelta = new Vector2(900, _size);
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
					_activeGO.GetComponent<RectTransform>().sizeDelta = new Vector2(900, _size);
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
		switch (index){
			case 0:
			_portalCanvas.SetActive(true);
			_mainUI.SetActive(false);
			break;
			case 1:
			_portalCanvas.SetActive(false);
			_mainUI.SetActive(true);
			break;
		}
	}
}

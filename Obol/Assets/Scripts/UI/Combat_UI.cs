using UnityEngine;
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
	public ExitDetect _exitDetect;	
	public GameObject _blackout;
	public bool _addResources;
	public int _resIndex;
	public bool _ticker;
	public Text _currentHP;
	public Text _maxHP;

	// Use this for initialization
	void Start () {	
		_boneEndTxt = GameObject.Find("BoneCollected").GetComponent<Text>();
		_ironEndTxt = GameObject.Find("IronCollected").GetComponent<Text>();
		_sulphurEndTxt = GameObject.Find("SulphurCollected").GetComponent<Text>();
		_crystalEndTxt = GameObject.Find("CrystalCollected").GetComponent<Text>();
		_bonePrice = GameObject.Find("BonePrice").GetComponent<Text>();
		_bonePrice.text = "X " + _manager._prices[0];	
		_ironPrice = GameObject.Find("IronPrice").GetComponent<Text>();
		_ironPrice.text = "X " + _manager._prices[1];	
		_sulphurPrice = GameObject.Find("SulphurPrice").GetComponent<Text>();
		_sulphurPrice.text = "X " + _manager._prices[2];	
		_crystalPrice = GameObject.Find("CrystalPrice").GetComponent<Text>();
		_crystalPrice.text = "X " + _manager._prices[3];
		_obols = GameObject.Find("CurrentObols").GetComponent<Text>();
		_obols.text = _manager._obols.ToString();
		_blackout = GameObject.Find("Blackout");
		_blackout.SetActive(false);
		_currentHP = GameObject.Find("CurrentHP").GetComponent<Text>();
		_maxHP = GameObject.Find("MaxHP").GetComponent<Text>();
		_activeGO = _startGO;
		_gameOverImage = _endGO.GetComponent<Image>();
		_endGO.SetActive(false);
		_startText = GameObject.Find("Start Text");
		_startText.SetActive(false);
		_exitDetect = GameObject.Find("Exit").GetComponent<ExitDetect>();
		_counter = GameObject.Find("Counters").GetComponent<CombatCounters>();
		_boneTxt = GameObject.Find("BoneTxt").GetComponent<Text>();
		_ironTxt = GameObject.Find("Iron").GetComponent<Text>();
		_sulphurTxt = GameObject.Find("Sulphur").GetComponent<Text>();
		_crystalTxt = GameObject.Find("Crystal").GetComponent<Text>();
		_enemiesTxt = GameObject.Find("EnemiesKilled").GetComponent<Text>();
		_resTxt = GameObject.Find("ResCollected").GetComponent<Text>();
		_hpBar = GameObject.Find("HP").GetComponent<RectTransform>();
		UpdateUI();
	}

	void Update (){
		if (_imageActive){
			EnlargeSprite();
		}
		if (_addResources){
			CollectWinnings();
		}
	}
	
	public void UpdateUI(){
		_boneTxt.text = "" + _counter._resources[0];
		_ironTxt.text = "" + _counter._resources[1];
		_sulphurTxt.text = "" + _counter._resources[2];
		_crystalTxt.text = "" + _counter._resources[3];
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
		AddResources();
	}

	void EnlargeSprite(){
		if (!_switch){
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

	void AddResources(){
		_addResources = true;
		_boneEndTxt.text = (_victory) ? _counter._resources[0].ToString() : "0";
		_ironEndTxt.text = (_victory) ? _counter._resources[1].ToString() : "0";
		_sulphurEndTxt.text = (_victory) ? _counter._resources[2].ToString() : "0";
		_crystalEndTxt.text = (_victory) ? _counter._resources[3].ToString() : "0";
		_obols.text = _manager._obols.ToString();
	}

	void CollectWinnings(){
		if (!_victory){
			_resIndex = _manager._prices.Count;
			_addResources = false;
			_exitDetect.ExitPrompt();
		}
		if (_ticker){
			if (_counter._resources[_resIndex] > 0){
				if (_victory){
					_counter._resources[_resIndex]--;
					_manager._obols += _manager._prices[_resIndex];
					AddResources();
				}				
			}
			else{
				_resIndex++;
			}
			if(_resIndex >= _manager._prices.Count){
				_addResources = false;
				_exitDetect.ExitPrompt();
			}
			_ticker = false;
		}
		else{
			_ticker = true;
		}		
	}
}

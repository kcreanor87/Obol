using UnityEngine;
using UnityEngine.UI;

public class Combat_UI : MonoBehaviour {

	public Text _boneTxt, _ironTxt, _sulphurTxt, _crystalTxt, _enemiesTxt, _resTxt;
	public RectTransform _hpBar;
	public int _hpMax = 560; 
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
	public float _size = 10.0f;
	public float _timer = 2.0f;
	public bool _switch;
	public bool _imageActive = true;
	public GameObject _startText;
	public ExitDetect _exitDetect;

	// Use this for initialization
	void Start () {
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
	}
	
	public void UpdateUI(){
		_boneTxt.text = "" + _counter._resources[0];
		_ironTxt.text = "" + _counter._resources[1];
		_sulphurTxt.text = "" + _counter._resources[2];
		_crystalTxt.text = "" + _counter._resources[3];
		_enemiesTxt.text = (_counter._totalEnemies - _counter._enemiesKilled).ToString();
		_resTxt.text = (_counter._resourcesAvailable - _counter._resourcesCollected).ToString();
		var HPwidth = (float) ((float)_CombatManager._currentHealth / _CombatManager._maxHealth) * _hpMax;
		_hpBar.sizeDelta = new Vector2(HPwidth, 23);
		if (_counter._resourcesCollected >= _counter._resourcesAvailable && _counter._enemiesKilled >= _counter._totalEnemies){
			GameOver(true);
		}
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
			if (_size <= 150.0f){
				_size += 5.0f;
				_activeGO.GetComponent<RectTransform>().sizeDelta = new Vector2(900, _size);
			}
			else{
				_switch = true;
				_startText.SetActive(true);
			}
		}
		_timer -= Time.deltaTime;
		if (_timer <= 0){
			if (!_gameOver){			
				_startText.SetActive(false);
				if (_size >= 10.0f){
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
				Time.timeScale = 0.0f;
				_exitDetect.ExitPrompt();
			}
		}	
			
	}
}

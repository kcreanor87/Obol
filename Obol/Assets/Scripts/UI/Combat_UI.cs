using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Combat_UI : MonoBehaviour {

	public Text _boneTxt, _ironTxt, _sulphurTxt, _crystalTxt, _enemiesTxt, _resTxt;
	public RectTransform _hpBar;
	public int _hpMax = 560; 
	public CombatCounters _counter;
	public GameObject _damageText;

	// Use this for initialization
	void Start () {
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
	
	public void UpdateUI(){
		_boneTxt.text = "" + _counter._resources[0];
		_ironTxt.text = "" + _counter._resources[1];
		_sulphurTxt.text = "" + _counter._resources[2];
		_crystalTxt.text = "" + _counter._resources[3];
		_enemiesTxt.text = (_counter._totalEnemies - _counter._enemiesKilled).ToString();
		_resTxt.text = (_counter._resourcesAvailable - _counter._resourcesCollected).ToString();
		var HPwidth = (float) ((float)_CombatManager._currentHealth / _CombatManager._maxHealth) * _hpMax;
		_hpBar.sizeDelta = new Vector2(HPwidth, 23);
	}

	public void DamageText(Transform target, int damage){
		var pos = Camera.main.WorldToScreenPoint(target.position);
		var textGO = (GameObject) Instantiate(_damageText, transform);
		var txt = textGO.GetComponent<Text>();
		var script = textGO.GetComponent<DamageText>();
		script._target = target;
		txt.text = damage.ToString();
		textGO.GetComponent<RectTransform>().anchoredPosition = pos;
	}
}

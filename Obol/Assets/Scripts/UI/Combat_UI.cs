using UnityEngine;
using UnityEngine.UI;

public class Combat_UI : MonoBehaviour {

	public Text _boneTxt, _ironTxt, _sulphurTxt, _crystalTxt, _enemiesTxt, _resTxt;
	public CombatCounters _counter;

	// Use this for initialization
	void Start () {
		_counter = GameObject.Find("Counters").GetComponent<CombatCounters>();
		_boneTxt = GameObject.Find("BoneTxt").GetComponent<Text>();
		_ironTxt = GameObject.Find("Iron").GetComponent<Text>();
		_sulphurTxt = GameObject.Find("Sulphur").GetComponent<Text>();
		_crystalTxt = GameObject.Find("Crystal").GetComponent<Text>();
		_enemiesTxt = GameObject.Find("EnemiesKilled").GetComponent<Text>();
		_resTxt = GameObject.Find("ResCollected").GetComponent<Text>();
		UpdateUI();
	}
	
	public void UpdateUI(){
		_boneTxt.text = "" + _counter._resources[0];
		_ironTxt.text = "" + _counter._resources[1];
		_sulphurTxt.text = "" + _counter._resources[2];
		_crystalTxt.text = "" + _counter._resources[3];
		_enemiesTxt.text = (_counter._totalEnemies - _counter._enemiesKilled).ToString();
		_resTxt.text = (_counter._resourcesAvailable - _counter._resourcesCollected).ToString(); 
	}
}

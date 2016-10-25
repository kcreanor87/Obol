using UnityEngine;
using UnityEngine.UI;

public class NonCombat_UI : MonoBehaviour {

	public Text _boneTxt, _ironTxt, _sulphurTxt, _crystalTxt, _obols;
	public RectTransform _hpBar;
	public int _hpMax = 580;
	public GameObject _damageText;
	public Text _currentHP;
	public Text _maxHP;

	// Use this for initialization
	void Start () {	
		_obols = GameObject.Find("CurrentObols").GetComponent<Text>();
		_obols.text = _manager._obols.ToString();
		_currentHP = GameObject.Find("CurrentHP").GetComponent<Text>();
		_maxHP = GameObject.Find("MaxHP").GetComponent<Text>();
		_hpBar = GameObject.Find("HP").GetComponent<RectTransform>();
	}
}

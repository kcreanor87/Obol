using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NonCombat_UI : MonoBehaviour {

	public Text _boneTxt, _ironTxt, _silverTxt, _crystalTxt, _obols;
	public RectTransform _hpBar;
	public int _hpMax = 580;
	public GameObject _damageText;
	public Text _currentHP;
	public Text _maxHP;

	public List <GameObject> _canvases = new List <GameObject>();

	// Use this for initialization
	void Start () {	
		_obols = GameObject.Find("CurrentObols").GetComponent<Text>();		
		_currentHP = GameObject.Find("CurrentHP").GetComponent<Text>();
		_maxHP = GameObject.Find("MaxHP").GetComponent<Text>();
		_hpBar = GameObject.Find("HP").GetComponent<RectTransform>();
		_boneTxt = GameObject.Find("BoneTxt").GetComponent<Text>();
		_ironTxt = GameObject.Find("IronTxt").GetComponent<Text>();
		_silverTxt = GameObject.Find("SilverTxt").GetComponent<Text>();
		_crystalTxt = GameObject.Find("CrystalTxt").GetComponent<Text>();
		UpdateUI();
		for (int i = 0; i < _canvases.Count; i++){
			_canvases[i].SetActive(false);
		}
		_canvases[0].SetActive(true);
	}

	public void UpdateUI(){
		_obols.text = _manager._obols.ToString();
		_boneTxt.text = _manager._resources[0].ToString();
		_ironTxt.text = _manager._resources[1].ToString();
		_silverTxt.text = _manager._resources[2].ToString();
		_crystalTxt.text = _manager._resources[3].ToString();
	}

	public void OpenCanvas(int index){
		_canvases[0].SetActive(false);
		_canvases[index].SetActive(true);
	}

	public void CloseCanvas(int index){
		_canvases[index].SetActive(false);
		_canvases[0].SetActive(true);		
	}
}

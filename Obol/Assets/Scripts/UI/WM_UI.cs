using UnityEngine;
using UnityEngine.UI;

public class WM_UI : MonoBehaviour {

	public static Text _woodTxt, _stoneTxt, _ironTxt, _coalTxt, _steelTxt, _diamondTxt, _obolTxt;
	public static GameObject _mapToggle;

	// Use this for initialization
	void Start () {
		_woodTxt = GameObject.Find("Wood").GetComponent<Text>();
		_stoneTxt = GameObject.Find("Stone").GetComponent<Text>();
		_ironTxt = GameObject.Find("Iron").GetComponent<Text>();
		_coalTxt = GameObject.Find("Coal").GetComponent<Text>();
		_obolTxt = GameObject.Find("Obols").GetComponent<Text>();
		_steelTxt = GameObject.Find("Steel").GetComponent<Text>();
		_diamondTxt = GameObject.Find("Diamonds").GetComponent<Text>();
		UpdateUI();
	}
	
	public static void UpdateUI(){
		_woodTxt.text = "Wood: " + _manager._resources[0];
		_stoneTxt.text = "Stone: " + _manager._resources[1];
		_ironTxt.text = "Iron: " + _manager._resources[2];
		_coalTxt.text = "Coal: " + _manager._resources[3];
		_steelTxt.text = "Steel: " + _manager._resources[4];
		_diamondTxt.text = "Diamonds: " + _manager._resources[5];
		_obolTxt.text = "Obols: " + _manager._obols;
	}
}

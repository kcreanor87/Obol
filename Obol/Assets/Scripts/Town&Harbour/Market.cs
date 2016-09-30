using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour {

	public MarketSpawn _marketSpawn;
	public Text _buy0, _buy1, _buy2, _buy3, _buy4, _buy5;
	public Text _sell0, _sell1, _sell2, _sell3, _sell4, _sell5;
	public Text _stock0, _stock1, _stock2, _stock3, _stock4, _stock5;
	public Text _obols;
	public Text _multipleText;
	public int _multiple = 1;

	void Awake(){

		_marketSpawn = gameObject.GetComponent<MarketSpawn>();
		_buy0 = GameObject.Find("WoodBP").GetComponent<Text>();
		_buy1 = GameObject.Find("StoneBP").GetComponent<Text>();
		_buy2 = GameObject.Find("IronBP").GetComponent<Text>();
		_buy3 = GameObject.Find("CoalBP").GetComponent<Text>();
		_buy4 = GameObject.Find("SteelBP").GetComponent<Text>();
		_buy5 = GameObject.Find("DiamondBP").GetComponent<Text>();

		_sell0 = GameObject.Find("WoodSP").GetComponent<Text>();
		_sell1 = GameObject.Find("StoneSP").GetComponent<Text>();
		_sell2 = GameObject.Find("IronSP").GetComponent<Text>();
		_sell3 = GameObject.Find("CoalSP").GetComponent<Text>();
		_sell4 = GameObject.Find("SteelSP").GetComponent<Text>();
		_sell5 = GameObject.Find("DiamondSP").GetComponent<Text>();

		_stock0 = GameObject.Find("WoodStock").GetComponent<Text>();
		_stock1 = GameObject.Find("StoneStock").GetComponent<Text>();
		_stock2 = GameObject.Find("IronStock").GetComponent<Text>();
		_stock3 = GameObject.Find("CoalStock").GetComponent<Text>();
		_stock4 = GameObject.Find("SteelStock").GetComponent<Text>();
		_stock5 = GameObject.Find("DiamondStock").GetComponent<Text>();

		_obols = GameObject.Find("ObolsStock").GetComponent<Text>();

		_multipleText = GameObject.Find("Multiply").GetComponent<Text>();
	}

	public void BuyResource(int resource){
		if (_manager._obols >= (_multiple * _marketSpawn._buyPrices[resource])){
			_manager._resources[resource] += _multiple;
			_manager._obols -= (_marketSpawn._buyPrices[resource] * _multiple);
			UpdatePrices();
			WM_UI.UpdateUI();
		}		
	}

	public void UpdatePrices(){
		_buy0.text = (_multiple * _marketSpawn._buyPrices[0]).ToString();
		_buy1.text =( _multiple * _marketSpawn._buyPrices[1]).ToString();
		_buy2.text = (_multiple * _marketSpawn._buyPrices[2]).ToString();
		_buy3.text = (_multiple * _marketSpawn._buyPrices[3]).ToString();

		_obols.text = _manager._obols.ToString();
	}	

	public void SwitchMultiple(){
		switch (_multiple){
			case 1: 
			_multiple = 10;
			_multipleText.text = "x 10";
			break;
			case 10: 
			_multiple = 100;
			_multipleText.text = "x 100";
			break;
			case 100:
			_multiple = 1;
			_multipleText.text = "x 1";
			break;
			default:
			_multiple = 1;
			break;
		}
		UpdatePrices();
	}
}

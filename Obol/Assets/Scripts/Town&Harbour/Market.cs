using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour {

	public MarketSpawn _marketSpawn;
	public Text _buy0, _buy1, _buy2, _buy3;
	public Text _sell0, _sell1, _sell2, _sell3;
	public Text _stock0, _stock1, _stock2, _stock3;
	public Text _obols;
	public Text _multipleText;
	public NonCombat_UI _ui;
	public int _multiple = 1;

	void Awake(){
		_ui = GameObject.Find("Non-Combat UI").GetComponent<NonCombat_UI>();
		_marketSpawn = gameObject.GetComponent<MarketSpawn>();
		_buy0 = GameObject.Find("BoneBP").GetComponent<Text>();
		_buy1 = GameObject.Find("IronBP").GetComponent<Text>();
		_buy2 = GameObject.Find("SilverBP").GetComponent<Text>();
		_buy3 = GameObject.Find("CrystalBP").GetComponent<Text>();

		_sell0 = GameObject.Find("BoneSP").GetComponent<Text>();
		_sell1 = GameObject.Find("IronSP").GetComponent<Text>();
		_sell2 = GameObject.Find("SilverSP").GetComponent<Text>();
		_sell3 = GameObject.Find("CrystalSP").GetComponent<Text>();

		_stock0 = GameObject.Find("BoneVolume").GetComponent<Text>();
		_stock1 = GameObject.Find("IronVolume").GetComponent<Text>();
		_stock2 = GameObject.Find("SilverVolume").GetComponent<Text>();
		_stock3 = GameObject.Find("CrystalVolume").GetComponent<Text>();

		_obols = GameObject.Find("MerchObols").GetComponent<Text>();

		_multipleText = GameObject.Find("Multiply").GetComponent<Text>();

		UpdatePrices();
	}

	public void BuyResource(int resource){
		if (_manager._obols >= (_multiple * _marketSpawn._buyPrices[resource])){
			_manager._resources[resource] += _multiple;
			_manager._obols -= (_marketSpawn._buyPrices[resource] * _multiple);
			_ui.UpdateUI();
			UpdatePrices();
		}
	}

	public void SellResource(int resource){
		if (_manager._resources[resource] >= _multiple){
			_manager._resources[resource] -= _multiple;
			_manager._obols += (_marketSpawn._sellPrices[resource] * _multiple);
			_ui.UpdateUI();
			UpdatePrices();
		}
	}

	public void SellAll(){
		for (int i = 0; i < _manager._resources.Count; i++){
			_manager._obols += (_manager._resources[i] * _marketSpawn._sellPrices[i]);
			_manager._resources[i] = 0;
		}
		_ui.UpdateUI();
		UpdatePrices();
	}

	public void UpdatePrices(){
		_buy0.text = (_multiple * _marketSpawn._buyPrices[0]).ToString();
		_buy1.text =( _multiple * _marketSpawn._buyPrices[1]).ToString();
		_buy2.text = (_multiple * _marketSpawn._buyPrices[2]).ToString();
		_buy3.text = (_multiple * _marketSpawn._buyPrices[3]).ToString();

		_sell0.text = (_multiple * _marketSpawn._sellPrices[0]).ToString();
		_sell1.text = (_multiple * _marketSpawn._sellPrices[1]).ToString();
		_sell2.text = (_multiple * _marketSpawn._sellPrices[2]).ToString();
		_sell3.text = (_multiple * _marketSpawn._sellPrices[3]).ToString();

		_stock0.text = _manager._resources[0].ToString();
		_stock1.text = _manager._resources[1].ToString();
		_stock2.text = _manager._resources[2].ToString();
		_stock3.text = _manager._resources[3].ToString();

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

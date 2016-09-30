using UnityEngine;
using System.Collections.Generic;

public class MarketSpawn : MonoBehaviour {

	public List <float> _basePrice = new List <float>();
	public List <int> _buyPrices = new List <int>();
	public List <bool> _activeBuildings = new List<bool>();
	public float _marketBuyMod = 1.0f;
 	public Transform _player;
	public float _rumourMod = 1.0f;
	public int _rumourType;
	public bool _seen;

	// Use this for initialization
	void Awake () {
		_player	= GameObject.Find("Player").GetComponent<Transform>();
		SpawnPrices();
		if (_manager._combatOver){
			GeneratePrices();
			_manager._combatOver = false;
		} 
		UpdatePrices();	
	}

	void SpawnPrices(){
		if (NewGame._newGame){
			_basePrice.Add(Random.Range(100.0f, 150.0f));
			_basePrice.Add(Random.Range(150.0f, 250.0f));
			_basePrice.Add(Random.Range(250.0f, 400.0f));
			_basePrice.Add(Random.Range(500.0f, 750.0f));
		}		

		for (int i = 0; i < _basePrice.Count; i++){
			_buyPrices.Add(Mathf.FloorToInt(_basePrice[i]*_marketBuyMod));
		}
	}
	
	public void GeneratePrices(){
		_basePrice[0] = Random.Range(100.0f, 150.0f);
		_basePrice[1] = Random.Range(150.0f, 250.0f);
		_basePrice[2] = Random.Range(250.0f, 400.0f);
		_basePrice[3] = Random.Range(500.0f, 750.0f);

		_basePrice[_rumourType] *= _rumourMod;
	}

	public void UpdatePrices(){
		for (int i = 0; i < _basePrice.Count; i++){
			_buyPrices[i] = Mathf.FloorToInt(_basePrice[i] * _marketBuyMod);
		}

		_manager._prices[0] = _buyPrices[0];
		_manager._prices[1] = _buyPrices[1];
		_manager._prices[2] = _buyPrices[2];
		_manager._prices[3] = _buyPrices[3];

		for (int i = 0; i < _manager._prices.Count; i++){
			print(_manager._prices[i]);
		}
	}
}

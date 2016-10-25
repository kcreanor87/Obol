using UnityEngine;
using System.Collections.Generic;

public class MarketSpawn : MonoBehaviour {

	public List <float> _basePrice = new List <float>();
	public List <int> _buyPrices = new List <int>();
	public List <int> _sellPrices = new List<int>();
	public float _rumourMod = 1.0f;
	public int _rumourType;

	// Use this for initialization
	void Awake () {
		SpawnPrices();
		UpdatePrices();	
	}

	void SpawnPrices(){
		if (NewGame._newGame){
			_basePrice.Add(Random.Range(5.0f, 10.0f));
			_basePrice.Add(Random.Range(10.0f, 25.0f));
			_basePrice.Add(Random.Range(25.0f, 40.0f));
			_basePrice.Add(Random.Range(50.0f, 75.0f));
		}
		for (int i = 0; i < _basePrice.Count; i++){
			_buyPrices.Add(Mathf.FloorToInt(_basePrice[i]));
			_sellPrices.Add(Mathf.FloorToInt(_basePrice[i]));
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
			_buyPrices[i] = Mathf.FloorToInt(_basePrice[i]);
		}

		for (int i = 0; i < _manager._prices.Count; i++){
			print(_manager._prices[i]);
		}
	}
}

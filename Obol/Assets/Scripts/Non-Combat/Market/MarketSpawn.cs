using UnityEngine;
using System.Collections.Generic;

public class MarketSpawn : MonoBehaviour {

	public List <int> _basePrice = new List <int>();
	public List <int> _buyPrices = new List <int>();
	public List <int> _sellPrices = new List<int>();
	public int _rumourType;

	// Use this for initialization
	void Awake () {
		SpawnPrices();
	}

	void SpawnPrices(){
		_basePrice.Add(10);
		_basePrice.Add(20);
		_basePrice.Add(50);
		_basePrice.Add(100);

		for (int i = 0; i < _basePrice.Count; i++){
			_buyPrices.Add(Mathf.FloorToInt(_basePrice[i] * 1.2f));
			_sellPrices.Add(Mathf.FloorToInt(_basePrice[i] * 0.8f));
		}
	}
}

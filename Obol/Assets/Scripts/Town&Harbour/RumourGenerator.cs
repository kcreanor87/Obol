using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RumourGenerator : MonoBehaviour {

	public Text _rumourText;
	public Text _rumourButtonText;
	public GameObject _rumourGO;
	public Text _rumourTypeTxt, _rumourModTxt;
	public Button _rumourButton;
	public MarketSpawn _marketSpawn;
	public TownCanvas _townCanvas;
	public SaveGame _saveGame;
	public bool _rumourActive;
	public int _cost = 50;
	public int _loadedRumourType;
	public float _value = 1.0f;
	public bool _increase;
	public bool _noRumour;

	// Use this for initialization
	void Start () {
		_marketSpawn = gameObject.GetComponent<MarketSpawn>();
		_rumourText = GameObject.Find("RumourText").GetComponent<Text>();
		_rumourButton = GameObject.Find("Get Rumour").GetComponent<Button>();
		_rumourButtonText = GameObject.Find("RumourButtonText").GetComponent<Text>();
		_rumourTypeTxt = GameObject.Find("RumourType").GetComponent<Text>();
		_rumourModTxt = GameObject.Find("RumourMod").GetComponent<Text>();
		_rumourGO = GameObject.Find("RumourGO");
		_townCanvas = gameObject.GetComponent<TownCanvas>();
		_rumourGO.SetActive(false);
		_saveGame = GameObject.Find("Loader").GetComponent<SaveGame>();
		if (_rumourActive) LoadRumour();
	}

	public void GenerateRumour(){
		_marketSpawn._rumourMod = 1.0f;
		var chance = Random.Range(0, 101);
		_loadedRumourType = Random.Range(0, _manager._resources.Count);
		if (chance > 75){			
			//Increase in price
			_value = (Random.Range(1.5f, 2.0f));
			_marketSpawn._rumourMod = _value;
			_marketSpawn._rumourType = _loadedRumourType;
			_increase = true;
			_rumourText.text = "" + _manager._resourceNames[_loadedRumourType] + " is scarce and prices will rise";
		}
		else if (chance > 50){
			//Decrese in price
			_value = (Random.Range(0.3f, 0.6f));
			_marketSpawn._rumourMod = _value;
			_marketSpawn._rumourType = _loadedRumourType;
			_increase = false;			
			_rumourText.text = "Sources of " + _manager._resourceNames[_loadedRumourType] + " are abundant at the moment.";
		}
		else{
			//You hear nothing
			_rumourText.text = "I've heard nothing but local anecdotes and petty arguments";
			_marketSpawn._rumourMod = 1.0f;
			_noRumour = true;
		}
		SpawnUI();
		_manager._obols -= _cost;
		WM_UI.UpdateUI();
		_marketSpawn.GeneratePrices();
		_rumourActive = true;
		_rumourButton.interactable = false;
		_saveGame.Save();
	}

	public void ClearRumour(){
		_rumourGO.SetActive(false);
		_value = 1.0f;
		_marketSpawn._rumourMod = 1.0f;
		_rumourActive = false;
		_noRumour = false;
		_marketSpawn.GeneratePrices();
	}

	public void EnterText(){
		_rumourText.text = (_rumourActive) ? "There is nothing new to find out, you should come back later" : "For a few coins I could let you in on the latest rumours";
		_rumourButtonText.text = "Rumour (" + _cost + "o)";
		_rumourButton.interactable = (!_rumourActive && _manager._obols >= _cost);
	}

	void SpawnUI(){
		_rumourGO.SetActive(true);
		_rumourTypeTxt.text = (!_noRumour) ? _manager._resourceNames[_marketSpawn._rumourType] : "None";
		_rumourModTxt.text = (_marketSpawn._rumourMod > 1.0f) ? "+" : "-";
	}

	public void LoadRumour(){
		_marketSpawn._rumourMod = _value;
		_marketSpawn._rumourType = _loadedRumourType;
		SpawnUI();
		WM_UI.UpdateUI();
		_marketSpawn.GeneratePrices();
		_rumourActive = true;
		_rumourButton.interactable = false;
	}
}

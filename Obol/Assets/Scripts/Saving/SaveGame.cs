using UnityEngine;
using System.Collections.Generic;

public class SaveGame : MonoBehaviour {

	public RumourGenerator _rumourScript;
	public TownCanvas _townCanvas;
	public MarketSpawn _marketSpawn;

	void Awake(){
		PopulateLists();
		if (!NewGame._newGame) Load();
		else PlayerPrefs.DeleteAll();
	}

	void PopulateLists(){
		_rumourScript = GameObject.Find("TownCanvas").GetComponent<RumourGenerator>();
		_townCanvas = GameObject.Find("TownCanvas").GetComponent<TownCanvas>();
		_marketSpawn = GameObject.Find("TownCanvas").GetComponent<MarketSpawn>();
	}

	public void Save(){
		SaveTownStatus();
		SavePlayerResources();
		SaveCombatStats();
		SavePrices();
		NewGame._newGame = false;
	}

	public void Load(){
		LoadPrices();
		LoadTownStatus();
		LoadCombatStats();
		LoadPlayerResources();
		LoadRumour();	
	}

	void SavePrices(){
		PlayerPrefs.SetInt("Resource Count", _marketSpawn._basePrice.Count);
		for (int i = 0; i < _marketSpawn._basePrice.Count; i++){
			PlayerPrefs.SetFloat("Price" + i, _marketSpawn._basePrice[i]);
		}	
	}

	void LoadPrices(){
		for (int i = 0; i < PlayerPrefs.GetInt("Resource Count"); i++){
			_marketSpawn._basePrice.Add(PlayerPrefs.GetFloat("Price" + i));
		}	
	}

	void SaveTownStatus(){
		PlayerPrefs.SetInt("Total Buildings", 0);
		for (int i = 0; i < _townCanvas._activeBuildings.Count; i++){
			PlayerPrefs.SetInt("Active Buildings" + i, (_townCanvas._activeBuildings[i] ? 1 : 0));
			PlayerPrefs.SetInt("Total Buildings", PlayerPrefs.GetInt("Total Buildings") + 1);
		}				
	}

	void LoadTownStatus(){
		for (int i = 0; i < PlayerPrefs.GetInt("Total Buildings"); i++){
			_townCanvas._activeBuildings.Add(PlayerPrefs.GetInt("Active Buildings" + i) > 0);
		}
	}

	void SavePlayerResources(){
		for (int i = 0; i < _manager._resources.Count; i++){
			PlayerPrefs.SetInt("Resource" + i, _manager._resources[i]);
		}
		PlayerPrefs.SetInt("Obols", _manager._obols);
		PlayerPrefs.SetInt("Repute", _manager._repute);
		for (int i = 0; i < _manager._factoryOuput.Count; i++){
			PlayerPrefs.SetInt("Output" + i, _manager._factoryOuput[i]);
		}
	}

	void LoadPlayerResources(){
		for (int i = 0; i < _manager._resources.Count; i++){
			_manager._resources[i] = PlayerPrefs.GetInt("Resource" + i);
		}
		_manager._obols = PlayerPrefs.GetInt("Obols");
		_manager._repute = PlayerPrefs.GetInt("Repute");
		for (int i = 0; i < _manager._factoryOuput.Count; i++){
			_manager._factoryOuput[i] = PlayerPrefs.GetInt("Output" + i);
		}
	}

	void SaveRumour(){
		PlayerPrefs.SetFloat("Value", _rumourScript._value);
		PlayerPrefs.SetInt("RumourActive", (_rumourScript._rumourActive ?1: 0));
		PlayerPrefs.SetInt("RumourType", _rumourScript._loadedRumourType);
		PlayerPrefs.SetInt("Increase", (_rumourScript._increase ? 1 : 0));
	}

	void LoadRumour(){
		_rumourScript._rumourActive = (PlayerPrefs.GetInt("RumourActive") > 0);
		_rumourScript._loadedRumourType = PlayerPrefs.GetInt("RumourType");
		_rumourScript._increase = (PlayerPrefs.GetInt("Increase") > 0);
		_rumourScript._value = PlayerPrefs.GetFloat("Value");
	}

	void SaveCombatStats(){
		PlayerPrefs.SetInt("STR", _CombatManager._str);
		PlayerPrefs.SetInt("DEX", _CombatManager._dex);
		PlayerPrefs.SetInt("VIT", _CombatManager._vit);
		PlayerPrefs.SetInt("Init", _CombatManager._init);
		PlayerPrefs.SetInt("Ranged", _CombatManager._weaponDb._rangedDatabase.IndexOf(_CombatManager._equipRanged));
		PlayerPrefs.SetInt("Head", _CombatManager._armourDb._headDatabase.IndexOf(_CombatManager._headSlot));
		PlayerPrefs.SetInt("Chest", _CombatManager._armourDb._chestDatabase.IndexOf(_CombatManager._chestSlot));
		PlayerPrefs.SetInt("Legs", _CombatManager._armourDb._legDatabase.IndexOf(_CombatManager._legSlot));
		for (int i = 0; i < _CombatManager._skills.Count; i++){
			PlayerPrefs.SetInt("Skill" + i, _CombatManager._skills[i]);
		}
	}

	void LoadCombatStats(){
		_CombatManager._str = PlayerPrefs.GetInt("STR");
		_CombatManager._dex = PlayerPrefs.GetInt("DEX");
		_CombatManager._vit = PlayerPrefs.GetInt("VIT");
		_CombatManager._init = PlayerPrefs.GetInt("Init");
		_CombatManager._currentHealth = PlayerPrefs.GetInt("CurrentHealth");
		_CombatManager._equipRanged = _CombatManager._weaponDb._rangedDatabase[PlayerPrefs.GetInt("Ranged")];
		_CombatManager._headSlot = _CombatManager._armourDb._headDatabase[PlayerPrefs.GetInt("Head")];
		_CombatManager._chestSlot = _CombatManager._armourDb._chestDatabase[PlayerPrefs.GetInt("Chest")];
		_CombatManager._legSlot = _CombatManager._armourDb._legDatabase[PlayerPrefs.GetInt("Legs")];
		_CombatManager._skills.Clear();
		for (int i = 0; i < 10; i++){
			if (PlayerPrefs.HasKey("Skill" + i)){
				_CombatManager._skills.Add(i);
			}
		}
		_CombatManager.CalculateStats();
	}

	public void CombatOverSave(){
		SavePlayerResources();
	}
}

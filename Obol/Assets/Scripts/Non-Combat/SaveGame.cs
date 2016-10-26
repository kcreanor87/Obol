using UnityEngine;

public class SaveGame : MonoBehaviour {
	
	public MarketSpawn _marketSpawn;
	public static bool _combat;

	void Awake(){
		PopulateLists();
		if (!NewGame._newGame) Load();
		else {
			PlayerPrefs.DeleteAll();
		}
	}

	void PopulateLists(){
		_marketSpawn = GameObject.Find("MerchantScreen").GetComponent<MarketSpawn>();
	}

	public void Save(){
		SaveCombatStats();
		SavePrices();
		SaveResources();
		SaveLocations();
		NewGame._newGame = false;
	}

	public void Load(){
		LoadPrices();
		LoadResources();
		LoadCombatStats();
		LoadLocations();
	}

	void SaveResources(){
		for (int i = 0; i < _manager._resources.Count; i++){
			PlayerPrefs.SetInt("Resources" + i, _manager._resources[i]);
		}
		PlayerPrefs.SetInt("Obols", _manager._obols);
	}

	void LoadResources(){
		for (int i = 0; i < _manager._resources.Count; i++){
			_manager._resources[i] = PlayerPrefs.GetInt("Resources" + i);
		}
		_manager._obols = PlayerPrefs.GetInt("Obols");
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

	void SaveCombatStats(){
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

	void SaveLocations(){
		for (int i = 2; i < _manager._activeLevels.Count; i++){
			PlayerPrefs.SetInt("Level" + i, _manager._activeLevels[i] ? 1 : 0);
		}
		for (int i = 0; i < _manager._activePortals.Count; i++){
			PlayerPrefs.SetInt("Level Portals" + i, _manager._activePortals[i]);
		}
	}

	void LoadLocations(){
		for (int i = 0; i < 10; i++){
			if (PlayerPrefs.HasKey("Level" + i)){
				_manager._activeLevels.Add(true);
				if (i > 1){
					_manager._activePortals.Add(0);
				}				
			}
		}
		for (int i = 0; i < _manager._activePortals.Count; i++){
			_manager._activePortals[i] = PlayerPrefs.GetInt("Level Portals" + i);
		}
	}
}

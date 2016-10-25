using UnityEngine;

public class SaveGame : MonoBehaviour {
	
	public MarketSpawn _marketSpawn;
	public static bool _combat;

	void Awake(){
		if (!_combat){
			PopulateLists();
			if (!NewGame._newGame) Load();
			else PlayerPrefs.DeleteAll();
		}		
	}

	void PopulateLists(){
		_marketSpawn = GameObject.Find("TownCanvas").GetComponent<MarketSpawn>();
	}

	public void Save(){
		SaveCombatStats();
		SavePrices();
		NewGame._newGame = false;
	}

	public void Load(){
		LoadPrices();
		LoadCombatStats();
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
}

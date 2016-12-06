using UnityEngine;

public class SaveGame : MonoBehaviour {	

	void Awake(){
		if (!NewGame._newGame) Load();
		else {
			PlayerPrefs.DeleteAll();
		}
	}

	public void Save(){
		SaveExp();
		SaveEquipment();
		SaveRanks();
		SaveItems();
		SaveLevels();
		SaveChatStates();
		SaveObols();
		NewGame._newGame = false;
		print("Game Saved");
	}

	public void CombatSave(){
		SaveExp();
		SaveRanks();
		SaveLevels();
		SaveObols();
		print("Combat Save");
	}

	public void Load(){	
		LoadExp();
		LoadEquipment();
		LoadRanks();
		LoadItems();
		LoadChatStates();
		LoadLevels();
		LoadObols();
		print ("Game Loaded");			
	}

	void SaveEquipment(){
		PlayerPrefs.SetInt("Ranged", _CombatManager._weaponDb._rangedDatabase.IndexOf(_CombatManager._equipRanged));
		PlayerPrefs.SetInt("Head", _CombatManager._armourDb._headDatabase.IndexOf(_CombatManager._headSlot));
		PlayerPrefs.SetInt("Chest", _CombatManager._armourDb._chestDatabase.IndexOf(_CombatManager._chestSlot));
		PlayerPrefs.SetInt("Legs", _CombatManager._armourDb._legDatabase.IndexOf(_CombatManager._legSlot));
	}

	void LoadEquipment(){
		_CombatManager._equipRanged = _CombatManager._weaponDb._rangedDatabase[PlayerPrefs.GetInt("Ranged")];
		_CombatManager._headSlot = _CombatManager._armourDb._headDatabase[PlayerPrefs.GetInt("Head")];
		_CombatManager._chestSlot = _CombatManager._armourDb._chestDatabase[PlayerPrefs.GetInt("Chest")];
		_CombatManager._legSlot = _CombatManager._armourDb._legDatabase[PlayerPrefs.GetInt("Legs")];
		_CombatManager.CalculateStats();
	}

	void SaveChatStates(){
		for (int i = 0; i < _manager._npcChat.Count; i++){
			PlayerPrefs.SetInt("ChatActive" + i, (_manager._npcChat[i] ? 1: 0));
		}
		for (int i = 0; i < _manager._chatState.Count; i++){
			PlayerPrefs.SetInt("ChatState" + i, _manager._chatState[i]);
		}
	}

	void LoadChatStates(){
		for (int i = 0; i < _manager._npcChat.Count; i++){
			_manager._npcChat[i] = (PlayerPrefs.GetInt("ChatActive" + i) > 0);
		}
		for (int i = 0; i <  _manager._chatState.Count; i++){
			 _manager._chatState[i] = PlayerPrefs.GetInt("ChatState" + i);
		}
	}	

	void SaveLevels(){
		for (int i = 0; i < _manager._totalLevels; i++){
			PlayerPrefs.SetInt("Level" + i, (_manager._activeLevels[i] ? 1 : 0));
		}
	}

	void LoadLevels(){
		for (int i = 0; i < _manager._totalLevels; i++){
			_manager._activeLevels[i] = (PlayerPrefs.GetInt("Level" + i) > 0);
		}
	}

	void SaveRanks(){
		PlayerPrefs.SetInt("Level", _manager._level);
		PlayerPrefs.SetInt("AvailRanks", _manager._availableRanks);
		PlayerPrefs.SetInt("VitBonus", _CombatManager._vitRanks);
		PlayerPrefs.SetInt("AttBonus", _CombatManager._attRanks);
		PlayerPrefs.SetInt("DefBonus", _CombatManager._defRanks);
		PlayerPrefs.SetInt("SpdBonus", _CombatManager._dexRanks);
	}

	void LoadRanks(){
		_manager._level = PlayerPrefs.GetInt("Level");
		_manager._availableRanks = PlayerPrefs.GetInt("AvailRanks");
		_CombatManager._vitRanks = PlayerPrefs.GetInt("VitBonus");
		_CombatManager._attRanks = PlayerPrefs.GetInt("AttBonus");
		_CombatManager._defRanks = PlayerPrefs.GetInt("DefBonus");
		_CombatManager._dexRanks = PlayerPrefs.GetInt("SpdBonus");
	}

	void SaveItems(){
		for (int i = 0; i < _CombatManager._weaponDb._rangedDatabase.Count; i++){
			PlayerPrefs.SetInt("Weapon" + i, (_CombatManager._weaponDb._rangedDatabase[i]._bought) ? 1 : 0);
		}
		for (int i = 0; i < _CombatManager._armourDb._headDatabase.Count; i++){
			PlayerPrefs.SetInt("Head" + i, (_CombatManager._armourDb._headDatabase[i]._bought) ? 1 : 0);
		}
		for (int i = 0; i < _CombatManager._armourDb._chestDatabase.Count; i++){
			PlayerPrefs.SetInt("Chest" + i, (_CombatManager._armourDb._chestDatabase[i]._bought) ? 1 : 0);
		}
		for (int i = 0; i < _CombatManager._armourDb._legDatabase.Count; i++){
			PlayerPrefs.SetInt("Legs" + i, (_CombatManager._armourDb._legDatabase[i]._bought) ? 1 : 0);
		}
	}

	void LoadItems(){
		for (int i = 0; i < _CombatManager._weaponDb._rangedDatabase.Count; i++){
			_CombatManager._weaponDb._rangedDatabase[i]._bought = PlayerPrefs.GetInt("Weapon" + i) > 0;
		}
		for (int i = 0; i < _CombatManager._armourDb._headDatabase.Count; i++){
			_CombatManager._armourDb._headDatabase[i]._bought = PlayerPrefs.GetInt("Head" + i) > 0;
		}
		for (int i = 0; i < _CombatManager._armourDb._chestDatabase.Count; i++){
			_CombatManager._armourDb._chestDatabase[i]._bought = PlayerPrefs.GetInt("Chest" + i) > 0;
		}
		for (int i = 0; i < _CombatManager._armourDb._legDatabase.Count; i++){
			_CombatManager._armourDb._legDatabase[i]._bought = PlayerPrefs.GetInt("Legs" + i) > 0;
		}
	}

	void SaveExp(){
		PlayerPrefs.SetInt("Level", _manager._level);
		PlayerPrefs.SetInt("CurrentXP", _manager._currentXP);		
		PlayerPrefs.SetInt("PrevXP", _manager._prevXP);
		PlayerPrefs.SetInt("NextXP", _manager._nextLvlXP);
	}

	void LoadExp(){
		_manager._level = PlayerPrefs.GetInt("Level");
		_manager._currentXP = PlayerPrefs.GetInt("CurrentXP");
		_manager._prevXP = PlayerPrefs.GetInt("PrevXP");
		_manager._nextLvlXP = PlayerPrefs.GetInt("NextXP");
	}

	void SaveObols(){
		PlayerPrefs.SetInt("Obols", _manager._obols);
	}

	void LoadObols(){
		_manager._obols = PlayerPrefs.GetInt("Obols");
	}
}

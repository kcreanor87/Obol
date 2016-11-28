using UnityEngine;

public class SaveGame : MonoBehaviour {	

	void Awake(){
		if (!NewGame._newGame) Load();
		else {
			PlayerPrefs.DeleteAll();
		}
	}

	public void Save(){
		SaveCombatStats();
		SaveBlessings();
		SaveResources();
		SaveLocations();
		SaveItems();
		SaveCollectables();
		SaveChatStates();
		NewGame._newGame = false;
		print("Game Saved");
	}

	public void Load(){
		LoadCollectables();
		LoadResources();
		LoadBlessings();
		LoadItems();
		LoadCombatStats();
		LoadChatStates();
		LoadLocations();
		print ("Game Loaded");

			
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

	void SaveCombatStats(){
		PlayerPrefs.SetInt("Ranged", _CombatManager._weaponDb._rangedDatabase.IndexOf(_CombatManager._equipRanged));
		PlayerPrefs.SetInt("Head", _CombatManager._armourDb._headDatabase.IndexOf(_CombatManager._headSlot));
		PlayerPrefs.SetInt("Chest", _CombatManager._armourDb._chestDatabase.IndexOf(_CombatManager._chestSlot));
		PlayerPrefs.SetInt("Legs", _CombatManager._armourDb._legDatabase.IndexOf(_CombatManager._legSlot));
	}

	void SaveItems(){
		for (int i = 0; i < _CombatManager._itemsUnlocked.Count; i++){
			PlayerPrefs.SetInt("Unlocked" + i, (_CombatManager._itemsUnlocked[i] ? 1 : 0));
		}
		for (int i = 0; i < _CombatManager._itemLevels.Count; i++){
			PlayerPrefs.SetInt("ItemLevel" + i, (_CombatManager._itemLevels[i]));
		}
		for (int i = 0; i < _CombatManager._itemsEquipped.Count; i++){
			PlayerPrefs.SetInt("ItemEquipped" + i, _CombatManager._itemsEquipped[i]);
		}
	}

	void LoadItems(){
		for (int i = 0; i < _CombatManager._itemsUnlocked.Count; i++){
			_CombatManager._itemsUnlocked[i] = (PlayerPrefs.GetInt("Unlocked" + i) > 0);
		}
		for (int i = 0; i < _CombatManager._itemLevels.Count; i++){
			_CombatManager._itemLevels[i] = PlayerPrefs.GetInt("ItemLevel" + i);
		}
		for (int i = 0; i < _CombatManager._itemsEquipped.Count; i++){
			_CombatManager._itemsEquipped[i] = PlayerPrefs.GetInt("ItemEquipped" + i);
		}
	}

	void SaveCollectables(){
		for (int i = 0; i < _CombatManager._collectables.Count; i++){
			PlayerPrefs.SetInt("Collectables" + i, (_CombatManager._collectables[i] ? 1: 0));
		}
	}

	void LoadCollectables(){
		for (int i = 0; i < _CombatManager._collectables.Count; i++){
			_CombatManager._collectables[i] = (PlayerPrefs.GetInt("Collectables" + i) > 0);
		}
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

	void LoadCombatStats(){
		_CombatManager._equipRanged = _CombatManager._weaponDb._rangedDatabase[PlayerPrefs.GetInt("Ranged")];
		_CombatManager._headSlot = _CombatManager._armourDb._headDatabase[PlayerPrefs.GetInt("Head")];
		_CombatManager._chestSlot = _CombatManager._armourDb._chestDatabase[PlayerPrefs.GetInt("Chest")];
		_CombatManager._legSlot = _CombatManager._armourDb._legDatabase[PlayerPrefs.GetInt("Legs")];
		_CombatManager.CalculateStats();
	}

	void SaveLocations(){
		for (int i = 2; i < _manager._activeLevels.Count; i++){
			PlayerPrefs.SetInt("Level" + i, (_manager._activeLevels[i] ? 1 : 0));
		}
		for (int i = 0; i < _manager._activePortals.Count; i++){
			PlayerPrefs.SetInt("Level Portals" + i, (_manager._activePortals[i]));
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

	void SaveBlessings(){
		PlayerPrefs.SetInt("Blessings", _CombatManager._blessings);
		PlayerPrefs.SetInt("AvailBlessings", _CombatManager._availBlessings);
		PlayerPrefs.SetInt("VitBonus", _CombatManager._vitBonus);
		PlayerPrefs.SetInt("AttBonus", _CombatManager._attBlessings);
		PlayerPrefs.SetInt("DefBonus", _CombatManager._defBlessings);
		PlayerPrefs.SetInt("SpdBonus", _CombatManager._spdBonus);
	}

	void LoadBlessings(){
		_CombatManager._blessings = PlayerPrefs.GetInt("Blessings");
		_CombatManager._availBlessings = PlayerPrefs.GetInt("AvailBlessings");
		_CombatManager._vitBonus = PlayerPrefs.GetInt("VitBonus");
		_CombatManager._attBlessings = PlayerPrefs.GetInt("AttBonus");
		_CombatManager._defBlessings = PlayerPrefs.GetInt("DefBonus");
		_CombatManager._spdBonus = PlayerPrefs.GetInt("SpdBonus");
	}

	public void CombatSave(){
		SaveLocations();
		SaveResources();
		SaveCollectables();
	}
}

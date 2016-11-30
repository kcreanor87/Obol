using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Smith : MonoBehaviour {

	public List <Button> _weapons = new List <Button>();
	public List <Button> _head = new List <Button>();
	public List <Button> _chest = new List <Button>();
	public List <Button> _legs = new List <Button>();

	public Button _buyWeapon;
	public Button _buyArmour;

	public Weapon _activeWeapon;
	public Armour _activeArmour;

	public int _activeType;
	public int _activeIndex;

	public GameObject _smithGO;
	public GameObject _weaponCanvas;
	public GameObject _headCanvas;
	public GameObject _chestCanvas;
	public GameObject _legCanvas;

	public GameObject _weaponStats;
	public GameObject _armourStats;

	public void OpenCanvas(){
		_smithGO.SetActive(true);
		SetOpenCanvas(3);
	}

	public void CloseCanvas(){
		_smithGO.SetActive(false);
	}

	void CheckWeaponAvailability(){
		for (int i = 0; i < _weapons.Count; i++){
			_activeWeapon = _CombatManager._weaponDb._rangedDatabase[i];
			var text = _weapons[i].GetComponentInChildren<Text>();
			if (_manager._level < _activeWeapon._levelReq){
				_weapons[i].interactable = false;
				text.text = "Unlocked Lvl " + _activeWeapon._levelReq;
			}
			else{
				_weapons[i].interactable = true;
				text.text = _activeWeapon._name;
			}
		}
	}

	void CheckHelmAvailability(){
		for (int i = 0; i < _head.Count; i++){
			_activeArmour = _CombatManager._armourDb._headDatabase[i];
			_head[i].interactable = (_manager._level >= _activeArmour._levelReq);
		}
	}

	void CheckChestAvailability(){
		for (int i = 0; i < _chest.Count; i++){
			_activeArmour = _CombatManager._armourDb._chestDatabase[i];
			_chest[i].interactable = (_manager._level >= _activeArmour._levelReq);
		}
	}

	void CheckLegAvailability(){
		for (int i = 0; i < _legs.Count; i++){
			_activeArmour = _CombatManager._armourDb._legDatabase[i];
			_legs[i].interactable = (_manager._level >= _activeArmour._levelReq);
		}
	}

	public void SetOpenCanvas(int type){

		ResetCanvases();

		switch(type){
			case 0:
			_headCanvas.SetActive(true);
			CheckHelmAvailability();
			break;
			case 1:
			_chestCanvas.SetActive(true);
			CheckChestAvailability();
			break;
			case 2:
			_legCanvas.SetActive(true);
			CheckLegAvailability();
			break;
			case 3:
			_weaponCanvas.SetActive(true);
			CheckWeaponAvailability();
			break;
		}
		_activeType = type;
	}

	void ResetCanvases(){
		_headCanvas.SetActive(false);
		_chestCanvas.SetActive(false);
		_legCanvas.SetActive(false);
		_weaponCanvas.SetActive(false);
		_weaponStats.SetActive(false);
		_armourStats.SetActive(false);
	}

	public void SetActiveWeapon(int weapon){
		_activeWeapon = _CombatManager._weaponDb._rangedDatabase[weapon];
		_activeIndex = weapon;
		_weaponStats.SetActive(true);
		_buyWeapon.interactable = (!_activeWeapon._bought && _manager._obols >= _activeWeapon._cost);
	}

	public void SetActiveArmour(int armour){
		switch(_activeType){
			case 0:
			_activeArmour = _CombatManager._armourDb._headDatabase[armour];
			break;
			case 1:
			_activeArmour = _CombatManager._armourDb._chestDatabase[armour];
			break;
			case 2:
			_activeArmour = _CombatManager._armourDb._legDatabase[armour];
			break;
		}
		_armourStats.SetActive(true);
		_activeIndex = armour;
		_buyArmour.interactable = (!_activeArmour._bought && _manager._obols >= _activeArmour._cost);
	}

	public void BuyWeapon(){
		_manager._obols -= _activeWeapon._cost;
		_CombatManager._weaponDb._rangedDatabase[_activeIndex]._bought = true;
		_buyWeapon.interactable = false;
	}

	public void BuyArmour(){
		_manager._obols -= _activeArmour._cost;
		switch(_activeType){
			case 0:
			_CombatManager._armourDb._headDatabase[_activeIndex]._bought = true;
			break;
			case 1:
			_CombatManager._armourDb._chestDatabase[_activeIndex]._bought = true;
			break;
			case 2:
			_CombatManager._armourDb._legDatabase[_activeIndex]._bought = true;
			break;
		}
		_buyArmour.interactable = false;
	}
}
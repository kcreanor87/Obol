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

	public Sprite _boughtSprite;
	public Sprite _equippedSprite;
	public Sprite _affordableSprite;
	public Sprite _nonAffordable;
	public Sprite _notUnlocked;

	public Text _currentWeapName, _currentWeapDam, _currentWeapFire, _currentWeapRad;
	public Text _newWeapName, _newWeapDam, _newWeapFire, _newWeapRad;
	public Text _weaponCost;

	public Text _currentArmName, _currentArmBns, _currentArmWeight;
	public Text _newArmName, _newArmBns, _newArmWeight;
	public Text _armourCost;

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
			var image = _weapons[i].GetComponent<Image>();
			if (_manager._level < _activeWeapon._levelReq){
				_weapons[i].interactable = false;
				text.text = "Unlocked Lvl " + _activeWeapon._levelReq;
				image.sprite = _notUnlocked;
			}
			else{				
				if (_activeWeapon._bought){					
					image.sprite = (_CombatManager._equipRanged._id == _activeWeapon._id) ? _equippedSprite : _boughtSprite;
				}
				else{
					image.sprite = (_manager._obols >= _activeWeapon._cost) ? _affordableSprite : _nonAffordable;
				}
				_weapons[i].interactable = true;
				text.text = _activeWeapon._name;
			}
		}
	}

	void CheckHelmAvailability(){
		for (int i = 0; i < _head.Count; i++){
			_activeArmour = _CombatManager._armourDb._headDatabase[i];
			var text = _head[i].GetComponentInChildren<Text>();
			var image = _head[i].GetComponent<Image>();
			if (_manager._level < _activeArmour._levelReq){
				_head[i].interactable = false;
				text.text = "Unlocked Lvl " + _activeArmour._levelReq;
				image.sprite = _notUnlocked;
			}
			else{
				if (_activeArmour._bought){					
					image.sprite = (_CombatManager._headSlot._id == _activeArmour._id) ? _equippedSprite : _boughtSprite;
				}
				else{
					image.sprite = (_manager._obols >= _activeArmour._cost) ? _affordableSprite : _nonAffordable;
				}
				_head[i].interactable = true;
				text.text = _activeArmour._name;
			}
		}
	}

	void CheckChestAvailability(){
		for (int i = 0; i < _chest.Count; i++){
			_activeArmour = _CombatManager._armourDb._chestDatabase[i];			
			var text = _chest[i].GetComponentInChildren<Text>();
			var image = _chest[i].GetComponent<Image>();
			if (_manager._level < _activeArmour._levelReq){
				_chest[i].interactable = false;
				text.text = "Unlocked Lvl " + _activeArmour._levelReq;
				image.sprite = _notUnlocked;
			}
			else{
				if (_activeArmour._bought){					
					image.sprite = (_CombatManager._chestSlot._id == _activeArmour._id) ? _equippedSprite : _boughtSprite;
				}
				else{
					image.sprite = (_manager._obols >= _activeArmour._cost) ? _affordableSprite : _nonAffordable;
				}
				_chest[i].interactable = true;
				text.text = _activeArmour._name;
			}

		}
	}

	void CheckLegAvailability(){
		for (int i = 0; i < _legs.Count; i++){
			_activeArmour = _CombatManager._armourDb._legDatabase[i];
			var text = _legs[i].GetComponentInChildren<Text>();
			var image = _legs[i].GetComponent<Image>();
			if (_manager._level < _activeArmour._levelReq){
				_legs[i].interactable = false;
				text.text = "Unlocked Lvl " + _activeArmour._levelReq;
				image.sprite = _notUnlocked;
			}
			else{
				if (_activeArmour._bought){					
					image.sprite = (_CombatManager._legSlot._id == _activeArmour._id) ? _equippedSprite : _boughtSprite;
				}
				else{
					image.sprite = (_manager._obols >= _activeArmour._cost) ? _affordableSprite : _nonAffordable;
				}
				_legs[i].interactable = true;
				text.text = _activeArmour._name;
			}
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
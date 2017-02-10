using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Smith : MonoBehaviour {

	public List <Button> _weapons = new List <Button>();
	public List <Button> _head = new List <Button>();
	public List <Button> _chest = new List <Button>();
	public List <Button> _legs = new List <Button>();

	public Text _buyWeapon;
	public Text _buyArmour;

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

	public GameObject _itemSelected;

	public Sprite _boughtSprite;
	public Sprite _equippedSprite;
	public Sprite _affordableSprite;
	public Sprite _nonAffordable;
	public Sprite _notUnlocked;

	public Text _currentWeapDam, _currentWeapFire, _currentWeapRad;
	public Text _newWeapName, _newWeapDam, _newWeapFire, _newWeapRad;
	public Text _weaponCost;

	public Text _currentArmBns, _currentArmWeight;
	public Text _newArmName, _newArmBns, _newArmWeight;
	public Text _armourCost;

	public NonCombat_UI _ui;

	public PrefabControl _prefabControls;

	public Button _weaponSelect;
	public Button _headSelect;
	public Button _chestSelect;
	public Button _legSelect;

	public bool _open;
	public int _activeMenu;

	public List <Button> _tabs = new List <Button>();

	public Color _highlighted;
	public Color _normal;

	public int _idStore;

	public void OpenCanvas(){
		_smithGO.SetActive(true);
		SetOpenCanvas(3);
		_ui._uiOpen = true;
		_open = true;
		_activeMenu = 3;
	}

	public void CloseCanvas(){
		_smithGO.SetActive(false);
		_ui._uiOpen = false;
		_open = false;
	}

	void Awake(){
		CollectElements();		
	}

	void Update(){
		if (_open){
			if (Input.GetButtonDown("RB")){
				_activeMenu++;
				if (_activeMenu == 4){
					_activeMenu = 0;
				}
				SetOpenCanvas(_activeMenu);
			}
			else if (Input.GetButtonDown("LB")){
				_activeMenu--;
				if (_activeMenu < 0){
					_activeMenu = 3;
				}
				SetOpenCanvas(_activeMenu);
			}
		}		
	}

	void CollectElements(){
		_currentWeapDam = GameObject.Find("CurrentWeapDam").GetComponent<Text>();
		_currentWeapFire = GameObject.Find("CurrentWeapFR").GetComponent<Text>();
		_currentWeapRad = GameObject.Find("CurrentWeapRad").GetComponent<Text>();
		_newWeapName = GameObject.Find("WeaponName").GetComponent<Text>();
		_newWeapDam = GameObject.Find("NewWeapDam").GetComponent<Text>();
		_newWeapFire = GameObject.Find("NewWeapFR").GetComponent<Text>();
		_newWeapRad = GameObject.Find("NewWeapRad").GetComponent<Text>();
		_weaponCost = GameObject.Find("NewWeapCost").GetComponent<Text>();
		_currentArmBns = GameObject.Find("CurrentArmDef").GetComponent<Text>();
		_currentArmWeight = GameObject.Find("CurrentArmWeight").GetComponent<Text>();
		_newArmName = GameObject.Find("ArmourName").GetComponent<Text>();
		_newArmBns = GameObject.Find("NewArmDef").GetComponent<Text>();
		_newArmWeight = GameObject.Find("NewArmWeight").GetComponent<Text>();
		_armourCost = GameObject.Find("NewArmCost").GetComponent<Text>();

		_ui = gameObject.GetComponent<NonCombat_UI>();

		_prefabControls = GameObject.Find("Player").GetComponent<PrefabControl>();
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
		_activeType = type;
		switch(type){
			case 0:
			CheckHelmAvailability();
			_headCanvas.SetActive(true);
			SetActiveArmour(0);
			_headSelect.Select();		
			break;
			case 1:
			CheckChestAvailability();
			_chestCanvas.SetActive(true);
			SetActiveArmour(0);
			_chestSelect.Select();			
			break;
			case 2:
			CheckLegAvailability();
			_legCanvas.SetActive(true);	
			SetActiveArmour(0);
			_legSelect.Select();		
			break;
			case 3:
			CheckWeaponAvailability();
			_weaponCanvas.SetActive(true);	
			SetActiveWeapon(0);
			_weaponSelect.Select();		
			break;
		}		
		_prefabControls.EquipGOs();
		for (int i = 0; i < _tabs.Count; i++){
			_tabs[i].GetComponent<Image>().color = (i == type) ? _highlighted : _normal;
		}
	}

	void ResetCanvases(){
		_headCanvas.SetActive(false);
		_chestCanvas.SetActive(false);
		_legCanvas.SetActive(false);
		_weaponCanvas.SetActive(false);
		_weaponStats.SetActive(false);
		_armourStats.SetActive(false);
		_itemSelected.SetActive(false);
	}

	public void SetActiveWeapon(int weapon){
		_activeWeapon = _CombatManager._weaponDb._rangedDatabase[weapon];
		_activeIndex = weapon;
		_itemSelected.SetActive(true);
		_itemSelected.transform.position = _weapons[weapon].transform.position;
		UpdateWeaponText();
		_weaponStats.SetActive(true);		
		if (_activeWeapon._bought){
			if (_activeWeapon._id == _CombatManager._equipRanged._id){
				_buyWeapon.text = "Equipped";
				_buyWeapon.color = Color.white;
			}
			else{
				_buyWeapon.text = "Equip";
				_buyWeapon.color = Color.white;
			}
		}
		else{
			_buyWeapon.text = "Buy";
			_buyWeapon.color = (_activeWeapon._cost > _manager._obols) ? _highlighted : Color.white;
		}
		_prefabControls.PreviewItems(_activeType, weapon);
	}

	public void SetActiveArmour(int armour){
		_itemSelected.SetActive(true);
		_idStore = 0;	
		switch(_activeType){
			case 0:
			_activeArmour = _CombatManager._armourDb._headDatabase[armour];
			_itemSelected.transform.position = _head[armour].transform.position;
			_idStore = _CombatManager._headSlot._id;		
			break;
			case 1:
			_activeArmour = _CombatManager._armourDb._chestDatabase[armour];
			_itemSelected.transform.position = _chest[armour].transform.position;
			_idStore = _CombatManager._chestSlot._id;	
			break;
			case 2:
			_activeArmour = _CombatManager._armourDb._legDatabase[armour];
			_itemSelected.transform.position = _legs[armour].transform.position;
			_idStore = _CombatManager._legSlot._id;	
			break;
		}
		if (_activeArmour._bought){
			if (_activeArmour._id == _idStore){
				_buyArmour.text = "Equipped";
				_buyArmour.color = Color.white;
			}
			else{
				_buyArmour.text = "Equip";
				_buyArmour.color = Color.white;
			}			
		}
		else{
			_buyArmour.text = "Buy";
			_buyArmour.color = (_activeArmour._cost > _manager._obols) ? _highlighted : Color.white;
		}
		_prefabControls.PreviewItems(_activeType, armour);
		UpdateArmourText();
		_armourStats.SetActive(true);
		_activeIndex = armour;
	}
	public void CheckStateWeapon(){
		if (_CombatManager._weaponDb._rangedDatabase[_activeIndex]._bought == true){
			EquipWeapon();
		}
		else{
			BuyWeapon();
		}
	}

	public void BuyWeapon(){
		if (_manager._obols >= _activeWeapon._cost && !_activeWeapon._bought){
			_manager._obols -= _activeWeapon._cost;
			_CombatManager._weaponDb._rangedDatabase[_activeIndex]._bought = true;
			CheckWeaponAvailability();
			SetActiveWeapon(_activeIndex);
			EquipWeapon();
			_ui.UpdateUI();
		}
		else if (_activeWeapon._bought && _activeWeapon._id != _CombatManager._equipRanged._id){
			CheckWeaponAvailability();
			SetActiveWeapon(_activeIndex);
			EquipWeapon();
			_ui.UpdateUI();
		}
	}

	public void EquipWeapon(){
		_CombatManager._equipRanged = _activeWeapon;				
		CheckWeaponAvailability();
		_activeWeapon = _CombatManager._equipRanged;
		_CombatManager.CalculateStats();
		SetActiveWeapon(_activeIndex);
		UpdateWeaponText();		
	}

	public void EquipArmour(){
		switch(_activeType){
			case 0:
			_CombatManager._headSlot = _activeArmour;
			CheckHelmAvailability();
			_activeArmour = _CombatManager._headSlot;
			break;
			case 1:
			_CombatManager._chestSlot = _activeArmour;
			CheckChestAvailability();
			_activeArmour = _CombatManager._chestSlot;
			break;
			case 2:
			_CombatManager._legSlot = _activeArmour;
			CheckLegAvailability();
			_activeArmour = _CombatManager._legSlot;
			break;
		}
		_CombatManager.CalculateStats();
		SetActiveArmour(_activeIndex);
		UpdateArmourText();
	}

	public void BuyArmour(){
		if (_manager._obols >= _activeArmour._cost && !_activeArmour._bought){
			_manager._obols -= _activeArmour._cost;
				switch(_activeType){
				case 0:
				_CombatManager._armourDb._headDatabase[_activeIndex]._bought = true;
				CheckHelmAvailability();
				break;
				case 1:
				_CombatManager._armourDb._chestDatabase[_activeIndex]._bought = true;
				CheckChestAvailability();
				break;
				case 2:
				_CombatManager._armourDb._legDatabase[_activeIndex]._bought = true;
				CheckLegAvailability();
				break;				
			}
			SetActiveArmour(_activeIndex);
			EquipArmour();
			_ui.UpdateUI();
		}
		else if (_activeArmour._bought && _activeArmour._id != _idStore){
			SetActiveArmour(_activeIndex);
			EquipArmour();
			_ui.UpdateUI();
		}		
	}

	void UpdateWeaponText(){
		_newWeapName.text = _activeWeapon._name;
		
		_currentWeapDam.text = _CombatManager._rangedDam.ToString();
		_currentWeapFire.text = _CombatManager._fireRate + "s";
		_currentWeapRad.text = _CombatManager._radius + "m";

		_newWeapDam.text = Mathf.FloorToInt((float) _activeWeapon._dam * _CombatManager._attBonus).ToString();
		ChangeTextColor(_newWeapDam, (float) _activeWeapon._dam, (float) _CombatManager._equipRanged._dam);

		_newWeapFire.text = _activeWeapon._fireRate + "s";
		ChangeTextColor(_newWeapFire, _CombatManager._equipRanged._fireRate, _activeWeapon._fireRate);

		_newWeapRad.text = _activeWeapon._radius + "m";
		ChangeTextColor(_newWeapRad, _activeWeapon._radius, _CombatManager._equipRanged._radius);

		_weaponCost.text = (_activeWeapon._bought) ? "-" : _activeWeapon._cost.ToString();
		_weaponCost.color = Color.white;
		if (!_activeWeapon._bought && _manager._obols < _activeWeapon._cost) _weaponCost.color = Color.red; 
	}

	void ChangeTextColor(Text text, float valA, float valB){
		if (valA > valB){
			text.color = Color.green;
		}
		else if (valA < valB){
			text.color = Color.red;
		}
		else{
			text.color = Color.white;
		}
	}

	void UpdateArmourText(){
		_newArmName.text = _activeArmour._name;
		_newArmBns.text = Mathf.FloorToInt((float) _activeArmour._armourBonus * _CombatManager._defBonus).ToString();		
		_newArmWeight.text = _activeArmour._weight + "kg";
		switch(_activeType){
			case 0:
			_currentArmBns.text = _CombatManager._headSlot._armourBonus.ToString();
			_currentArmWeight.text = _CombatManager._headSlot._weight + "kg";
			ChangeTextColor(_newArmBns, (float) _activeArmour._armourBonus, (float) _CombatManager._headSlot._armourBonus);
			ChangeTextColor(_newArmWeight, _CombatManager._headSlot._weight, _activeArmour._weight);
			break;
			case 1:
			_currentArmBns.text = _CombatManager._chestSlot._armourBonus.ToString();
			_currentArmWeight.text = _CombatManager._chestSlot._weight + "kg";
			ChangeTextColor(_newArmBns, (float) _activeArmour._armourBonus, (float) _CombatManager._chestSlot._armourBonus);
			ChangeTextColor(_newArmWeight, _CombatManager._chestSlot._weight, _activeArmour._weight);
			break;
			case 2:
			_currentArmBns.text = _CombatManager._legSlot._armourBonus.ToString();
			_currentArmWeight.text = _CombatManager._legSlot._weight + "kg";
			ChangeTextColor(_newArmBns, (float) _activeArmour._armourBonus, (float) _CombatManager._legSlot._armourBonus);
			ChangeTextColor(_newArmWeight, _CombatManager._legSlot._weight, _activeArmour._weight);
			break;
		}
		_armourCost.text = (_activeArmour._bought) ? "-" : _activeArmour._cost.ToString();
		_armourCost.color = Color.white;
		if (!_activeArmour._bought && _manager._obols < _activeArmour._cost) _armourCost.color = Color.red;
	}
}
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Smith : MonoBehaviour {

	public GameObject _weaponScreen, _armourScreen, _headSlots, _chestSlots, _legSlots;
	//Armour text
	public Text _currentName, _currentDef, _currentWgt, _upgradeName, _upgradeDef, _upgradeWgt;
	//Weapon Text
	public Text _currentDam, _currentFR, _currentRad, _weapName, _upWeapName, _upDam, _upFR, _upRad;
	//Plyare stat Text
	public Text _totalDef, _totalHP, _totalSpd, _equipDam, _equipFR, _equipRad;
	public Text _armourCost, _weaponCost;
	public int _activeItem;
	public int _activeSlot;
	public int _cost;
	public bool _affordable;
	public Shooting _shooting;
	public Button _weapUpgradeButton;
	public Button _armUpgradeButton;

	public NonCombat_UI _ui;

	public List <Button> _buttons = new List <Button>();

	public PlayerControls_NonCombat _playerControls;

	void Awake(){
		_weapUpgradeButton = GameObject.Find("WeaponUpgrade").GetComponent<Button>();	
		_armUpgradeButton = GameObject.Find("ArmourUpgrade").GetComponent<Button>();
		_weapUpgradeButton.interactable = false;
		_armUpgradeButton.interactable = false;

		_playerControls = GameObject.Find("Player").GetComponent<PlayerControls_NonCombat>();

		_ui = GameObject.Find("Non-Combat UI").GetComponent<NonCombat_UI>();

		_shooting = GameObject.Find("Launcher").GetComponent<Shooting>();	

		_armourCost = GameObject.Find("ArmourCost").GetComponent<Text>();
		_weaponCost = GameObject.Find("WeaponCost").GetComponent<Text>();

		_currentDef = GameObject.Find("CurrentDef").GetComponent<Text>();
		_currentWgt = GameObject.Find("CurrentWgt").GetComponent<Text>();
		_currentName = GameObject.Find("CurrentName").GetComponent<Text>();
		_upgradeDef = GameObject.Find("UpgradedDef").GetComponent<Text>();
		_upgradeWgt = GameObject.Find("UpgradedWgt").GetComponent<Text>();
		_upgradeName = GameObject.Find("UpgradedName").GetComponent<Text>();

		_currentDam = GameObject.Find("CurrentDam").GetComponent<Text>();
		_currentFR = GameObject.Find("CurrentFR").GetComponent<Text>();
		_currentRad = GameObject.Find("CurrentRad").GetComponent<Text>();
		_weapName = GameObject.Find("WeaponName").GetComponent<Text>();
		_upWeapName = GameObject.Find("UpgWeaponName").GetComponent<Text>();
		_upDam = GameObject.Find("UpgradedDam").GetComponent<Text>();
		_upFR = GameObject.Find("UpgradedFR").GetComponent<Text>();
		_upRad = GameObject.Find("UpgradedRad").GetComponent<Text>();

		_totalDef = GameObject.Find("TotalDef").GetComponent<Text>();
		_totalHP = GameObject.Find("TotalHP").GetComponent<Text>();
		_totalSpd = GameObject.Find("TotalSpd").GetComponent<Text>();
		_equipDam = GameObject.Find("EquipDam").GetComponent<Text>();
		_equipFR = GameObject.Find("EquipFR").GetComponent<Text>();
		_equipRad = GameObject.Find("EquipRad").GetComponent<Text>();

		_armourScreen.SetActive(false);
		_headSlots.SetActive(false);
		_chestSlots.SetActive(false);
		_legSlots.SetActive(false);
	}
	void Start(){
		UpdateStats();
		CheckUnlocked();
	}

	void CheckUnlocked(){
		for (int i = 0; i < _CombatManager._itemsUnlocked.Count; i++){
			_buttons[i].interactable = _CombatManager._itemsUnlocked[i];
		}
	}

	void UpdateStats(){
		_totalDef.text = _CombatManager._armourRating.ToString();
		_totalHP.text = _CombatManager._maxHealth.ToString();
		_totalSpd.text = _CombatManager._speed.ToString();
		_equipDam.text = _CombatManager._rangedDam.ToString();
		_equipFR.text = _CombatManager._fireRate.ToString();
		_equipRad.text = _CombatManager._radius.ToString();
		CalculateCost();
		UpgradeInfo();
		_shooting.SwitchProjectile();
		_playerControls.UpdateMesh();
	}

	void CalculateCost(){
		if (_activeSlot > 0 && _CombatManager._itemLevels[_activeItem] <= 10){
			_cost = (_CombatManager._itemLevels[_activeItem] * 1000);
			_weaponCost.text = _cost.ToString();
			_armourCost.text = _cost.ToString();
		}
		else{
			_weaponCost.text = "-";
			_armourCost.text = "-";
		}
		_affordable = (_manager._obols >= _cost);
		
	}

	public void EquipWeapon(int i){
		_CombatManager._equipRanged = _CombatManager._weaponDb._rangedDatabase[i];
		_activeSlot = 1;
		_activeItem = i;
		_CombatManager._itemsEquipped[0] = _activeItem;
		_CombatManager.CalculateStats();
		UpdateStats();
	}

	public void EquipHeadSlot(int i){
		_CombatManager._headSlot = _CombatManager._armourDb._headDatabase[i + 1];
		_activeSlot = 2;
		_activeItem = 4 + i;
		_CombatManager._itemsEquipped[1] = _activeItem;
		_CombatManager.CalculateStats();
		UpdateStats();
		if (i < 0) ToggleScreens(1);
	}

	public void EquipChestSlot(int i){
		_CombatManager._chestSlot = _CombatManager._armourDb._chestDatabase[i + 1];
		_activeSlot = 3;
		_activeItem = 8 + i;
		_CombatManager._itemsEquipped[2] = _activeItem;
		_CombatManager.CalculateStats();
		UpdateStats();
		if (i < 0) ToggleScreens(2);
	}

	public void EquipLegSlot(int i){
		_CombatManager._legSlot = _CombatManager._armourDb._legDatabase[i + 1];
		_activeSlot = 4;
		_activeItem = 12 + i;
		_CombatManager._itemsEquipped[3] = _activeItem;
		_CombatManager.CalculateStats();		
		UpdateStats();
		if (i < 0) ToggleScreens(3);
	}

	public void ToggleScreens(int i){	
		CheckUnlocked();
		_activeSlot = 0;
		_weapUpgradeButton.interactable = false;
		_armUpgradeButton.interactable = false;		
		UpdateStats();	
		_armourScreen.SetActive(i > 0);
		_weaponScreen.SetActive(i == 0);
		_headSlots.SetActive(i == 1);
		_chestSlots.SetActive(i == 2);
		_legSlots.SetActive(i == 3);
	}

	public void UpgradeItem(){
		_manager._obols -= _cost;
		_ui.UpdateUI();
		_CombatManager._itemLevels[_activeItem]++;
		_CombatManager._itemsEquipped[_activeSlot - 1] = _activeItem;
		_CombatManager.CalculateStats();
		UpdateStats();
	}

	void UpgradeInfo(){
		if (_activeSlot > 0){
			_weapUpgradeButton.interactable = ((_CombatManager._itemLevels[_activeItem] <= 10) && _affordable);
			_armUpgradeButton.interactable = ((_CombatManager._itemLevels[_activeItem] <= 10) && _affordable);
		} 
		switch (_activeSlot){
			//Default
			case 0:
			_currentName.text = "-";
			_currentDef.text = "-";
			_currentWgt.text = "-";
			_upgradeDef.text = "-";
			_upgradeName.text = "-";
			_upgradeWgt.text = "-";
			break;
			//Weapon slot
			case 1:
			if (_CombatManager._itemLevels[_activeItem] <= 10){
				_upWeapName.text = _CombatManager._equipRanged._name + " +" + (_CombatManager._itemLevels[_CombatManager._itemsEquipped[_activeSlot - 1]]);
				_upDam.text = ((_CombatManager._equipRanged._dam * (0.9f + (_CombatManager._itemLevels[_CombatManager._itemsEquipped[0]] + 1) * 0.1f)) * _CombatManager._attBonus).ToString();
				_upFR.text = _CombatManager._fireRate.ToString();
				_upRad.text = _CombatManager._radius.ToString();
			}
			else{
				_upWeapName.text = "-";
				_upDam.text = "-";
				_upFR.text = "-";
				_upRad.text ="-";
			}
			if (_CombatManager._itemLevels[_activeItem] > 1){
				_weapName.text = _CombatManager._equipRanged._name + " +" + (_CombatManager._itemLevels[_activeItem] - 1);
			}
			else{
				_weapName.text = _CombatManager._equipRanged._name;
			} 
			_currentDam.text = _CombatManager._rangedDam.ToString();
			_currentFR.text = _CombatManager._fireRate.ToString();
			_currentRad.text = _CombatManager._radius.ToString();
			break;
			//Head slot
			case 2:
			if (_CombatManager._itemLevels[_activeItem] <= 10){				
				_upgradeName.text = _CombatManager._headSlot._name + " +" + (_CombatManager._itemLevels[_activeItem] + 1);
				_upgradeDef.text = Mathf.FloorToInt(_CombatManager._headSlot._armourBonus * (0.9f + (_CombatManager._itemLevels[_activeItem] + 1) * 0.1f) * _CombatManager._defBonus).ToString();
				_upgradeWgt.text = "- " + _CombatManager._headSlot._weight;
			}
			else{
				_upgradeDef.text = "-";
				_upgradeName.text = "-";
				_upgradeWgt.text = "-";
			}
			if (_CombatManager._itemLevels[_activeItem] > 1){
				_currentName.text = _CombatManager._headSlot._name + " +" + (_CombatManager._itemLevels[_activeItem] -1);
				}
			else{
				_currentName.text = _CombatManager._headSlot._name;
			}			
			_currentDef.text = _CombatManager._headBonus.ToString();
			_currentWgt.text = "- " + _CombatManager._headSlot._weight;
			break;
			//Chest slot
			case 3:
			if (_CombatManager._itemLevels[_activeItem] <= 10){				
				_upgradeName.text = _CombatManager._chestSlot._name + " +" + (_CombatManager._itemLevels[_CombatManager._itemsEquipped[_activeSlot - 1]]);
				_upgradeDef.text = Mathf.FloorToInt(_CombatManager._chestSlot._armourBonus * (0.9f + (_CombatManager._itemLevels[_activeItem] + 1) * 0.1f) * _CombatManager._defBonus).ToString();
				_upgradeWgt.text = "- " + _CombatManager._chestSlot._weight;
			}
			else{
				_upgradeDef.text = "-";
				_upgradeName.text = "-";
				_upgradeWgt.text = "-";
			}
			if (_CombatManager._itemLevels[_activeItem] > 1){
				_currentName.text = _CombatManager._chestSlot._name + " +" + (_CombatManager._itemLevels[_CombatManager._itemsEquipped[_activeSlot - 1]] -1);
				}
			else{
				_currentName.text = _CombatManager._chestSlot._name;
			}			
			_currentDef.text = _CombatManager._chestBonus.ToString();
			_currentWgt.text = "- " + _CombatManager._chestSlot._weight;
			break;
			//Leg Slot
			case 4:
			if (_CombatManager._itemLevels[_activeItem] <= 10){				
				_upgradeName.text = _CombatManager._legSlot._name + " +" + (_CombatManager._itemLevels[_CombatManager._itemsEquipped[_activeSlot - 1]]);
				_upgradeDef.text = Mathf.FloorToInt(_CombatManager._legSlot._armourBonus * (0.9f + (_CombatManager._itemLevels[_activeItem] + 1) * 0.1f) * _CombatManager._defBonus).ToString();
				_upgradeWgt.text = "- " + _CombatManager._legSlot._weight;
			}
			else{
				_upgradeDef.text = "-";
				_upgradeName.text = "-";
				_upgradeWgt.text = "-";
			}
			if (_CombatManager._itemLevels[_activeItem] > 1){
				_currentName.text = _CombatManager._legSlot._name + " +" + (_CombatManager._itemLevels[_CombatManager._itemsEquipped[_activeSlot - 1]] -1);
				}
			else{
				_currentName.text = _CombatManager._legSlot._name;
			}			
			_currentDef.text = _CombatManager._legBonus.ToString();
			_currentWgt.text = "- " + _CombatManager._legSlot._weight;
			break;

		}
	}
}

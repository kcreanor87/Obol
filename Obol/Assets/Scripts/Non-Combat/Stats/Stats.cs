using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Stats : MonoBehaviour {

	//Player stat text
	public Text _playerName, _att, _def, _vit, _dex, _mech;
	public Text _avaialRanks;
	public Text _hp, _dps, _ar, _heal, _speed;
	public Text _level, _exp;

	//Weapon stat text
	public Text _weaponName, _weaponDam, _weaponFR, _weaponRad;

	//Armour stat text
	public Text _headName, _headVal, _headWgt;
	public Text _chestName, _chestVal, _chestWgt;
	public Text _legName, _legVal, _legWgt;
	public Text _totalVal, _totalWgt;

	public GameObject _statScreen;
	public NonCombat_UI _ui;
	public Camera _hudCam;

	void Awake(){
		CollectElements();
	}

	public void OpenCanvas(){
		UpdateStats();
		_statScreen.SetActive(true);
		_hudCam.enabled = true;
		_ui._uiOpen = true;
	}

	public void CloseCanvas(){
		_statScreen.SetActive(false);
		_ui._uiOpen = false;
		_hudCam.enabled = false;
	}

	void CollectElements(){
		_playerName = GameObject.Find("PlayerName").GetComponent<Text>();
		_att = GameObject.Find("AttTxt").GetComponent<Text>();
		_def = GameObject.Find("DefTxt").GetComponent<Text>();
		_vit = GameObject.Find("VitTxt").GetComponent<Text>();
		_dex = GameObject.Find("DexTxt").GetComponent<Text>();
		_mech = GameObject.Find("MechTxt").GetComponent<Text>();

		_avaialRanks = GameObject.Find("PointsTxt").GetComponent<Text>();

		_hp = GameObject.Find("HPTxt").GetComponent<Text>();
		_dps = GameObject.Find("DPSTxt").GetComponent<Text>();
		_ar = GameObject.Find("ARTxt").GetComponent<Text>();
		_heal = GameObject.Find("HealingTxt").GetComponent<Text>();
		_speed = GameObject.Find("SpeedTxt").GetComponent<Text>();

		_level = GameObject.Find("PlayerLevel").GetComponent<Text>();
		_exp = GameObject.Find("CurrentXP").GetComponent<Text>();

		_weaponName = GameObject.Find("WeaponNameTxt").GetComponent<Text>();
		_weaponDam = GameObject.Find("WeaponDamTxt").GetComponent<Text>();
		_weaponFR = GameObject.Find("WeaponFRTxt").GetComponent<Text>();
		_weaponRad = GameObject.Find("WeaponRadTxt").GetComponent<Text>();

		_headName = GameObject.Find("HeadARName").GetComponent<Text>();
		_headVal = GameObject.Find("HeadARTxt").GetComponent<Text>();
		_headWgt = GameObject.Find("HeadWgtTxt").GetComponent<Text>();

		_chestName = GameObject.Find("ChestARName").GetComponent<Text>();
		_chestVal = GameObject.Find("ChestARTxt").GetComponent<Text>();
		_chestWgt = GameObject.Find("ChestWgtTxt").GetComponent<Text>();

		_legName = GameObject.Find("LegARName").GetComponent<Text>();
		_legVal = GameObject.Find("LegARTxt").GetComponent<Text>();
		_legWgt = GameObject.Find("LegWgtTxt").GetComponent<Text>();

		_totalVal = GameObject.Find("TotalARTxt").GetComponent<Text>();
		_totalWgt = GameObject.Find("TotalWgtTxt").GetComponent<Text>();

		_statScreen = GameObject.Find("PlayerStats");

		_ui = gameObject.GetComponent<NonCombat_UI>();

		_hudCam = GameObject.Find("HUDcam").GetComponent<Camera>();
	}

	public void UpdateStats(){
		_CombatManager.CalculateStats();
		UpdatePlayerStats();
		UpdateWeaponStats();
		UpdateArmourStats();
	}

	void UpdatePlayerStats(){
		_playerName.text = _manager._name;
		_att.text = _CombatManager._attRanks.ToString();
		_def.text = _CombatManager._defRanks.ToString();
		_vit.text = _CombatManager._vitRanks.ToString();
		_dex.text = _CombatManager._dexRanks.ToString();
		_mech.text = _CombatManager._mechRanks.ToString();

		_avaialRanks.text = _manager._availableRanks.ToString();

		_hp.text = _CombatManager._currentHealth + "/" + _CombatManager._maxHealth;
		_dps.text = ((float)_CombatManager._equipRanged._dam / _CombatManager._equipRanged._fireRate).ToString();
		_ar.text = _CombatManager._armourRating.ToString();
		_heal.text = (_CombatManager._maxHealth / 100) + " hp/s";
		_speed.text = (_CombatManager._speed / 10).ToString();

		_level.text = _manager._level.ToString();
		_exp.text = _manager._currentXP + "/" + _manager._nextLvlXP;
	}

	void UpdateWeaponStats(){
		_weaponName.text = _CombatManager._equipRanged._name;
		_weaponDam.text = _CombatManager._rangedDam.ToString();
		_weaponFR.text = _CombatManager._equipRanged._fireRate + "s";
		_weaponRad.text = _CombatManager._equipRanged._radius + "m";
	}

	void UpdateArmourStats(){
		_headName.text = _CombatManager._headSlot._name;
		_headVal.text = _CombatManager._headBonus.ToString();
		_headWgt.text = _CombatManager._headSlot._weight + "kg";

		_chestName.text = _CombatManager._chestSlot._name;
		_chestVal.text = _CombatManager._chestBonus.ToString();
		_chestWgt.text = _CombatManager._chestSlot._weight + "kg";

		_legName.text = _CombatManager._legSlot._name;
		_legVal.text = _CombatManager._legBonus.ToString();
		_legWgt.text = _CombatManager._legSlot._weight + "kg";

		_totalVal.text = _CombatManager._armourRating.ToString();
		_totalWgt.text = _CombatManager._speedPenalty + "kg";
	}
}
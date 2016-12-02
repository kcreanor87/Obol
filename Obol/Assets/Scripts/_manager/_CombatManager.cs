//Store the bulk of the static variables that
//make up the player statistics and game progress

using UnityEngine;
using System.Collections.Generic;

public class _CombatManager : MonoBehaviour {

	//PLAYER STATS;
	public static int _maxHealth;
	public static int _currentHealth;
	public static int _rangedDam;
	public static int _armourRating;

	//UPGRADABLE STATS
	public static int _attRanks;
	public static int _defRanks;
	public static int _vitRanks;
	public static int _dexRanks;
	public static int _mechRanks;

	//BONUSES
	public static float _attBonus = 1.0f;
	public static float _defBonus = 1.0f;	

	//ITEM SLOTS
	public static Weapon _equipRanged;
	public static Armour _headSlot;
	public static Armour _chestSlot;
	public static Armour _legSlot;

	public static int _headBonus;
	public static int  _chestBonus;
	public static int _legBonus;

	public static float _fireRate;
	public static float _radius;

	public static float _speed;
	public static float _speedPenalty;

	//Scripts accessed
	public static WeaponDatabase _weaponDb;
	public static ArmourDatabase _armourDb;

	//Initialized once only, stop _manager GO from being destroyed when loading a new scene
	void Awake () {	
		DontDestroyOnLoad(gameObject);
		_weaponDb = gameObject.GetComponent<WeaponDatabase>();
		_armourDb = gameObject.GetComponent<ArmourDatabase>();
		_equipRanged = _weaponDb._rangedDatabase[0];
		_headSlot = _armourDb._headDatabase[0];
		_chestSlot = _armourDb._chestDatabase[0];
		_legSlot = _armourDb._legDatabase[0];
		CalculateStats();	
	}

	//Calculate derivative stats
	public static void CalculateStats(){
		//Speed
		_speedPenalty = _headSlot._weight + _chestSlot._weight + _legSlot._weight;		
		_speed = 100.0f + (2.5f * _dexRanks) - _speedPenalty;
		//Health
		_maxHealth = 700 + 50 *_vitRanks;
		_currentHealth = _maxHealth;
		//Weapon Stats
		_attBonus = 1.0f + (float) 0.05f * _attRanks;
		_rangedDam = Mathf.FloorToInt(_equipRanged._dam * _attBonus);
		_fireRate = _equipRanged._fireRate;
		_radius = _equipRanged._radius;
		//Armour Stats
		_defBonus = 1.0f + (float) 0.05f * _defRanks;
		_headBonus = Mathf.FloorToInt(_headSlot._armourBonus * _defBonus);
		_chestBonus = Mathf.FloorToInt(_chestSlot._armourBonus * _defBonus);
		_legBonus = Mathf.FloorToInt(_legSlot._armourBonus * _defBonus);
		_armourRating = _headBonus + _chestBonus + _legBonus;
		_manager._totalRanks = _attRanks + _defRanks + _vitRanks + _dexRanks + _mechRanks;	
	}
}

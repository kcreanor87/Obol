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
	public static int _vit;

	//ITEM SLOTS
	public static Weapon _equipRanged;
	public static Armour _headSlot;
	public static Armour _chestSlot;
	public static Armour _legSlot;

	//ITEM LEVELS
	public static List<int> _itemLevels = new List <int>();
	public static List<int> _itemsEquipped = new List<int>();
	//ITEM UNLOCKS
	public static List<bool> _itemsUnlocked = new List <bool>();

	public static int _headBonus;
	public static int  _chestBonus;
	public static int _legBonus;

	public static float _fireRate;
	public static float _radius;

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
		for (int i = 0; i < 16; i++){
			_itemLevels.Add(1);
		}
		for (int i = 0; i < 4; i++){
			_itemsEquipped.Add(0);
		}
		for (int i = 0; i < 16; i++){
			_itemsUnlocked.Add(false);
		}
		_itemsUnlocked[0] = true;
		CalculateStats();		
	}

	//Calculate derivative stats
	public static void CalculateStats(){
		_rangedDam = Mathf.FloorToInt(_equipRanged._dam * (0.9f + _itemLevels[_itemsEquipped[0]] * 0.1f));
		_fireRate = _equipRanged._fireRate;
		_radius = _equipRanged._radius;
		_headBonus = Mathf.FloorToInt(_headSlot._armourBonus * (0.9f + _itemLevels[_itemsEquipped[1]] * 0.1f));
		_chestBonus = Mathf.FloorToInt(_chestSlot._armourBonus * (0.9f + _itemLevels[_itemsEquipped[2]] * 0.1f));
		_legBonus = Mathf.FloorToInt(_legSlot._armourBonus * (0.9f + _itemLevels[_itemsEquipped[3]] * 0.1f));
		_armourRating = _headBonus + _chestBonus + _legBonus;
		_maxHealth = 700 + 30 *_vit;
		_currentHealth = _maxHealth;
		print (_headBonus);
	}
}

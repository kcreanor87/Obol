using UnityEngine;
using System.Collections.Generic;

public class WeaponDatabase: MonoBehaviour{

	public List<Weapon> _rangedDatabase = new List<Weapon>();

	void Awake(){

		_rangedDatabase.Add(new Weapon(200, "Wooden Cannon", 6.5f, 20));
		_rangedDatabase.Add(new Weapon(201, "Iron Cannon", 7.5f, 30));
		_rangedDatabase.Add(new Weapon(202, "Steel Cannon", 8.5f, 40));
		_rangedDatabase.Add(new Weapon(203, "Diamond Cannon", 10.0f, 100));
	}
}

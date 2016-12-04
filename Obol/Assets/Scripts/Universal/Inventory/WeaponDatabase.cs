using UnityEngine;
using System.Collections.Generic;

public class WeaponDatabase: MonoBehaviour{

	public List<Weapon> _rangedDatabase = new List<Weapon>();

	void Awake(){

		_rangedDatabase.Add(new Weapon(200, "Bone Shot", 8.5f, 50, 0.25f, 100, 1, true));
		_rangedDatabase.Add(new Weapon(201, "Iron Cannon", 30.0f, 700, 1.5f, 200, 2, false));
		_rangedDatabase.Add(new Weapon(202, "Quicksilver", 3.0f, 400, 0.1f, 300, 3, false));
		_rangedDatabase.Add(new Weapon(203, "The Duke", 8.5f, 400, 0.45f, 400, 4, false));
	}
}

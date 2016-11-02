using UnityEngine;
using System.Collections.Generic;

public class WeaponDatabase: MonoBehaviour{

	public List<Weapon> _rangedDatabase = new List<Weapon>();

	void Awake(){

		_rangedDatabase.Add(new Weapon(200, "Bone Shot", 8.5f, 50, 0.25f, "Shoots fast, but doesn't do a lot of damage."));
		_rangedDatabase.Add(new Weapon(201, "Iron Cannon", 30.0f, 700, 1.5f, "Very slow, but shots explode doing devastating amounts of damage in a huge area."));
		_rangedDatabase.Add(new Weapon(202, "Quicksilver", 2.0f, 400, 0.1f, "Incredibly fast weapon, doing good damage but only to enemies that are hit directly"));
		_rangedDatabase.Add(new Weapon(203, "The Duke", 8.5f, 400, 0.45f, "Top end weapon with a decent fire rate, doing good damage in a large area"));
	}
}

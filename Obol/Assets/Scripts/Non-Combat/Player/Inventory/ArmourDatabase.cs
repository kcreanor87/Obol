using UnityEngine;
using System.Collections.Generic;

public class ArmourDatabase: MonoBehaviour{

	public List<Armour> _headDatabase = new List<Armour>();
	public List<Armour> _chestDatabase = new List<Armour>();
	public List<Armour> _legDatabase = new List<Armour>();

	void Awake(){

		_headDatabase.Add(new Armour(300, "None", 0, 1.0f));
		_headDatabase.Add(new Armour(301, "Bone Helm", 20, 1.0f));
		_headDatabase.Add(new Armour(302, "Iron Helm", 40, 1.0f));
		_headDatabase.Add(new Armour(303, "Silver Helm", 60, 1.0f));
		_headDatabase.Add(new Armour(304, "Crystal Helm", 80, 1.0f));

		_chestDatabase.Add(new Armour(400, "None", 0, 1.0f));
		_chestDatabase.Add(new Armour(401, "Bone Plate", 40, 1.0f));
		_chestDatabase.Add(new Armour(402, "Iron Plate", 80, 1.0f));
		_chestDatabase.Add(new Armour(403, "Silver Plate", 120, 1.0f));
		_chestDatabase.Add(new Armour(404, "Crystal Plate", 160, 1.0f));

		_legDatabase.Add(new Armour(500, "None", 0, 1.0f));
		_legDatabase.Add(new Armour(501, "Bone Boots", 20, 1.0f));
		_legDatabase.Add(new Armour(502, "Iron Boots", 40, 1.0f));
		_legDatabase.Add(new Armour(503, "Silver Boots", 60, 1.0f));
		_legDatabase.Add(new Armour(504, "Crystal Boots", 80, 1.0f));
	}
}

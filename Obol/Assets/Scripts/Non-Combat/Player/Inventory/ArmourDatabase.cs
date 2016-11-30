using UnityEngine;
using System.Collections.Generic;

public class ArmourDatabase: MonoBehaviour{

	public List<Armour> _headDatabase = new List<Armour>();
	public List<Armour> _chestDatabase = new List<Armour>();
	public List<Armour> _legDatabase = new List<Armour>();

	void Awake(){

		_headDatabase.Add(new Armour(300, "None", 0, 0.0f, 0, 0, true));
		_headDatabase.Add(new Armour(301, "Bone Helm", 30, 5.0f, 100, 1, false));
		_headDatabase.Add(new Armour(302, "Iron Helm", 60, 10.0f, 200, 2, false));
		_headDatabase.Add(new Armour(303, "Silver Helm", 30, 2.0f, 300, 3, false));
		_headDatabase.Add(new Armour(304, "Crystal Helm", 50, 4.0f, 400, 4, false));

		_chestDatabase.Add(new Armour(400, "None", 0, 0.0f, 100, 1, true));
		_chestDatabase.Add(new Armour(401, "Bone Plate", 50, 10.0f, 100, 1, false));
		_chestDatabase.Add(new Armour(402, "Iron Plate", 125, 19.0f, 200, 2, false));
		_chestDatabase.Add(new Armour(403, "Silver Plate", 50, 4.0f, 300, 3, false));
		_chestDatabase.Add(new Armour(404, "Crystal Plate", 70, 9.0f, 400, 4, false));

		_legDatabase.Add(new Armour(500, "None", 0, 0.0f, 0, 0, true));
		_legDatabase.Add(new Armour(501, "Bone Boots", 20, 5.0f, 100, 1, false));
		_legDatabase.Add(new Armour(502, "Iron Boots", 40, 10.0f, 200, 2, false));
		_legDatabase.Add(new Armour(503, "Silver Boots", 20, 2.0f, 300, 3, false));
		_legDatabase.Add(new Armour(504, "Crystal Boots", 30, 4.0f, 400, 4, false));
	}
}

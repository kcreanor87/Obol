using UnityEngine;
using System.Collections.Generic;

public class TurretDatabase: MonoBehaviour{

	public List<Turret> _turretDatabase = new List<Turret>();

	void Awake(){
		_turretDatabase.Add(new Turret(600, "Machine Gun", 8.5f, 50, 0.25f, 100, 0, true, 0));
		_turretDatabase.Add(new Turret(601, "Lascannon", 8.5f, 500, 2.5f, 200, 1, true, 0));
	}
}

using UnityEngine;
using System.Collections.Generic;

public class TurretDatabase: MonoBehaviour{

	public List<Turret> _turretDatabase = new List<Turret>();

	void Awake(){
		_turretDatabase.Add(new Turret(600, "Machine Gun", 8.5f, 50, 0.25f, 100, 0, true, 0));
		_turretDatabase.Add(new Turret(601, "Burst", 8.5f, 20, 0.8f, 200, 1, true, 1));
		_turretDatabase.Add(new Turret(602, "Slow", 8.5f, 0, 0.2f, 300, 2, true, 2));
	}
}

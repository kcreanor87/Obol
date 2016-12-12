﻿using UnityEngine;
using System.Collections.Generic;

public class TurretDatabase: MonoBehaviour{

	public List<Turret> _turretDatabase = new List<Turret>();

	void Awake(){
		_turretDatabase.Add(new Turret(600, "Machine Gun", 8.5f, 50, 0.25f, 100, 0, true, 0.0f, 0));
		_turretDatabase.Add(new Turret(601, "Burst", 8.5f, 200, 0.8f, 200, 1, true, 0.0f, 1));
		_turretDatabase.Add(new Turret(602, "Slow", 8.5f, 0, 0.2f, 300, 2, true, 0.0f, 2));
		_turretDatabase.Add(new Turret(603, "Boost Player", 8.5f, 0, 0.2f, 300, 2, true, 1.5f, 3));
		_turretDatabase.Add(new Turret(604, "Boost Resources", 8.5f, 0, 0.2f, 300, 2, true, 2.0f, 4));
	}
}
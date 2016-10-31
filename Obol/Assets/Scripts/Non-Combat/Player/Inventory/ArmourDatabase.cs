using UnityEngine;
using System.Collections.Generic;

public class ArmourDatabase: MonoBehaviour{

	public List<Armour> _headDatabase = new List<Armour>();
	public List<Armour> _chestDatabase = new List<Armour>();
	public List<Armour> _legDatabase = new List<Armour>();

	void Awake(){

		_headDatabase.Add(new Armour(300, "None", 0, 0.0f, ""));
		_headDatabase.Add(new Armour(301, "Bone Helm", 20, 1.0f, "More skull for your skull. About as effective as that sounds."));
		_headDatabase.Add(new Armour(302, "Iron Helm", 100, 2.0f, "Oustandingly strong. Astronomically heavy."));
		_headDatabase.Add(new Armour(303, "Silver Helm", 40, 0.5f, "Very light, yet surprisingly tough helm."));
		_headDatabase.Add(new Armour(304, "Crystal Helm", 85, 1.0f, "Top of the range helm, providing a great balance between weight and strength"));

		_chestDatabase.Add(new Armour(400, "None", 0, 0.0f, ""));
		_chestDatabase.Add(new Armour(401, "Bone Plate", 60, 2.0f, "Armour suitable for blocking attacks from small children or irate Chihuahuas"));
		_chestDatabase.Add(new Armour(402, "Iron Plate", 200, 4.0f, "Become a one-man bunker, with matching manoeuvrability"));
		_chestDatabase.Add(new Armour(403, "Silver Plate", 90, 1.0f, "Fast and Glamorous. Suits you."));
		_chestDatabase.Add(new Armour(404, "Crystal Plate", 160, 2.0f, "Armour fit for a King during a revolt"));

		_legDatabase.Add(new Armour(500, "None", 0, 0.0f, ""));
		_legDatabase.Add(new Armour(501, "Bone Boots", 20, 1.0f, "These look awful, are painfully uncomfortable and fairly useless in combat. An all-rounder"));
		_legDatabase.Add(new Armour(502, "Iron Boots", 100, 2.0f, "The only shoes known to block a point-blank cannon shot"));
		_legDatabase.Add(new Armour(503, "Silver Boots", 40, 0.5f, "Catwalk or racetrack: these do both."));
		_legDatabase.Add(new Armour(504, "Crystal Boots", 80, 1.0f, "You're a rich kid, don't try to hide it"));
	}
}

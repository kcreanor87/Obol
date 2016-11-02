using UnityEngine;
using System.Collections.Generic;

public class _manager : MonoBehaviour {

	public static List <int> _resources = new List <int>();
	public static int _obols = 20000;
	public static List <string> _resourceNames = new List<string>();
	public static List <int> _prices = new List <int>();
	public static bool _combatOver;

	public static int _portal;
	public static int _level;

	public static List <bool> _activeLevels = new List <bool>();
	public static List <int> _activePortals = new List <int>();

	public static List <int> _factoryOuput = new List<int>();

	void Awake(){
		_resourceNames.Clear();
		_resourceNames.Add("Bone");
		_resourceNames.Add("Iron");
		_resourceNames.Add("Silver");
		_resourceNames.Add("Crystal");

		_resources.Clear();
		_resources.Add(500);
		_resources.Add(500);
		_resources.Add(500);
		_resources.Add(500);

		_manager._activeLevels.Add(true);
		_manager._activeLevels.Add(true);

		_manager._activePortals.Add(0);
		_manager._activePortals.Add(0);

		DontDestroyOnLoad(gameObject);
	}
}

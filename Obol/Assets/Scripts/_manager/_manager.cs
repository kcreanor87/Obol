using UnityEngine;
using System.Collections.Generic;

public class _manager : MonoBehaviour {

	public static List <int> _resources = new List <int>();
	public static int _obols;
	public static List <string> _resourceNames = new List<string>();
	public static List <int> _prices = new List <int>();
	public static bool _combatOver;

	public static List <int> _factoryOuput = new List<int>();

	void Awake(){
		_resourceNames.Clear();
		_resourceNames.Add("Bone");
		_resourceNames.Add("Iron");
		_resourceNames.Add("Sulphur");
		_resourceNames.Add("Crystal");

		_resources.Clear();
		_resources.Add(1000);
		_resources.Add(1000);
		_resources.Add(1000);
		_resources.Add(1000);
		_resources.Add(0);
		_resources.Add(0);

		_prices.Add(0);
		_prices.Add(0);
		_prices.Add(0);
		_prices.Add(0);

		DontDestroyOnLoad(gameObject);
	}
}

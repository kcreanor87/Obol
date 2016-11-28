using UnityEngine;
using System.Collections.Generic;

public class _manager : MonoBehaviour {

	public static int _obols = 200000;

	public static int _portal;
	public static int _level;

	public static List <bool> _activeLevels = new List <bool>();
	public static List <int> _activePortals = new List <int>();

	public static List <bool> _npcChat = new List <bool>();
	public static List <int> _chatState = new List <int>();

	void Awake(){

		for (int i = 0; i < 6; i++){
			_npcChat.Add(false);
			_chatState.Add(0);
		}

		_manager._activeLevels.Add(true);
		_manager._activeLevels.Add(true);

		_manager._activePortals.Add(0);
		_manager._activePortals.Add(0);

		DontDestroyOnLoad(gameObject);
	}
}

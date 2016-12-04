using UnityEngine;
using System.Collections.Generic;

public class _manager : MonoBehaviour {

	public static string _name;

	public static int _obols = 200;

	public static int _portal;
	public static int _level;
	public static int _currentXP;
	public static int _nextLvlXP;
	public static int _prevXP;

	public static int _totalRanks;
	public static int _availableRanks;

	public static int _totalLevels = 5;
	public static List <bool> _activeLevels = new List <bool>();

	public static List <bool> _npcChat = new List <bool>();
	public static List <int> _chatState = new List <int>();

	void Awake(){
		for (int i = 0; i < 6; i++){
			_npcChat.Add(false);
			_chatState.Add(0);
		}

		for (int i = 0; i < _totalLevels; i++){
			_activeLevels.Add(false);
		}
		_activeLevels[0] = true;

		DontDestroyOnLoad(gameObject);

		CheckXP();
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Space)){
			_currentXP += 100;			
			CheckXP();
		}
	}

	public void CheckXP(){		
		if (_currentXP >= _nextLvlXP){
			LevelUp();
		}
	}

	void LevelUp(){
		_level++;
		_availableRanks = _level - _totalRanks;
		UpdateXP();
	}

	void UpdateXP(){
		_prevXP = _nextLvlXP;
		_nextLvlXP = (_level + 1)*(_level + 1)*200;
		if (_currentXP >= _nextLvlXP) LevelUp();
	}
}

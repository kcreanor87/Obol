using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDetect : MonoBehaviour {

	public GameObject _exitPrompt;
	public SaveGame _saveGame;
	public CombatCounters _counters;

	void Start(){
		_counters = GameObject.Find("Counters").GetComponent<CombatCounters>();
		_exitPrompt = GameObject.Find("ExitButton");
		_saveGame = GameObject.Find("Loader").GetComponent<SaveGame>();
		_exitPrompt.SetActive(false);
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player"){
			ExitPrompt();
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Player"){
			_exitPrompt.SetActive(false);
		}
	}

	public void Exit(){
		for (int i = 0; i < _counters._resources.Count; i++){
			_manager._resources[i] += _counters._resources[i];
		}
		_saveGame.CombatOverSave();
		SaveGame._combat = false;
		Time.timeScale = 1.0f;
		SceneManager.LoadScene("TownCentre");
	}

	public void ExitPrompt(){
		_exitPrompt.SetActive(true);
	}
}

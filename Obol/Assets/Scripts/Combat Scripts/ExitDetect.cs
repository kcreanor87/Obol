using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDetect : MonoBehaviour {

	public GameObject _exitPrompt;
	public GameObject _escapePrompt;
	public SaveGame _saveGame;
	public CombatCounters _counters;
	public Combat_UI _ui;

	void Start(){
		_counters = GameObject.Find("Counters").GetComponent<CombatCounters>();
		_exitPrompt = GameObject.Find("ExitButton");
		_escapePrompt = GameObject.Find("EscapeButton");
		_ui = GameObject.Find("UI").GetComponent<Combat_UI>();
		_saveGame = GameObject.Find("Loader").GetComponent<SaveGame>();
		_exitPrompt.SetActive(false);
		_escapePrompt.SetActive(false);
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player"){
			EscapePrompt();
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Player"){
			_escapePrompt.SetActive(false);
		}
	}

	public void Escape(){
		_ui.GameOver(true);
		_escapePrompt.SetActive(false);
	}

	public void Exit(){
		_saveGame.CombatOverSave();
		SaveGame._combat = false;
		Time.timeScale = 1.0f;
		_manager._combatOver = true;
		SceneManager.LoadScene("TownCentre");
	}

	public void EscapePrompt(){
		_escapePrompt.SetActive(true);
	}

	public void ExitPrompt(){
		_exitPrompt.SetActive(true);
	}
}

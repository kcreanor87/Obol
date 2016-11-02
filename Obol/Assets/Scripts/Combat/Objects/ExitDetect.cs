using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDetect : MonoBehaviour {

	public GameObject _exitPrompt;
	public SaveGame _saveGame;
	public CombatCounters _counters;
	public Combat_UI _ui;

	void Start(){
		_counters = GameObject.Find("Counters").GetComponent<CombatCounters>();
		_ui = GameObject.Find("UI").GetComponent<Combat_UI>();
		_saveGame = GameObject.Find("Loader").GetComponent<SaveGame>();
		_exitPrompt = GameObject.Find("ExitPrompt");
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
		NewGame._newGame = true;
		SceneManager.LoadScene("Crypt");
	}

	public void ExitPrompt(){
		_exitPrompt.SetActive(true);
	}
}

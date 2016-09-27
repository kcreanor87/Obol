using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDetect : MonoBehaviour {

	public GameObject _exitPrompt;
	public SaveGame _saveGame;

	void Start(){
		_exitPrompt = GameObject.Find("ExitButton");
		_saveGame = GameObject.Find("Loader").GetComponent<SaveGame>();
		_exitPrompt.SetActive(false);
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player"){
			_exitPrompt.SetActive(true);
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Player"){
			_exitPrompt.SetActive(false);
		}
	}

	public void Exit(){
		_saveGame.CombatOverSave();
		SceneManager.LoadScene("TownCentre");
	}
}

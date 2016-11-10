using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivatePortal : MonoBehaviour {

	public bool _active;
	public int _index;
	public SaveGame _saveGame;

	void Start(){
		_active = _manager._activePortals[SceneManager.GetActiveScene().buildIndex - 2] >= _index;
		_saveGame = GameObject.Find("Loader").GetComponent<SaveGame>();
	}

	void OnTriggerEnter(Collider col){		
		if (!_active && col.tag == "Player"){
			_active = true;
			_saveGame.CombatSave();
			_manager._activePortals[SceneManager.GetActiveScene().buildIndex - 2]++;
		}		
	}
}

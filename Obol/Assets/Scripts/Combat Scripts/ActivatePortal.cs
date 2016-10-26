using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivatePortal : MonoBehaviour {

	public bool _active;
	public int _index;

	void Start(){
		_active = _manager._activePortals[SceneManager.GetActiveScene().buildIndex - 3] >= _index;
	}

	void OnTriggerEnter(Collider col){		
		if (!_active && col.tag == "Player"){
			print(col.name);
			_active = true;
			_manager._activePortals[SceneManager.GetActiveScene().buildIndex - 3]++;
		}		
	}
}

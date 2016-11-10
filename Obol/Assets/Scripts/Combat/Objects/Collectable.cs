using UnityEngine;

public class Collectable : MonoBehaviour {

	public bool _obolStash;
	public bool _resourceStash;
	public int _resType;
	public int _value;	
	public Combat_UI _ui;
	public SaveGame _saveGame;
	public int _id;
	public string _text;

	void Start(){
		_ui = GameObject.Find("UI").GetComponent<Combat_UI>();
		_saveGame = GameObject.Find("Loader").GetComponent<SaveGame>();
		gameObject.SetActive(!_CombatManager._collectables[_id]);

		switch (_resType){
			case 0:
			_text = "Bone";
			break;
			case 1:
			_text = "Iron";
			break;
			case 2:
			_text = "Silver";
			break;
			case 3:
			_text = "Crystal";
			break;
			case 4:
			_text = "Obols";
			break;
		}
	}


	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") Collect();
	}

	void Collect(){
		if (_obolStash) _manager._obols += _value;
		if (_resourceStash) _manager._resources[_resType] += _value;
		_ui.PopUpBox(_value, _text);
		_CombatManager._collectables[_id] = true;
		_saveGame.CombatSave();
		_ui.UpdateUI();
		Destroy(gameObject);
	}
}

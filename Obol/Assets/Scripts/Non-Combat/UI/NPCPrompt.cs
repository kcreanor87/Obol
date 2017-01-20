using UnityEngine;
using System.Collections;

public class NPCPrompt : MonoBehaviour {

	public int _type;
	public NonCombat_UI _ui;

	void Start(){
		_ui = GameObject.Find("Non-Combat UI").GetComponent<NonCombat_UI>();
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player"){
			_ui.OpenPrompt(_type);
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Player"){
			_ui.ClosePrompt();
		}
	}
}

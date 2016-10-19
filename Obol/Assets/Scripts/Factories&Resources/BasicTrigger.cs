using UnityEngine;
using System.Collections;

public class BasicTrigger : MonoBehaviour {

	public GameObject _triggerGO;
	public bool _replacement;
	public GameObject _replacementGO;

	void OnTriggerEnter (Collider col){
		if (col.tag == "Player"){
			_triggerGO.SetActive(true);
			if (_replacement) _replacementGO.SetActive(false);
		}
	}
}

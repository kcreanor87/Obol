using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ExitDetect : MonoBehaviour {

	public SaveGame _saveGame;
	public GameObject _fadeOut;

	void Start(){
		_saveGame = GameObject.Find("Loader").GetComponent<SaveGame>();
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player"){
			StartCoroutine(FadeOut());
		}
	}

	public IEnumerator FadeOut(){
		_fadeOut.SetActive(true);
		yield return new WaitForSeconds(2.0f);
		SceneManager.LoadScene("Crypt");
	}
}

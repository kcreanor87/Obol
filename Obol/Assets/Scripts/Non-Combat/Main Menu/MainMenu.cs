using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public GameObject _promptGO;
	public GameObject _introButtonts;
	public Button _resumeButton;

	void Start(){
		_promptGO = GameObject.Find("NewGamePrompt");
		_introButtonts = GameObject.Find("IntroButtons");
		_resumeButton.interactable = (PlayerPrefs.HasKey("Obols"));
		_promptGO.SetActive(false);
	}

	public void ResumeGame(){
		SceneManager.LoadScene("Crypt");
		NewGame._newGame = false;
	}

	public void NewGamePrompt(){
		_promptGO.SetActive(true);
		_introButtonts.SetActive(false);
	}

	public void CancelNewGame(){
		_promptGO.SetActive(false);
		_introButtonts.SetActive(true);
	}

	public void StartNewGame(){
		NewGame._newGame = true;
		PlayerPrefs.DeleteAll();
		SceneManager.LoadScene("Level0");
	}

	public void ExitGame(){
		Application.Quit();
	}
}

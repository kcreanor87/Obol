using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public GameObject _promptGO;
	public GameObject _introButtonts;
	public Button _resumeButton;
	public Button _newGameButton;
	public Button _noButton;

	void Start(){
		_promptGO = GameObject.Find("NewGamePrompt");
		_introButtonts = GameObject.Find("IntroButtons");	
		CheckForData();	
		_promptGO.SetActive(false);
	}

	void CheckForData(){
		if (PlayerPrefs.HasKey("Obols")){
			_resumeButton.interactable = true;
			_resumeButton.Select();
		}
		else{
			_resumeButton.interactable = false;
			_newGameButton.Select();
		}
	}

	public void ResumeGame(){
		SceneManager.LoadScene("Crypt");
		NewGame._newGame = false;
	}

	public void NewGamePrompt(){
		_promptGO.SetActive(true);
		_noButton.Select();
		_introButtonts.SetActive(false);
	}

	public void CancelNewGame(){
		_promptGO.SetActive(false);
		_introButtonts.SetActive(true);
		CheckForData();		
	}

	public void StartNewGame(){
		NewGame._newGame = true;
		PlayerPrefs.DeleteAll();
		SceneManager.LoadScene("Crypt");
	}

	public void ExitGame(){
		Application.Quit();
	}
}

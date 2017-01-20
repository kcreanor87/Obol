using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;


public class PortalControls : MonoBehaviour {

	public GameObject _portalCanvas;

	public List <Button> _activeLevels = new List <Button>();
	public List <Outline> _outlines = new List <Outline>();

	public int _level;

	public NonCombat_UI _ui;

	public GameObject _fadeOut;

	// Use this for initialization
	void Awake () {
		_fadeOut = GameObject.Find("FadeOut");
		_fadeOut.SetActive(false);
		_portalCanvas = GameObject.Find("PortalScreen");
		_ui = gameObject.GetComponent<NonCombat_UI>();
		for (int i = 0; i < _activeLevels.Count; i++){
			_outlines.Add(_activeLevels[i].GetComponent<Outline>());
		}
		CheckLevelStates();
	}

	public void OpenCanvas(){
		_portalCanvas.SetActive(true);
		_ui._uiOpen = true;
		CheckLevelStates();
		_activeLevels[0].Select();
	}

	public void CloseCanvas(){
		_portalCanvas.SetActive(false);
	}

	public void LevelSelect(int level){
		_level = level + 2;
		HighlightLevel(level);
	}

	public void Travel(){
		StartCoroutine(Fade());
	}

	public IEnumerator Fade(){
		_fadeOut.SetActive(true);
		yield return new WaitForSeconds(1.2f);
		SceneManager.LoadScene(_level);
	}

	void CheckLevelStates(){
		for (int i = 0; i < _manager._activeLevels.Count; i++){
			_activeLevels[i].interactable = _manager._activeLevels[i];
		}
	}

	void HighlightLevel(int index){
		for (int i = 0; i < _activeLevels.Count; i++){
			_outlines[i].enabled = (index == i);			
		}
	}
}

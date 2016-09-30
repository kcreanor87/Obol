using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TownCanvas : MonoBehaviour {

	public MarketSpawn _marketSpawn;
	public Market _market;
	public Canvas _canvas;
	public RumourGenerator _rumourGen;
	public List <GameObject> _buildings = new List<GameObject>();
	public List <Button> _workshopButtons = new List <Button>();
	public Text _townName;
	public Text _cost, _preReqs, _buildName, _buildDesc;
	public AdditionalResources _additionalResources;
	public List <BuildingCost> _buildingCosts = new List <BuildingCost>();
	public bool _affordable;
	public Button _buildButton;
	public int _activeBuilding;
	public SaveGame _saveGame;
	public GameObject _mapToggle;
	public List <bool> _activeBuildings = new List <bool>();
	public List <GameObject> _buildingGOs = new List <GameObject>();

	void Start(){
		if (NewGame._newGame) PopulateBuildings();
		for (int i = 0; i < _buildingGOs.Count; i++){
			_buildingGOs[i].SetActive(_activeBuildings[i]);
		}
		CollectTextElements();	
		_saveGame = GameObject.Find("Loader").GetComponent<SaveGame>();	
		_buildButton = GameObject.Find("BuildButton").GetComponent<Button>();
		_additionalResources = gameObject.GetComponent<AdditionalResources>();
		_rumourGen = gameObject.GetComponent<RumourGenerator>();
		_canvas = gameObject.GetComponent<Canvas>();
		_market = gameObject.GetComponent<Market>();
		_canvas.enabled = false;
		for (int i = 0; i < _buildings.Count; i++){
			_buildings[i].SetActive(false);
		}
		
	}

	void CollectTextElements(){
		_cost = GameObject.Find("Cost").GetComponent<Text>();
		_preReqs = GameObject.Find("PreReqs").GetComponent<Text>();
		_buildName = GameObject.Find("BuildingName").GetComponent<Text>();
		_buildDesc = GameObject.Find("BuildingDescription").GetComponent<Text>();
	}

	public void OpenCanvas(string name){
		switch (name){
			case "MarketGO":
			//Open Market Screen
			EnableCanvas(0);
			break;
			case "WorkshopGO":
			//Open Workshop screen
			UpdateWorkshopButtons();
			EnableCanvas(1);
			break;
			case "RumourGO":
			//Open Rumour screen
			CheckRumour();
			EnableCanvas(2);
			break;
			case "RefineryGO":
			_additionalResources.CheckResources();
			EnableCanvas(3);
			break;
			case "SmithGO":
			EnableCanvas(4);
			break;
			case "AcademyGO":
			EnableCanvas(5);
			break;
			case "CaravanGO":
			EnableCanvas(7);
			break;	
		}
		_market._multiple = 1;
		_market.UpdatePrices();
	}
	void EnableCanvas(int index){
		_canvas.enabled = true;
		_buildings[index].SetActive(true);
	}

	public void CloseCanvas(){
		CloseAllBuildings();
		_canvas.enabled = false;
		WM_UI.UpdateUI();
		_saveGame.Save();
	}

	public void CloseBuilding(int index){
		_buildings[index].SetActive(false);
	}

	void CloseAllBuildings(){
		for (int i = 0; i < _buildings.Count; i++){
			_buildings[i].SetActive(false);
		}
	}
	void UpdateWorkshopButtons(){
		for (int i = 0; i < _workshopButtons.Count; i++){
			var image = _workshopButtons[i].gameObject.GetComponent<Image>();
			CheckAffordability(i);
			if (_affordable && !_activeBuildings[i]){
				image.color = Color.green;
			}			
			else if (!_affordable && !_activeBuildings[i]){
				image.color = Color.red;
			}
			else{
				image.color = Color.white;
			}
		}
	}

	public void OpenBuildCanvas(int index){
		_activeBuilding = index;
		if (!_activeBuildings[index]){
			_cost.text = _buildingCosts[index]._cost.ToString();
			_buildName.text = _buildingCosts[index]._name;
			_buildDesc.text = _buildingCosts[index]._desc;
			_preReqs.text = (_buildingCosts[index]._preReq > 0) ? _preReqs.text = _buildingCosts[_buildingCosts[index]._preReq]._name : null;
			CheckAffordability(index);
			_buildButton.interactable = _affordable;
		}
		else{
			_cost.text = null;
			_buildName.text = _buildingCosts[index]._name;
			_buildDesc.text = _buildDesc.text = _buildingCosts[index]._desc;
			_preReqs.text = null;	
		}
		_buildings[6].SetActive(true);
		_buildButton.gameObject.SetActive(!_activeBuildings[index]);
	}

	public void PurchaseBuilding(){
		_manager._obols -= _buildingCosts[_activeBuilding]._cost;
		WM_UI.UpdateUI();
		_activeBuildings[_activeBuilding] = true;
		_buildingGOs[_activeBuilding].SetActive(true);
		UpdateWorkshopButtons();
		_market.UpdatePrices();
		CloseBuilding(6);
	}

	void CheckAffordability(int buildingType){
		var increment = 0;
		if (_manager._obols >= _buildingCosts[buildingType]._cost) increment++;
		if (_activeBuildings[_buildingCosts[buildingType]._preReq]) increment++;
		_affordable = (increment == 2);
	}

	void PopulateBuildings(){
		_activeBuildings.Add(true);
		_activeBuildings.Add(true);
		_activeBuildings.Add(false);
		_activeBuildings.Add(false);
		_activeBuildings.Add(false);
		_activeBuildings.Add(false);
	}

	void CheckRumour(){
		_rumourGen.EnterText();
	}
	public void StartCombat(int index){
		_saveGame.Save();
		SaveGame._combat = true;
		SceneManager.LoadScene(3 + index);
	}
}

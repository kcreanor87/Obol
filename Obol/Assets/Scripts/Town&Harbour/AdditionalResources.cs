using UnityEngine;
using UnityEngine.UI;

public class AdditionalResources : MonoBehaviour {

	public Button _steelButton;
	public Button _diamondButton;
	public Text _buttonText;
	public Text _ironText;
	public Text _sulphurText;
	public Text _crystalText;

	void Start(){

		_steelButton = GameObject.Find("CreateSteel").GetComponentInChildren<Button>();
		_diamondButton = GameObject.Find("CreateDiamond").GetComponentInChildren<Button>();
		_ironText = GameObject.Find("AvailableIron").GetComponent<Text>();
		_sulphurText = GameObject.Find("AvailableSulphur").GetComponent<Text>();
		_crystalText = GameObject.Find("AvailableCrystal").GetComponent<Text>();
	}

	public void CheckResources(){
		_steelButton.interactable = (_manager._resources[1] > 9 && _manager._resources[2] > 9);
		_diamondButton.interactable = (_manager._resources[3] > 9);	
		_ironText.text = "Iron: " + _manager._resources[1];
		_sulphurText.text = "Sulphur: " +_manager._resources[2];
		_crystalText.text = "Crystal: " + _manager._resources[3];
	}
	
	public void AddResource(int type){
		if (type > 0){
			_manager._resources[3] -= 10;
			_manager._resources[5] += 1;
		}
		else{
			_manager._resources[1] -= 10;
			_manager._resources[2] -= 10;
			_manager._resources[4] += 1;
		}
		CheckResources();
		WM_UI.UpdateUI();
	}
}

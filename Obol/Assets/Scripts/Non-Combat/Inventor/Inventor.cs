using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventor : MonoBehaviour {

	public GameObject _weapons;
	public GameObject _armour;
	public GameObject _headSlots;
	public GameObject _chestSlots;
	public GameObject _legSlots;
	public Button _unlockButton;

	public List <Sprite> _buttonSprites = new List <Sprite>();

	public Text _costText;
	public Text _itemName;
	public Text _itemDesc;
	
	public Text _bone, _iron, _silver, _crystal; 

	public int _cost;
	public int _itemType;
	public int _activeItem;

	void Start(){
		_itemName = GameObject.Find("ItemName").GetComponent<Text>();
		_itemDesc = GameObject.Find("ItemDescription").GetComponent<Text>();
		_costText = GameObject.Find("UnlockCost").GetComponent<Text>();
		_bone = GameObject.Find("InventorBone").GetComponent<Text>();
		_iron = GameObject.Find("InventorIron").GetComponent<Text>();
		_silver = GameObject.Find("InventorSilver").GetComponent<Text>();
		_crystal = GameObject.Find("InventorCrystal").GetComponent<Text>();
		_unlockButton = GameObject.Find("UnlockButton").GetComponent<Button>();
		_unlockButton.interactable = false;
		UpdateUI();
		OpenCanas(0);
	}

	void UpdateUI(){
		_bone.text = _manager._resources[0].ToString();
		_iron.text = _manager._resources[1].ToString();
		_silver.text = _manager._resources[2].ToString();
		_crystal.text = _manager._resources[3].ToString();
	}

	public void OpenCanas(int i){
		_itemType = 4;
		UpdateInfo(0);
		_weapons.SetActive(i == 0);
		_armour.SetActive(i > 0);
		_headSlots.SetActive(i == 1);
		_chestSlots.SetActive(i == 2);
		_legSlots.SetActive(i == 3);
		_itemType = i;
	}

	public void UpdateInfo(int i){
		switch(_itemType){
			case (0):
			_itemName.text = _CombatManager._weaponDb._rangedDatabase[i]._name;
			_itemDesc.text = _CombatManager._weaponDb._rangedDatabase[i]._desc;
			_activeItem = i;
			_cost = 100;	
			break;
			case(1):
			_itemName.text = _CombatManager._armourDb._headDatabase[i + 1]._name;
			_itemDesc.text = _CombatManager._armourDb._headDatabase[i + 1]._desc;
			_activeItem = 4 + i;
			_cost = 100;
			break;
			case(2):
			_itemName.text = _CombatManager._armourDb._chestDatabase[i + 1]._name;
			_itemDesc.text = _CombatManager._armourDb._chestDatabase[i + 1]._desc;
			_activeItem = 8 + i;
			_cost = 200;
			break;
			case(3):
			_itemName.text = _CombatManager._armourDb._legDatabase[i + 1]._name;
			_itemDesc.text = _CombatManager._armourDb._legDatabase[i + 1]._desc;
			_activeItem = 12 + i;
			_cost = 100;
			break;
			case(4):
			_itemName.text = "-";
			_itemDesc.text = "Select an item to see it's description";			
			break;
		}
		if (_itemType < 4){
			if (_CombatManager._itemsUnlocked[_activeItem]){
				_unlockButton.image.sprite = _buttonSprites[4];
				_unlockButton.interactable = false;
				_costText.text = "Unlocked!";
			}
			else{
				_unlockButton.interactable = (_manager._resources[i] >= _cost);
				_costText.text = _cost.ToString();
				_unlockButton.image.sprite = _buttonSprites[i];
			}
		}
		else{
			_costText.text = "-";
			_unlockButton.interactable = false;
			_unlockButton.image.sprite = _buttonSprites[4];
		}		
	}

	public void UnlockItem(){
		_CombatManager._itemsUnlocked[_activeItem] = true;
		var item = _activeItem - (4 *_itemType);
		UpdateInfo(item);
		_manager._resources[item] -= _cost;
		UpdateUI();
	}
}

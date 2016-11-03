using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Priest : MonoBehaviour {

	public Text _vitText;
	public Text _attText;
	public Text _defText;
	public Text _spdText;

	public Text _currVit;
	public Text _currAtt;
	public Text _currDef;
	public Text _currSpd;

	public Text _availBlessings;

	public Button _buyButton;

	public int _cost;
	public Text _costText;

	public List <Button> _buttons = new List <Button>();

	public NonCombat_UI _ui;

	void Awake(){
		FindObjects();
	}

	void Start(){
		CheckAvailability();
	}

	void FindObjects(){
		_ui = GameObject.Find("Non-Combat UI").GetComponent<NonCombat_UI>();
		_vitText = GameObject.Find("VitText").GetComponent<Text>();
		_attText = GameObject.Find("AttText").GetComponent<Text>();
		_defText = GameObject.Find("DefText").GetComponent<Text>();
		_spdText = GameObject.Find("SpdText").GetComponent<Text>();

		_costText = GameObject.Find("BlessingCost").GetComponent<Text>();

		_currVit = GameObject.Find("CurrentVitBns").GetComponent<Text>();
		_currAtt = GameObject.Find("CurrentAttBns").GetComponent<Text>();
		_currDef = GameObject.Find("CurrentDefBns").GetComponent<Text>();
		_currSpd = GameObject.Find("CurrentSpdBns").GetComponent<Text>();

		_buyButton = GameObject.Find("BuyBlessing").GetComponent<Button>();

		_availBlessings = GameObject.Find("AvailableBlessings").GetComponent<Text>();
	}

	void CheckAvailability(){
		_cost = Mathf.Max(500, 1000 * (_CombatManager._blessings + _CombatManager._availBlessings));

		_CombatManager.CalculateStats();

		_buttons[0].interactable = (_CombatManager._availBlessings > 0 && _CombatManager._vitBonus < 20);
		_buttons[1].interactable = (_CombatManager._availBlessings > 0 && _CombatManager._attBlessings < 20);
		_buttons[2].interactable = (_CombatManager._availBlessings > 0 && _CombatManager._defBlessings < 20);
		_buttons[3].interactable = (_CombatManager._availBlessings > 0 && _CombatManager._spdBonus < 20);

		_vitText.text = _CombatManager._vitBonus.ToString();
		_attText.text = _CombatManager._attBlessings.ToString();
		_defText.text = _CombatManager._defBlessings.ToString();
		_spdText.text = _CombatManager._spdBonus.ToString();

		_currVit.text = _CombatManager._maxHealth.ToString();
		_currAtt.text = (_CombatManager._attBlessings * 10) + " %";
		_currDef.text = (_CombatManager._defBlessings * 10 )+ " %";
		_currSpd.text = (_CombatManager._speed + _CombatManager._speedPenalty).ToString();

		_costText.text = _cost.ToString();

		_availBlessings.text = "Points to Allocate: " + _CombatManager._availBlessings;
		_buyButton.interactable = (_CombatManager._availBlessings + _CombatManager._blessings < 80 && _manager._obols >= _cost);

		_ui.UpdateUI();
	}

	public void BuyBlessing(){
		_CombatManager._availBlessings++;
		_manager._obols -= _cost;
		CheckAvailability();
	}

	public void Reset(){
		_CombatManager._availBlessings += _CombatManager._blessings;
		_CombatManager._blessings = 0;
		_CombatManager._vitBonus = 0;
		_CombatManager._attBlessings = 0;
		_CombatManager._defBlessings = 0;
		_CombatManager._spdBonus = 0;
		CheckAvailability();
	}

	public void AddBlessing(int i){
		switch(i){
			case 0:
			_CombatManager._vitBonus++;
			break;
			case 1:
			_CombatManager._attBlessings++;
			break;
			case 2:
			_CombatManager._defBlessings++;
			break;
			case 3:
			_CombatManager._spdBonus++;
			break;
		}
		_CombatManager._blessings++;
		_CombatManager._availBlessings--;
		CheckAvailability();
	}
}

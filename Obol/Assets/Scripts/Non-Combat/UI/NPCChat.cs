using UnityEngine;

public class NPCChat : MonoBehaviour {

	public string _text;
	public NonCombat_UI _ui;
	public int _pos;

	void Start(){
		_ui = gameObject.GetComponent<NonCombat_UI>();
	}

	public void UpdateText(int character){
		switch (character){
			case 1:
			MerchantChat();
			break;
			case 2:
			SmithChat();
			break;
			case 3:
			PriestChat();
			break;
			case 4:
			InventorChat();
			break;
			case 5:
			ThiefChat();
			break;
		}
	}

	void MerchantChat(){
		switch (_manager._chatState[1]){
			case 0:
			_text = "Well isnt this a surprise. Nobody has come through those doors in a thousand years. Hmmm...";
			break;
			case 1:
			_text = "... But if you're able to take down the Warden, then you might just be able to survive down here...";
			break;
			case 2:
			_text = "...Tell you what, why don't we see if we can help each other out? ...";
			break;
			case 3:
			_text = "...The Underworld is a dangerous place, but there are riches to be had by those willing to find them...";
			break;
			case 4:
			_text = "...I will pay you a good price on any resources you can harvest. I can also sell any you might find yourself in need of...";
			break;
			case 5:
			_text = "...I'm sure together we can earn enough to pay our way out of here...";
			break;
			case 6:
			_text = "..Use the Portal down there to travel the Underworld, but be sure to bring me what you find before someone or somthing takes if from you...";
			break;
			case 7:
			_text = "...Good luck, Wanderer.";
			break;
			case 8:
			_text = "Error";
			break;
		}			
		if (_manager._chatState[1] > 7){
			_manager._npcChat[1] = false;			
			_ui.OpenCanvas(1);
			_ui._npcChatGO.SetActive(false);
		} 		
		_manager._chatState[1]++;			
	}

	void SmithChat(){
		switch (_manager._chatState[2]){
			case 0:
			_text = "Bring me any weapons and armour you have, and I shall store them safely while you are away. I could maybe even fix them up a little for you, if you bring me the resources.";
			break;
			case 1:
			_text = "Error";
			break;
		}			
		if (_manager._chatState[2] > 0){
			_manager._npcChat[2] = false;			
			_ui.OpenCanvas(2);
			_ui._npcChatGO.SetActive(false);
		} 		
		_manager._chatState[2]++;			
	}

	void PriestChat(){
		switch (_manager._chatState[3]){
			case 0:
			_text = "Blessing be upon you, Wanderer...";
			break;
			case 1:
			_text = "...Let the light of the Gods shine upon you in this dark place...";
			break;
			case 2:
			_text = "...For a small donation, of course.";
			break;
			case 3:
			_text = "Error";
			break;
		}			
		if (_manager._chatState[3] > 2){
			_manager._npcChat[3] = false;			
			_ui.OpenCanvas(3);
			_ui._npcChatGO.SetActive(false);
		} 		
		_manager._chatState[3]++;			
	}

	void InventorChat(){
		switch (_manager._chatState[4]){
			case 0:
			_text = "*ho-hum*";
			break;
			case 1:
			_text = "*hum hum hum*";
			break;
			case 2:
			_text = "...Oh!...";
			break;
			case 3:
			_text = "Ah! A fresh pair of sockets has come to view my creations! Wonderful!";
			break;
			case 4:
			_text = "...These are mere trinkets, however... If you want, I can make you the finest equipment in the whole of the Underworld...";
			break;
			case 5:
			_text = "... Only, I don't have the resources. That, I'm afraid, is up to you...";
			break;
			case 6:
			_text = "...Bring me what you find and I'll have you armed to the teeth in no time!";
			break;
			case 7:
			_text = "Error";
			break;
		}			
		if (_manager._chatState[4] > 6){
			_manager._npcChat[4] = false;			
			_ui.OpenCanvas(4);
			_ui._npcChatGO.SetActive(false);
		} 		
		_manager._chatState[4]++;			
	}

	void ThiefChat(){
		switch (_manager._chatState[5]){
			case 0:
			_text = "Well isnt this a surprise. Nobody has come through those doors in a thousand years. Hmmm...";
			break;
			case 1:
			_text = "... But if you're able to take down the Warden, then you might just be able to survive down here...";
			break;
			case 2:
			_text = "...Tell you what, why don't we see if we can help each other out? ...";
			break;
			case 3:
			_text = "...The Underworld is a dangerous place, but there are riches to be had by those willing to find them...";
			break;
			case 4:
			_text = "...I will pay you a good price on any resources you can harvest. I can also sell any you might find yourself in need of...";
			break;
			case 5:
			_text = "...I'm sure together we can earn enough to pay our way out of here...";
			break;
			case 6:
			_text = "..Use the Portal down there to travel the Underworld, but be sure to bring me what you find before someone or somthing takes if from you...";
			break;
			case 7:
			_text = "...Good luck, Wanderer.";
			break;
			case 8:
			_text = "Error";
			break;
		}			
		if (_manager._chatState[5] > 7){
			_manager._npcChat[5] = false;			
			_ui.OpenCanvas(5);
			_ui._npcChatGO.SetActive(false);
		} 		
		_manager._chatState[5]++;			
	}
}

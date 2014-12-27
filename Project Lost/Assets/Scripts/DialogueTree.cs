using UnityEngine;
using System.Collections;

public class DialogueTree : MonoBehaviour {
	//trigger in the scene 
	public ITrigger dialogueTrigger;
	//initiates dialogue based on dialogueTrigger
	public bool dialogueTriggered;
	//the catalog of all textboxes in this NPC's dialogue
	public TextBox[] textBoxes;

	//translates playerID into textbox values
	public PlayerIdentity playerID;


	int i;
	bool isDialogueTerminated;

	//resets any variables that allow dialogue to be triggered again.
	void ResetDialogue(bool reset){
		if (isDialogueTerminated) {
			isDialogueTerminated = false;
			i = 0;
		}
	}

	//Passes the PlayerID values to its TextBoxes then tells them to compile the text based on this information
	void InstantiateTextBoxes(PlayerIdentity p){
	
		for (int j = 0; j < textBoxes.Length; j++) {
		
			textBoxes[j].InstantiateValues(p.firstName, p.lastName, p.gender);
			textBoxes[j].CompileText();
		
		}

	}




	void OnGUI(){

		//Determines if dialogue options should be rendered.
		if (dialogueTrigger.IsTriggered && !isDialogueTerminated) {

			GUI.TextField(new Rect(400, 400, 300, 20), textBoxes[i].text);
			textBoxes[i].hasBeenRead = true;

			if(textBoxes[i].nextTextBoxID.Length != 0){

				for(int j = 0; j < textBoxes[i].nextTextBoxID.Length; j++){
					
					if(GUI.Button(new Rect(400, (450 + ((j+1) * 30)), 300, 20), textBoxes[textBoxes[i].nextTextBoxID[j]].text)){
						
						isDialogueTerminated = textBoxes[textBoxes[i].nextTextBoxID[j]].terminatesDialogue;
						dialogueTrigger.ResetTrigger(isDialogueTerminated);
						textBoxes[textBoxes[i].nextTextBoxID[j]].hasBeenRead = true;
						if(!textBoxes[textBoxes[i].nextTextBoxID[j]].terminatesDialogue){
							i = textBoxes[textBoxes[i].nextTextBoxID[j]].nextTextBoxID[0];
						}
						
						
					}
					
				}

			}
		
		}

	}





	

	void Start() {

		i = 0;
		isDialogueTerminated = false;
		InstantiateTextBoxes (playerID);

	}

	void Update(){

		ResetDialogue (isDialogueTerminated);

	}





}

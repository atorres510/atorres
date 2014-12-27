using UnityEngine;
using System.Collections;

public class DialogueTrigger : ITrigger {

	public bool toggleTextField;


	//disables the trigger after player executes the prompt
	void disableTrigger(){

		TriggerBoxCollider2D.enabled = false;
		toggleTextField = false;

	}

	//reinstates the collider for the when the player ->
	//leaves the dialogue so a prompt can be displayed again
	void enableTrigger(bool enable){

		if (enable) {
			TriggerBoxCollider2D.enabled = true;
		}

	}

	public override void ResetTrigger(bool reset){

		if (reset) {
			IsTriggered = false;
			toggleTextField = true;
			enableTrigger(reset);
		
		}

	}
	
	
	void OnTriggerStay2D(Collider2D col){

		//prompts player to press space to engage the dialogue.
		if (col.tag == "Sally") {

			toggleTextField = true;

		
			if(Input.GetKeyDown(KeyCode.Space)){

				IsTriggered = true;
				disableTrigger();
			}

		}


	}

	//removes prompt when player is out of range
	void OnTriggerExit2D(Collider2D col){
		if (col.tag == "Sally") {
			toggleTextField = false;
		}
	}



	void OnGUI(){
		//prompt display
		if (toggleTextField) {
			string inputPrompt = "Space";
			inputPrompt = GUI.TextField(new Rect(400, 500, 55, 25), inputPrompt);
		}

	}

	void Start(){

		toggleTextField = false;


	}




	void Update(){





	}







}

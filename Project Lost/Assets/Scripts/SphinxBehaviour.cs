using UnityEngine;
using System.Collections;

public class SphinxBehaviour : MonoBehaviour {

	BoxCollider2D boxCollider2D;
	DialogueTree dialogueTree;


	//passes the specific text box ID# to allow the player through,
	//then passage trigger checks if it has been read
	public int triggerTextBoxID;
	bool passageTrigger;

	//continues to check if player has met the dialogue requirements to pass this sphinx
	void allowPlayerPassage(){

		passageTrigger = dialogueTree.textBoxes [triggerTextBoxID].hasBeenRead;
		
		if (passageTrigger) {
			
			boxCollider2D.enabled = false;
			
		}
	
	}


	void Start(){
		//instantiations
		boxCollider2D = gameObject.GetComponent<BoxCollider2D> ();
		dialogueTree = gameObject.GetComponent<DialogueTree> ();
	
	}


	void Update(){

		allowPlayerPassage ();

	}






}

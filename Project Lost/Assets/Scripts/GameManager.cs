using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	//*******GameManger requires manual hook up of the Maze Start, Maze End, and all dialogue triggers*******//


	public ITrigger mazeStart;
	public ITrigger mazeEnd;

	//contains a list of the dialogue triggers in the level to check for activity
	public DialogueTrigger[] dialogueTriggers;

	//holds the current trigger for later use
	ITrigger currentDialogueTrigger;

	GameObject player;

	//GUI cues
	bool isPaused;
	bool isDialogueTriggered;
	bool isLevelCompleted;

	void LevelSetUp(){
		//instantiation
		player = GameObject.FindGameObjectWithTag ("Sally");
		isPaused = false;
		isLevelCompleted = false;
		isDialogueTriggered = false;
		currentDialogueTrigger = null;
		//positions player start
		player.transform.position = mazeStart.ReturnTriggerPosition();
	
	}
	
	//resets the level
	void Restart(){
	
		Application.LoadLevel (0);

	}

	//pauses the game, disabling player control
	void Pause(){

		PlayerController2D playerController2D = player.GetComponent<PlayerController2D> ();
		playerController2D.TogglePlayerController ();
		//Light playerLight = player.GetComponentInChildren<Light> ();
		//playerLight.enabled = false;


		//prevents pause GUI overlap
		if (!isLevelCompleted && !isDialogueTriggered) {
			isPaused = !isPaused;

			//toggles dialogue textfield prompt when pausing game over a trigger to prevent GUI overlap
			for(int i = 0; i < dialogueTriggers.Length; i++){
			
				BoxCollider2D triggerCol = dialogueTriggers[i].GetComponent<BoxCollider2D>();
				triggerCol.enabled = !triggerCol.enabled;

				if(dialogueTriggers[i].toggleTextField){
					dialogueTriggers[i].toggleTextField = !dialogueTriggers[i].toggleTextField;
				}
				

			}
		}

		else{}

	
	}

	//pauses the game and will eventually load the next scene
	void LevelCompleted(){

		if (mazeEnd.IsTriggered) {

			isLevelCompleted = true;
			Pause ();

		}
	
	}

	void DialogueTriggered(){
		//while there is no currentTrigger, check to make sure one has not been activated
		if (currentDialogueTrigger == null) {
			for (int i = 0; i < dialogueTriggers.Length; i++) {
				
				isDialogueTriggered = dialogueTriggers[i].IsTriggered;
				//pauses player movement and sets current Trigger for later use
				if(isDialogueTriggered){
					//Debug.Log(isDialogueTriggered);
					Pause ();
					currentDialogueTrigger = dialogueTriggers[i];
					//Debug.Log("Dialogue Triggered");
				}
				
			}

		}


		else{
			//checks to see if the current dialogue has been reset, ->
			//unpauses player movement, and clears the current trigger
			if (currentDialogueTrigger.IsTriggered == false) {
				
				currentDialogueTrigger = null;
				Pause();
				
			}

		}


	}

	
	void OnGUI(){

		if (GUI.Button (new Rect (10, 10, 125, 25), "Restart")) {
				
			Restart();
		
		}

		if (isPaused) {
				
			string pausedString = "Paused";
			pausedString = GUI.TextField(new Rect(400, 300, 55, 25), pausedString);
		
		}

		if (isLevelCompleted) {
				
			string levelCompletedString = "You escaped.";
			levelCompletedString = GUI.TextField (new Rect (400, 500, 80, 25), levelCompletedString);

		}


	
	
	}

	
	void Awake(){

		LevelSetUp ();

	}

	void Update(){

		//pauses the game
		if (Input.GetKeyDown (KeyCode.Escape)) {

			Pause();
			Debug.Log("Pause");
		}
	
		//checks if the level is completed. continues to run if the level is not completed.
		if (!isLevelCompleted) {
			LevelCompleted ();
		}

		//checks the array of dialogue triggers so the character movement ->
		//is restricted with Pause() if dialogue has been trigged
		DialogueTriggered ();


	}




}

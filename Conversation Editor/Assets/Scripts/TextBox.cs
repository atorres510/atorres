using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextBox : MonoBehaviour {



	int windowRectLeft;
	int windowRectTop;
	int windowRectWidth;
	int windowRectHeigth;

	

	/*public*/ string text; //Holds the text displayed 
	/*public*/ int textBoxID; //Individual box ID number.  Used to determine textbox order 
	/*public*/ int windowID; //Window ID for GUI.Window
	/*public*/ int textBoxType; // 0- Textfield for NPC response/prompt, 1- Button for player's dialogue choice
	/*public*/ bool terminatesDialogue;
	/*public*/ bool hasBeenRead; //Remembers player choice and allows for player choice to have consequence.
	
	///*public*/ int[] nextTextBoxID; //Finds the next text box in the dialogue tree by textBoxID #

	/*public*/ List<int> nextTextBoxID = new List<int>();





	//Methods and variables for player ID values

	int playerGender; //Stores player characters gender. Passed through from DialogueTree.cs
	string playerFirstName; // Stores player's first, last, and fully compiled name
	string playerLastName;
	string playerFullName;
	//public bool isTest; // For Testing Purposes Only

	string[] texts; //Holds the fragments of the unprocessed text
	string compiledText;//The string that is constantly added to
	string tempText;//Temporary chuck of processed text added to the compiled text

	//assigns player ID values passed from DialogueTree
	public void InstantiateValues(string fName, string lName, int gender){

		playerGender = gender;
		playerFirstName = fName;
		playerLastName = lName;
		playerFullName = fName + " " + lName;

	}


	public string Text{ 
		get{return text;}
	}

	public int WindowID{ 
		get{return windowID;} 
		set{windowID = value;}
	}

	//Replaces the [keyword] to the appropriate gender name
	bool ReturnGenderSignifier(string s){
		
		if (s == "Sir/Ma'am") {
			if (playerGender == 0) {
				
				tempText = "Sir";
				return true;
				
			}
			
			if (playerGender == 1) {
				
				tempText = "Ma'am";
				return true;
				
			}
			else{return false;}
		}
		
		if(s == "Mr./Mrs."){
			if(playerGender == 0){
				
				tempText = "Mr.";
				return true;
				
			}
			
			if(playerGender == 1){
				
				tempText = "Mrs.";
				return true;
				
			}

			else{return false;}
		}

		else{return false;}
		
	}

	//Replaces [keyword] with player's name
	bool ReturnPlayerName(string s){

		if (s == "PlayerFirstName") {
			tempText = playerFirstName;
			return true;
		}

		if (s == "PlayerLastName") {
			tempText = playerLastName;
			return true;
		}

		if (s == "PlayerFullName") {
			tempText = playerFullName;
			return true;
		} 
		else {//pass
			return false;
		}


	}

	//splits the texts into chunks.  anything within brackets  should be a keywords 
	void SplitText(){
	
		texts = text.Split(new char[] {'[',']'});
	
	}





	//Takes the fragments of texts[] made in SplitText and runs them through the keyword methods and
		//then adds them to the total compiled text.
	void AddText(string s){

		if (ReturnGenderSignifier (s)) {

			compiledText = compiledText + tempText;


		}


		else if (ReturnPlayerName (s)) {
		
			compiledText = compiledText + tempText;

		}


		else {

			tempText = s;
			compiledText = compiledText + tempText;

		}

	}


	//Calls the splitText and AddText, then reassembles the text for GUI use.
	public void CompileText(){

		SplitText ();

		for (int i = 0; i < texts.Length; i++) {
		
			AddText(texts[i]);
			//Debug.Log(tempText);

		}

		text = compiledText;

	}


	/*public void AssignWindowID(int boxID){

		windowID = boxID;
		Debug.Log (windowID);



	}*/

	/*void UpdateTransform(Rect r){

		Vector3 v = camera.ScreenToWorldPoint (r.position);

		gameObject.transform.position = v;
		

	}*/









	//Rect windowRect = new Rect (windowRectLeft, windowRectTop, windowRectWidth, windowRectHeigth);
	Rect windowRect = new Rect (50, 60, 250, 200);
	//Rect windowRect;
	
	void OnGUI () {
		//windowRect = GUI.Window (windowID, new Rect (windowRectLeft, windowRectTop, windowRectWidth, windowRectHeigth), WindowFunction, "Node_" + windowID);
		windowRect = GUI.Window (windowID, windowRect, WindowFunction, "Node_" + windowID);
	
	
	}
	
	void WindowFunction (int windowID) {



		terminatesDialogue = GUI.Toggle(new Rect(10, 30, 140, 25), terminatesDialogue, "Terminates Dialogue");
		text = GUI.TextArea(new Rect(10, 90, 230, 100), text);	
		GUI.DragWindow(new Rect(0,0,1000,20));
		//if (GUI.Button (new Rect(150, 30, 60, 25), "Options")) {

		//}
		//GUI.DragWindow();
	}








	void Start(){

		text = "Enter Text";
		//windowRectLeft = 50;
		//windowRectTop = 60;
		//windowRectWidth = 250;
		//windowRectHeigth = 180;

	}
	



	void Update(){

		//UpdateTransform (windowRect);


	}




}

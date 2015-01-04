using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextBox : MonoBehaviour {
	
	public Editor editor;
	public Camera camera;
	public GameObject lineSegmentPrefab;

	int windowRectLeft;
	int windowRectTop;
	int windowRectWidth;
	int windowRectHeigth;
	bool isCollapsed;

	

	/*public*/ string text; //Holds the text displayed 
	/*public*/ int textBoxID; //Individual box ID number.  Used to determine textbox order 
	/*public*/ int windowID; //Window ID for GUI.Window
	/*public*/ int textBoxType; // 0- Textfield for NPC response/prompt, 1- Button for player's dialogue choice
	/*public*/ bool terminatesDialogue;
	/*public*/ bool hasBeenRead; //Remembers player choice and allows for player choice to have consequence.
	
	///*public*/ int[] nextTextBoxID; //Finds the next text box in the dialogue tree by textBoxID #

	/*public*/ List<int> nextTextBoxID = new List<int>();





	//Methods and variables for player ID values

	//int playerGender; //Stores player characters gender. Passed through from DialogueTree.cs
	//string playerFirstName; // Stores player's first, last, and fully compiled name
	//string playerLastName;
	//string playerFullName;
	//public bool isTest; // For Testing Purposes Only

	string[] texts; //Holds the fragments of the unprocessed text
	string compiledText;//The string that is constantly added to
	string tempText;//Temporary chuck of processed text added to the compiled text

	//assigns player ID values passed from DialogueTree
	/*public void InstantiateValues(string fName, string lName, int gender){

		playerGender = gender;
		playerFirstName = fName;
		playerLastName = lName;
		playerFullName = fName + " " + lName;

	}*/


	public string Text{ 
		get{return text;}
	}

	public int WindowID{ 
		get{return windowID;} 
		set{windowID = value;}
	}

	//Replaces the [keyword] to the appropriate gender name
	/*bool ReturnGenderSignifier(string s){
		
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


	//reassembles the text for GUI use.
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
	public Rect windowRect = new Rect (50, 60, 250, 200);
	Rect windowRectCollapsed = new Rect(50, 60, 100, 70);
	//Rect windowRect;

	public Vector2 oldscrollposition = new Vector2(0,0);
	
	void OnGUI () {
		//windowRect = GUI.Window (windowID, new Rect (windowRectLeft, windowRectTop, windowRectWidth, windowRectHeigth), WindowFunction, "Node_" + windowID);


		if(!isCollapsed){

			windowRect = GUI.Window (windowID, windowRect, WindowFunction, "Node_" + windowID);

		}

		if(isCollapsed){

			windowRectCollapsed = GUI.Window(windowID, windowRectCollapsed, WindowFunction, "Node_" + windowID);
  		 	
		}

		WindowSync();


		windowRect.y -= editor.scrollPosition.y - oldscrollposition.y;
		windowRect.x -= editor.scrollPosition.x - oldscrollposition.x;
		windowRectCollapsed.y -= editor.scrollPosition.y - oldscrollposition.y;
		windowRectCollapsed.x -= editor.scrollPosition.x - oldscrollposition.x;
		
		oldscrollposition = editor.scrollPosition;
		
	
	}
	
	void WindowFunction (int windowID) {

			//editor.selectedWindow = windowID;
			//Debug.Log("This window is selected: " + windowID);
			//GUI.BeginGroup(new Rect(0, 0, 250, 200));

		if(Event.current.button == 1 && Event.current.type == EventType.MouseUp &&  (editor.currentWindow == this || editor.currentWindow == null)){
			
			GUI.BringWindowToFront(windowID);
			editor.selectedWindow = this;
			editor.currentWindow = this;
			Debug.Log("Window #" + editor.selectedWindow.windowID + " has been selected.");
			
		}
		
		if(Event.current.button == 1 && Event.current.type == EventType.MouseUp && (editor.currentWindow != this)){
			
			bool pass = false;
			
			foreach (int element in editor.currentWindow.nextTextBoxID){
				
				if(element == this.windowID){
					
					pass = true;
					Debug.Log("This node is already linked");
					
				}
				
			}

		

			if(!pass){
					
				editor.currentWindow.nextTextBoxID.Add(this.windowID);
				DrawLine(editor.currentWindow, this);
				Debug.Log("Node " + editor.currentWindow.windowID + " is linked to node " + this.windowID);
				editor.selectedWindow = null;
				editor.currentWindow = null;
				
			}
		}



		if(isCollapsed){

			isCollapsed = GUI.Toggle(new Rect(10, 25, 140, 25), isCollapsed, "Collapse"); 
			GUI.DragWindow(new Rect(0,0,1000,20));

		}

		else{

			//editor.currentWindow.nextTextBoxID.Add(this.windowID);
			//Debug.Log("Node " + editor.currentWindow.windowID + "is linked to node " + editor.currentWindow.nextTextBoxID[0]);
			//Debug.Log("linked");
			//editor.selectedWindow = null;
			//editor.currentWindow = null;
			
			//editor.currentWindow.nextTextBoxID.IndexOf(

			isCollapsed = GUI.Toggle(new Rect(10, 25, 140, 25), isCollapsed, "Collapse"); 
			//terminatesDialogue = GUI.Toggle(new Rect(10, 40, 140, 25), terminatesDialogue, "Terminates Dialogue");
			text = GUI.TextArea(new Rect(10, 90, 230, 100), text);	
			GUI.DragWindow(new Rect(0,0,1000,20));
			
			//GUI.EndGroup();
			
			//if (GUI.Button (new Rect(150, 30, 60, 25), "Options")) {
			
			//}
			//GUI.DragWindow();
				
			}

	}


	void WindowSync(){

		if(!isCollapsed){

			windowRectCollapsed.x = windowRect.x;
			windowRectCollapsed.y = windowRect.y;

		}

		else{

			windowRect.x = windowRectCollapsed.x;
			windowRect.y = windowRectCollapsed.y;

		}

		//Debug.Log (windowRect.x + "       " + windowRect.y);

	}


	//draws three lines to connect nodes
	public void DrawLine(TextBox t1, TextBox t2){








		Debug.Log (t1.windowID);
		Debug.Log (t2.windowID);

		Vector3 tPos = windowRect.position;

		Rect rect1 = t1.windowRect;
		Rect rect2 = t2.windowRect;

		/*float x1 = rect1.x;
		float y1 = rect1.y;

		Vector3 v1 = new Vector3 (x1, y1, Camera.main.nearClipPlane);
		Vector3 v2 = Camera.main.ScreenToWorldPoint (v1);

		float x2 = rect2.x;
		float y2 = rect2.y;
		
		Vector3 v3 = new Vector3 (x2, y2, Camera.main.nearClipPlane);
		Vector3 v4 = Camera.main.ScreenToWorldPoint (v3);
	*/	

		Debug.Log (rect1.x + "    " + rect1.y);




		Vector3 v1 = camera.ScreenToWorldPoint (new Vector3 (rect1.x, rect1.y, camera.nearClipPlane));
		Vector3 v2 = camera.ScreenToWorldPoint (new Vector3 (rect2.x, rect2.y, camera.nearClipPlane));

		Debug.Log (v1.x + "    " + v1.y);


		//Distance between nodes

		float distanceX = v2.x - (v1.x + 250);
		float distanceY = v2.y - (v1.y - 100);

		//Divide distance x 

		float distanceXhalved = distanceX / 2;

		//instantiate segment1
		GameObject lineSegmentclone1 = Instantiate (lineSegmentPrefab) as GameObject;
		LineSegment lineSegment1 = lineSegmentclone1.GetComponent<LineSegment> ();
		lineSegment1.InstantiateLineSegment (v1.x, v1.y, v2.x, v1.y);
		lineSegment1.origin = t1;
		lineSegment1.destination = t2;
		//instantiate segment2
		//GameObject lineSegmentclone2 = Instantiate (lineSegmentPrefab) as GameObject;
		//LineSegment lineSegment2 = lineSegmentclone2.GetComponent<LineSegment> ();
		//lineSegment2.InstantiateLineSegment ((distanceXhalved), (rect1.y + 100), distanceXhalved, (distanceY));
		//lineSegment2.origin = t1;
		//lineSegment2.destination = t2;
		//instantiate segment3
		//GameObject lineSegmentclone3 = Instantiate (lineSegmentPrefab) as GameObject;
		//LineSegment lineSegment3 = lineSegmentclone3.GetComponent<LineSegment> ();
		//lineSegment3.InstantiateLineSegment ((distanceXhalved), (distanceY), distanceX, (distanceY));
		//lineSegment3.origin = t1;
		//lineSegment3.destination = t2;
	


	
	}






	public string ThisBoxToString(){

		string compiledString;
		string terminatesDialogueString = terminatesDialogue.ToString();
		string windowIDString = windowID.ToString();
		string nextTextBoxIDString = "{";

		foreach(int element in nextTextBoxID){

			string s = element.ToString();





			if(element == (nextTextBoxID[nextTextBoxID.Count - 1])){

				nextTextBoxIDString = nextTextBoxIDString + s;


			}

			else{nextTextBoxIDString = nextTextBoxIDString + s + ", ";}




		}

		nextTextBoxIDString = nextTextBoxIDString + "}";





		compiledString = "[" + text + "], " + terminatesDialogueString + ", " + windowIDString + ", " + nextTextBoxIDString;

		return compiledString;


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

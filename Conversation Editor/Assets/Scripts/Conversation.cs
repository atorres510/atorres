using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class Conversation : MonoBehaviour {
	
	public TextAsset textFile;

	//public 



	List<textBox> textBoxes;

	class textBox{

		public string text; 
		public int windowID;
		public int type;
		public List<int> nextWindowID;
		public bool terminatesDialogue;

		public textBox(string t, int id, int ty, List<int> idArray, bool td){

			text = t; 
			windowID = id;
			type = ty;
			nextWindowID = idArray;
			terminatesDialogue = td;

		}


	}





	void ReadTextFile(){

		Debug.Log ("ReadTextFile()");

		textBoxes = new List<textBox>();
		string[] nodes;


		if (textFile != null) {
				
			nodes = (textFile.text.Split( '\n' ));



			for (int i = 0; i < nodes.Length; i++) {

				if(nodes[i] != ""){

					textBoxes.Add(NodetoTextBox(nodes[i]));

				}
			
				
			}

		
		}

		else{

			Debug.LogError("Text file missing.");

		}

	}


	textBox NodetoTextBox(string n){

		n.TrimStart ();
		n.TrimEnd ();

		string[] firstSplit; 	//splits the line into its its basic variables, which are seperated by ','
								// within the first split array, 0 = text, 1 = next windows array, 2 = terminates dialogue, 3 = window id, 4 = nodetype;

		string[] textArray;		//extracts text seperated by '[]'
		string[] nextWindowsIDArray;// extracts the next nodes in the tree for this text box indicated by {}
		string terminatesDialogueString;
		string windowIDString;
		string windowTypeString;

		string text = "";
		int windowID = 0;
		int windowType = 0;
		List<int> nextWindowID = new List<int> ();
		bool terminatesDialogue = false;

		firstSplit = n.Split (new char[] {','});

		textArray = firstSplit [0].Split (new char[] {'[',']'}, System.StringSplitOptions.RemoveEmptyEntries);
		nextWindowsIDArray = firstSplit [1].Split (new char[] {'{','}'}, System.StringSplitOptions.RemoveEmptyEntries);
		terminatesDialogueString = firstSplit [2].Trim();
		windowIDString = firstSplit [3].Trim();
		windowTypeString = firstSplit [4].Trim();

		//builds textarray into the text to be displayed by this node
		for (int i = 0; i < textArray.Length; i++) {
				
			text = text + textArray[i];
		
		}

		text.TrimStart ();
		text.TrimEnd ();


		//builds the list of nodes that will succeed this node
		for (int i = 0; i < nextWindowsIDArray.Length; i++) {

			//Debug.Log(i + " : " + nextWindowsIDArray[i]);

			if(nextWindowsIDArray[i] != ""){

				string[] tempArray = nextWindowsIDArray[i].Split(new char[]{';', ' '}, System.StringSplitOptions.RemoveEmptyEntries);

				for(int j = 0; j < tempArray.Length; j++){
					//Debug.Log(j + " : " + tempArray[j]);

					if(tempArray[j] != ""){
						tempArray[j].Trim();
						//Debug.Log(tempArray[j]);
						int tempID = int.Parse(tempArray[j]);
						nextWindowID.Add(tempID);

					}


				}


			}

		}




		//determines whether this node terminates dialogue
		if (terminatesDialogueString == "False") {
				
			terminatesDialogue = false;
		
		}

		else if(terminatesDialogueString == "True"){

			terminatesDialogue = true;

		}

		else{
			Debug.LogError("Cannot create TextBox: TerminatesDialogueString format invalid");
		}


		windowID = int.Parse(windowIDString);
		windowType = int.Parse (windowTypeString);
	

		Debug.Log ("Text: " + text);

		foreach(int element in nextWindowID) {
				
			Debug.Log("nextWindowID: " + element);

		}

		Debug.Log ("Terminates Dialogue: " + terminatesDialogue);
		Debug.Log ("Window ID: " + windowID);
		Debug.Log ("WindowType: " + windowType);



		textBox t = new textBox (text, windowID, windowType, nextWindowID, terminatesDialogue);

	
		return t;
	
	}










	void Start(){

		ReadTextFile ();

	}

	

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;


public class Editor : MonoBehaviour {

	public GameObject textBoxPrefab;

	List<TextBox> textboxes = new List<TextBox> ();


	int totalTextBoxes;
	string filename;






	void InstantiateTextBox(){
	
		//Debug.Log (totalTextBoxes);

		GameObject textBoxClone = Instantiate (textBoxPrefab) as GameObject;

		TextBox t = textBoxClone.GetComponent<TextBox> ();

		t.WindowID = totalTextBoxes;
		//Debug.Log ("WindowID  " + t.WindowID);

		textboxes.Add (t);
		//Debug.Log (textboxes [totalTextBoxes]);
	

		totalTextBoxes++;
		
	
	}

	

	//Convert Textbox members into a string 
	string TextBoxToString(TextBox t){

		//GameObject TextBoxObject = GameObject.FindGameObjectWithTag("TextBox");
		//TextBox t = TextBoxObject.GetComponent<TextBox>();


		return t.Text;


	}

	
	void OnGUI(){

		if(GUI.Button(new Rect(10, 10, 40, 25), "Save")){

			WriteAllText(filename);

		}

		if(GUI.Button(new Rect(10, 35, 80, 25), "Add Node")){
			
			InstantiateTextBox();

		}





		filename = GUI.TextField(new Rect(60, 10, 60, 25), filename);





	}


	//Saves project under file name in the Saves folder within Assets
	void WriteAllText(string filename){

		if (filename != "") {
		
			string path = Application.dataPath + "/Saves/" + filename + ".txt";

			StreamWriter sw = new StreamWriter (path, false);

			foreach (TextBox element in textboxes){

				sw.WriteLine (element.Text);
			}

			sw.Close ();
			/*StreamWriter sw = new StreamWriter (path, false);
			
			sw.WriteLine (s);
			sw.Close ();*/
		
		
		}



		else {

			Debug.Log("File Name Required");

		}





		//File.WriteAllText(path, newString);





	}






	void Start () {
	
			filename = "";
			totalTextBoxes = 0;


	}
	






}

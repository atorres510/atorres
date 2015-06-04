using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Textbox : MonoBehaviour {

	public Text UIText;
	public RectTransform UIPanel;

	TextAsset textFile;
	string text;
	string[] lines;

	RectTransform rectTransform;
	float unitPerLine = 15;
	float unitPerChar = 6;


	public TextAsset TextFile{

		set{

			textFile = value;

		}



	}



	string[] CutIntoLines(string text){

		string[] cutLines = text.Split (new char[] {'\n'});


		for (int i = 0; i < cutLines.Length; i++) {
		
			//Debug.Log(cutLines[i]);
		
		
		
		
		}

		return cutLines;

	}


	int CountLines(string[] lines){
	
	
		return (lines.Length + 1);
	
	
	}



	int CountCharacters(string line){
	
	
		return line.Length;
	
	
	
	}


	int ReturnLongestLineLength(string[] lines){
	
		string longestLineSoFar = "";


		for (int i = 0; i < lines.Length; i++) {
		
			int characters = CountCharacters(lines[i]);

			if(longestLineSoFar.Length < characters){

				longestLineSoFar = lines[i];

			}

			else{/*pass*/}


		}
	
		Debug.Log ("Longest Line So Far is: " + longestLineSoFar);
	
		return CountCharacters(longestLineSoFar);
	
	}



	void FormatTextBoxSize (RectTransform r, int numberofChar, int numberOfLines){

		//finds the right width by using the number of characters in the longest line
		float rectWidth = numberofChar * unitPerChar;
		//fids the right heigth of by finding number of lines
		float rectHeigth = numberOfLines * unitPerLine;

		Vector2 adjustedSize = new Vector2 (rectWidth, rectHeigth);


		UIText.rectTransform.sizeDelta = (adjustedSize);

		UIPanel.sizeDelta = (adjustedSize);





	}



	void Start(){

		//text = UIText.text;
		text = textFile.text;
		UIText.text = text;
		rectTransform = gameObject.GetComponent<RectTransform> ();
		lines = CutIntoLines (text);
		FormatTextBoxSize(rectTransform, ReturnLongestLineLength(lines), CountLines(lines));


		 



	}





















}

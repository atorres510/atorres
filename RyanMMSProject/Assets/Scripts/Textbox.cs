using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Textbox : MonoBehaviour {

	public Text UIText;
	public GameObject UIPanel;

	string text;
	string[] lines;

	RectTransform rectTransform;
	float unitPerLine = 15;
	float unitPerChar = 6;






	string[] CutIntoLines(string text){

		string[] cutLines = text.Split (new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);


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

		rectTransform.sizeDelta = (new Vector2(rectWidth, rectHeigth));





	}



	void Start(){


		rectTransform = gameObject.GetComponent<RectTransform> ();
		lines = CutIntoLines (text);
		FormatTextBoxSize(rectTransform, ReturnLongestLineLength(lines), CountLines(lines));


		 



	}





















}

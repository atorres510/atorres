using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SpriteLibrary : MonoBehaviour {

	public string folderPath;

	//Stores sprites in arrays by faction.  Uses piece Designation to ID piece sprite type.
	Dictionary<string, Sprite[]> factionSprites = new Dictionary<string, Sprite[]>();



	void CreateSpriteDictionary(string path){

		string[] filePaths = Directory.GetFiles (path);

		foreach (string filePath in filePaths) {

			string cleanedPath = CleanString(filePath);

			Sprite[] sprites = ReturnSpriteArray(cleanedPath);

			string key = cleanedPath.Substring(37);
			key = key.Trim();

			Debug.Log(key);

			factionSprites.Add(key, sprites);
		
		}
	

	}

	//cleans filepath, removing "Assets/Resources/", "\", and ".meta" from the string. also trims whitespace
	string CleanString(string s){

		//removes "Assets/Resources/"
		string clean  = s.Substring(17);
		//removes ".meta"
		clean = clean.Remove(clean.Length - 5);
		//removes "\"
		string [] cleanedStrings = clean.Split ('\\');
		//addes all the cleaned strings and connects the path
		clean = cleanedStrings[0] + "/" + cleanedStrings[1];
		//trims whitespace on ends
		clean = clean.Trim ();

		return clean;
	
	}




	//reads path and creates a sprite array of the assets found
	Sprite[] ReturnSpriteArray(string path){

		Sprite[] sprites = Resources.LoadAll<Sprite>(path);

		return sprites;

	}


	void Start(){

		//string abc = "abcdefghijklm\nopqrstuvwxyz";
		//string[] abcCut = abc.Split('\\');

		//foreach (string word in abcCut) {
				
		//	Debug.Log(word);
		
		//}
	
		CreateSpriteDictionary (folderPath);
	

	}







}

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class SpriteLibrary : MonoBehaviour {

	//Stores sprites in arrays by faction.  Uses piece Designation to ID piece sprite type.
	


	void InstantiateSpriteArrays(){

		string spritePath = "Assets/Resources/Sprites/FeudalSprites/FactionSprites"; 

		string[] filePaths = Directory.GetFiles (spritePath);

		string[] factions; 

		foreach (string filePath in filePaths) {
				
			Debug.Log(filePath);


		
		
		}
	

	}



	void Start(){


		//InstantiateSpriteArrays ();
	
	}







}

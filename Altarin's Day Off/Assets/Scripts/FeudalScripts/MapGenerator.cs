using UnityEngine;
using System.Collections;
using System.IO;

public class MapGenerator : MonoBehaviour {


	public TextAsset map;
	string tileSet;
	public int mapSize;

	public GameObject normalTilePrefab;
	public f_Tile[] specialTiles; // holds tile types of special tiles to replace tiles with.


	

	void InstantiateTile(int x, int y, Vector3 position, GameObject prefab){

		GameObject tile;

		tile = Instantiate (prefab, position, Quaternion.identity) as GameObject;

		f_Tile t = tile.GetComponent<f_Tile> ();

		t.x = x;
		t.y = y;

	}

	//cleans the parameter and returns what comes after the colon.
	string ReadMapParameters(string param){

		param = param.Trim();
		string[] s = param.Split(':');
		return s[1].Trim();

	}

	//cleans the string and returns a list of corrdinates in (x,y) format
	string[] ReadSpecialTileCoordinates(string coor){
		//cleans and removes the title of the special tile a
		coor = coor.Trim();
		char[] delimiters = new char[] {'{' , '}',};
		string[] firstSplit = coor.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries);

		//takes the second entry, which contains all the coordinates and splits them into a new array
		firstSplit[1] = firstSplit[1].Trim();
		delimiters = new char[] {'\n'};
		string[] secondSplit = firstSplit[1].Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries);

		//cleans each element before returning the new coordinate array
		for (int i = 0; i < secondSplit.Length; i++){

			secondSplit[i] = secondSplit[i].Trim();

		}

		return secondSplit;

	}

	//takes in the text asset and produces readable variables for the map generator.
	void ReadMapText(TextAsset map){

		//hashtags designate the size, tile set, and tile coordinate lists into string sections
		string[] sections = map.text.Split( '#' );

		//takes first section (size and tile set) and creates map parameters
		sections[0] = sections[0].Trim();
		string[] mapParameters = sections[0].Split( '\n');

		//fills map size parameter
		string size = ReadMapParameters(mapParameters[0]);
		mapSize = int.Parse(size);

		//fills tile set parameter
		string set = ReadMapParameters(mapParameters[1]);
		tileSet = set;

		//fills the special tile coordinate sets 
		string[] marshCoordinates = ReadSpecialTileCoordinates(sections[1]);
		string[] mountainCoordinates = ReadSpecialTileCoordinates(sections[2]);


		//for debugging
		//Debug.Log(" '" + sections[1] + "' ");
		foreach (string s in mountainCoordinates){
			Debug.Log(s);
		}


	}

	//generates the map by reading the map file
	void GenerateMap(int size){

		Vector3 tilePos = new Vector3 (0, 0, 0);


		for(int y = 0; y < size; y++){

			tilePos.y = y * 1.06f;

			for(int x = 0; x < size; x++){

				tilePos.x = x * 1.06f;
				bool isSpecialUsed = false;

				for(int i = 0; i < specialTiles.Length; i++){

					if(specialTiles[i].x == x && specialTiles[i].y == y){

						specialTiles[i].transform.position = tilePos;
						isSpecialUsed = true;
					
						break;
					}

					else{}
					
				}

				if(!isSpecialUsed){

					InstantiateTile(x, y, tilePos, normalTilePrefab);

				}

				else{}
			}
			
		}
	
	}


	void Awake () {
	

		//GenerateMap (mapSize);
		ReadMapText(map);

	}

}

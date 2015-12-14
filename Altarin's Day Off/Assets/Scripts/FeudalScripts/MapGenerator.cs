using UnityEngine;
using System.Collections;
using System.IO;

public class MapGenerator : MonoBehaviour {


	public TextAsset map; //hold .txt of the map to be converted into usable information
	string tileSet; //holds type of sprite set 
	int mapSize; // square map.  area = mapSize^2

	//prefabs
	GameObject normalTilePrefab;
	GameObject marshTilePrefab;
	GameObject mountainTilePrefab;

	//arrays of all tiles, marsh tiles, and mountain tiles.
	f_Tile[,] tiles;
	Coordinate[] marshTiles;
	Coordinate[] mountainTiles;

	//public f_Tile[] specialTiles; // holds coordinates of special tiles to replace tiles with.

	#region Get Methods
	//get methods for private members that may need public access
	public int GetMapSize(){

		return mapSize;

	}

	public f_Tile[,] GetTiles(){

		return tiles;

	}

	public string GetTileSet(){

		return tileSet;

	}
	#endregion

	#region Coordinate class & methods
	//object that holds coordinates in (x,y) fashion to store specific coordinates.
	 class Coordinate{

		public int x;
		public int y;

		public Coordinate(int xCoordinate, int yCoordinate){
			
			x = xCoordinate;
			y = yCoordinate;

		}

	}

	//compares two sets of x,y coordinates, returning true if they are the same, false otherwise. 
	//overload for individual x & y inputs
	bool CompareCoordinates(int x1, int y1, int x2, int y2){

		//are x and y of each set the same?
		if(x1 == x2 && y1 == y2){
			
			return true;
			
		}
		
		else{
			
			return false;
			
		}
		
	}

	//compares two sets of x,y coordinates, returning true if they are the same, false otherwise. 
	//overload for individual x & y input and coordinate input
	bool CompareCoordinates(int x, int y, Coordinate coordinate){

		//are x and y of each set the same?
		if(x == coordinate.x && y == coordinate.y){
			
			return true;
			
		}
		
		else{
			
			return false;
			
		}
		
	}

	//compares two sets of x,y coordinates, returning true if they are the same, false otherwise. 
	//overload for coordinate inputs
	bool CompareCoordinates(Coordinate coordinate1, Coordinate coordinate2){

		//are x and y of each set the same?
		if(coordinate1.x == coordinate2.x && coordinate1.y == coordinate2.y){
			
			return true;
			
		}
		
		else{
			
			return false;
			
		}
		
	}
	#endregion

	#region Conversion and Reading Methods
	//takes "(x,y)" string format and converts it to a coorinate class 
	Coordinate ConvertToCoordinate(string coordinate){

		//cleans and splits the string version of coordinate into string x and y values
		coordinate = coordinate.Trim();
		char[] delimiters = new char[] {'(' , ',', ')'};
		string[] XandY  = coordinate.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries);

		//string x and string y
		string strX = XandY[0];
		string strY = XandY[1];

		//converts x and y string into integers
		int newX = int.Parse(strX);
		int newY = int.Parse(strY);

		//constructs new coordinate to be returned by fxn.
		Coordinate newCoordinate = new Coordinate(newX, newY);

		return newCoordinate;

	}

	Coordinate[] ConvertStringArrayToCoordinateArray(string[] s){

		Coordinate[] c = new Coordinate[s.Length];


		for(int i = 0; i < s.Length; i++){

			c[i] = ConvertToCoordinate(s[i]);

		}


		return c;


	}

	//cleans the parameter and returns what comes after the colon.
	string ConvertToMapParameter(string param){

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
		string size = ConvertToMapParameter(mapParameters[0]);
		mapSize = int.Parse(size);

		//fills tile set parameter
		string set = ConvertToMapParameter(mapParameters[1]);
		tileSet = set;

		//fills the special tile coordinate sets (special1 = marsh, special2 = mountain)
		string[] specialCoordinates1 = ReadSpecialTileCoordinates(sections[1]);
		string[] specialCoordinates2 = ReadSpecialTileCoordinates(sections[2]);
		marshTiles = ConvertStringArrayToCoordinateArray(specialCoordinates1);
		mountainTiles = ConvertStringArrayToCoordinateArray(specialCoordinates2);


	}
	#endregion

	#region Map Construction Methods

	void SetTilePrefabs(){

		string filePath = "Prefabs/FeudalPrefabs/Tiles";

		normalTilePrefab = Resources.Load<GameObject>(filePath + "/f_NormalTile");
		marshTilePrefab = Resources.Load<GameObject>(filePath + "/f_RoughTerrainTile");
		mountainTilePrefab = Resources.Load<GameObject>(filePath + "/f_MountainTile");

	}

	//checks input coordinates and sees if it matches with any of the marsh or mountain tile coordinates
	//also returns appropriate prefab as game object
	GameObject ReturnSpecialTile(int x, int y){

		//create new coordinate for ease of input. probably unnecessary
		Coordinate inputCoordinate = new Coordinate(x,y);

		//checks all marsh tiles by coordinate, and returns the marshTilePrefab should one exsist
		for(int i = 0; i < marshTiles.Length; i++){
			
			if(CompareCoordinates(inputCoordinate, marshTiles[i])){
				
				return marshTilePrefab;

			}

		}

		//checks all mountain tiles by coordinate, and returns the mountainTilePrefab should one exsist
		for(int i = 0; i < mountainTiles.Length; i++){
			
			if(CompareCoordinates(x, y, mountainTiles[i])){
				
				return mountainTilePrefab;
				
			}
			
		}

		//if no special tile is found on this coodinate, return a normal tile
		return normalTilePrefab;

	}

	void InstantiateTile(int x, int y, Vector3 position){
		//temp tile and tileprefab 
		GameObject tile;
		GameObject tilePrefab;

		//assigns appropriate prefab should the method return a special tile
		tilePrefab = ReturnSpecialTile(x, y);

		tile = Instantiate (tilePrefab, position, Quaternion.identity) as GameObject;
		
		f_Tile t = tile.GetComponent<f_Tile> ();
		
		t.x = x;
		t.y = y;

		tiles[x,y] = t;
		
	}

	//generates the map by reading the map file
	public void GenerateMap(){

		//set up methods
		SetTilePrefabs();
		ReadMapText(map);

		//size of map.  too lazy to change size to mapSize
		int size = mapSize;

		//assigns length of tiles array based on map size.
		tiles = new f_Tile[size,size];
		Debug.Log (tiles.Length);

		Vector3 tilePos = new Vector3 (0, 0, 0);

		//goes through all the coordinates and instantiates tiles
		//also adds them to tiles[,].
		for(int y = 0; y < size; y++){

			tilePos.y = y * 1.06f;

			for(int x = 0; x < size; x++){

				tilePos.x = x * 1.06f;

				InstantiateTile(x, y, tilePos);

			}
			
		}
	
	}
	#endregion


	void Awake () {

		//GenerateMap ();

	}

}

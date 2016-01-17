using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class f_SetUpManager : MonoBehaviour {

	public f_GameManager f_gameManager;
	public MapGenerator mapGenerator;
	public SpriteLibrary spriteLibrary;
	public GameObject emptyObject;
	public GameObject emptyTile; //used to hold pieces not on the board.  should have coordinates (-1,-1) and be untagged.
	public GameObject trayObject;
	public GameObject planningButtonObject;
	public GameObject utilityPanelObject;
	public GameObject bannerObject;
	GameObject screenObject;
	public Canvas canvas;

	Player[] players;
	Player myPlayer;
	Camera playerCamera;
	GameObject selectedObject;
	GameObject oldSelectedObject;
	Vector2 selectedObjectPosOld;

	int[] buttonIDs;

	GameObject lastCastleSelected;
	GameObject currentCastleBeingPlaced;
	//GameObject castleBlack;

	bool isPlacingPieces;
	bool isPlacingCastle;
	public bool isWhiteSetUp;
	public bool isSetUp = false;
	bool isOffline;




	//uses GUI elements to allow for the set up of the game
	//holds the ability to click and drag units onto tiles

	#region MouseControls
	void MouseControls(bool placingCastle){
		
		
		//initial left click selects the piece
		/*if (Input.GetMouseButtonDown (0)) {
			if(isPlacingCastle){
				
				SelectCastle(isWhiteSetUp);
				
			}
			
			else{
				
				SelectPiece(isWhiteSetUp);
			}
			
			
		}*/
		//when held, the object is draggable
		/*if(Input.GetMouseButton(0)){
			
			ClickandDrag(selectedObject);
			
		}*/
		
		//initial left click selects the piece 
		//drops piece onto tile or back to its original position and clears variables if
		//selected object is not empty.
		if(Input.GetMouseButtonDown(0)){

			if(selectedObject == emptyObject){

				if(isPlacingCastle){
					
					SelectCastle(isWhiteSetUp);
					
				}
				
				else{
					
					SelectPiece(isWhiteSetUp);

				}

			}

			else{

				if(isPlacingCastle){
					
					DropCastle();
					
				}
				
				else{
						
					DropPiece();

				}

			}

		}

		MoveSelectedObjectToCorrectSortingLayer();
		
	}

	void ClickandDrag(GameObject g){

		if (g != null) {
				
				g.transform.position = MousePosition();

		}

		else {
			//pass
		}

	}

	Vector3 MousePosition(){

		Vector3 v3Pos = playerCamera.ScreenToWorldPoint(Input.mousePosition);
		Vector3 mousePos = new Vector3(v3Pos.x, v3Pos.y, -10.0f);
		return mousePos;

	}

	void MoveSelectedObjectToCorrectSortingLayer(){

		if(selectedObject == emptyObject && oldSelectedObject != emptyObject){

			SpriteRenderer r = oldSelectedObject.GetComponent<SpriteRenderer>();

			if(oldSelectedObject.tag == "f_Tile"){

				r.sortingLayerID = 3;

				oldSelectedObject.GetComponent<f_Castle>().castleGreens.GetComponent<SpriteRenderer>().sortingLayerID = 4;

			}

			else if(oldSelectedObject.tag == "f_Piece"){

				r.sortingLayerID = 2;

			}

			else{}


		}


		else{

			SpriteRenderer r = selectedObject.GetComponent<SpriteRenderer>();
			r.sortingLayerID = 7; //places object in selectedObject layer
			
			if(selectedObject.GetComponent<f_Castle>() != null){
				
				f_Castle c = selectedObject.GetComponent<f_Castle>();
				
				c.castleGreens.GetComponent<SpriteRenderer>().sortingLayerID = 7;
				
			}

		}
		
		oldSelectedObject = selectedObject;

	}

	void SelectCastle(bool isWhitePlacing){

		int layerMask = 1 << LayerMask.NameToLayer("Castle");
		RaycastHit2D hit = Physics2D.Raycast(MousePosition(), Vector2.right, 0.01f, layerMask);
		
		if(hit.collider != null && hit.collider.tag == "f_Tile"){
			f_Castle c = hit.collider.gameObject.GetComponent<f_Castle>();


			if(c.isWhite == isWhitePlacing){

				selectedObjectPosOld = hit.collider.gameObject.transform.position;
				selectedObject = hit.collider.gameObject;
				lastCastleSelected = selectedObject;
				c.occupiedTile.isOccupied = false;
				c.occupiedTile = null;
				Debug.Log(selectedObject + " is selected.");

			}

			else{}


		}
	}

	void DropCastle(){

		int layerMask = 1 << LayerMask.NameToLayer("Tile");
		RaycastHit2D hit = Physics2D.Raycast (MousePosition(), Vector3.right, 0.01f, layerMask);
		
		if(hit.collider != null && hit.collider.tag == "f_ScreenObject"){
			
			Debug.Log("screenobject");
			f_Tile t = selectedObject.GetComponent<f_Tile>();
			if(t != null){

				//f_Castle c = selectedObject.GetComponent<f_Castle>();

				selectedObject.transform.position = selectedObjectPosOld;
				selectedObjectPosOld = Vector2.zero;
				selectedObject = emptyObject;
			
				
			}
			
		}
		
		else if (hit.collider != null && hit.collider.tag == "f_Tile") {

			f_Tile t = hit.collider.GetComponent<f_Tile>();

			if(selectedObject != emptyObject){

				f_Castle c = selectedObject.GetComponent<f_Castle>();

				/*UI_Element castleUIElement = selectedObject.GetComponent<UI_Element>();
				UI_Element greensUIElement = c.castleGreens.GetComponent<UI_Element>();
				castleUIElement.enabled = false;
				greensUIElement.enabled = false;*/ //<--still need?


				c.gameObject.transform.localScale = Vector3.one;
				c.castleGreens.gameObject.transform.localScale = Vector3.one;



				c.transform.position = t.transform.position;
				c.castleGreens.transform.position = t.transform.position;
				c.x = t.x;
				c.y = t.y;
				c.occupiedTile = t;
				t.isOccupied = true;
			

				selectedObjectPosOld = Vector2.zero;
				selectedObject = emptyObject;

			}

			else{}


		}

		else{
			
			selectedObject.transform.position = selectedObjectPosOld;
			selectedObjectPosOld = Vector2.zero;
			selectedObject = emptyObject;

		}


	}

	void SelectPiece(bool isWhitePlacing){

	
		int layerMask = 1 << LayerMask.NameToLayer("Piece");
		RaycastHit2D hit = Physics2D.Raycast(MousePosition(), Vector2.right, 0.01f, layerMask);
		

		if(hit.collider != null && hit.collider.tag == "f_Piece"){
			f_Piece p = hit.collider.gameObject.GetComponent<f_Piece>();
			if(p.isWhite == isWhitePlacing){

				if(p.occupiedTile != null){

					/*UI_Element pieceUIelement = p.GetComponent<UI_Element>();
					p.transform.localScale = currentLocalScale;
					pieceUIelement.enabled = true;
					pieceUIelement.SetUpElement();*/ //<----is this still needed?

					p.occupiedTile.isOccupied = false;
					p.lastOccupiedTile = p.occupiedTile;
					p.occupiedTile = null;

				}

				else{}

				selectedObjectPosOld = hit.collider.gameObject.transform.position;
				selectedObject = hit.collider.gameObject;
				Debug.Log(selectedObject + " is selected.");

				SpriteRenderer r = selectedObject.GetComponent<SpriteRenderer>();
				r.sortingLayerID = 7; //places object in selectedObject layer


				hit = Physics2D.Raycast (MousePosition(), Vector3.right, 0.01f, layerMask);
				

			}


			else{}
		}
	
	
	}

	void DropPiece(){

		//check if dropping on castle
		int layerMask = 1 << LayerMask.NameToLayer("Castle");
		RaycastHit2D hit = Physics2D.Raycast (MousePosition(), Vector3.right, 0.01f, layerMask);

		
		if (hit.collider != null && hit.collider.tag == "f_Tile") {
			
			f_Tile t = hit.collider.GetComponent<f_Tile>();
			//Debug.Log(t);
			f_Piece p = selectedObject.GetComponent<f_Piece>();
			//UI_Element pieceUIElement = selectedObject.GetComponent<UI_Element>(); still need?
			
			if(selectedObject != emptyObject && isValidPiecePlacement(selectedObject, t)){
				
				//pieceUIElement.enabled = false; //still needed?
				selectedObject.transform.localScale = Vector3.one;
				SpriteRenderer r = selectedObject.GetComponent<SpriteRenderer>();
				r.sortingLayerID = 2; //changes layer back to piece layer
				
				p.transform.position = t.transform.position;
				p.x = t.x;
				p.y = t.y;
				p.occupiedTile = t;
				t.isOccupied = true;
				selectedObjectPosOld = Vector2.zero;
				selectedObject = emptyObject;
				
			}
			
			
			else{

				ReturnToLastOccupiedTile(selectedObject);

				//selectedObject.transform.position = selectedObjectPosOld;
				//selectedObjectPosOld = Vector2.zero;

				//p.transform.position = p.lastOccupiedTile.transform.position;
				//p.occupiedTile = p.lastOccupiedTile;
				//selectedObject = emptyObject;
				
			}

		}

		//check if placing on tile
		else{

			
			layerMask = 1 << LayerMask.NameToLayer("Tile");
			hit = Physics2D.Raycast (MousePosition(), Vector3.right, 0.01f, layerMask);
			

			if(hit.collider != null && hit.collider.tag == "f_ScreenObject"){

				Debug.Log("screenobject");
				ReturnToLastOccupiedTile(selectedObject);
				/*f_Piece p = selectedObject.GetComponent<f_Piece>();
				if(p != null){
					
					p.transform.position = p.lastOccupiedTile.transform.position;
					p.occupiedTile = p.lastOccupiedTile;
					selectedObject = emptyObject;
					
				}*/
				
			}





			else if (hit.collider != null && hit.collider.tag == "f_Tile") {

				Debug.Log("tile");
				f_Tile t = hit.collider.GetComponent<f_Tile>();
				//Debug.Log(t);
				f_Piece p = selectedObject.GetComponent<f_Piece>();
				//UI_Element pieceUIElement = selectedObject.GetComponent<UI_Element>(); no longer needed?
				if(selectedObject != emptyObject && isValidPiecePlacement(selectedObject, t)){
					
					//pieceUIElement.enabled = false; //no longer needed?
					selectedObject.transform.localScale = Vector3.one;
					SpriteRenderer r = selectedObject.GetComponent<SpriteRenderer>();
					r.sortingLayerID = 2; //changes layer back to piece layer

					
					p.transform.position = t.transform.position;
					p.x = t.x;
					p.y = t.y;
					p.occupiedTile = t;
					t.isOccupied = true;
					selectedObjectPosOld = Vector2.zero;
					selectedObject = emptyObject;
					
				}
				
				
				else{

					ReturnToLastOccupiedTile(selectedObject);


					//if(p != null){

						//selectedObject.transform.position = selectedObjectPosOld;
						//selectedObjectPosOld = Vector2.zero;
						
						//p.transform.position = p.lastOccupiedTile.transform.position;
						//p.occupiedTile = p.lastOccupiedTile;
						//selectedObject = emptyObject;


					//}
					

					
				}
				
				
		
				
			}
			
			
			
			/*else if(hit.collider != null && hit.collider.tag == "f_TrayObject"){
				if(selectedObject != emptyObject){
					
					//for(int i = 0; i < slots.Length; i++){
						
						//if(!slots[i].isOccupied){
							
					f_Piece p = selectedObject.GetComponent<f_Piece>();
					UI_Element pieceUIElement = selectedObject.GetComponent<UI_Element>();
					//f_Tile t = slots[i];

					f_Tile t = nextAvailableTraySlot;

					UI_Element slotUIelement = t.GetComponent<UI_Element>();

					p.transform.localScale = currentLocalScale;

					pieceUIElement.enabled = true;
					pieceUIElement.xRatio = slotUIelement.xRatio;
					pieceUIElement.yRatio = slotUIelement.yRatio;
					pieceUIElement.SetUpElement();

					
					p.transform.position = t.transform.position;
					p.x = t.x;
					p.y = t.y;
					p.occupiedTile = t;
					t.isOccupied = true;
					selectedObjectPosOld = Vector2.zero;
					selectedObject = emptyObject;
					//break;
							
					//	}
						
						//else{
						//	//pass
					//	}
						
						
						
					//}
					
				
				}
				
				else{
					//pass
				}
				
			}*/


			else{

				ReturnToLastOccupiedTile(selectedObject);
				//selectedObject.transform.position = selectedObjectPosOld;
				//selectedObjectPosOld = Vector2.zero;
				/*f_Piece p = selectedObject.GetComponent<f_Piece>();
				if(p != null){


					p.transform.position = p.lastOccupiedTile.transform.position;
					p.occupiedTile = p.lastOccupiedTile;
					selectedObject = emptyObject;

				}*/
		
				
			}

		}


	}
	#endregion

	#region PiecePlacement 
	void ReturnToLastOccupiedTile(GameObject piece){

		if (piece.tag == "f_Piece") {
				
			Debug.Log("ReturnToLastOccupiedTile");

			f_Piece p = piece.GetComponent<f_Piece>();

			if(p != null){

				//if the last occupied tile returns a tile on the game board.
				if(p.lastOccupiedTile.tag == "f_Tile"){

					/*UI_Element pieceUIElement = p.GetComponent<UI_Element>();

					pieceUIElement.enabled = false;*/ //<--still necessary?
					selectedObject.transform.localScale = Vector3.one;

					p.transform.position = p.lastOccupiedTile.transform.position;
					p.occupiedTile = p.lastOccupiedTile;
					selectedObject = emptyObject;



				}

				//if the last occupied tile is untagged (a tray slot tile).
				else{
					/*
					UI_Element pieceUIElement = selectedObject.GetComponent<UI_Element>();
					//f_Tile t = slots[i];
					
					f_Tile t = nextAvailableTraySlot;
					
					UI_Element slotUIelement = t.GetComponent<UI_Element>();
					
					p.transform.localScale = currentLocalScale;
					
					pieceUIElement.enabled = true;
					pieceUIElement.xRatio = slotUIelement.xRatio;
					pieceUIElement.yRatio = slotUIelement.yRatio;
					pieceUIElement.SetUpElement();
					
					
					p.transform.position = t.transform.position;
					p.x = t.x;
					p.y = t.y;
					p.occupiedTile = t;
					t.isOccupied = true;
					selectedObjectPosOld = Vector2.zero;
					selectedObject = emptyObject;
					//break;*/ //<--is any of this still necessary?

				}


				

				
			}
		
		
		
		}

	
	}

	bool isValidPiecePlacement(GameObject g, f_Tile t){

		f_Piece p = g.GetComponent<f_Piece>();

		if(t.isOccupied == true){

			Debug.Log("This is not a valid piece placement: Cannot place on top of other pieces");
			return false;

		}



		// if player tries to place unit on a mountain
		else if(t.tileType == 3){

			Debug.Log("This is not a valid piece placement: Cannot Place Units on Mountains");
			return false;

		}

		//if player tries to place a mounted unit on rough terrain
		else if((p.pieceDesignator >= 2 && p.pieceDesignator <= 4) || (p.pieceDesignator >= 10 && p.pieceDesignator <= 12)){
			if(t.tileType == 2){

				Debug.Log("This is not a valid piece placement: Cannot place mounted units on Rough Terrain");
				return false;

			}

			else{

				return true;

			}


		}

		//if player tries to place an archer on his own castle
		else if(p.pieceDesignator == 5 || p.pieceDesignator == 13){
			//Debug.Log("hi");
			//Debug.Log(t);

			int layerMask = 1 << LayerMask.NameToLayer("Castle");
			RaycastHit2D hit = Physics2D.Raycast (MousePosition(), Vector3.right, 0.01f, layerMask);


			if(hit.collider != null){
				f_Castle c = hit.collider.GetComponent<f_Castle>();

				Debug.Log("hello");

				if(p.isWhite == c.isWhite){
					Debug.Log("This is not a valid piece placement: Archer cannot be placed in Friendly Castle Walls");
					return false;
					
				}
				
				else{
					return true;
				}
					
			}

			else {
				return true;
			}

		}

		else{
			return true;
		}


	}

	bool isValidCastlePlacement(GameObject selectedCastle){
		
		f_Castle c = selectedCastle.GetComponent<f_Castle> ();
		
		f_Tile t = c.castleGreens;
		
		//holds length of board and needs definition outside of this fxn
		int boardLength = 23;
		
		//if greens are out of bounds return false
		if (t.x < 0 || t.y < 0 || t.x > boardLength || t.y > boardLength) {
			
			Debug.Log("This is not valid castle placement:  Castle Greens are out of bounds");
			return false;
			
		}
		
		else return true;
		
	}

	void RotateCastle(f_Castle c){


		int r = c.rotation + 1;


		if(r >= 4){

			c.rotation = 0;
			Debug.Log(c.rotation);


		}


		else{

			c.rotation++;
			Debug.Log(c.rotation);

		}


		if(c.rotation == 0){

			//Vector3 v = new Vector3.right * 0;
			c.gameObject.transform.Rotate(Vector3.forward * 90f);
			Debug.Log("Vector.rotate = " + Vector3.zero);

		}

		else if(c.rotation == 1){

			//Vector3 v = new Vector3.right * 90;
			//c.gameObject.transform.Rotate(Vector3.forward);
			c.gameObject.transform.Rotate(Vector3.forward * 90f);
			Debug.Log("Vector.rotate = " + Vector3.forward * 90f);
			
		}

		else if(c.rotation == 2){

			//Vector3 v = new Vector3.right * 180;
			//c.gameObject.transform.Rotate(Vector3.zero);
			c.gameObject.transform.Rotate(Vector3.forward * 90f);
			Debug.Log("Vector.rotate = " + Vector3.forward * 180f);
			
		}

		else if(c.rotation == 3){

			//Vector3 v = new Vector3.right * 270;
			//c.gameObject.transform.Rotate(Vector3.zero);
			c.gameObject.transform.Rotate(Vector3.forward * 90f);
			Debug.Log("Vector.rotate = " + (Vector3.forward * 270f));
		}


		else{}

		//Debug.Log(c.rotation);



	}

	#endregion

	#region Tray & Tray Buttons

	//returns piece/castle based on the button.  used by each individual button's onclick().  
	//buttons should also have the parameter for this method set via inspector.
	public void GetSelectedObjectFromButton(int buttonID){

		int id = buttonIDs[buttonID];
		Debug.Log(id);
		//if placing castle, and id = castle, make that selected object.
		if((id - ((myPlayer.pieceSet.Length + 1) * (myPlayer.playerNumber - 1))) > myPlayer.pieceSet.Length && isPlacingCastle){

			selectedObject = myPlayer.castle.gameObject;
			SpriteRenderer r = selectedObject.GetComponent<SpriteRenderer>();
			r.sortingLayerID = 7;
			lastCastleSelected = selectedObject;
			Debug.Log(selectedObject);

		}

		else if(!isPlacingCastle){

			for(int i = 0; i < myPlayer.pieceSet.Length; i++){
				
				if(myPlayer.pieceSet[i].pieceID == id){
					
					//deals with issue of pieces being returned to player's hand without having to be
					//selected.
					if(myPlayer.pieceSet[i].occupiedTile != null){
						
						myPlayer.pieceSet[i].occupiedTile.isOccupied = false;
						
					}
					
					else{}

					selectedObject = myPlayer.pieceSet[i].gameObject;
					SpriteRenderer r = selectedObject.GetComponent<SpriteRenderer>();
					r.sortingLayerID = 7;
					Debug.Log(selectedObject);
					break;
					
				}
				
				else{}
				
			}

		}

		else{}
		
	}

	//Disables trayobject that resides in the canvas
	void DestroyTray(){

		trayObject.SetActive(false);
	
	}

	//uses the setup manager's empty tile to determine if pieces are still occupying it.
	bool IsTrayClear(f_Piece[] playerPieceSet){

		for (int i = 0; i < playerPieceSet.Length; i++) {

			f_Tile t = playerPieceSet[i].occupiedTile;

			//empty tile should have an (x,y) coordinate that is negative (eg -1,-1).
			//this will designate that the tile is not on the map and therefore the tray is not clear.
			if(t.x < 0 && t.y < 0){
				
				return false;

			}

		}
		
		return true;
	
	}
	#endregion

	#region UI Button Methods
	//button method that confirms piece placement and marks the player as ready to move onto the 
	//game/playing phase.
	public void FinishPlanning(){
	
		if(IsTrayClear(myPlayer.pieceSet)){
			//destroys the tray, last button used, and sets member variables 
			DestroyTray();
			planningButtonObject.SetActive(false);
			Destroy(screenObject);
			f_Castle c = lastCastleSelected.GetComponent<f_Castle>();
			//isSetUp = false;
			c.isSetup = false;
			myPlayer.isReady = true;
			isPlacingPieces = false;

		}

		else{

			Debug.Log("All pieces must be placed.");

		}
	
	}

	//button method that confirms that castle has valid placement and moves the player into the piece placing phase.
	public void ConfirmCastlePlacement(){

		//checks if the castle placement is within the bounds of the board before moving on
		if(isValidCastlePlacement(lastCastleSelected)){
			
			f_Castle c = lastCastleSelected.GetComponent<f_Castle>();
			c.occupiedTile.isOccupied = false;
			c.occupiedTile = null;
			
			isPlacingCastle = false;

		}
		
		else{}
	
	}

	public void RotateMyPlayerCastle(){

		RotateCastle(myPlayer.castle);

	}
	
	
	
	
	
	
	
	
	
	#endregion
	
	#region SetUpMethods
	
	#region Player and Network Methods
	//finds network manager and syncronizes offline/online status between managers
	void FindNetworkManager(){
		
		NetworkManager networkManager = FindObjectOfType<NetworkManager> ();
		
		if (networkManager != null) {
			
			Debug.Log("Network Manager Found");
			isOffline = networkManager.isOffline;
			f_gameManager.isOffline = networkManager.isOffline;
			
			
		}
		
		else{
			
			Debug.Log("Network Manager Not Found");
			
		}
		
	}

	//whether game is online or offline, find all instantiated players from network manager, create
	//players array and assign myPlayer
	void SetPlayers(){

		players = FindObjectsOfType<Player> ();

		//looks through players array and finds myPlayer
		Debug.Log ("player list length " + players.Length);
		for (int i = 0; i < players.Length; i++) {
			Debug.Log("Looking for MyPlayer");
			if(players[i].isMyPlayer){
				
				myPlayer = players[i];
				Debug.Log("MyPlayer found.");

			}

			Debug.Log("Player " + players[i].playerNumber);
	
		}

		//sends my player to game manager
		f_gameManager.myPlayer = myPlayer;

		//assign's canvas' camera to player's camera.
		canvas.worldCamera = myPlayer.GetComponent<Camera>();

		//enables player camera w/ controls
		playerCamera = myPlayer.gameObject.GetComponent<Camera> ();
		CameraController c = myPlayer.GetComponent<CameraController>();
		c.enabled = true;

			//enables audio for my player only
		//myPlayer.SetUpAudio();


	}
	#endregion

	#region Piece Instantiation Methods

	//returns an array of prefabs loaded from the resources folder.
	//prefabs in this folder require a specific order for proper instantiation later
	GameObject[] ReturnPiecePrefabsArray(){

		string filePath = "Prefabs/FeudalPrefabs/Pieces";

		return Resources.LoadAll<GameObject>(filePath);

	}

	//instantiates, sets variables, assigns sprite, and returns the finished product as a f_piece.
	f_Piece ReturnInstantiatedPiece(bool isWhite, f_Piece.Faction fctn, int designator, GameObject prefab){

		//temp game object for instantiation
		GameObject g = Instantiate(prefab) as GameObject;

		//sprite assignment using faction (as a string) as the key for the dictionary
		SpriteRenderer pieceSprite = g.GetComponent<SpriteRenderer>();
		string key = fctn.ToString();
		key = key.Trim();
		pieceSprite.sprite = spriteLibrary.GetSprite(key, designator);

		//adjusts the pieceDesignator if the piece is not white.
		//white pieces keep the original designator.
		if(!isWhite){
			
			designator = (designator + 8);
			
		}

		//sets variables and returns the final piece.
		f_Piece p = g.GetComponent<f_Piece>();
		p.SetVariables(isWhite, fctn, designator, emptyTile);
		p.lastOccupiedTile = emptyTile.GetComponent<f_Tile>();
		
		return p;


	}

	//takes the player, returning a full 13-piece set for that player.
	// called in initiateSetup.
	f_Piece[] ReturnPieceSet(Player player){

		bool isWhite = player.isWhite;
		f_Piece.Faction faction = player.faction;

		//holds the prefabs retreived from the resources folder.
		GameObject[] piecePrefabs = ReturnPiecePrefabsArray();

		//set to be returned at the end of the fxn.
		f_Piece[] pieceSet = new f_Piece[13];

		//holds the int that designates the correct prefab from piecePrefabs
		int pieceDesignator = 0;
	
		//instantiate (01king, 02prince, 03duke) x 1
		for(int i = 0; i < 3; i ++){

			pieceDesignator = i + 1;

			pieceSet[i] = ReturnInstantiatedPiece(isWhite, faction, pieceDesignator, piecePrefabs[pieceDesignator]);

		}

		//instantiate 04knight x 2
		for(int i = 3; i < 5; i++){

			pieceDesignator = 4;

			pieceSet[i] = ReturnInstantiatedPiece(isWhite, faction, pieceDesignator, piecePrefabs[pieceDesignator]);

		}

		//instantiate 05mage x 1
		pieceDesignator = 5;
		pieceSet[5] = ReturnInstantiatedPiece(isWhite, faction, pieceDesignator, piecePrefabs[pieceDesignator]);

		//instantiate 06sergeant x 2
		for(int i = 6; i < 8; i++){
			
			pieceDesignator = 6;
			
			pieceSet[i] = ReturnInstantiatedPiece(isWhite, faction, pieceDesignator, piecePrefabs[pieceDesignator]);
			
		}

		//instantiate 07pikeman x 4
		for(int i = 8; i < 12; i++){
			
			pieceDesignator = 7;
			
			pieceSet[i] = ReturnInstantiatedPiece(isWhite, faction, pieceDesignator, piecePrefabs[pieceDesignator]);
			
		}

		//instantiate 08squire x 1
		pieceDesignator = 8;
		pieceSet[12] = ReturnInstantiatedPiece(isWhite, faction, pieceDesignator, piecePrefabs[pieceDesignator]);



		return pieceSet;


	}

	//takes a player's piece set and give them a unique set of numbers for each of their pieces.
	//to be used in game manager. should only be used for myPlayer, as the other player will pass their 
	//ID's later.. called in initiateSetup.
	void SetPlayerPieceIDs(Player player){

		f_Piece[] pieces = player.pieceSet;

		for(int i = 0; i < pieces.Length; i++){

			//assigns ID by adding the lenght of the pieces x the player's number to generate a 
			//number unique to that piece.  length has an added + 1 to accomidate for an additional ID 
			//for castle identification in the getselectedobject method.  
			pieces[i].pieceID = ((i + 1) + ((player.playerNumber - 1) * (pieces.Length + 1)));

		}
		
	}

	#endregion

	#region Castle Instantion Methods

	//returns an array of prefabs loaded from the resources folder.
	//prefabs in this folder require a specific order for proper instantiation later
	GameObject ReturnACastleTilePrefab(string prefabName){

		string filePath = "Prefabs/FeudalPrefabs/Tiles/";

		GameObject prefab = Resources.Load<GameObject>(filePath + prefabName);

		return prefab;

	}

	//instantiates castle greens and castle, and assigns them to the player
	//also sets up castle and assigns 
	void InstantiateCastleTiles(Player player){

		GameObject castleObject = Instantiate(ReturnACastleTilePrefab("Castle")) as GameObject;
		GameObject castleGreensObject = Instantiate(ReturnACastleTilePrefab("CastleGreens")) as GameObject;

		player.castle = castleObject.GetComponent<f_Castle>();
		player.castleGreens = castleGreensObject.GetComponent<f_Tile>();

		player.castle.SetUpCastle(player, spriteLibrary, player.castleGreens, emptyTile);

	}




	#endregion

	#region ScreenObject Methods

	//retrieves prefab from resources folder and returns it as an instantiated game object.
	GameObject ReturnScreenObject(){

		string filePath = "Prefabs/FeudalPrefabs/Screen";

		GameObject screenPrefab = Resources.Load<GameObject>(filePath);

		GameObject screen = Instantiate(screenPrefab) as GameObject;

		return screen;
	
	}

	//for setting up screen
	f_Tile FindAnchorTile(int x, int y){
		
		f_Tile anchorTile = null;
		GameObject[] tiles = GameObject.FindGameObjectsWithTag ("f_Tile");
		
		for (int i = 0; i < tiles.Length; i++) {
			
			f_Tile tile = tiles[i].GetComponent<f_Tile>();
			
			if(tile.x == x && tile.y == y){
				
				anchorTile = tile;
				break;
			}
			
			
		}
		
		return anchorTile;
		
		
	}

	//set up screen 
	void SetUpScreen(){

		screenObject = ReturnScreenObject();

		if (screenObject != null) {
			
			ScreenBehavior screen = screenObject.GetComponent<ScreenBehavior>();
		
			//determines y coordinate of anchor tile.  x coordinate will always be 11 regardless of army side.
			int xAnchor = 11;
			int yAnchor;

			//(11,11)
			if(myPlayer.isWhite){

				yAnchor = 11;

			}
			//(11,12)
			else{
				
				yAnchor = 12;
				
			}

			//passes members to screen method.  sprite ID 10 denotes screen sprite.
			screen.SetUpScreen(FindAnchorTile(xAnchor, yAnchor), myPlayer.isWhite, 
			                   spriteLibrary.GetSprite(myPlayer.faction.ToString(), 10));
			
		}
		
	}
	#endregion

	public void InitiateSetup(){

		Debug.Log ("Initiateing Setup");

		FindNetworkManager();

		SetPlayers();
		
		//pieces and castle tiles instantiated and assigned here for each player. 
		for(int i = 0; i < players.Length; i++){

			players[i].pieceSet = ReturnPieceSet(players[i]);
			InstantiateCastleTiles(players[i]);
			SetPlayerPieceIDs(players[i]);

		}
		//where pieceID's are assigned to myplayer's pieceset.
		//SetPlayerPieceIDs(myPlayer);


		mapGenerator.GenerateMap();

		SetUpScreen();

		//set UI colors and sprites.
		Image bannerImage = bannerObject.GetComponent<Image>();
		bannerImage.sprite = spriteLibrary.GetSprite(myPlayer.faction.ToString(), 9); //9 is the banner's sprite designator.
		UtilityPanelBehaviour panel = utilityPanelObject.GetComponent<UtilityPanelBehaviour>();
		panel.SetPanelColor(myPlayer.faction);
		TrayBehaviour tray = trayObject.GetComponent<TrayBehaviour>();
		tray.SetUpTray(myPlayer, spriteLibrary);
		buttonIDs = tray.GetButtonIDs(); //passes the list of IDs coupled to the buttons for use in set up manager
		
		if (myPlayer.isWhite) {
			
			isWhiteSetUp = true;
			
		}
		
		else{
			
			isWhiteSetUp = false;
			
		}
	
		isSetUp = true; //for GUI
		isPlacingPieces = true;
		isPlacingCastle = true;
	

		//trayObject.SetActive(true);
		//isWhiteSetUp = true;
		//selectedObject = emptyObject;
		//CreateTray(trayObject);
		//FillTray();
		
	
	
	}

	void ArePlayersReady(){

		int[] playersReady = new int[players.Length];
		
		for (int i = 0; i < players.Length; i++) {
			
			if(players[i].isReady){
				
				playersReady[i] = 1;
				
			}
			
		}

		int readyTotal = 0;

		for (int i = 0; i < playersReady.Length; i++) {
			
			readyTotal += playersReady[i];
			
		}

		//Debug.Log ("Ready Total: " + readyTotal);

		if (readyTotal == players.Length) {

			//myPlayer.isReady = false;

			isSetUp = false;


			//StartCoroutine("GameManagerSetUp");

			f_gameManager.myPlayer = myPlayer;
			f_gameManager.players = players;

			/*for(int i = 0; i < players.Length; i++){

				if(players[i].isWhite){

					f_gameManager.whitePieces = players[i].pieceSet;
					Debug.Log("whitepieces");

				}

				else if(!players[i].isWhite){

					f_gameManager.blackPieces = players[i].pieceSet;
					Debug.Log("blackpieces");
				}

			}*/
			//f_gameManager.gameOn = true;
			f_gameManager.SetUpBoard();
			
		}
		
	}


	IEnumerator GameManagerSetUp(){

	
		if (myPlayer.isWhite) {
			Debug.Log("Starting GameManager SetUp");
			f_gameManager.myPlayer = myPlayer;
			f_gameManager.gameOn = true;
			f_gameManager.SetUpBoard();
			yield return null;
		
		}

		else{

			Debug.Log("Waiting for other players.");
			yield return new WaitForSeconds(2f);
			Debug.Log("Starting GameManager SetUp");
			f_gameManager.myPlayer = myPlayer;
			f_gameManager.gameOn = true;
			f_gameManager.SetUpBoard();
			yield return null;

		}
	
	
	
	}

	#endregion

	void Update () {

		if(isSetUp){

			if(isPlacingPieces){

				MouseControls (isPlacingCastle);
			
			}


			if(!isOffline){

				ArePlayersReady();

			}
		
		}
		

		ClickandDrag (selectedObject);
	
	}

}

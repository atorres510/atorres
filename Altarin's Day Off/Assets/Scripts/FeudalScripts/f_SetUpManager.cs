using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class f_SetUpManager : MonoBehaviour {

	public f_GameManager f_gameManager;
	public GameObject emptyObject;
	public GameObject emptyTile;
	public GameObject trayObject;

	GameObject selectedObject;
	Vector2 selectedObjectPosOld;

	GameObject lastCastleSelected;
	GameObject currentCastleBeingPlaced;
	//GameObject castleBlack;

	bool isPlacingCastle;
	public bool isWhiteSetUp;
	bool isSetUp;





	//uses GUI elements to allow for the set up of the game
	//holds the ability to click and drag units onto tiles



	void ClickandDrag(GameObject g){

		if (g != null) {
				
				//Vector3 v3Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				///Vector2 mousePos = new Vector2(v3Pos.x, v3Pos.y);
				g.transform.position = MousePosition();
				

		}

		else {
			//pass
		}

	}

	Vector2 MousePosition(){

		Vector3 v3Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mousePos = new Vector2(v3Pos.x, v3Pos.y);
		return mousePos;

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
		
		
		if (hit.collider != null && hit.collider.tag == "f_Tile") {


			f_Tile t = hit.collider.GetComponent<f_Tile>();

			if(selectedObject != emptyObject){

				f_Castle c = selectedObject.GetComponent<f_Castle>();

				c.transform.position = t.transform.position;
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

					p.occupiedTile.isOccupied = false;
					p.lastOccupiedTile = p.occupiedTile;
					p.occupiedTile = null;

				}

				else{}

				selectedObjectPosOld = hit.collider.gameObject.transform.position;
				selectedObject = hit.collider.gameObject;
				Debug.Log(selectedObject + " is selected.");


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
			
			if(selectedObject != emptyObject && isValidPiecePlacement(selectedObject, t)){
				

				
				p.transform.position = t.transform.position;
				p.x = t.x;
				p.y = t.y;
				p.occupiedTile = t;
				t.isOccupied = true;
				selectedObjectPosOld = Vector2.zero;
				selectedObject = emptyObject;
				
			}
			
			
			else{
				
				//selectedObject.transform.position = selectedObjectPosOld;
				//selectedObjectPosOld = Vector2.zero;

				p.transform.position = p.lastOccupiedTile.transform.position;
				p.occupiedTile = p.lastOccupiedTile;
				selectedObject = emptyObject;
				
			}

		}

		//check if placing on tile
		else{

			
			layerMask = 1 << LayerMask.NameToLayer("Tile");
			hit = Physics2D.Raycast (MousePosition(), Vector3.right, 0.01f, layerMask);
			
			
			if (hit.collider != null && hit.collider.tag == "f_Tile") {
				
				f_Tile t = hit.collider.GetComponent<f_Tile>();
				//Debug.Log(t);
				f_Piece p = selectedObject.GetComponent<f_Piece>();
				if(selectedObject != emptyObject && isValidPiecePlacement(selectedObject, t)){
					

					
					p.transform.position = t.transform.position;
					p.x = t.x;
					p.y = t.y;
					p.occupiedTile = t;
					t.isOccupied = true;
					selectedObjectPosOld = Vector2.zero;
					selectedObject = emptyObject;
					
				}
				
				
				else{

					
					//selectedObject.transform.position = selectedObjectPosOld;
					//selectedObjectPosOld = Vector2.zero;
					
					p.transform.position = p.lastOccupiedTile.transform.position;
					p.occupiedTile = p.lastOccupiedTile;
					selectedObject = emptyObject;
					
				}
				
				
		
				
			}
			
			
			
			else if(hit.collider != null && hit.collider.tag == "f_TrayObject"){
				if(selectedObject != emptyObject){
					
					for(int i = 0; i < slots.Length; i++){
						
						if(!slots[i].isOccupied){
							
							f_Piece p = selectedObject.GetComponent<f_Piece>();
							f_Tile t = slots[i];
							
							
							p.transform.position = t.transform.position;
							p.x = t.x;
							p.y = t.y;
							p.occupiedTile = t;
							t.isOccupied = true;
							selectedObjectPosOld = Vector2.zero;
							selectedObject = emptyObject;
							break;
							
						}
						
						else{
							//pass
						}
						
						
						
					}
					
				
				}
				
				else{
					//pass
				}
				
			}

			else{
				
				//selectedObject.transform.position = selectedObjectPosOld;
				//selectedObjectPosOld = Vector2.zero;
				f_Piece p = selectedObject.GetComponent<f_Piece>();
				p.transform.position = p.lastOccupiedTile.transform.position;
				p.occupiedTile = p.lastOccupiedTile;
				selectedObject = emptyObject;
				
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
			Debug.Log("hi");
			Debug.Log(t);

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


	void RotateCastle(GameObject g){
		f_Castle c = g.GetComponent<f_Castle>();

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






	void MouseControls(bool placingCastle){


		//initial left click selects the piece
		if (Input.GetMouseButtonDown (0)) {
			if(isPlacingCastle){

				SelectCastle(isWhiteSetUp);
	
			}

			else{

				SelectPiece(isWhiteSetUp);
			}


		}
		//when held, the object is draggable
		if(Input.GetMouseButton(0)){

				ClickandDrag(selectedObject);
		
		}

		//drops piece onto tile or back to its original position and clears variables
		if(Input.GetMouseButtonUp(0)){

			if(isPlacingCastle){

				DropCastle();

			}

			else{


				DropPiece();
			}
			//DropPiece();
			//selectedObject = emptyObject;
		
		}

	}




	//tray that holds pieces for placement on the board 

	//Rect tray = new Rect(Screen.width, 50, 400, 900);

	f_Tile[] slots;
	Vector3 oldTrayPosition;

	//create tray with slots
	void CreateTray(GameObject trayObject){

		Vector3 tray = trayObject.transform.position;
		oldTrayPosition = tray;

		slots = new f_Tile[15];
		int i = 0;
		int rows = 5;
		int columns = 3;

		// gives the upper most left corner of the trayObject with a leeway of 0.5x0.5 
		Vector3 trayOrigin = new Vector3((tray.x - 2.0f), (tray.y + 5.5f), 1);
		//Vector3 convertedPos = Camera.main.ScreenToWorldPoint(rectPos);


		//Vector3 trayPos = Camera.main.ScreenToWorldPoint(tray.center);
		//trayPos.z = 2.0f;
		//trayObject.transform.position = trayPos;




		for(int y = 0; y < rows; y++){
			float yAdjust = ((- 2f) * y);
			for(int x = 0; x < columns; x++){
				Vector3 adjust = new Vector3(2f * x, yAdjust, 0);

				//Vector3 rectPos = tray.center;


				//Vector3 adjustedPos = convertedPos + adjust;
				Vector3 adjustedPos = trayOrigin + adjust;

				adjustedPos.z = 1.0f;
				//Vector3 adjustedPos = new Vector3(convertedPos.x, convertedPos.y, 1.0f);
				GameObject g = Instantiate(emptyTile, adjustedPos, Quaternion.identity) as GameObject;
				Debug.Log(g);
				slots[i] = g.GetComponent<f_Tile>();

				g.tag = "Untagged";

				SpriteRenderer r = g.GetComponent<SpriteRenderer>();
				r.enabled = false;

				//Debug.Log(slots[i] + ", " + g.transform.position);

				i++;
				//Rect r = guiTexture.GetScreenRect();				
				
			}

		}
			
	}

	GameObject[] pieces;
	//List<GameObject> piecesInTray = new List<GameObject>();

	void FillTray(){

		pieces = GameObject.FindGameObjectsWithTag("f_Piece");


		int j = 0;

		//find the pieces
		for(int i = 0; i < pieces.Length; i++){

			f_Piece p = pieces[i].GetComponent<f_Piece>();

			if(isWhiteSetUp == p.isWhite){

				p.transform.position = slots[j].gameObject.transform.position;
				slots[j].isOccupied = true;
				p.occupiedTile = slots[j];
				//piecesInTray.Add(p.gameObject);
				j++;

			}

			else{

				//pass
			}

		}

		//find the castle
		GameObject[] tiles = GameObject.FindGameObjectsWithTag ("f_Tile");

		for (int i = 0; i < tiles.Length; i++) {
				
			f_Tile t = tiles[i].GetComponent<f_Tile>();

			if(t.tileType == 5){

				f_Castle c = tiles[i].GetComponent<f_Castle>();

				if(isWhiteSetUp == c.isWhite){
					
					c.transform.position = slots[j].gameObject.transform.position;
					slots[j].isOccupied = true;
					c.rotation = 3;
					c.occupiedTile = slots[j];
					c.occupiedTile.isOccupied = true;
					currentCastleBeingPlaced = c.gameObject;
					j++;
					
				}

				else{

					//pass
				}

			}


			else{
				
				//pass
			}
		
		
		}

	}

	void SyncTrayAssets(){

		Vector3 dPosition = trayObject.transform.position - oldTrayPosition;
		
		//transform.position += dPosition;
		
		//oldTrayPosition = trayObject.transform.position;


		for(int i = 0; i < slots.Length; i++){

			slots[i].transform.position += dPosition;

		}



		for (int j = 0; j < pieces.Length; j++) {
				
			f_Piece p = pieces[j].GetComponent<f_Piece>();

			if(p.occupiedTile != null){

				pieces[j].transform.position = p.occupiedTile.transform.position;

			}

		
		
		
		}

		f_Castle c = currentCastleBeingPlaced.GetComponent<f_Castle> ();
		if (c.occupiedTile != null) {
				
			currentCastleBeingPlaced.transform.position = c.occupiedTile.transform.position; 
		
		}


		oldTrayPosition = trayObject.transform.position;


		
		/*//check if any piece in the tray is not in the list and add them
		for (int i = 0; i < pieces.Length; i++) {

			f_Piece p = pieces[i].GetComponent<f_Piece>();

			for(int j = 0; j < slots.Length; j++){

				if(p.occupiedTile == slots[j] && !piecesInTray.Contains(pieces[i])){
					
					piecesInTray.Add(pieces[i]);
					
				}

				else{}

			}


		}






		//check if any piece from the tray have left the tray
		foreach (GameObject element in piecesInTray) {
			
			f_Piece p = element.GetComponent<f_Piece>();
			
			for(int i = 0; i < slots.Length; i++){
				
				if(p.occupiedTile != slots[i]){
					
					piecesInTray.Remove(element);
					
				}
				
				else{}

				slots[i].transform.position += dPosition;

			}


			element.transform.position += dPosition;

		}*/


	}

	//Destroys tray and the tiles within slots[]
	void DestroyTray(){

		Destroy(trayObject);

		for(int i = 0; i < slots.Length; i++){

			Destroy (slots[i].gameObject);


		}
	
	}












	bool isValidCastlePlacement(GameObject selectedCastle){

		f_Castle c = selectedCastle.GetComponent<f_Castle> ();

		f_Tile t = c.castleGreens;

		//holds length of board and needs definition outside of this fxn
		int boardLength = 24;

		//if greens are out of bounds return false
		if (t.x < 0 || t.y < 0 || t.x > boardLength || t.y > boardLength) {
				
			Debug.Log("This is not valid castle placement:  Castle Greens are out of bounds");
			return false;
		
		}

		else return true;

	
	
	}






	void OnGUI(){

		if (isSetUp) {
				
			if (!isWhiteSetUp) {
				
				if (GUI.Button (new Rect (10, 50, 150, 25), "Start Game")) {
					
					//f_Castle c = lastCastleSelected.GetComponent<f_Castle>();
					//c.ReplaceOccupiedTile(c);
					//c.SetUpCastleGreens(c.castleGreens);
					isSetUp = false;
					f_Castle c = lastCastleSelected.GetComponent<f_Castle>();
					c.isSetup = false;
					//c.SetUpCastleGreens(c.castleGreens);
					//c.ReplaceOccupiedTile(c);
					DestroyTray();
					f_gameManager.SetUpBoard();
					StartCoroutine(f_gameManager.Game());
					
				}
				
				
			}
			
			else{
				
				if (GUI.Button (new Rect (10, 50, 150, 25), "Finished Planning")) {
					
					
					//f_gameManager.SetUpBoard();
					//StartCoroutine(f_gameManager.Game());
					f_Castle c = lastCastleSelected.GetComponent<f_Castle>();
					c.isSetup = false;
					
					
					isWhiteSetUp = false;
					isPlacingCastle = true;
					FillTray();
					
					
				}
				
			}
				
		
			if(isPlacingCastle){
				//press to finish castle placement and move on to unit placement
				if (GUI.Button (new Rect (10, 90, 150, 25), "Place Units")) {
					//checks if the castle placement is within the bounds of the board before moving on
					if(isValidCastlePlacement(lastCastleSelected)){

						f_Castle c = lastCastleSelected.GetComponent<f_Castle>();
						c.occupiedTile.isOccupied = false;
						c.occupiedTile = null;

						isPlacingCastle = false;
					
						//c.isSetup = false;
						//c.SetUpCastleGreens(c.castleGreens);
						//c.ReplaceOccupiedTile(c);



					}

					else{}
					

					
					
				}
				
				if (GUI.Button (new Rect (10, 130, 150, 25), "Rotate Castle")) {
					
					
					RotateCastle(lastCastleSelected);
					
					
					
					
				}
				
				
				
			}

		

			///create a tray that holds pieces for placement on the map.

			//GUI.Box(tray, "");


		




		}

	
	}



	void Awake () {

		isSetUp = true;
		isPlacingCastle = true;
		isWhiteSetUp = true;
		selectedObject = emptyObject;
		CreateTray(trayObject);
		FillTray();
		
	
	}
	




	void Update () {

		if(isSetUp){

			MouseControls (isPlacingCastle);
			SyncTrayAssets();
		}
		

		//ClickandDrag (selectedObject);
	
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class f_SetUpManager : MonoBehaviour {

	public f_GameManager f_gameManager;
	public GameObject emptyObject;
	public GameObject emptyTile;
	public GameObject trayObject;
	public GameObject screenObject;


	Player myPlayer;
	Camera playerCamera;
	GameObject selectedObject;
	Vector2 selectedObjectPosOld;

	GameObject lastCastleSelected;
	GameObject currentCastleBeingPlaced;
	//GameObject castleBlack;

	bool isPlacingPieces;
	bool isPlacingCastle;
	public bool isWhiteSetUp;
	bool isSetUp;
	bool isOffline = false;




	//uses GUI elements to allow for the set up of the game
	//holds the ability to click and drag units onto tiles

	void FindNetworkManager(){
		
		NetworkManager networkManager = FindObjectOfType<NetworkManager> ();
		
		if (networkManager != null) {
			
			isOffline = false;
			Debug.Log("Online: Network Manager Found");
			
		}
		
		else{
			
			isOffline = true;
			Debug.Log("Offline: Network Manager Not Found");
			
		}
		
		
	}

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

	Vector3 MousePosition(){

		Vector3 v3Pos = playerCamera.ScreenToWorldPoint(Input.mousePosition);
		Vector3 mousePos = new Vector3(v3Pos.x, v3Pos.y, -10.0f);
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
				UI_Element castleUIElement = selectedObject.GetComponent<UI_Element>();
				UI_Element greensUIElement = c.castleGreens.GetComponent<UI_Element>();
				castleUIElement.enabled = false;
				greensUIElement.enabled = false;


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

					UI_Element pieceUIelement = p.GetComponent<UI_Element>();
					p.transform.localScale = currentLocalScale;
					pieceUIelement.enabled = true;
					pieceUIelement.SetUpElement();




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
			UI_Element pieceUIElement = selectedObject.GetComponent<UI_Element>();
			
			if(selectedObject != emptyObject && isValidPiecePlacement(selectedObject, t)){
				
				pieceUIElement.enabled = false;
				selectedObject.transform.localScale = Vector3.one;
				
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
				UI_Element pieceUIElement = selectedObject.GetComponent<UI_Element>();
				if(selectedObject != emptyObject && isValidPiecePlacement(selectedObject, t)){
					
					pieceUIElement.enabled = false;
					selectedObject.transform.localScale = Vector3.one;
					
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
			
			
			
			else if(hit.collider != null && hit.collider.tag == "f_TrayObject"){
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
				
			}


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


	void ReturnToLastOccupiedTile(GameObject piece){

		if (piece.tag == "f_Piece") {
				
			Debug.Log("ReturnToLastOccupiedTile");

			f_Piece p = piece.GetComponent<f_Piece>();

			if(p != null){

				//if the last occupied tile returns a tile on the game board.
				if(p.lastOccupiedTile.tag == "f_Tile"){

					UI_Element pieceUIElement = p.GetComponent<UI_Element>();

					pieceUIElement.enabled = false;
					selectedObject.transform.localScale = Vector3.one;

					p.transform.position = p.lastOccupiedTile.transform.position;
					p.occupiedTile = p.lastOccupiedTile;
					selectedObject = emptyObject;



				}

				//if the last occupied tile is untagged (a tray slot tile).
				else{
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
	float oldFov;
	Vector3 oldCameraPosition;

	//create tray with slots
	void CreateTray(GameObject trayObject){

		Vector3 tray = trayObject.transform.position;
		oldTrayPosition = tray;
		oldFov = playerCamera.orthographicSize;
		oldCameraPosition = playerCamera.transform.position;

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

		//UI_Element trayUIElement = trayObject.GetComponent<UI_Element> ();

		//values found using trial and error.  Fuck it.
		float xRatioOrigin = 0.785f;
		float yRatioOrigin = 0.85f;


		for(int y = 0; y < rows; y++){
			float yAdjust = ((- 2f) * y);
			for(int x = 0; x < columns; x++){
				Vector3 adjust = new Vector3(2f * x, yAdjust, 0);

				//Vector3 rectPos = tray.center;


				//Vector3 adjustedPos = convertedPos + adjust;
				Vector3 adjustedPos = trayOrigin + adjust;

				adjustedPos.z = -9.0f;
				//Vector3 adjustedPos = new Vector3(convertedPos.x, convertedPos.y, 1.0f);
				GameObject g = Instantiate(emptyTile, adjustedPos, Quaternion.identity) as GameObject;
				//Debug.Log(g);
				//g.AddComponent<UI_Element>();
				slots[i] = g.GetComponent<f_Tile>();


				UI_Element slotUIElement = g.AddComponent("UI_Element") as UI_Element;
				//Vector3 slotScreenPoint = playerCamera.WorldToScreenPoint(adjustedPos);
				//slotUIElement.xRatio = slotScreenPoint.x/Screen.width;
				//slotUIElement.yRatio = slotScreenPoint.y/Screen.height;
				//slotUIElement.SetUpElement();

				slotUIElement.xRatio = xRatioOrigin + (0.074f * (x));
				slotUIElement.yRatio = yRatioOrigin - (0.1f * (y));
				slotUIElement.SetUpElement();

				g.tag = "Untagged";
				//slots[i].tileType = 6;

				SpriteRenderer r = g.GetComponent<SpriteRenderer>();
				r.enabled = false;

				//Debug.Log(slots[i] + ", " + g.transform.position);

				i++;
				//Rect r = guiTexture.GetScreenRect();				
				
			}

		}

		//nextAvailableTraySlot = slots [i];
			
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


				p.transform.position = new Vector3(slots[j].gameObject.transform.position.x,
						slots[j].gameObject.transform.position.y, -10.0f);
				//p.transform.position = slots[j].transform.position;

				slots[j].isOccupied = true;
				p.occupiedTile = slots[j];

				//adds UI element script to piece and has it sync to its assigned slot on the tray.
				UI_Element slotUIelement = slots[j].GetComponent<UI_Element>();
				UI_Element pieceUIElement = p.gameObject.AddComponent("UI_Element") as UI_Element;
				pieceUIElement.xRatio = slotUIelement.xRatio;
				pieceUIElement.yRatio = slotUIelement.yRatio;
				pieceUIElement.SetUpElement();
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

					UI_Element castleUIElement = c.gameObject.AddComponent("UI_Element") as UI_Element;
					UI_Element slotUIElement = slots[j].GetComponent<UI_Element>();

					castleUIElement.xRatio = slotUIElement.xRatio;
					castleUIElement.yRatio = slotUIElement.yRatio;
					castleUIElement.SetUpElement();

					f_Tile greens = c.castleGreens;

					UI_Element greensUIElement = greens.gameObject.AddComponent("UI_Element") as UI_Element;
					greensUIElement.xRatio = slotUIElement.xRatio;
					greensUIElement.yRatio = slotUIElement.yRatio;
					greensUIElement.SetUpElement();
					
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

		nextAvailableTraySlot = slots [j];

	}

	Vector3 currentLocalScale;
	f_Tile nextAvailableTraySlot; //passes the next available slot to DropPiece;

	void SyncTrayAssets(){



		if (trayObject == null) {
				
			//pass
		
		}


		else{

			//takes the first slot on the tray and returns its local scale for the pieces;
			currentLocalScale = slots[0].transform.localScale;

			//checks if the next available slot has been used and if so, find a new one
			//if(nextAvailableTraySlot.isOccupied){

				for(int i = 0; i < slots.Length; i++){
					
					if(!slots[i].isOccupied){
						
						nextAvailableTraySlot = slots[i];
						
					}
					
					
				}

			//}








			float fov = playerCamera.orthographicSize;
			//Vector3 dPosition = trayObject.transform.position - oldTrayPosition;
			Vector3 dPosition = playerCamera.transform.position - oldCameraPosition;




			//for(int i = 0; i < slots.Length; i++){
				
				//float aspectRatio = fov / oldFov;
				//slots[i].transform.localScale = slots[i].transform.localScale * aspectRatio;
				//slots[i].transform.position += dPosition;

				
				
			//}

			
			/*for (int j = 0; j < pieces.Length; j++) {
				
				f_Piece p = pieces[j].GetComponent<f_Piece>();
				
				if(p.occupiedTile != null){
					
					
					//float aspectRatio = fov/oldFov;
					//pieces[j].transform.localScale = pieces[j].transform.localScale * aspectRatio;

					//Vector3 adjustedPos = new Vector3 (p.occupiedTile.transform.position.x, p.occupiedTile.transform.position.y, -10.0f);
					//pieces[j].transform.position = adjustedPos;

					//if(pieces[j].transform.position != adjustedPos){
					
					//	pieces[j].transform.position = adjustedPos;
					
					//}

					//Vector3 adjustedPos = new Vector3 (dPosition.x, dPosition.y, 0);
					//pieces[j].transform.position += adjustedPos;
					
				}
				
				
				
				
				
				
			}*/
			
			/*f_Castle c = currentCastleBeingPlaced.GetComponent<f_Castle> ();
			if (c.occupiedTile != null) {

				//float aspectRatio = fov / oldFov;
				//currentCastleBeingPlaced.transform.localScale = currentCastleBeingPlaced.transform.localScale * aspectRatio;
				Vector3 adjustedPos = new Vector3 (c.occupiedTile.transform.position.x, c.occupiedTile.transform.position.y, -10.0f);
				currentCastleBeingPlaced.transform.position = adjustedPos; 
			}*/
			
			oldTrayPosition = trayObject.transform.position;
			oldCameraPosition = playerCamera.transform.position;
			oldFov = fov;
			
			
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
		int boardLength = 23;

		//if greens are out of bounds return false
		if (t.x < 0 || t.y < 0 || t.x > boardLength || t.y > boardLength) {
				
			Debug.Log("This is not valid castle placement:  Castle Greens are out of bounds");
			return false;
		
		}

		else return true;

	
	
	}





	void OnGUI(){

		if (isSetUp) {

			if(isOffline){

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
				

				
				
			}

			//online play
			else{

				if(!myPlayer.isReady){
					if (GUI.Button (new Rect (10, 50, 150, 25), "Finished Planning")) {	

						if(IsTrayClear(slots)){

							DestroyTray();
							Destroy(screenObject);
							f_Castle c = lastCastleSelected.GetComponent<f_Castle>();
							//isSetUp = false;
							c.isSetup = false;
							myPlayer.isReady = true;
							isPlacingPieces = false;


						}

						else{

							Debug.Log("Must finish placing all units");


						}
						
						//f_gameManager.SetUpBoard();
						//StartCoroutine(f_gameManager.Game());


						//isSetUp = false;
						
						
						//isWhiteSetUp = false;
						//isPlacingCastle = true;
						//FillTray();
						
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
							//isPlacingPieces = true;
							
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

				if (myPlayer.isReady && !f_gameManager.gameOn) {
					
					GUI.TextField(new Rect(Screen.width * 0.5f, Screen.height * 0.5f, 150, 25), "Player is ready!");
					Debug.Log("My player is ready.");
					
				}

			}

		}


	
	}

	bool IsTrayClear(f_Tile[] slots){

	
		for (int i = 0; i < slots.Length; i++) {

			if(slots[i].isOccupied){

				return false;


			}
				
		
		
		}

		return true;
	
	
	
	}



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






	Player[] players;

	public void InitiateSetup(){
		
		Debug.Log ("Initiate Setup");

		players = FindObjectsOfType<Player> ();

		Debug.Log ("player list length " + players.Length);
		for (int i = 0; i < players.Length; i++) {
			Debug.Log("Looking for MyPlayer");
			if(players[i].isMyPlayer){

				myPlayer = players[i];
				f_gameManager.myPlayer = myPlayer;
				myPlayer.SetUpAudio();

				Debug.Log("MyPlayer found.");
			}

			Debug.Log("Player " + players[i].playerNumber);
		
		}

		if (screenObject != null) {
				
			ScreenBehavior screen = screenObject.GetComponent<ScreenBehavior>();

			screen.myPlayer = myPlayer;

			if(myPlayer.isWhite){

				screen.SetUpPosition(FindAnchorTile(11,11));

			}


			else{

				screen.SetUpPosition(FindAnchorTile(11,12));


			}




		
		}


		
		playerCamera = myPlayer.gameObject.GetComponent<Camera> ();
		CameraController c = myPlayer.GetComponent<CameraController>();
		c.enabled = true;
		myPlayer.SetUpAudio ();

		trayObject.SetActive(true);
		
		if (myPlayer.isWhite) {
			
			isWhiteSetUp = true;
			
		}
		
		else{
			
			isWhiteSetUp = false;
			
		}
		
		Debug.Log ("is MyPlayer white: " + myPlayer.isWhite);
		
		isSetUp = true;
		isPlacingPieces = true;
		isPlacingCastle = true;
		FindNetworkManager ();
		if (isOffline) {
				
			f_gameManager.isOffline = true;
		
		}

		else{

			f_gameManager.isOffline = false;

		}
		//isWhiteSetUp = true;
		selectedObject = emptyObject;
		CreateTray(trayObject);
		FillTray();
		
	
	
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






	void Awake () {

		isSetUp = false;

		if (isOffline) {
				
			InitiateSetup();
		
		}


		/*player = FindObjectOfType<Player> ();
		playerCamera = player.gameObject.GetComponent<Camera> ();
		Debug.Log (player);

		if (player.isWhite) {
				
			isWhiteSetUp = true;
		
		}

		else{

			isWhiteSetUp = false;

		}

		Debug.Log ("iswhite: " + player.isWhite);

		isSetUp = true;
		isPlacingCastle = true;
		//isWhiteSetUp = true;
		selectedObject = emptyObject;
		CreateTray(trayObject);
		FillTray();*/
		
	
	}
	






	void Update () {

		if(isSetUp){

			if(isPlacingPieces){

				MouseControls (isPlacingCastle);

			}



			ArePlayersReady();
		}
		

		//ClickandDrag (selectedObject);
	
	}

	void FixedUpdate(){

		if (isPlacingPieces) {
				
			SyncTrayAssets();
		
		}

	}
}

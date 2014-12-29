using UnityEngine;
using System.Collections;

public class f_SetUpManager : MonoBehaviour {

	public f_GameManager f_gameManager;
	public GameObject emptyObject;

	GameObject selectedObject;
	Vector2 selectedObjectPosOld;

	GameObject lastCastleSelected;
	//GameObject castleBlack;

	bool isPlacingCastle;
	bool isWhiteSetUp;
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

				f_Tile c = selectedObject.GetComponent<f_Tile>();
				
				c.transform.position = t.transform.position;
				c.x = t.x;
				c.y = t.y;

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

		//Vector3 v3Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//Vector2 mousePos = new Vector2(v3Pos.x, v3Pos.y);
		
		//Debug.Log(mousePos);

		RaycastHit2D hit = Physics2D.Raycast(MousePosition(), Vector2.right, 0.01f);
		
		if(hit.collider != null && hit.collider.tag == "f_Piece"){
			f_Piece p = hit.collider.gameObject.GetComponent<f_Piece>();
			if(p.isWhite == isWhitePlacing){

				selectedObjectPosOld = hit.collider.gameObject.transform.position;
				selectedObject = hit.collider.gameObject;
				Debug.Log(selectedObject + " is selected.");
				int layerMask = 1 << LayerMask.NameToLayer("Tile");
				hit = Physics2D.Raycast (MousePosition(), Vector3.right, 0.01f, layerMask);
				
				
				if (hit.collider != null && hit.collider.tag == "f_Tile") {

					f_Tile t = hit.collider.GetComponent<f_Tile>();
					t.isOccupied = false;


				}

			}


			else{}
		}
	
	
	
	}
	

	void DropPiece(){

		//Vector3 v3Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 piecePos = new Vector2(selectedObject.transform.position.x, selectedObject.transform.position.y);
		
		//Debug.Log(mousePos);

		/*int layerMask = 1 << LayerMask.NameToLayer("Piece");
		RaycastHit2D hit = Physics2D.Raycast (piecePos, Vector3.right, 0.01f, layerMask);

		//protects from placing on top of another unit
		if(hit.collider != null && hit.collider.tag == "f_Piece"){

			Debug.Log("this is not a valid move");
			selectedObject.transform.position = selectedObjectPosOld;
			selectedObjectPosOld = Vector2.zero;
			selectedObject = emptyObject;


		}*/

		//else{

			int layerMask = 1 << LayerMask.NameToLayer("Tile");
			RaycastHit2D hit = Physics2D.Raycast (MousePosition(), Vector3.right, 0.01f, layerMask);

			
			if (hit.collider != null && hit.collider.tag == "f_Tile") {
				
				f_Tile t = hit.collider.GetComponent<f_Tile>();
				
				
				if(selectedObject != emptyObject && isValidPiecePlacement(selectedObject, t)){
					
					f_Piece p = selectedObject.GetComponent<f_Piece>();
					
					p.transform.position = t.transform.position;
					p.x = t.x;
					p.y = t.y;
					t.isOccupied = true;
					selectedObjectPosOld = Vector2.zero;
					selectedObject = emptyObject;
					
				}
				
				
				else{

					selectedObject.transform.position = selectedObjectPosOld;
					selectedObjectPosOld = Vector2.zero;
					selectedObject = emptyObject;

				}
				
				
				//Debug.Log("hit3");
				
			}
			/*if (hit.collider != null && hit.collider.tag == "f_Tile") {
				
			Debug.Log(hit.collider.gameObject);
			Debug.Log("hit");
		
			}*/
			
			else{
				
				selectedObject.transform.position = selectedObjectPosOld;
				selectedObjectPosOld = Vector2.zero;
				selectedObject = emptyObject;
				
			}
			
			
			//Debug.Log ("hit1");

		//}

	}


	bool isValidPiecePlacement(GameObject g, f_Tile t){

		f_Piece p = g.GetComponent<f_Piece>();

		if(t.isOccupied == true){

			Debug.Log("This is not a valid piece placement: Cannot place on top of other pieces");
			return false;

		}



		// if player tries to place unit on a mountain
		else if(t.tileType == 3){

			Debug.Log("This is not a valid piece placement");
			return false;

		}

		//if player tries to place a mounted unit on rough terrain
		else if((p.pieceDesignator >= 2 && p.pieceDesignator <= 4) || (p.pieceDesignator >= 10 && p.pieceDesignator <= 12)){
			if(t.tileType == 2){

				Debug.Log("This is not a valid piece placement");
				return false;

			}

			else{

				return true;

			}


		}

		//if player tries to place an archer on his own castle
		else if(p.pieceDesignator == 5 || p.pieceDesignator == 13){

			if(t.tileType == 5){
				f_Castle c = t.GetComponent<f_Castle>();
				if(p.isWhite == c.isWhite){

					return false;

				}

				else{
					Debug.Log("This is not a valid piece placement");
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
					f_gameManager.SetUpBoard();
					StartCoroutine(f_gameManager.Game());
					
				}
				
				
			}
			
			else{
				
				if (GUI.Button (new Rect (10, 50, 150, 25), "Finished Planning")) {
					
					
					//f_gameManager.SetUpBoard();
					//StartCoroutine(f_gameManager.Game());
					//f_Castle c = lastCastleSelected.GetComponent<f_Castle>();
					//c.ReplaceOccupiedTile(c);
					//c.SetUpCastleGreens(c.castleGreens);
					
					
					
					isWhiteSetUp = false;
					isPlacingCastle = true;
					f_Castle c = lastCastleSelected.GetComponent<f_Castle>();
					c.isSetup = false;
					
					
				}
				
			}
				
		
			if(isPlacingCastle){
				
				if (GUI.Button (new Rect (10, 90, 150, 25), "Place Units")) {
					
					
					
					isPlacingCastle = false;
					
					
					
				}
				
				if (GUI.Button (new Rect (10, 130, 150, 25), "Rotate Castle")) {
					
					
					RotateCastle(lastCastleSelected);
					
					
					
					
				}
				
				
				
			}

		
		}

	
	}



	void Awake () {

		isSetUp = true;
		isPlacingCastle = true;
		isWhiteSetUp = true;
	
	}
	

	void Update () {

		if(isSetUp){

			MouseControls (isPlacingCastle);

		}

		//ClickandDrag (selectedObject);
	
	}
}

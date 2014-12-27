using UnityEngine;
using System.Collections;

public class f_SetUpManager : MonoBehaviour {

	public f_GameManager f_gameManager;
	public GameObject emptyObject;

	GameObject selectedObject;
	Vector2 selectedObjectPosOld;





	//uses GUI elements to allow for the set up of the game
	//holds the ability to click and drag units onto tiles



	void ClickandDrag(GameObject g){

		if (g != null) {
				
				Vector3 v3Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector2 mousePos = new Vector2(v3Pos.x, v3Pos.y);
				g.transform.position = mousePos;
				

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

	void SelectPiece(){

		//Vector3 v3Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//Vector2 mousePos = new Vector2(v3Pos.x, v3Pos.y);
		
		//Debug.Log(mousePos);
		RaycastHit2D hit = Physics2D.Raycast(MousePosition(), Vector2.right, 0.01f);
		
		if(hit.collider != null && hit.collider.tag == "f_Piece"){

			selectedObjectPosOld = hit.collider.gameObject.transform.position;
			selectedObject = hit.collider.gameObject;
			Debug.Log(selectedObject + " is selected.");
			
		}
	
	
	
	}


	

	void DropPiece(){

		Vector3 v3Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//Vector2 piecePos = new Vector2(selectedObject.transform.position.x, selectedObject.transform.position.y);
		
		//Debug.Log(mousePos);
		int layerMask = 8;
		RaycastHit hit; 


		if (Physics.Raycast (v3Pos, Vector3.right, 0.01f)) {
				
			Debug.Log("hit3");
		
		}
		/*if (hit.collider != null && hit.collider.tag == "f_Tile") {
				
			Debug.Log(hit.collider.gameObject);
			Debug.Log("hit");
		
		}*/



		selectedObjectPosOld = Vector2.zero;
		selectedObject = emptyObject;
		//Debug.Log ("hit1");
	
	
	}


	void MouseControls(){

		//initial left click selects the piece
		if (Input.GetMouseButtonDown (0)) {

			SelectPiece ();

		}
		//when held, the object is draggable
		if(Input.GetMouseButton(0)){

			ClickandDrag(selectedObject);
		
		}

		//drops piece onto tile or back to its original position and clears variables
		if(Input.GetMouseButtonUp(0)){

			DropPiece();
			//selectedObject = emptyObject;
		
		}

	}
		



	void OnGUI(){

		
		if (GUI.Button (new Rect (10, 50, 150, 25), "Start Game")) {
				

			f_gameManager.SetUpBoard();
			StartCoroutine(f_gameManager.Game());
		
		}
	
	
	}



	void Start () {
	
	}
	

	void Update () {
		MouseControls ();
		//ClickandDrag (selectedObject);
	
	}
}

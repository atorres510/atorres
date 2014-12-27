using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public int x;
	public int y;

	public bool isOccupied;
	public int isThreatened;  //0 = No Threat, 1 = White Threat, 2 = Black Threat, 3 = W&B Threat

	MeshCollider meshCollider;
	
	GameObject gameManagerObject;
	GameManager gameManager;
	GameObject mainCameraObject;
	Camera mainCamera;




	public GameObject ReturnThisTile(){

		return gameObject;

	}




	void DisableCollider(bool occupied){

		if (occupied) {

			meshCollider.enabled = false;

		}

		else {

			meshCollider.enabled = true;

		}
	}










	void OnMouseOver(){

		if (Input.GetMouseButtonDown (0)) {

			if(gameManager.isPlayer1Turn == gameManager.selectedPiece.isWhite){

				bool isThisMoveValid = gameManager.VerifyMove(gameObject);
				if(isThisMoveValid){
					//gameManager.selectedTile = gameObject;
					gameManager.selectedPiece.Move(gameObject);
					//Debug.Log(gameManager.selectedTile.transform.position);
					Debug.Log("This is a valid move.");
				}

				if(!isThisMoveValid){
					Debug.Log("This move is not valid: King is vulnerable to check");
				}

			}
		
		}
	}




	void OnGUI(){


		/*if (gameManager.toggleTileGUI) {

			Vector3 textFieldPos = mainCamera.WorldToScreenPoint (transform.position);

			GUI.Box (new Rect (textFieldPos.x, (Screen.height - textFieldPos.y), 20, 20), isThreatened.ToString ());

		}*/

	}





	// Use this for initialization
	void Start () {
	
		meshCollider = gameObject.GetComponent<MeshCollider> ();
		gameManagerObject = GameObject.FindGameObjectWithTag ("GameManager");
		gameManager = gameManagerObject.GetComponent<GameManager> ();
		mainCameraObject = GameObject.FindGameObjectWithTag ("MainCamera");
		mainCamera = mainCameraObject.GetComponent<Camera> ();


	
	}
	
	// Update is called once per frame
	void Update () {

		DisableCollider (isOccupied);

	
	}
}

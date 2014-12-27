using UnityEngine;
using System.Collections;

public abstract class Piece : MonoBehaviour {

	public GameObject startTile;
	
	public bool isSelected;
	public bool isWhite;
	public bool turnTurner;

	public bool isCheckBlocked;

	public int x;
	public int y;

	public int pieceDesignator;
	

	public Tile occupiedTile;
	public GameObject gameManagerObject;
	public GameManager gameManager;


	public abstract void Move (GameObject targetTile);

	public abstract void ThreatenTiles (bool isUpdate);

	public abstract bool isValidMove (bool checkForBlock);

	//public abstract bool isValidMove ();

	public void InstantiateVariables(){

		gameManagerObject = GameObject.FindGameObjectWithTag ("GameManager");
		gameManager = gameManagerObject.GetComponent<GameManager> ();
		//Debug.Log (gameManager);
		
	}
	

	public void StartPosition(){

		transform.position = startTile.transform.position;

		occupiedTile = startTile.GetComponent<Tile> ();
		occupiedTile.isOccupied = true;
		//Debug.Log (occupiedTile.gameObject.transform.position);

	}


	public void ChangePosition(GameObject tile){

		if(tile != null && tile.tag == "Tile"){

			occupiedTile.isOccupied = false;

			transform.position = tile.transform.position;
			occupiedTile = tile.GetComponent<Tile> ();
			occupiedTile.isOccupied = true;


		} 

		if(tile != null && tile.tag == "Piece"){
		
			Piece p = tile.GetComponent<Piece>();
			transform.position = tile.transform.position;

			occupiedTile.isOccupied = false;

			occupiedTile = p.occupiedTile;
			occupiedTile.isOccupied = true;

		
		}

		//else {
			//Debug.Log("No tile selected");
		//}
	}
	
	public void UpdateCoordinates(int newX, int newY){

		gameManager.coordinates[x, y] = 0;

		gameManager.coordinates[newX, newY] = pieceDesignator;

		x = newX;
		y = newY;

	}



	public void DestroyTargetPiece(){

		//Piece p = target.GetComponent<Piece>();

		gameManager.coordinates[x, y] = 0;

		Destroy (gameObject);
	
	}


	public bool V3Equal(Vector3 a, Vector3 b){

		return Vector3.SqrMagnitude (a - b) < 0.0001;
	
	}


	public bool MyKingIsInCheck(int x, int y, bool isCheckForBlock){

		//check against putting friendly king at risk.
		if (!isCheckBlocked) {
			int KingDesignator = gameManager.coordinates [x, y];
			//Tile t = gameManager.tileCoordinates [x, y].GetComponent<Tile> ();
			//if(isWhite && t.isThreatened == 2 || !isWhite && t.isThreatened == 1 || t.isThreatened == 3){
			if (isWhite && KingDesignator == 6 || !isWhite && KingDesignator == 12) {
					//isCheckBlocked = true;
					return true;

			} 

			else {
					return false;
			}
		}


		else {
			//checks if the check is being blocked
			Tile t = gameManager.tileCoordinates [x, y].GetComponent<Tile> ();
			if(isWhite && t.isThreatened == 2 || !isWhite && t.isThreatened == 1 || t.isThreatened == 3){

				return true;

			}

			return false;
		}




	}

	


	void OnMouseOver(){
		if (Input.GetMouseButtonDown (0)) {

			if(gameManager.isPlayer1Turn == isWhite){

				gameManager.selectedPiece = gameObject.GetComponent<Piece>();
				Debug.Log(gameManager.selectedPiece + " is selected");

			}

			if(gameManager.isPlayer1Turn != isWhite){
				bool isThisMoveValid = gameManager.VerifyMove(gameObject);
				if(isThisMoveValid){

					gameManager.selectedPiece.Move(gameObject);
					Debug.Log("This move is valid");
				}

				if(!isThisMoveValid){

					Debug.Log("This move is not valid: King is vulnerable to check");

				}

			}
		}
	}


	void Update(){

		//if (gameManager.selectedPiece == gameObject.GetComponent<Piece>) {

			//occupiedTile.gameObject.renderer.material.color = Color.red;
		
		//}

		//occupiedTile.gameObject.renderer.material = Material ass


	}




	


}

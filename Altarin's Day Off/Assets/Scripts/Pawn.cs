using UnityEngine;
using System.Collections;

public class Pawn : Piece {

	public override bool isValidMove (bool checkForBlock){
		if (isWhite) {//White Pawn

			int newX = x + 1;
			int newY = y + 1;
			
			if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
				if (!MyKingIsInCheck (newX, newY, checkForBlock)) {
					
				} 

				else {
					return false;
				}
			}
			
			newX = x + 1;
			newY = y - 1;
			
			if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
				if (!MyKingIsInCheck (newX, newY, checkForBlock)) {
					
				} 

				else {
					return false;
				}
				
			}
		}

		else{//Black Pawn

			int newX = x - 1;
			int newY = y - 1;
			
			if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
				if (!MyKingIsInCheck (newX, newY, checkForBlock)) {
					
				} 

				else {
					
					return false;
					
				}
			}
			
			newX = x - 1;
			newY = y + 1;
			
			if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
				if (!MyKingIsInCheck (newX, newY, checkForBlock)) {
					
				} 

				else {
					
					return false;
					
				}
				
			}

		}

		return true;

	}


	public override void ThreatenTiles(bool isUpdate){

		if (isWhite) {//White Pawn
			int newX = x + 1;
			int newY = y + 1;
			if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
					ThreatAssignment (isUpdate, newX, newY);
			}

			newX = x - 1;
			newY = y + 1;
			if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
					ThreatAssignment (isUpdate, newX, newY);
			}

		} 

		else {//Black Pawn

			int newX = x + 1;
			int newY = y - 1;
			if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
				ThreatAssignment (isUpdate, newX, newY);
			}
			
			newX = x - 1;
			newY = y - 1;
			if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
				ThreatAssignment (isUpdate, newX, newY);
			}


		}
	}

	public void ThreatAssignment(bool isUpdate, int x, int y){
		
		Tile t = gameManager.tileCoordinates[x, y].GetComponent<Tile>();
		
		if(!isUpdate){
			
			if(t.isThreatened == 0){
				if(isWhite){
					
					t.isThreatened = 1;
					
				}
				
				if(!isWhite){
					
					t.isThreatened = 2;
					
				}
			}
			else{
				
				t.isThreatened = 0;
				
			}
			
		}
		
		else{ //Update
			
			if(t.isThreatened < 3 && t.isThreatened != 0){
				
				if(isWhite && t.isThreatened == 2 || !isWhite && t.isThreatened == 1){
					
					t.isThreatened = 3;
					
				}
				
				else{
					
				}
				
			}
			
			if(t.isThreatened == 0){
				if(isWhite){
					
					t.isThreatened = 1;
					
				}
				
				if(!isWhite){
					
					t.isThreatened = 2;
					
				}
				
			}
			
			else{}
			
		}
		
	}

	public override void Move(GameObject targetTile){

		/*
		if(isWhite){
		//Debug.Log("This piece is white");
			if(y == 1){
				//Debug.Log("This is the pawn's first move");

				if(gameManager.coordinates[x, (y+2)] == 0 && targetTile == gameManager.tileCoordinates[x, (y+2)]){
					//Debug.Log ("There is no piece here");
					//Debug.Log("This is a valid move");
					ThreatenTiles(false);
					occupiedTile.isThreatened = 0;
					ChangePosition(targetTile);
					UpdateCoordinates(x, (y + 2));
					ThreatenTiles(true);
					//Debug.Log(x);
					//Debug.Log(y);
					turnTurner = true;

				}

				if(gameManager.coordinates[x, (y+1)] == 0 && targetTile == gameManager.tileCoordinates[x, (y+1)]){
					//Debug.Log("This is a space ahead of the pawn");
					//Debug.Log("This is a valid move");
					ThreatenTiles(false);
					occupiedTile.isThreatened = 0;
					ChangePosition(targetTile);
					UpdateCoordinates(x, (y + 1));
					ThreatenTiles(true);
					turnTurner = true;
						
				}

				if(targetTile.tag == "Piece"){
					if(x != 7){
						//Debug.Log("hi");
						if(gameManager.coordinates[(x+1),(y+1)] < 7 && gameManager.coordinates[(x+1),(y+1)] != 0){
							//Debug.Log ("Can take piece");
							Piece targetPiece = targetTile.GetComponent<Piece>();
							Debug.Log (targetPiece + " is the target");
							if(targetPiece.isWhite != isWhite && targetPiece.x == (x+1) && targetPiece.y == (y+1)){

								ThreatenTiles(false);
								occupiedTile.isThreatened = 0;
								ChangePosition(targetTile);
								UpdateCoordinates(targetPiece.x, targetPiece.y);
								targetPiece.DestroyTargetPiece();
								ThreatenTiles(true);
								turnTurner = true;
								
								
							}
						}
					}

					if(x != 0){
						if(gameManager.coordinates[(x-1),(y+1)] < 7 && gameManager.coordinates[(x-1),(y+1)] != 0){
							//Debug.Log ("Can take piece");

							Piece targetPiece = targetTile.GetComponent<Piece>();
							Debug.Log (targetPiece + " is the target");
							if(targetPiece.isWhite != isWhite && targetPiece.x == (x-1) && targetPiece.y == (y+1)){

								ThreatenTiles(false);
								occupiedTile.isThreatened = 0;
								ChangePosition(targetTile);
								UpdateCoordinates(targetPiece.x, targetPiece.y);
								targetPiece.DestroyTargetPiece();
								ThreatenTiles(true);
								turnTurner = true;
								
								
							}
						} 
					}
				}

			}

			else{

				if(gameManager.coordinates[x, (y+1)] == 0 && targetTile == gameManager.tileCoordinates[x, (y+1)]){
					ThreatenTiles(false);
					occupiedTile.isThreatened = 0;
					ChangePosition(targetTile);
					UpdateCoordinates(x, (y + 1));
					ThreatenTiles(true);
					turnTurner = true;
						
				}
				if(targetTile.tag == "Piece"){
					if(x != 7){
						if(gameManager.coordinates[(x+1),(y+1)] < 7 && gameManager.coordinates[(x+1),(y+1)] != 0){
							//Debug.Log ("Can take piece");
							Piece targetPiece = targetTile.GetComponent<Piece>();
							Debug.Log (targetPiece + " is the target");
							if(targetPiece.isWhite != isWhite && targetPiece.x == (x+1) && targetPiece.y == (y+1)){
							
								ThreatenTiles(false);
								occupiedTile.isThreatened = 0;
								ChangePosition(targetTile);
								UpdateCoordinates(targetPiece.x, targetPiece.y);
								targetPiece.DestroyTargetPiece();
								ThreatenTiles(true);
								turnTurner = true;


							}
						}
					}

					if(x != 0){
						if(gameManager.coordinates[(x-1),(y+1)] < 7 && gameManager.coordinates[(x-1),(y+1)] != 0){
							//Debug.Log ("Can take piece");
							Piece targetPiece = targetTile.GetComponent<Piece>();
							Debug.Log (targetPiece + " is the target");
							if(targetPiece.isWhite != isWhite && targetPiece.x == (x-1) && targetPiece.y == (y+1)){

								ThreatenTiles(false);
								occupiedTile.isThreatened = 0;
								ChangePosition(targetTile);
								UpdateCoordinates(targetPiece.x, targetPiece.y);
								targetPiece.DestroyTargetPiece();
								ThreatenTiles(true);
								turnTurner = true;
								
								
							}
						} 
					}
				}

			}

		}



		if(!isWhite){
			//Debug.Log("This piece is white");
			if(y == 6){
				//Debug.Log("This is the pawn's first move");
				
				if(gameManager.coordinates[x, (y-2)] == 0 && targetTile == gameManager.tileCoordinates[x, (y-2)]){
					//Debug.Log ("There is no piece here");
					//Debug.Log("This is a valid move");
					ThreatenTiles(true);
					occupiedTile.isThreatened = 0;
					ChangePosition(targetTile);
					UpdateCoordinates(x, (y - 2));
					ThreatenTiles(false);
					turnTurner = true;

				}
				
				if(gameManager.coordinates[x, (y-1)] == 0 && targetTile == gameManager.tileCoordinates[x, (y-1)]){
					//Debug.Log("This is a space ahead of the pawn");
					//Debug.Log("This is a valid move");
					ThreatenTiles(false);
					occupiedTile.isThreatened = 0;
					ChangePosition(targetTile);
					UpdateCoordinates(x, (y - 1));
					ThreatenTiles(true);
					turnTurner = true;
				}

				if(targetTile.tag == "Piece"){
					if(x != 7){
						//Debug.Log("hi");
						if(gameManager.coordinates[(x+1),(y-1)] >= 7 && gameManager.coordinates[(x+1),(y-1)] != 0){
							//Debug.Log ("Can take piece");
							Piece targetPiece = targetTile.GetComponent<Piece>();
							Debug.Log (targetPiece + " is the target");
							if(targetPiece.isWhite != isWhite && targetPiece.x == (x+1) && targetPiece.y == (y-1)){

								ThreatenTiles(false);
								occupiedTile.isThreatened = 0;
								ChangePosition(targetTile);
								UpdateCoordinates(targetPiece.x, targetPiece.y);
								targetPiece.DestroyTargetPiece();
								ThreatenTiles(true);
								turnTurner = true;
								
								
							}
						}
					}
					
					if(x != 0){
						if(gameManager.coordinates[(x-1),(y-1)] >= 7 && gameManager.coordinates[(x-1),(y-1)] != 0){
							//Debug.Log ("Can take piece");
							Piece targetPiece = targetTile.GetComponent<Piece>();
							Debug.Log (targetPiece + " is the target");
							if(targetPiece.isWhite != isWhite && targetPiece.x == (x-1) && targetPiece.y == (y-1)){

								ThreatenTiles(false);
								occupiedTile.isThreatened = 0;
								ChangePosition(targetTile);
								UpdateCoordinates(targetPiece.x, targetPiece.y);
								targetPiece.DestroyTargetPiece();
								ThreatenTiles(true);
								turnTurner = true;
								
								
							}
						} 
					}
				}
			}
			
			else{
				
				if(gameManager.coordinates[x, (y-1)] == 0 && targetTile == gameManager.tileCoordinates[x, (y-1)]){
					//Debug.Log("This is a space ahead of the pawn");
					//Debug.Log("This is a valid move");
					ThreatenTiles(false);
					occupiedTile.isThreatened = 0;
					ChangePosition(targetTile);
					UpdateCoordinates(x, (y - 1));
					ThreatenTiles(true);
					turnTurner = true;

				}

				if(targetTile.tag == "Piece"){
					if(x != 7){
						//Debug.Log("hi");
						if(gameManager.coordinates[(x+1),(y-1)] >= 7 && gameManager.coordinates[(x+1),(y-1)] != 0){
							//Debug.Log ("Can take piece");
							Piece targetPiece = targetTile.GetComponent<Piece>();
							Debug.Log (targetPiece + " is the target");
							if(targetPiece.isWhite != isWhite && targetPiece.x == (x+1) && targetPiece.y == (y-1)){

								ThreatenTiles(false);
								occupiedTile.isThreatened = 0;
								ChangePosition(targetTile);
								UpdateCoordinates(targetPiece.x, targetPiece.y);
								targetPiece.DestroyTargetPiece();
								ThreatenTiles(true);
								turnTurner = true;
								
								
							}
						}
					}
					
					if(x != 0){
						if(gameManager.coordinates[(x-1),(y-1)] >= 7 && gameManager.coordinates[(x-1),(y-1)] != 0){
							//Debug.Log ("Can take piece");
							Piece targetPiece = targetTile.GetComponent<Piece>();
							Debug.Log (targetPiece + " is the target");
							if(targetPiece.isWhite != isWhite && targetPiece.x == (x-1) && targetPiece.y == (y-1)){

								ThreatenTiles(false);
								occupiedTile.isThreatened = 0;
								ChangePosition(targetTile);
								UpdateCoordinates(targetPiece.x, targetPiece.y);
								targetPiece.DestroyTargetPiece();
								ThreatenTiles(true);
								turnTurner = true;
								
								
							}
						} 
					}
				}

			}

		}*/


	}




	void Start () {

		InstantiateVariables ();

	}

}

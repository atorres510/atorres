﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class f_Piece : MonoBehaviour {

	public GameObject startTile;
	
	public bool isSelected;
	public bool isWhite;
	public bool turnTurner;
	public bool isRoyalty;
	
	public int x;
	public int y;
	
	public int pieceDesignator;
	//pieceID assigns a unique ID number given to each piece.  Used to identified which pieces need to be destroyed in gamemanagers
	//updatePieceSet()
	public int pieceID; 

	public enum Faction {CLAN, BATTALION, NONE};
	public Faction faction;

	public f_Tile occupiedTile;
	public f_Tile lastOccupiedTile; // used in setupManager
	public GameObject f_gameManagerObject;
	public f_GameManager f_gameManager;
	public SpriteRenderer spriteRenderer;

	public List<f_Tile> MovementTiles = new List<f_Tile>();
	
	
	public abstract void ProjectMovementTiles ();
	
	public abstract bool isTileOccupied (f_Tile t);

	public abstract bool isValidMove (f_Tile t);
	
	//public abstract void Move (GameObject targetTile);

	//public abstract void ThreatenTiles (bool isUpdate);


	public void InstantiateVariables(){
		
		f_gameManagerObject = GameObject.FindGameObjectWithTag ("f_GameManager");
		f_gameManager = f_gameManagerObject.GetComponent<f_GameManager> ();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();

	}

	//toggles collider to prevent player interaction once a piece has used its move
	public void TogglePieceCollider(bool usedTurn){
		BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
		if (!f_gameManager.isGameOver) {
				

			
			collider.enabled = !usedTurn;
			ToggleGrayScale(usedTurn);
		
		}

		else{

			collider.enabled = false;

		}

	}



	//called in toggle piece collider.  toggles spriterenderer color to communicate a piece has used up its move.
	public void ToggleGrayScale(bool toggleOn){


		if(toggleOn){

			spriteRenderer.color = Color.grey;

		}

		else{

			spriteRenderer.color = Color.white;

		}
	
	
	}


	
	
	public void StartPosition(){
		
		transform.position = startTile.transform.position;
		
		occupiedTile = startTile.GetComponent<f_Tile> ();
		occupiedTile.isOccupied = true;
		//Debug.Log (occupiedTile);
		//Debug.Log (occupiedTile.isOccupied);
		//Debug.Log (gameObject);
		//Debug.Log (occupiedTile.gameObject.transform.position);
		
	}
	
	//overload for tile use
	public void ChangePosition(f_Tile tile){
		
		if (tile != null) {

			HighlightMovementTiles(MovementTiles, false);

			if(f_gameManager.isOffline){

				occupiedTile.isOccupied = false;
				f_gameManager.coordinates[x,y] = 0;
				
				transform.position = tile.transform.position;
				occupiedTile = tile;
				occupiedTile.isOccupied = true;
				UpdateCoordinates(tile.x, tile.y);

			}



			else{


				occupiedTile.isOccupied = false;
				f_gameManager.coordinates[x,y] = 0;
				
				transform.position = tile.transform.position;
				occupiedTile = tile;
				occupiedTile.isOccupied = true;
				UpdateCoordinates(tile.x, tile.y);

			}



			f_gameManager.selectedPiece.turnTurner = true;

			f_gameManager.selectedPiece = f_gameManager.emptyPiece;




		}

		else{
			Debug.Log("No tile selected");
		}
	}

	//overload for piece use
	public void ChangePosition(f_Piece piece){

		if(piece != null && piece.isWhite != this.isWhite){
			HighlightMovementTiles(MovementTiles, false);

			transform.position = piece.transform.position;

			if(f_gameManager.isOffline){

				occupiedTile.isOccupied = false;
				occupiedTile = piece.occupiedTile;
				occupiedTile.isOccupied = true;
				UpdateCoordinates(piece.x, piece.y);
				DestroyTargetPiece(piece);
			
			}

			else{

				occupiedTile.isOccupied = false;
				occupiedTile = piece.occupiedTile;
				occupiedTile.isOccupied = true;
				UpdateCoordinates(piece.x, piece.y);
				DestroyTargetPiece(piece);

			}
			
		
			//f_gameManager.selectedPiece.MovementTiles.Clear ();
			f_gameManager.selectedPiece.turnTurner = true;
			
			f_gameManager.selectedPiece = f_gameManager.emptyPiece;
		}
		
		else {
			Debug.Log("No tile selected");
		}
	}
	
	void UpdateCoordinates(int newX, int newY){

		f_gameManager.coordinates[x, y] = 0;
		
		f_gameManager.coordinates[newX, newY] = pieceDesignator;
		
		x = newX;
		y = newY;

		//Debug.Log (f_gameManager.coordinates [newX, newY]);
		
	}
	
	
	
	public void DestroyTargetPiece(f_Piece p){
		
		//Piece p = target.GetComponent<Piece>();
		
		//f_gameManager.coordinates[p.x, p.y] = 0;
		if (p.isRoyalty) {
			if(p.isWhite){
				for(int i = 0; i < f_gameManager.whiteRoyalties.Length; i++){
					
					if(f_gameManager.whiteRoyalties[i] == p.pieceDesignator){
						
						f_gameManager.whiteRoyalties[i] = 0;
						
					}
					
				}
			}

			else{
				for(int i = 0; i < f_gameManager.blackRoyalties.Length; i++){
					
					if(f_gameManager.blackRoyalties[i] == p.pieceDesignator){
						
						f_gameManager.blackRoyalties[i] = 0;
						
					}
					
				}

			}

		}

		else{}


		GameObject g = p.gameObject;
		Destroy (g);

		


		
	}

	/*public void SyncToTray(f_Tile tile){

		//is tray tile
		if (tile.tileType == 6) {

			UI_Element tileUIElement = tile.GetComponent<UI_Element>();
			transform.position += tileUIElement.dPosition;

		}

	
	
	
	}*/




	//highlights possible moves for a piece
	public void HighlightMovementTiles(List<f_Tile> l, bool isHighlight){

		foreach (f_Tile t in l){

			SpriteRenderer s = t.GetComponent<SpriteRenderer>();


			if(isHighlight){

				if(this.isWhite){
					if(f_gameManager.coordinates[t.x,t.y] > 8){


						if(isValidMove(t)){

							s.sprite = t.highLightedHostileTexture;

						}

					
						//Debug.Log("hostile");
					}

					else if(f_gameManager.coordinates[t.x, t.y] <9 && f_gameManager.coordinates[t.x, t.y] != 0){

						//pass
						//Debug.Log("pass");
					}

					else{

						if(isValidMove(t)){

							s.sprite = t.highLightedTexture;

						}

						else{
							//pass

						}
					}
		
				}


				else{
					if(f_gameManager.coordinates[t.x,t.y] > 8){

						//pass
						
					}
					
					else if(f_gameManager.coordinates[t.x, t.y] <8 && f_gameManager.coordinates[t.x, t.y] != 0){
						//
						if(isValidMove(t)){
							
							s.sprite = t.highLightedHostileTexture;
							
						}

						
					}
					
					else{
						
						if(isValidMove(t)){
							
							s.sprite = t.highLightedTexture;
							
						}
						
						else{
							//pass
							
						}
						
					}
					
				}


			}

			else{

				s.sprite = t.defaultTexture;


			}



		}
	
		//MovementTiles.Clear();
	
	}


	bool toggle;
	
	void OnMouseOver(){

		if (f_gameManager.gameOn) {
				
			if (Input.GetMouseButtonDown (0)) {
				
				//clears movement tile projection
				if(f_gameManager.selectedPiece == this){

					this.HighlightMovementTiles(this.MovementTiles, false);
					this.MovementTiles.Clear();
					f_gameManager.selectedPiece = f_gameManager.emptyPiece;
					
				}
				
				else if (f_gameManager.isPlayer1Turn != this.isWhite){
					
					//toggles the enemy projection of movement tiles
					if(f_gameManager.selectedPiece == f_gameManager.emptyPiece){
						
						/*if(!toggle){
							
							this.ProjectMovementTiles();
							this.HighlightMovementTiles(this.MovementTiles, true);
							toggle = true;
						}
						
						else{
							
							
							this.HighlightMovementTiles(this.MovementTiles, false);
							this.MovementTiles.Clear();
							toggle = false;
							
						}*/
						
						
					}
					
					else{
						
						//check if this piece is in the range of the selected piece and check if it can be taken
						foreach (f_Tile t in f_gameManager.selectedPiece.MovementTiles){
							
							if(t.x == x && t.y == y){
								
								//block any piece not on the castle greens from taking a piece occupying the castle
								if((this.occupiedTile.tileType == 5) && (f_gameManager.selectedPiece.occupiedTile.tileType != 4)){
									
									//pass
									
								}
								//allow any piece on the castle greens to take the piece occupying the castle
								else if ((this.occupiedTile.tileType == 5) && (f_gameManager.selectedPiece.occupiedTile.tileType == 4)){
									//checks if the selected piece is an archer, and if so allows the target to be taken without moving
									if(f_gameManager.selectedPiece.pieceDesignator == 5 || f_gameManager.selectedPiece.pieceDesignator == 13){
										Debug.Log(f_gameManager.selectedPiece + " has taken " + this);
										this.occupiedTile.isOccupied = false;
										f_gameManager.coordinates[this.x, this.y] = 0;
										DestroyTargetPiece(this);
										f_gameManager.selectedPiece.HighlightMovementTiles(f_gameManager.selectedPiece.MovementTiles, false);
										//f_gameManager.selectedPiece.MovementTiles.Clear();
										f_gameManager.selectedPiece.turnTurner = true;
										f_gameManager.selectedPiece = f_gameManager.emptyPiece;
									}
									
									else{
										Debug.Log(f_gameManager.selectedPiece + " has taken " + this);
										f_gameManager.selectedPiece.ChangePosition(this);
										
									}
									
									
								}
								
								//if the piece isn't in the castle, allow it to be taken.
								else if(this.occupiedTile.tileType != 5){
									
									//checks if the selected piece is an archer, and if so allows the target to be taken without moving
									if(f_gameManager.selectedPiece.pieceDesignator == 5 || f_gameManager.selectedPiece.pieceDesignator == 13){
										Debug.Log(f_gameManager.selectedPiece + " has taken " + this);
										this.occupiedTile.isOccupied = false;
										f_gameManager.coordinates[this.x, this.y] = 0;
										DestroyTargetPiece(this);
										f_gameManager.selectedPiece.HighlightMovementTiles(f_gameManager.selectedPiece.MovementTiles, false);
										//f_gameManager.selectedPiece.MovementTiles.Clear();
										f_gameManager.selectedPiece.turnTurner = true;
										f_gameManager.selectedPiece = f_gameManager.emptyPiece;
									}
									
									else{
										if(f_gameManager.selectedPiece.isValidMove(this.occupiedTile)){
											Debug.Log(f_gameManager.selectedPiece + " has taken " + this);
											f_gameManager.selectedPiece.ChangePosition(this);
										}
									}
								}
								
								else{
									
									
								}
								
							}
							
							else{
								
							}
							
						}
						
					}
					
				}
				
				
				else{
					//select this piece if no other piece is selected and if this piece matches the turn color project movement
					if(f_gameManager.selectedPiece == f_gameManager.emptyPiece){
						
						
						if(f_gameManager.isPlayer1Turn == this.isWhite && f_gameManager.isOffline || f_gameManager.myPlayer.isWhite == this.isWhite && 
						   	f_gameManager.myPlayer.isWhite == f_gameManager.isPlayer1Turn){
							//Debug.Log("3");
				
							f_gameManager.selectedPiece = gameObject.GetComponent<f_Piece>();
							Debug.Log(f_gameManager.selectedPiece + " is selected");
							f_gameManager.selectedPiece.ProjectMovementTiles();
							f_gameManager.selectedPiece.HighlightMovementTiles(f_gameManager.selectedPiece.MovementTiles, true);
							//Debug.Log(f_gameManager.selectedPiece.MovementTiles);
							//place this projection method in gamemanager .  have gamemanager project and then clean 
							//the tiles for each piece on the board.
							
						}
						
						else {
			
						}
						
						

					}
					
					//clear piece selection
					else{

						f_gameManager.selectedPiece.HighlightMovementTiles(f_gameManager.selectedPiece.MovementTiles, false);
						f_gameManager.selectedPiece = f_gameManager.emptyPiece;
						
					}
					
					
					
					//if selected piece is empty, project this pieces movement range
					//if selected piece is not empty then check if this piece in 
					//within the selected piece's range;  if so destroy this piece
					
					
					
				}
				
			}
		
		}


	
	}

	
	
	
	

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// red = black
/// blue = white
/// </summary>



public class f_Knight : f_Piece {

	int boardLength;

	//projects movement on activation to see available moves.  
	public override void ProjectMovementTiles (){
		 

		//if in castle, only allow for movement into greens
		if (this.occupiedTile.tileType == 5) {

			f_Castle castle = occupiedTile.GetComponent<f_Castle>();
			f_Tile greens = castle.castleGreens;

			MovementTiles.Add (greens);
			//Debug.Log("hi");
		
		}

		else{

			int i = x + 1;
			int j = y + 1;
			
			while (i < boardLength && j < boardLength) {
				//Debug.Log(i);
				//Debug.Log(j);
				//Debug.Log(gameObject);
				f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
				

				if(t.tileType == 5){

					MovementTiles.Add(t);
					//Debug.Log("hi");
					break;


				}

				else{

					if(!isTileOccupied(t)){
						
						MovementTiles.Add(t);
						i++;
						j++;
					}
					
					else{
						//if(t.o
						MovementTiles.Add(t);
						break;
						
					}

				}

				
				
				
			}
			
			
			
			i = x + 1;
			j = y - 1;
			
			while (i < boardLength && j >= 0) {
				f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
				
				
				if(t.tileType == 5){
					
					MovementTiles.Add(t);
					break;
					
					
				}
				
				else{
					
					if(!isTileOccupied(t)){
						
						MovementTiles.Add(t);
						i++;
						j--;
					}
					
					else{
						//if(t.o
						MovementTiles.Add(t);
						break;
						
					}
					
				}
				
				
			}
			
			i = x - 1;
			j = y - 1;
			
			while (i >= 0 && j >= 0) {
				f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
				
				
				if(t.tileType == 5){
					
					MovementTiles.Add(t);
					break;
					
					
				}
				
				else{
					
					if(!isTileOccupied(t)){
						
						MovementTiles.Add(t);
						i--;
						j--;
					}
					
					else{
						//if(t.o
						MovementTiles.Add(t);
						break;
						
					}
					
				}
				
			}
			
			
			i = x - 1;
			j = y + 1;
			
			while (i >= 0 && j < boardLength) {
				f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
				
				
				if(t.tileType == 5){
					
					MovementTiles.Add(t);
					break;
					
					
				}
				
				else{
					
					if(!isTileOccupied(t)){
						
						MovementTiles.Add(t);
						i--;
						j++;
					}
					
					else{
						//if(t.o
						MovementTiles.Add(t);
						break;
						
					}
					
				}
			}
			
			
			i = x;
			j = y + 1;
			
			while (j < boardLength) {
				f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
				
				
				if(t.tileType == 5){
					
					MovementTiles.Add(t);
					break;
					
					
				}
				
				else{
					
					if(!isTileOccupied(t)){
						
						MovementTiles.Add(t);
						
						j++;
					}
					
					else{
						//if(t.o
						MovementTiles.Add(t);
						break;
						
					}
					
				}
				
			}
			
			
			i = x + 1;
			j = y;
			
			while (i < boardLength) {
				f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
				
				
				if(t.tileType == 5){
					
					MovementTiles.Add(t);
					break;
					
					
				}
				
				else{
					
					if(!isTileOccupied(t)){
						
						MovementTiles.Add(t);
						i++;
						
					}
					
					else{
						//if(t.o
						MovementTiles.Add(t);
						break;
						
					}
					
				}
				
				
			}
			
			
			i = x;
			j = y - 1;
			
			while (j >= 0) {
				f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
				
				if(t.tileType == 5){
					
					MovementTiles.Add(t);
					break;
					
					
				}
				
				else{
					
					if(!isTileOccupied(t)){
						
						MovementTiles.Add(t);
						
						j--;
					}
					
					else{
						//if(t.o
						MovementTiles.Add(t);
						break;
						
					}
					
				}
				
				
			}
			
			
			i = x - 1;
			j = y;
			
			while (i >= 0) {
				f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
				
				
				if(t.tileType == 5){
					
					MovementTiles.Add(t);
					break;
					
					
				}
				
				else{
					
					if(!isTileOccupied(t)){
						
						MovementTiles.Add(t);
						i--;
						
					}
					
					else{
						//if(t.o
						MovementTiles.Add(t);
						break;
						
					}
					
				}
				
			}


		}

	}





	//makes one last check to make sure the selected tile designated 
	//for movement is a valid move for this piece to make 
	public override bool isValidMove (f_Tile t){

		f_Tile i = f_gameManager.tileCoordinates [x, y].GetComponent<f_Tile> ();

		if (this.isWhite) {
			if (f_gameManager.coordinates[t.x,t.y] > 8 || f_gameManager.coordinates[t.x,t.y] == 0){

				//if this piece occupies the greens
				if(i.tileType ==4){

					if(t.tileType == 4 || t.tileType == 5 || t.tileType == 0) {
						return true;
					}

					else{

						return false;
					}

				}

				//if this piece occupies the castle, only allow movement to the greens
				else if(i.tileType == 5){
					
					if(t.tileType == 4) {
						return true;
					}
					
					else{
						
						return false;
					}
					
				}


				//if not occupying castle or greens
				else if(t.tileType == 4 || t.tileType == 0) {
					return true;
				}

				else{
					return false;
				}
			}

			else {
				return false;
			}	
		}



		else {
			if (f_gameManager.coordinates[t.x,t.y] < 9 || f_gameManager.coordinates[t.x,t.y] == 0){

				//if this piece occupies the greens
				if(i.tileType ==4){
					
					if(t.tileType == 4 || t.tileType == 5 || t.tileType == 0) {
						return true;
					}
					
					else{
						
						return false;
					}
					
				}

				//if this piece occupies the castle, only allow movement to the greens
				else if(i.tileType == 5){
					
					if(t.tileType == 4) {
						return true;
					}
					
					else{
						
						return false;
					}
					
				}
				//if not occupying castle or greens
				else if(t.tileType == 4 || t.tileType == 0) {
					return true;
				}
				
				else{
					return false;
				}
			}
			
			else {
				return false;
			}	
		
		}

	}
	



	//checks if a tile is a special tile or occupied by an enemy/friendly unit. 
	//used in ProjectMovementTiles()
	public override bool isTileOccupied(f_Tile t){

		f_Tile i = f_gameManager.tileCoordinates [t.x, t.y].GetComponent<f_Tile> ();
		f_Tile j = f_gameManager.tileCoordinates [x, y].GetComponent<f_Tile> ();

		//if selected piece occupies the greens
		if (j.tileType == 4) {
				
			if (i.tileType == 2 || i.tileType == 3 || i.isOccupied) {

				return true;

			} 
			else {
				return false;
			}
		
		}

		//if selected piece occupies the castle only allow movement to the greens
		else if(j.tileType == 5){

			if(i.tileType == 2 || i.tileType == 3 || i.tileType == 0 || i.tileType == 5){

				return true;

			}

			else {

				return false;
			}

		}

		//if not occupying castle or greens
		else {
				
			if (i.tileType != 0 || i.isOccupied) {
				return true;
			} 
			
			else {
				return false;
			}
		
		}

	}







	// Use this for initialization
	void Start () {
	
		InstantiateVariables ();
		float l = Mathf.Sqrt (f_gameManager.tileCoordinates.Length);
		boardLength = (int)l;
		//Debug.Log (boardLength);


	}
	

}

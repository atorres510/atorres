using UnityEngine;
using System.Collections;

public class f_Archer : f_Piece {

	int boardLength;




	public override void ProjectMovementTiles (){

		if (this.occupiedTile.tileType == 5) {
			
			f_Castle castle = occupiedTile.GetComponent<f_Castle>();
			f_Tile greens = castle.castleGreens;
			
			MovementTiles.Add (greens);
			//Debug.Log("hi");
			
		}
		
		
		else {
			
			int i = x + 1;
			int j = y + 1;

			
			while (i < (x + 4) && j < (y + 4)) {
				if(i < 0 || j < 0 || i >= boardLength || j >= boardLength){
					break;
				}
				
				else{
					
					f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();

					//if tile in question is the castle, add it and then stop 
					if(t.tileType == 5){
						
						MovementTiles.Add(t);
						//Debug.Log("hi");
						break;
						
						
					}

					//if tile in question is the greens, 
					else if(t.tileType == 4){

						//adds the greens to movement tiles

						MovementTiles.Add (t);

						//check all tiles after the greens if they are occupied

						for(int a = i; a < (x + 4); a++){

							for(int b = j; b < (y + 4); b++){
								t = f_gameManager.tileCoordinates[a,b].GetComponent<f_Tile>();
								if(t.isOccupied){
									//if a tile that is occupied by a piece is found, add the tile to movement tiles and break the loop
									MovementTiles.Add(t);
									break;

								}

							}

						} 

						break;
					}
					
					else {
						
						if(!isTileOccupied(t)){
							
							MovementTiles.Add(t);
							i++;
							j++;
						}
						
						else{
							
							MovementTiles.Add(t);
							break;
						}
						
						
					}
					
					
				}
				
				
			}
			
			
			
			i = x + 1;
			j = y - 1;
			
			while (i < (x + 4) && j > (y - 4)) {
				if(i < 0 || j < 0 || i >= boardLength || j >= boardLength){
					break;
				}
				
				else{
					
					f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
					
					if(t.tileType == 5){
						
						MovementTiles.Add(t);
						//Debug.Log("hi");
						break;
						
						
					}

					//if tile in question is the greens, 
					else if(t.tileType == 4){
						
						//adds the greens to movement tiles
						
						MovementTiles.Add (t);
						
						//check all tiles after the greens if they are occupied
						
						for(int a = i; a < (x + 4); a++){
							
							for(int b = j; b > (y - 4); b--){
								t = f_gameManager.tileCoordinates[a,b].GetComponent<f_Tile>();
								if(t.isOccupied){
									//if a tile that is occupied by a piece is found, add the tile to movement tiles and break the loop
									MovementTiles.Add(t);
									break;
									
								}
								
							}
							
						} 
						
						break;
					}
					
					else {
						
						if(!isTileOccupied(t)){
							
							MovementTiles.Add(t);
							i++;
							j--;
						}
						
						else{
							
							MovementTiles.Add(t);
							break;
						}
						
					}
					
					
					
					
				}
				
				
			}
			
			i = x - 1;
			j = y - 1;
			
			while (i > (x - 4) && j > (y - 4)) {
				if(i < 0 || j < 0 || i >= boardLength || j >= boardLength){
					break;
				}
				
				else{
					
					f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
					
					if(t.tileType == 5){
						
						MovementTiles.Add(t);
						//Debug.Log("hi");
						break;
						
						
					}

					//if tile in question is the greens, 
					else if(t.tileType == 4){
						
						//adds the greens to movement tiles
						
						MovementTiles.Add (t);
						
						//check all tiles after the greens if they are occupied
						
						for(int a = i; a > (x - 4); a++){
							
							for(int b = j; b > (y - 4); b--){
								t = f_gameManager.tileCoordinates[a,b].GetComponent<f_Tile>();
								if(t.isOccupied){
									//if a tile that is occupied by a piece is found, add the tile to movement tiles and break the loop
									MovementTiles.Add(t);
									break;
									
								}
								
							}
							
						} 
						
						break;
					}
					
					else {
						
						if(!isTileOccupied(t)){
							
							MovementTiles.Add(t);
							i--;
							j--;
						}
						
						else{
							
							MovementTiles.Add(t);
							break;
						}
						
					}
					
					
					
				}
				
			}
			
			
			i = x - 1;
			j = y + 1;
			
			while (i > (x - 4) && j < (y + 4)) {
				if(i < 0 || j < 0 || i >= boardLength || j >= boardLength){
					break;
				}
				
				else{
					
					f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
					
					if(t.tileType == 5){
						
						MovementTiles.Add(t);
						//Debug.Log("hi");
						break;
						
						
					}

					//if tile in question is the greens, 
					else if(t.tileType == 4){
						
						//adds the greens to movement tiles
						
						MovementTiles.Add (t);
						
						//check all tiles after the greens if they are occupied
						
						for(int a = i; a > (x - 4); a--){
							
							for(int b = j; b < (y + 4); b++){
								t = f_gameManager.tileCoordinates[a,b].GetComponent<f_Tile>();
								if(t.isOccupied){
									//if a tile that is occupied by a piece is found, add the tile to movement tiles and break the loop
									MovementTiles.Add(t);
									break;
									
								}
								
							}
							
						} 
						
						break;
					}

					
					else {
						
						if(!isTileOccupied(t)){
							
							MovementTiles.Add(t);
							i--;
							j++;
						}
						
						else{
							
							MovementTiles.Add(t);
							break;
						}
						
						
					}
					
					
					
					
				}
			}
			
			
			i = x;
			j = y + 1;
			
			while (j < (y + 4)) {
				if(i < 0 || j < 0 || i >= boardLength || j >= boardLength){
					break;
				}
				
				else{
					
					f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
					
					if(t.tileType == 5){
						
						MovementTiles.Add(t);
						//Debug.Log("hi");
						break;
						
						
					}

					//if tile in question is the greens, 
					else if(t.tileType == 4){
						
						//adds the greens to movement tiles
						
						MovementTiles.Add (t);
						
						//check all tiles after the greens if they are occupied

							for(int b = j; b < (y + 4); b++){
								t = f_gameManager.tileCoordinates[x,b].GetComponent<f_Tile>();
								if(t.isOccupied){
									//if a tile that is occupied by a piece is found, add the tile to movement tiles and break the loop
									MovementTiles.Add(t);
									break;
									
								}
								
							}

						break;
					}
					
					else {
						
						if(!isTileOccupied(t)){
							
							MovementTiles.Add(t);
							
							j++;
						}
						
						else{
							
							MovementTiles.Add(t);
							break;
						}
						
					}
					
					
					
					
				}
				
				
			}
			
			
			i = x + 1;
			j = y;
			
			while (i < (x + 4)) {
				//Debug.Log(i);
				if(i < 0 || j < 0 || i >= boardLength || j >= boardLength){
					break;
				}
				
				else{
					
					f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
					
					if(t.tileType == 5){
						
						MovementTiles.Add(t);
						//Debug.Log("hi");
						break;
						
						
					}

					//if tile in question is the greens, 
					else if(t.tileType == 4){
						
						//adds the greens to movement tiles
						
						MovementTiles.Add (t);
						
						//check all tiles after the greens if they are occupied
						
						for(int a = i; a < (x + 4); a++){
						
							t = f_gameManager.tileCoordinates[a,j].GetComponent<f_Tile>();
							if(t.isOccupied){
								//if a tile that is occupied by a piece is found, add the tile to movement tiles and break the loop
								MovementTiles.Add(t);
								break;
								
							}
								

							
						} 
						
						break;
					}
					
					else {
						
						if(!isTileOccupied(t)){
							
							MovementTiles.Add(t);
							i++;
							
						}
						
						else{
							
							MovementTiles.Add(t);
							break;
						}
						
					}
					
					
					
				}
				
				
			}
			
			
			i = x;
			j = y - 1;
			
			while (j > (y - 4)) {
				//if out of bounds
				if(i < 0 || j < 0 || i >= boardLength || j >= boardLength){
					break;
				}
				
				else{
					
					f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
					
					if(t.tileType == 5){
						
						MovementTiles.Add(t);
						//Debug.Log("hi");
						break;
						
						
					}

					else if(t.tileType == 4){
						
						//adds the greens to movement tiles
						
						MovementTiles.Add (t);
						
						//check all tiles after the greens if they are occupied
						
						for(int b = j; b < (y - 4); b--){
							
							t = f_gameManager.tileCoordinates[x,j].GetComponent<f_Tile>();
							if(t.isOccupied){
								//if a tile that is occupied by a piece is found, add the tile to movement tiles and break the loop
								MovementTiles.Add(t);
								break;
								
							}
							
							
							
						} 
						
						break;
						
					} 
					
					else {
						
						if(!isTileOccupied(t)){
							
							MovementTiles.Add(t);
							
							j--;
						}
						
						else{
							
							MovementTiles.Add(t);
							break;
						}
						
					}
					
					
					
				}
				
				
			}
			
			
			i = x - 1;
			j = y;
			
			while (i > (x - 4)) {
				if(i < 0 || j < 0 || i >= boardLength || j >= boardLength){
					break;
				}
				
				else{
					
					f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
					
					if(t.tileType == 5){
						
						MovementTiles.Add(t);
						//Debug.Log("hi");
						break;
						
						
					}

					else if(t.tileType == 4){
						
						//adds the greens to movement tiles
						
						MovementTiles.Add (t);
						
						//check all tiles after the greens if they are occupied
						
						for(int a = i; a > (x - 4); a--){
							
							t = f_gameManager.tileCoordinates[a,j].GetComponent<f_Tile>();
							if(t.isOccupied){
								//if a tile that is occupied by a piece is found, add the tile to movement tiles and break the loop
								MovementTiles.Add(t);
								break;
								
							}
							
							
							
						} 
						
						break;
					}
					
					else {
						
						if(!isTileOccupied(t)){
							
							MovementTiles.Add(t);
							i--;
							
						}
						
						else{
							
							MovementTiles.Add(t);
							break;
						}
						
					}
					
				}
				
			}
			
		}
	
	}



	
	public override bool isTileOccupied (f_Tile t){

		//represents tile in question
		f_Tile i = f_gameManager.tileCoordinates [t.x, t.y].GetComponent<f_Tile> ();

		//represents currently occupied tile of the selected piece (this piece)
		f_Tile j = f_gameManager.tileCoordinates [x, y].GetComponent<f_Tile> ();


		//if selected piece occupies the greens
		if (j.tileType == 4) {
			
			if (i.tileType == 3 || i.isOccupied) {
				
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
		else{
			
			if (i.tileType == 3 || i.tileType == 4 || i.tileType == 5 ||  i.isOccupied) {
				return true;
			} 
			
			else {
				return false;
			}
			
		}
		

	}







	public override bool isValidMove (f_Tile t){

		//represents currently occupied tile by this piece
		f_Tile i = f_gameManager.tileCoordinates [x, y].GetComponent<f_Tile> ();


		if (this.isWhite) {
			if (f_gameManager.coordinates[t.x,t.y] > 8 || f_gameManager.coordinates[t.x,t.y] == 0){
				//if this piece occupies the greens
				if(i.tileType == 4){

					//determine if the castle is friendly or not and allow movement accordingly
					if(t.tileType == 5){

						f_Castle c = t.GetComponent<f_Castle>();
						//if the castle is the same color as archer, do not allow in
						if(c.isWhite){
							
							return false;
							
						}

						else{

							return true;

						}

					}



					//allow the piece to move anywhere else
					else {

						if(t.tileType == 4 || t.tileType == 0 || t.tileType == 2) {
							return true;
						}
						
						else{
							return false;
						}

					}



				}



				
				//if this piece occupies the castle, only allow movement to the greens.  Archer should not require this
				else if(i.tileType == 5){

					if(t.tileType == 4) {
						return true;
					}
					
					else{
						
						return false;
					}

				}
				
				
				//if not occupying castle or greens
				else if(t.tileType == 4 || t.tileType == 0 ||t.tileType == 2) {
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
				if(i.tileType == 4){
					
					//determine if the castle is friendly or not and allow movement accordingly
					if(t.tileType == 5){
						
						f_Castle c = t.GetComponent<f_Castle>();
						//if the castle is the same color as archer, do not allow in
						if(!c.isWhite){
							
							return false;
							
						}
						
						else{
							
							return true;
							
						}
						
					}
					
					
					
					//allow the piece to move anywhere else
					else {
						
						if(t.tileType == 4 || t.tileType == 0 || t.tileType == 2) {
							return true;
						}
						
						else{
							return false;
						}
						
					}
					
					
					
				}
				
				
				
				
				//if this piece occupies the castle, only allow movement to the greens.  Archer should not require this
				else if(i.tileType == 5){
					
					if(t.tileType == 4) {
						return true;
					}
					
					else{
						
						return false;
					}
					
				}
				
				
				//if not occupying castle or greens
				else if(t.tileType == 4 || t.tileType == 0 ||t.tileType == 2) {
					return true;
				}
				
				else{
					return false;
				}
			}
			
			else {
				return false;
			}	

				/*
				//if this piece occupies the greens
				if(i.tileType == 4){
					
					if(t.tileType == 4 || t.tileType == 5 || t.tileType == 0 || t.tileType == 2) {
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
				else if(t.tileType == 4 || t.tileType == 0 || t.tileType == 2) {
					return true;
				}
				
				else{
					return false;
				}
			}
			
			else {
				return false;
			}*/	
			
		}
		

	}








	void Start () {

		InstantiateVariables ();
		float l = Mathf.Sqrt (f_gameManager.tileCoordinates.Length);
		boardLength = (int)l;

	}



	void Update(){
		
		if (f_gameManager.gameOn) {
			TogglePieceCollider (turnTurner);
		}

		/*else{

			SyncToTray(occupiedTile);

		}*/

	

		
	}

	

}

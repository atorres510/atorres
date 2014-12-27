using UnityEngine;
using System.Collections;

public class Rook : Piece {

	public override bool isValidMove (bool checkForBlock){

		int total = 0; 
		int i = x;
		int j = y + 1;
		
		while (j < 8 && total == 0) {
			
			if(!MyKingIsInCheck(i, j, checkForBlock)){
				total = total + gameManager.coordinates[i, j];

				j++;
			}
			
			else{
				
				return false;
				
			}
			
		}
		
		total = 0; 
		i = x + 1;
		j = y;
		
		while (i < 8 && total == 0) {
			
			if(!MyKingIsInCheck(i, j, checkForBlock)){
				total = total + gameManager.coordinates[i, j];
				i++;

			}
			
			else{
				
				return false;
				
			}
			
		}
		
		total = 0; 
		i = x;
		j = y - 1;
		
		while (j >= 0 && total == 0) {
			
			if(!MyKingIsInCheck(i, j, checkForBlock)){
				total = total + gameManager.coordinates[i, j];
			
				j--;
			}
			
			else{
				
				return false;
				
			}
			
		}
		
		total = 0; 
		i = x - 1;
		j = y;
		
		while (i >= 0 && total == 0) {
			
			if(!MyKingIsInCheck(i, j, checkForBlock)){
				total = total + gameManager.coordinates[i, j];
				i--;

			}
			
			else{
				
				return false;
				
			}
			
		}

		return true;
	}

	public override void ThreatenTiles(bool isUpdate){
	
		int total = 0;
		int i = x + 1;
		/*if (i < 8) {
			total = gameManager.coordinates [i, y];
		} 
		else {
			total = 1;
		}*/
	
		while(total == 0 && i < 8){
			//Debug.Log (i);
			Tile t = gameManager.tileCoordinates[i, y].GetComponent<Tile>();

			if(!isUpdate){

				if(t.isThreatened == 0){
					if(isWhite){

						t.isThreatened = 1;
						total = total + gameManager.coordinates[i, y];

					
					}

					if(!isWhite){

						t.isThreatened = 2;
						total = total + gameManager.coordinates[i, y];


					}
				}
				else{
	
					t.isThreatened = 0;
					total = total + gameManager.coordinates[i, y];

				}

				i++;
				

			}

			else{ //Update

				if(t.isThreatened < 3 && t.isThreatened != 0){

					if(isWhite && t.isThreatened == 2 || !isWhite && t.isThreatened == 1){

						t.isThreatened = 3;
						total = total + gameManager.coordinates[i, y];
					

					}

					else{

						total = total + gameManager.coordinates[i, y];


					}
					
				}

				if(t.isThreatened == 0){
					if(isWhite){
						t.isThreatened = 1;
						total = total + gameManager.coordinates[i, y];

						
					}
					
					if(!isWhite){
						t.isThreatened = 2;
						total = total + gameManager.coordinates[i, y];

						
					}
					
				}

				else{
					//Debug.Log(i);

					total = total + gameManager.coordinates[i, y];

				}
				//Debug.Log(i);
				i++;
			}

		}

		total = 0;
		i = y + 1;
		/*if (i < 8) {
			total = gameManager.coordinates [x, i];
		}
		else {
			total = 1;
		}*/
		
		while(total == 0 && i < 8){
			
			Tile t = gameManager.tileCoordinates[x, i].GetComponent<Tile>();
			
			if(!isUpdate){
				if(t.isThreatened == 0){
					if(isWhite){
						t.isThreatened = 1;
						total = total + gameManager.coordinates[x, i];

						
					}
					
					else{
						t.isThreatened = 2;
						total = total + gameManager.coordinates[x, i];

						
					}
				}
				else{

					t.isThreatened = 0;
					total = total + gameManager.coordinates[x, i];


				}

				i++;
			}
			
			else{ // Update

				if(t.isThreatened < 3 && t.isThreatened != 0){
					
					if(isWhite && t.isThreatened == 2 || !isWhite && t.isThreatened == 1){
						
						t.isThreatened = 3;
						total = total + gameManager.coordinates[x, i];

						
					}
					
					else{
						
						total = total + gameManager.coordinates[x, i];

						
					}
					
				}

				if(t.isThreatened == 0){
					if(isWhite){
						t.isThreatened = 1;
						total = total + gameManager.coordinates[x, i];

						
					}
					
					else{
						t.isThreatened = 2;
						total = total + gameManager.coordinates[x, i];

						
					}
					
				}
				
				else{
					
					total = total + gameManager.coordinates[x, i];

				}
				i++;
				
			}
			
		}



		total = 0;
		i = x - 1;
		/*if (i >= 0) {
			total = gameManager.coordinates [i, y];
		}
		else {
			total = 1;
		}*/

		while(total == 0 && i >= 0){

			Tile t = gameManager.tileCoordinates[i, y].GetComponent<Tile>();

			if(!isUpdate){
				if(t.isThreatened == 0){
					if(isWhite){
						t.isThreatened = 1;
						total = total + gameManager.coordinates[i, y];

						
					}
					
					else{
						t.isThreatened = 2;
						total = total + gameManager.coordinates[i, y];

						
					}
				}
				else{
					
					t.isThreatened = 0;
					total = total + gameManager.coordinates[i, y];

					
				}
				i--;
			}
			
			else{ // UPDATE

				if(t.isThreatened < 3 && t.isThreatened != 0){
					
					if(isWhite && t.isThreatened == 2 || !isWhite && t.isThreatened == 1){
						
						t.isThreatened = 3;
						total = total + gameManager.coordinates[i, y];

						
					}
					
					else{
						
						total = total + gameManager.coordinates[i, y];

						
					}

					
				}

				if(t.isThreatened == 0){
					if(isWhite){
						t.isThreatened = 1;
						total = total + gameManager.coordinates[i, y];

						
					}
					
					else{
						t.isThreatened = 2;
						total = total + gameManager.coordinates[i, y];

						
					}

				}
				
				else{
					
					total = total + gameManager.coordinates[i, y];

				}

				i--;
				
			}

		}


		total = 0;
		i = y - 1;
		/*if (i >= 0) {
			total = gameManager.coordinates [x, i];
		}
		else {
			total = 1;
		}*/
		
		while(total == 0 && i >= 0){
			//Debug.Log(i);

			//Debug.Log(total);
			Tile t = gameManager.tileCoordinates[x, i].GetComponent<Tile>();
			//Debug.Log(gameManager.coordinates[x, i]);
			
			if(!isUpdate){
				if(t.isThreatened == 0){
					if(isWhite){
						t.isThreatened = 1;
						total = total + gameManager.coordinates[x, i];

						
					}
					
					else{
					//Debug.Log(total);
						t.isThreatened = 2;
						total = total + gameManager.coordinates[x, i];
					//	Debug.Log (total);

						
					}
				}
				else{
					//Debug.Log("Hi");
					t.isThreatened = 0;
					total = total + gameManager.coordinates[x, i];

					
				}

				//Debug.Log(total);

			}
			
			else{ // UPDATE

				if(t.isThreatened < 3 && t.isThreatened != 0){
					
					if(isWhite && t.isThreatened == 2 || !isWhite && t.isThreatened == 1){
						
						t.isThreatened = 3;
						total = total + gameManager.coordinates[x, i];

						
					}
					
					else{
						
						total = total + gameManager.coordinates[x, i];

						
					}
				}

				if(t.isThreatened == 0){
					if(isWhite){
						t.isThreatened = 1;
						total = total + gameManager.coordinates[x, i];

						
					}
					
					else{
						t.isThreatened = 2;
						total = total + gameManager.coordinates[x, i];

						
					}
					
				}
				
				else{
					
					total = total + gameManager.coordinates[x, i];

				}

			}

			i--;

		}

	}


	/*public void RookThreatAssignment(int direction, int axis, bool isUpdate){

		//direction index:  1 = up, 2 = right, 3 = down, 4 = left

		if (isUpdate) {
			if(direction%2 == 0){
				int i = x;
				Tile t = gameManager.tileCoordinates[i, y];






			}
		} 



		else {
				
		}

	
	
	
	
	}*/







	public override void Move(GameObject targetTile){

		if (targetTile.tag == "Tile") {
				
			Tile t = targetTile.GetComponent<Tile>();

			if(t.x == x){

				if(t.y > y){

					int total = 0;

					for(int i = t.y; i > y; i--){

						total = total + gameManager.coordinates[x, i];

					}

					if(total == 0){
						ThreatenTiles(false);
						occupiedTile.isThreatened = 0;
						ChangePosition(targetTile);
						UpdateCoordinates(t.x, t.y);
						ThreatenTiles(true);
						turnTurner = true;

					}

				}


				if(t.y < y){

					int total = 0;
					
					for(int i = t.y; i < y; i++){
						
						total = total + gameManager.coordinates[x, i];
						
					}
					
					if(total == 0){
						ThreatenTiles(false);
						occupiedTile.isThreatened = 0;
						ChangePosition(targetTile);
						UpdateCoordinates(t.x, t.y);
						ThreatenTiles (true);
						turnTurner = true;
						
					}
				
				}

			}


			if(t.y == y){

				if(t.x > x){

					int total = 0;
					
					for(int i = t.x; i > x; i--){
						
						total = total + gameManager.coordinates[i, y];
					
					}
					
					if(total == 0){
						ThreatenTiles(false);
						occupiedTile.isThreatened = 0;
						ChangePosition(targetTile);
						UpdateCoordinates(t.x, t.y);
						ThreatenTiles(true);
						turnTurner = true;
						
					}
				}


				if(t.x < x){

					int total = 0;
					
					for(int i = t.x; i < x; i++){

						total = total + gameManager.coordinates[i, y];
						
					}
					
					if(total == 0){
						ThreatenTiles(false);
						occupiedTile.isThreatened = 0;
						ChangePosition(targetTile);
						UpdateCoordinates(t.x, t.y);
						ThreatenTiles(true);
						turnTurner = true;
						
					}

				}

			}

		}


		if (targetTile.tag == "Piece") {
				
			Piece p = targetTile.GetComponent<Piece>();

			if(p.x == x){

				if(p.y > y){
					
					int total = 0;
					
					for(int i = (p.y - 1); i > y; i--){
						
						total = total + gameManager.coordinates[x, i];
						
					}
					
					if(total == 0){
						ThreatenTiles(false);
						occupiedTile.isThreatened = 0;
						ChangePosition(targetTile);
						UpdateCoordinates(p.x, p.y);
						p.DestroyTargetPiece();
						ThreatenTiles(true);
						turnTurner = true;
						
					}
					
				}

				
				if(p.y < y){
					
					int total = 0;

					for(int i = (p.y +1); i < y; i++){
						
						total = total + gameManager.coordinates[x, i];
						
					}
					
					if(total == 0){
						ThreatenTiles(false);
						occupiedTile.isThreatened = 0;
						ChangePosition(targetTile);
						UpdateCoordinates(p.x, p.y);
						p.DestroyTargetPiece();
						ThreatenTiles(true);
						turnTurner = true;
						
					}
					
				}
				
			}


			if(p.y == y){
				
				if(p.x > x){

					int total = 0;
					
					for(int i = (p.x - 1); i > x; i--){
						
						total = total + gameManager.coordinates[i, y];
						
					}
					
					if(total == 0){
						ThreatenTiles(false);
						occupiedTile.isThreatened = 0;
						ChangePosition(targetTile);
						UpdateCoordinates(p.x, p.y);
						p.DestroyTargetPiece();
						ThreatenTiles(true);
						turnTurner = true;
						
					}
				}


				if(p.x < x){
					
					int total = 0;
					
					for(int i = (p.x + 1); i < x; i++){
						
						total = total + gameManager.coordinates[i, y];
						
					}
					
					if(total == 0){
						ThreatenTiles(false);
						occupiedTile.isThreatened = 0;
						ChangePosition(targetTile);
						UpdateCoordinates(p.x, p.y);
						p.DestroyTargetPiece();
						ThreatenTiles(true);
						turnTurner = true;
						
					}
					
				}
				
			}
	
		}

	}
	

	





	void Start () {

		InstantiateVariables();

		ThreatenTiles (true);


	
	}
	

}

using UnityEngine;
using System.Collections;

public class Queen : Piece {

	public override bool isValidMove (bool checkForBlock){
		//Check upper-right diagonal spaces.
		int total = 0; 
		int i = x + 1;
		int j = y + 1;
		
		while (i < 8 && j < 8 && total == 0) {
			
			if(!MyKingIsInCheck(i, j, checkForBlock)){
				total = total + gameManager.coordinates[i, j];
				i++;
				j++;
			}
			
			else{
				
				return false;
				
			}
			
		}
		
		
		total = 0; 
		i = x + 1;
		j = y - 1;
		
		while (i < 8 && j >= 0 && total == 0) {
			
			if(!MyKingIsInCheck(i, j,checkForBlock)){
				total = total + gameManager.coordinates[i, j];
				i++;
				j--;
			}
			
			else{
				
				return false;
				
			}
			
		}
		
		total = 0; 
		i = x - 1;
		j = y - 1;
		
		while (i >= 0 && j >= 0 && total == 0) {
			
			if(!MyKingIsInCheck(i, j, checkForBlock)){
				total = total + gameManager.coordinates[i, j];
				i--;
				j--;
			}
			
			else{
				
				return false;
				
			}
			
		}
		
		total = 0; 
		i = x - 1;
		j = y + 1;
		
		while (i >= 0 && j < 8 && total == 0) {
			
			if(!MyKingIsInCheck(i, j, checkForBlock)){
				total = total + gameManager.coordinates[i, j];
				i--;
				j++;
				
			}
			
			else{
				
				return false;
				
			}
			
		}

		total = 0; 
		i = x;
		j = y + 1;
		
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
		int j = y + 1;
		
		while (i < 8 && j < 8 && total == 0) {
			
			ThreatAssignment(isUpdate, i, j);
			total = total + gameManager.coordinates[i, j];
			i++;
			j++;
			
		}
		
		
		total = 0; 
		i = x + 1;
		j = y - 1;
		
		while (i < 8 && j >= 0 && total == 0) {
			
			ThreatAssignment(isUpdate, i, j);
			total = total + gameManager.coordinates[i, j];
			i++;
			j--;
			
		}
		
		total = 0; 
		i = x - 1;
		j = y - 1;
		
		while (i >= 0 && j >= 0 && total == 0) {
			
			ThreatAssignment(isUpdate, i, j);
			total = total + gameManager.coordinates[i, j];
			i--;
			j--;
			
		}
		
		total = 0; 
		i = x - 1;
		j = y + 1;
		
		while (i >= 0 && j < 8 && total == 0) {
			
			ThreatAssignment (isUpdate, i, j);
			total = total + gameManager.coordinates [i, j];
			i--;
			j++;
		}

		//Check upper Y axis
		total = 0; 
		i = x;
		j = y + 1;
		
		while (j < 8 && total == 0) {
			
			ThreatAssignment (isUpdate, i, j);
			total = total + gameManager.coordinates [i, j];
			j++;

		}

		total = 0; 
		i = x + 1;
		j = y;
		
		while (i < 8 && total == 0) {
			
			ThreatAssignment (isUpdate, i, j);
			total = total + gameManager.coordinates [i, j];
			i++;
			
		}

		total = 0; 
		i = x;
		j = y - 1;
		
		while (j >= 0 && total == 0) {
			
			ThreatAssignment (isUpdate, i, j);
			total = total + gameManager.coordinates [i, j];
			j--;
			
		}

		total = 0; 
		i = x - 1;
		j = y;
		
		while (i >= 0 && total == 0) {
			
			ThreatAssignment (isUpdate, i, j);
			total = total + gameManager.coordinates [i, j];
			i--;
			
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
						ThreatenTiles(true);
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

			if(t.y > y){
				
				if(t.x > x){
					
					if((t.x - x) == (t.y - y)){
						int total = 0;
						int i = t.x;
						int j = t.y;
						
						while(i > x || j > y){
							
							total = total + gameManager.coordinates[i,j];
							i--;
							j--;
							
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
				
				if(t.x < x){
					
					if((x - t.x) == (t.y - y)){
						int total = 0;
						int i = t.x;
						int j = t.y;
						
						while(i < x || j > y){
							
							total = total + gameManager.coordinates[i,j];
							i++;
							j--;
							
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
			
			if(t.y < y){
				
				if(t.x > x){
					
					if((t.x - x) == (y - t.y)){
						int total = 0;
						int i = t.x;
						int j = t.y;
						
						while(i > x || j < y){

							total = total + gameManager.coordinates[i,j];
							i--;
							j++;
							
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
				
				
				if(t.x < x){
					if((x - t.x) == (y - t.y)){
						int total = 0;
						int i = t.x;
						int j = t.y;
						
						while(i < x || j < y){
							
							total = total + gameManager.coordinates[i,j];
							i++;
							j++;
							
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

			if(p.y > y){
				
				if(p.x > x){
					
					if((p.x - x) == (p.y - y)){
						int total = 0;
						int i = p.x - 1;
						int j = p.y - 1;
						
						while(i > x || j > y){
							
							total = total + gameManager.coordinates[i,j];
							i--;
							j--;
							
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
				
				if(p.x < x){
					
					if((x - p.x) == (p.y - y)){
						int total = 0;
						int i = p.x + 1;
						int j = p.y - 1;
						
						while(i < x || j > y){
							
							total = total + gameManager.coordinates[i,j];
							i++;
							j--;
							
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
			
			if(p.y < y){
				
				if(p.x > x){
					
					if((p.x - x) == (y - p.y)){
						int total = 0;
						int i = p.x - 1;
						int j = p.y + 1;
						
						while(i > x || j < y){
							
							total = total + gameManager.coordinates[i,j];
							i--;
							j++;
							
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
				
				
				if(p.x < x){
					if((x - p.x) == (y - p.y)){
						int total = 0;
						int i = p.x + 1;
						int j = p.y + 1;
						
						while(i < x || j < y){
							
							total = total + gameManager.coordinates[i,j];
							i++;
							j++;
							
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

	}

	void Start () {

		InstantiateVariables ();
		ThreatenTiles (true);
	
	}
	

}

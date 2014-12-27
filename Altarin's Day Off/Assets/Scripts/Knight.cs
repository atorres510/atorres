using UnityEngine;
using System.Collections;

public class Knight : Piece {

	public override bool isValidMove (bool checkForBlock){

		int newX = x + 2;
		int newY = y + 1;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			if(!MyKingIsInCheck(newX, newY, checkForBlock)){

			}
			
			else{
				
				return false;
				
			}
		}
		
		
		newX = x - 2;
		newY = y + 1;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			if(!MyKingIsInCheck(newX, newY, checkForBlock)){
				
			}
			
			else{
				
				return false;
				
			}
		}
		
		
		newX = x + 1;
		newY = y + 2;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			if(!MyKingIsInCheck(newX, newY, checkForBlock)){
				
			}
			
			else{
				
				return false;
				
			}
		}
		
		
		newX = x - 1;
		newY = y + 2;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			if(!MyKingIsInCheck(newX, newY, checkForBlock)){
				
			}
			
			else{
				
				return false;
				
			}
		}
		
		
		newX = x + 2;
		newY = y - 1;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			if(!MyKingIsInCheck(newX, newY, checkForBlock)){
				
			}
			
			else{
				
				return false;
				
			}
		}
		
		
		newX = x - 2;
		newY = y - 1;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			if(!MyKingIsInCheck(newX, newY, checkForBlock)){
				
			}
			
			else{
				
				return false;
				
			}
		}
		
		
		newX = x + 1;
		newY = y - 2;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			if(!MyKingIsInCheck(newX, newY, checkForBlock)){
				
			}
			
			else{
				
				return false;
				
			}
		}
		
		
		newX = x - 1;
		newY = y - 2;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			if(!MyKingIsInCheck(newX, newY, checkForBlock)){
				
			}
			
			else{
				
				return false;
				
			}
		}
		return true;
	}

	public override void ThreatenTiles(bool isUpdate){

		int newX = x + 2;
		int newY = y + 1;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			ThreatAssignment (isUpdate, newX, newY);
		}


		newX = x - 2;
		newY = y + 1;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			ThreatAssignment (isUpdate, newX, newY);
		}

		
		newX = x + 1;
		newY = y + 2;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			ThreatAssignment (isUpdate, newX, newY);
		}

		
		newX = x - 1;
		newY = y + 2;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			ThreatAssignment (isUpdate, newX, newY);
		}

		
		newX = x + 2;
		newY = y - 1;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			ThreatAssignment (isUpdate, newX, newY);
		}

		
		newX = x - 2;
		newY = y - 1;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			ThreatAssignment (isUpdate, newX, newY);
		}

		
		newX = x + 1;
		newY = y - 2;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			ThreatAssignment (isUpdate, newX, newY);
		}

		
		newX = x - 1;
		newY = y - 2;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			ThreatAssignment (isUpdate, newX, newY);
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



	public override void Move (GameObject targetTile){

		if (targetTile.tag == "Tile") {
				
			Tile t = targetTile.GetComponent<Tile>();

			if(Mathf.Abs((x - t.x)) == 1f && Mathf.Abs((y - t.y)) == 2f){

				ThreatenTiles(false);
				occupiedTile.isThreatened = 0;
				ChangePosition(targetTile);
				UpdateCoordinates(t.x, t.y);
				ThreatenTiles(true);
				turnTurner = true;

			}

			if(Mathf.Abs((x - t.x)) == 2f && Mathf.Abs((y - t.y)) == 1f){

				ThreatenTiles(false);
				occupiedTile.isThreatened = 0;
				ChangePosition(targetTile);
				UpdateCoordinates(t.x, t.y);
				ThreatenTiles(true);
				turnTurner = true;
				
			}
		
		}


		if (targetTile.tag == "Piece") {
			
			Piece p = targetTile.GetComponent<Piece>();
			
			if(Mathf.Abs((x - p.x)) == 1f && Mathf.Abs((y - p.y)) == 2f){

				ThreatenTiles(false);
				occupiedTile.isThreatened = 0;
				ChangePosition(targetTile);
				UpdateCoordinates(p.x, p.y);
				p.DestroyTargetPiece();
				ThreatenTiles(true);
				turnTurner = true;
				
			}
			
			if(Mathf.Abs((x - p.x)) == 2f && Mathf.Abs((y - p.y)) == 1f){

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






	void Start () {

		InstantiateVariables ();


		ThreatenTiles (true);
	
	}

}

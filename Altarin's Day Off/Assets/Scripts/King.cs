using UnityEngine;
using System.Collections;

public class King : Piece {

	public override bool isValidMove (bool checkForBlock){
		int newX = x; 
		int newY = y + 1;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			if(!MyKingIsInCheck(newX, newY, checkForBlock)){}
			
			else{
				return false;
			}
		}
		
		newX = x + 1; 
		newY = y + 1;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			if(!MyKingIsInCheck(newX, newY, checkForBlock)){}
			
			else{
				return false;
			}
		}
		
		newX = x + 1; 
		newY = y;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			if(!MyKingIsInCheck(newX, newY, checkForBlock)){}
			
			else{
				return false;
			}
		}
		
		newX = x + 1; 
		newY = y - 1;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			if(!MyKingIsInCheck(newX, newY, checkForBlock)){}
			
			else{
				return false;
			}
		}
		
		newX = x; 
		newY = y - 1;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			if(!MyKingIsInCheck(newX, newY, checkForBlock)){}
			
			else{
				return false;
			}
		}
		
		newX = x - 1; 
		newY = y - 1;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			if(!MyKingIsInCheck(newX, newY, checkForBlock)){}
			
			else{
				return false;
			}
		}
		
		newX = x - 1; 
		newY = y;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			if(!MyKingIsInCheck(newX, newY, checkForBlock)){}
			
			else{
				return false;
			}
		}
		
		newX = x - 1; 
		newY = y + 1;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			if(!MyKingIsInCheck(newX, newY, checkForBlock)){}
			
			else{
				return false;
			}
		}

		return true;
		
	}


	public override void ThreatenTiles(bool isUpdate){

		int newX = x; 
		int newY = y + 1;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			ThreatAssignment (isUpdate, newX, newY);
		}

		newX = x + 1; 
		newY = y + 1;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			ThreatAssignment (isUpdate, newX, newY);
		}

		newX = x + 1; 
		newY = y;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			ThreatAssignment (isUpdate, newX, newY);
		}

		newX = x + 1; 
		newY = y - 1;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			ThreatAssignment (isUpdate, newX, newY);
		}

		newX = x; 
		newY = y - 1;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			ThreatAssignment (isUpdate, newX, newY);
		}

		newX = x - 1; 
		newY = y - 1;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			ThreatAssignment (isUpdate, newX, newY);
		}

		newX = x - 1; 
		newY = y;
		if (newX < 8 && newY < 8 && newX >= 0 && newY >= 0) {
			ThreatAssignment (isUpdate, newX, newY);
		}

		newX = x - 1; 
		newY = y + 1;
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

			if(Mathf.Abs(x - t.x) == 1f && Mathf.Abs(y - t.y) == 1f){

				ThreatenTiles(false);
				occupiedTile.isThreatened = 0;
				ChangePosition(targetTile);
				UpdateCoordinates(t.x, t.y);
				ThreatenTiles(true);
				turnTurner = true;

			}

			if(Mathf.Abs(y - t.y) == 1f && x == t.x){

				ThreatenTiles(false);
				occupiedTile.isThreatened = 0;
				ChangePosition(targetTile);
				UpdateCoordinates(t.x, t.y);
				ThreatenTiles(true);
				turnTurner = true;
				
			}
			if(Mathf.Abs(x - t.x) == 1f && y == t.y){

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
			
			if(Mathf.Abs(x - p.x) == 1f && Mathf.Abs(y - p.y) == 1f){

				ThreatenTiles(false);
				occupiedTile.isThreatened = 0;
				ChangePosition(targetTile);
				UpdateCoordinates(p.x, p.y);
				p.DestroyTargetPiece();
				ThreatenTiles(true);
				turnTurner = true;
				
			}
			
			if(Mathf.Abs(y - p.y) == 1f && x == p.x){

				ThreatenTiles(false);
				occupiedTile.isThreatened = 0;
				ChangePosition(targetTile);
				UpdateCoordinates(p.x, p.y);
				p.DestroyTargetPiece();
				ThreatenTiles(true);
				turnTurner = true;
				
			}
			if(Mathf.Abs(x - p.x) == 1f && y == p.y){

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

	public bool IsKingInCheck(){

		if (isWhite && occupiedTile.isThreatened == 2 || !isWhite && occupiedTile.isThreatened == 1 || occupiedTile.isThreatened == 3) {
			//Debug.Log(gameObject + " is in check!");
			return true;
		}

		else {
			return false;
		}

	}

	public bool AssessCheckmate (int x, int y){

		gameManager.selectedPiece = gameObject.GetComponent<Piece> ();

		if (x < 8 && y < 8 && x >= 0 && y >= 0) {

			if(!gameManager.VerifyMove(gameManager.tileCoordinates[x, y])){

				return true;
			}

			else{return false;}
		}

	return true;
	}


	public bool IsCheckmate(){

		if (!IsKingInCheck()) {
			Debug.Log("poop1");
			return false;
		}

		if(gameManager.isCheckPieceVulnerable){
			Debug.Log("poop2");
			return false;
		}
		//ThreatenTiles (false);
		//gameManager.coordinates [x, y] = 0;
		//gameManager.UpdateThreatenedTiles ();
		//gameManager.coordinates [x, y] = pieceDesignator;

		//int total = 0;

		//GameObject[] pieces = new GameObject[32];
		
		//pieces = GameObject.FindGameObjectsWithTag("Piece");

		/*
		//Determines if checking pieces are vulerable to a counter-attack
		if (isWhite) {

			for (int a = 0; a <gameManager.whitePieces.Length; a++) {
				if(gameManager.whitePieces[a] != null){
					if(gameManager.whitePieces[a].pieceDesignator == 12){}

					else{
						gameManager.whitePieces [a].ThreatenTiles (true);

					}

				}
			}

			for(int a = 0; a <gameManager.blackPieces.Length; a++){
				if(gameManager.blackPieces[a] == null){}

				else{
					gameManager.blackPieces[a].ThreatenTiles(true);
					//Debug.Log (	gameManager.blackPieces[a]);

				}
				
			}
		}

		if(!isWhite){

			for(int a = 0; a <gameManager.blackPieces.Length; a++){
				if(gameManager.blackPieces[a] != null){
					if(gameManager.blackPieces[a].pieceDesignator == 6){}

					else{
						gameManager.blackPieces[a].ThreatenTiles(true);
					}

				}
			}

			for (int a = 0; a <gameManager.whitePieces.Length; a++) {
				if(gameManager.blackPieces[a] == null){}

				else{
					gameManager.whitePieces [a].ThreatenTiles (true);
				}
				
			}
		}


		for (int a = 0; a < pieces.Length; a++) {

			Piece p = pieces [a].GetComponent<Piece> ();

			if(isWhite != p.isWhite){
				if (!p.isValidMove (false)) {
					Debug.Log (pieces[a] + " is checking the king");
					if(!p.isValidMove(true)){

						//pass
						Debug.Log ("Check can be blocked");
						//Debug.Log(p);

					}


					if(p.isWhite && p.occupiedTile.isThreatened == 2 || !p.isWhite && p.occupiedTile.isThreatened == 1 || p.occupiedTile.isThreatened == 3){
						
						//pass
						Debug.Log("Checking piece is vulnerable");
						
					}
					
					if(p.occupiedTile.isThreatened == 0) {
						total++;
					}

					if(p.isValidMove(true)) {
						total++;
					}
		
				} 

			}

		}

		Debug.Log (total);

		if (total == 0) {
			ThreatenTiles (true);
			//gameManager.coordinates [x, y] = pieceDesignator;
			gameManager.UpdateThreatenedTiles ();
			return false;
		}*/
	

		int i = x; 
		int	j = y + 1;

		if (!AssessCheckmate(i, j)){
			ThreatenTiles (true);
			gameManager.coordinates [x, y] = pieceDesignator;
			Debug.Log("poop3");
			gameManager.UpdateThreatenedTiles ();
			return false;
		}
		
		i = x + 1; 
		j = y + 1;
		if (!AssessCheckmate(i, j)){
			ThreatenTiles (true);
			gameManager.coordinates [x, y] = pieceDesignator;
			gameManager.UpdateThreatenedTiles ();
			Debug.Log("poop4");
			return false;
		}
		
		i = x + 1; 
		j = y;
		if (!AssessCheckmate(i, j)){
			ThreatenTiles (true);
			gameManager.coordinates [x, y] = pieceDesignator;
			gameManager.UpdateThreatenedTiles ();
			Debug.Log("poop5");
			return false;
		}
		
		i = x + 1; 
		j = y - 1;
		if (!AssessCheckmate(i, j)){
			ThreatenTiles (true);
			gameManager.coordinates [x, y] = pieceDesignator;
			gameManager.UpdateThreatenedTiles ();
			Debug.Log("poop6");
			return false;
		}
		
		i = x; 
		j = y - 1;
		if (!AssessCheckmate(i, j)){
			ThreatenTiles (true);
			gameManager.coordinates [x, y] = pieceDesignator;
			gameManager.UpdateThreatenedTiles ();
			Debug.Log("poop7");
			return false;
		}
		
		i = x - 1; 
		j = y - 1;
		if (!AssessCheckmate(i, j)){
			ThreatenTiles (true);
			gameManager.coordinates [x, y] = pieceDesignator;
			gameManager.UpdateThreatenedTiles ();
			Debug.Log("poop8");
			return false;
		}
		
		i = x - 1; 
		j = y;
		if (!AssessCheckmate(i, j)){
			Debug.Log("poop9");
			ThreatenTiles (true);
			gameManager.coordinates [x, y] = pieceDesignator;
			gameManager.UpdateThreatenedTiles ();
			return false;
		}
		
		i = x - 1; 
		j = y + 1;
		if (!AssessCheckmate(i, j)){
			ThreatenTiles (true);
			Debug.Log("poop10");
			gameManager.coordinates [x, y] = pieceDesignator;
			gameManager.UpdateThreatenedTiles ();
			return false;
		}
		
		return true;

	}





	// Use this for initialization
	void Start () {

		InstantiateVariables ();

		ThreatenTiles (true);
	
	}
	

}

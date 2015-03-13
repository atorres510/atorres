using UnityEngine;
using System.Collections;


public class f_GameManager : Photon.MonoBehaviour {

	
	public f_Piece selectedPiece;
	public f_Piece emptyPiece;
	

	public bool isPlayer1Turn;
	bool isTurnPassed;

	//victory conditions and variables
	bool isGameOver;
	bool didWhiteWin;
	f_Castle whiteCastle;
	f_Castle blackCastle;
	public int[] whiteRoyalties;
	public int[] blackRoyalties;
	
	public int[,] coordinates; //holds all the coordinates of the pieces
	public GameObject[,] tileCoordinates; //holds all tiles
	
	public f_Piece[] whitePieces;
	public f_Piece[] blackPieces;

	public bool toggleTileGUI;

	public bool gameOn;
	public bool isOffline;

	public void SetUpBoard(){

		gameOn = true;
		GameObject[] tiles = new GameObject[576];
		//GameObject[] tiles = new GameObject[169];
		tiles = GameObject.FindGameObjectsWithTag("f_Tile");
		
		for(int i = 0; i < tiles.Length; i++){
			
			f_Tile t = tiles[i].GetComponent<f_Tile>();

			if(t.tileType == 5){
				
				
				f_Castle c = t.GetComponent<f_Castle>();
				
				if(c.isWhite){
					
					whiteCastle = c;
					//tileCoordinates[t.x, t.y] = tiles[i];
					
					
				}
				else{
					
					blackCastle = c;
					//tileCoordinates[t.x, t.y] = tiles[i];
				}
				
			}

			else if(t.tileType == 4){

				//pass
			}

			else{

				tileCoordinates[t.x, t.y] = tiles[i];

			}
			






		}

		whiteCastle.ReplaceOccupiedTile (whiteCastle.castleGreens);
		whiteCastle.ReplaceOccupiedTile (whiteCastle);
		blackCastle.ReplaceOccupiedTile (blackCastle.castleGreens);
		blackCastle.ReplaceOccupiedTile (blackCastle);

		
		int j = 0;
		int k = 0;

		int l = 0;
		int m = 0;
		
		GameObject[] pieces = new GameObject[26];
		
		pieces = GameObject.FindGameObjectsWithTag("f_Piece");
		
		for(int i = 0; i < pieces.Length; i++){
			
			f_Piece p = pieces[i].GetComponent<f_Piece>();
			
			coordinates[p.x, p.y] = p.pieceDesignator; // 0 Null; 1 B.Pawn; 2 B.Rook; etc;
			p.startTile = tileCoordinates[p.x, p.y];
			p.StartPosition();
			if(p.isWhite){
				whitePieces[j] = p;
				//Debug.Log(p);
				j++;

				if(p.isRoyalty){
					
					whiteRoyalties[l] = p.pieceDesignator;
					l++;
				}
			}
			
			if(!p.isWhite){
				blackPieces[k] = p;
				k++;

				if(p.isRoyalty){
					
					blackRoyalties[m] = p.pieceDesignator;
					m++;
				}

			}

			
		}


	}
	//manages the toggle of the GUI.button "Finished with Turn" such that atleast
	//one piece is moved before being allowed to pass the rest of the turn
	bool isTurnPassable(){

		f_Piece[] pieces;

		if (isPlayer1Turn) {
				
			pieces = whitePieces;
		
		}

		else{
			pieces = blackPieces;
		}

		for(int i = 0; i < pieces.Length; i++){

			if(pieces[i] != null){

				if(pieces[i].turnTurner){


					return true;

				}



			}

		}
		
		return false;
	}

	void ResetTurn(f_Piece[] pieces){

		selectedPiece.HighlightMovementTiles (selectedPiece.MovementTiles, false);

		for(int i = 0; i < pieces.Length; i++){
			
			if(pieces[i] != null){
				
				pieces[i].turnTurner = false;
				pieces[i].MovementTiles.Clear();

			}
			
		}

		isTurnPassed = false;
		selectedPiece = emptyPiece;

	}

	bool IsTurnOver(f_Piece[] pieces, bool turn){

		if (turn) {
				
			return true;
		
		}

		for(int i = 0; i < pieces.Length; i++){
			
			if(pieces[i] != null){
				
				if(!pieces[i].turnTurner){

					return false;

				}
				
			}
			
		}

		return true;
	
	}




	IEnumerator Player1Turn(){
		Debug.Log ("Player 1 Turn");
		
		bool isTurnOver = false;
		isPlayer1Turn = true;
		
		while(!isTurnOver) {
			
			isTurnOver = IsTurnOver(whitePieces, isTurnPassed);
			
			yield return null;
		}
		
		ResetTurn (whitePieces);

		if (CheckVictoryConditions ()) {
				
			DisableAllPieceColliders();
		
		}
		
		yield return null;
		
		
		
	}
	
	IEnumerator Player2Turn(){
		Debug.Log ("Player 2 Turn");
		
		isPlayer1Turn = false;
		bool isTurnOver = false;
		
		while(!isTurnOver) {
			
			isTurnOver = IsTurnOver(blackPieces, isTurnPassed);
			
			yield return null;
		}
		
		ResetTurn (blackPieces);

		if (CheckVictoryConditions ()) {
				
			DisableAllPieceColliders();
		
		}
		
		yield return null;
		
	}


	bool CheckVictoryConditions(){

		//condition1; castle capture
		//condition2; royalty slain


		//check condition 1
			// is castle occupied
				// is piece on the castle the same color as the castle
		if (whiteCastle.isOccupied) {
				
			int p = coordinates[whiteCastle.x, whiteCastle.y];
			// has black taken white castle;
			if(p > 8){

				Debug.Log("White Castle Captured!");
				isGameOver = true;
				didWhiteWin = false;
				return true;

			}
		
		}

		if (blackCastle.isOccupied) {

			int p = coordinates[blackCastle.x, blackCastle.y];
			// has white has taken castle;
			if(p <= 8 && p != 0){

				Debug.Log("Black Castle Captured");
				isGameOver = true;
				didWhiteWin = true;
				return true;
				
			}
		
		}


		//check condition 2
			// an array of royalty pieces 
				// check if it is empty;

		int wTotal = 0;
		int bTotal = 0;
				
		for (int i = 0; i < whiteRoyalties.Length; i++) {
				
			wTotal += whiteRoyalties[i];
		
		}

		for (int i = 0; i < blackRoyalties.Length; i++) {
			
			bTotal += blackRoyalties[i];
			
		}

		//all white royalties dead, black wins
		if (wTotal == 0) {

			Debug.Log("All White Royalties are Dead");
			isGameOver = true;
			didWhiteWin = false;
			return true;
		
		}

		if (bTotal == 0) {

			Debug.Log("All Black Royalties are Dead");
			isGameOver = true;
			didWhiteWin = true;
			return true;
			
		}




		//check condition 2
			// an array of royalty pieces 
				// check if it is empty;


		return false;
	
	}

	[RPC]
	public void UpdateCoordinates(int oldX, int oldY, int newX, int newY, int pieceID){
		
		coordinates[oldX, oldY] = 0;
		f_Tile t = tileCoordinates [oldX, oldY].GetComponent<f_Tile> ();
		t.isOccupied = false;
		
		coordinates[newX, newY] = pieceID;
		if (pieceID != 0) {
				
			t = tileCoordinates [oldX, oldY].GetComponent<f_Tile> ();
			t.isOccupied = true;
		
		}

		if (photonView.isMine) {
				
			photonView.RPC("UpdateCoordinates", PhotonTargets.All, oldX, oldY, newX, newY, pieceID);
		
		}

	}




	void DisableAllPieceColliders(){

		for(int i = 0; i < whitePieces.Length; i++){

			if(whitePieces[i] != null){
				BoxCollider2D b = whitePieces[i].GetComponent<BoxCollider2D>();
				b.enabled = false;
			}
			
		}


		for(int i = 0; i < blackPieces.Length; i++){

			if(blackPieces[i]){
				BoxCollider2D b = blackPieces[i].GetComponent<BoxCollider2D>();
				b.enabled = false;
			}


		}
		Debug.Log ("all piece colliders disabled");
	}

	
	public IEnumerator Game(){
		
		while(!isGameOver){
			
			yield return StartCoroutine(Player1Turn());
			yield return StartCoroutine(Player2Turn());
			
		}

		Debug.Log ("GameOver");


	}
	

	void OnGUI(){


		if (gameOn) {
				
			string player1Turn = " It is the White Turn";
			string player2Turn = " It is the Black Turn";
			string gameOverWhiteWin = " White wins!";
			string gameOverBlackWin = " Black wins!";
			string selectedPieceString = selectedPiece + " is selected.";
			
			if (!isGameOver) {
				if (isPlayer1Turn) {
					player1Turn = GUI.TextField (new Rect (10, 20, 125, 20), player1Turn, 25);
				}
				
				if (!isPlayer1Turn) {
					player2Turn = GUI.TextField (new Rect (10, 20, 125, 20), player2Turn, 25);
				}
			}
			
			if (isGameOver) {
				if(!didWhiteWin){
					gameOverBlackWin = GUI.TextField(new Rect(10, 20, 125, 20), gameOverBlackWin, 25);
				}
				
				if(didWhiteWin){
					gameOverWhiteWin = GUI.TextField(new Rect(10, 20, 125, 20), gameOverWhiteWin, 25);
				}
				
			}

			if(isTurnPassable()){

				if(GUI.Button(new Rect(10, 70, 150, 20), "Finish Turn")){

					isTurnPassed = true;
					Debug.Log("Turn passed: " + isTurnPassed);

				}

			}
			
			//toggleTileGUI = GUI.Toggle (new Rect (10, 40, 125, 20), toggleTileGUI, "Toggle Tile GUI");
			
			selectedPieceString = GUI.TextField (new Rect (10, Screen.height - 40, 300, 20), selectedPieceString, 300); 
		
		}

		else {
			//pass
		}

	}


	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){

		if (stream.isWriting == true) {

			stream.SendNext(isGameOver);
			stream.SendNext(didWhiteWin);
			stream.SendNext(isPlayer1Turn);
			stream.SendNext(gameOn);

		
		
		}

		else{

			isGameOver = (bool)stream.ReceiveNext();
			didWhiteWin = (bool)stream.ReceiveNext();
			isPlayer1Turn = (bool)stream.ReceiveNext();
			gameOn = (bool)stream.ReceiveNext();

		}
	
	
	}


	Player[] players;
	
	void ArePlayersReady(){

		int[] playersReady = new int[players.Length];
		
		for (int i = 0; i < players.Length; i++) {

			if(players[i].isReady){

				playersReady[i] = 1;


			}
		
		}

		int readyTotal = 0;


		for (int i = 0; i < playersReady.Length; i++) {
				
			readyTotal += playersReady[i];

		}

		if (readyTotal == players.Length) {
				
			gameOn = true;
			SetUpBoard();
		
		}
    
	}







	void Awake () {
		
		tileCoordinates = new GameObject[24,24];
		coordinates = new int[24,24];

		/*whitePieces = new f_Piece[13];
		blackPieces = new f_Piece[13];

		tileCoordinates = new GameObject[5,5];
		coordinates = new int[5,5];
		whitePieces = new f_Piece[2];
		blackPieces = new f_Piece[2];

		*/

		//tileCoordinates = new GameObject[13,13];
		//coordinates = new int[13,13];
		whitePieces = new f_Piece[13];
		blackPieces = new f_Piece[13];
		whiteRoyalties = new int[3];
		blackRoyalties = new int[3];
		
		isTurnPassed = false;
		gameOn = false;

		players = FindObjectsOfType<Player> ();
		Debug.Log ("Player " + players[0].playerNumber);
		Debug.Log ("Player " + players [1].playerNumber);
		//FindNetworkManager ();

		//toggleTileGUI = false;
		
		//SetUpBoard();

		//StartCoroutine (Game());
		
		//Debug.Log ("GameOver");
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!gameOn) {
				
			ArePlayersReady();
		
		
		}
		//selectedPiece.occupiedTile.gameObject.transform.renderer.material.color = Color.red;
	
		
		
	}
}

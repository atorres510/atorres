using UnityEngine;
using System.Collections;


public class f_GameManager : MonoBehaviour {

	
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
	
	public int[,] coordinates; //holds state of the board within it all the coordinates of the pieces
	public GameObject[,] tileCoordinates; //holds all tiles


	public f_Piece[] whitePieces;
	public f_Piece[] blackPieces;

	public bool toggleTileGUI;

	public bool gameOn;
	public bool isOffline;
	public PhotonView myPhotonView;
	public Player myPlayer;
	public Player[] players;
	public bool isWaiting;

	//Sets up the board at the beginning of the game.  Called by ArePlayersReady in setup manager.
	public void SetUpBoard(){
		Debug.Log ("Setup board");
	
		//gameOn = true;
		
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

		if (isOffline) {
			whiteCastle.ReplaceOccupiedTile (whiteCastle.castleGreens);
			whiteCastle.ReplaceOccupiedTile (whiteCastle);
			blackCastle.ReplaceOccupiedTile (blackCastle.castleGreens);
			blackCastle.ReplaceOccupiedTile (blackCastle);
		}

		else{

			if(myPlayer.isWhite){

				whiteCastle.ReplaceOccupiedTile (whiteCastle.castleGreens);
				whiteCastle.ReplaceOccupiedTile (whiteCastle);
				blackCastle.isSetup = false; //this variable does not get addressed in network multiplayer and 
												//is turned off here.

			}

			else{

				blackCastle.ReplaceOccupiedTile (blackCastle.castleGreens);
				blackCastle.ReplaceOccupiedTile (blackCastle);
				whiteCastle.isSetup = false;

			}


		}


		
		int j = 0;
		int k = 0;

		int l = 0;
		int m = 0;
		
		GameObject[] pieces = new GameObject[26];
		
		pieces = GameObject.FindGameObjectsWithTag("f_Piece");


		if (isOffline) {
			for(int i = 0; i < pieces.Length; i++){
				
				f_Piece p = pieces[i].GetComponent<f_Piece>();
				
				coordinates[p.x, p.y] = p.pieceDesignator; // 0 Null; 1 B.Pawn; 2 B.Rook; etc;
				p.startTile = tileCoordinates[p.x, p.y];
				p.StartPosition();
				if(p.isWhite){
					whitePieces[j] = p;
					Debug.Log("Adding to WhitePieces: " + p + ". PieceID : " + p.pieceID);
					j++;
					
					if(p.isRoyalty){
						
						whiteRoyalties[l] = p.pieceDesignator;
						l++;
					}
				}
				
				if(!p.isWhite){
					blackPieces[k] = p;

					Debug.Log("Adding to BlackPieces: " + p + ". PieceID : " + p.pieceID);
					k++;
					
					if(p.isRoyalty){
						
						blackRoyalties[m] = p.pieceDesignator;
						m++;
					}
					
				}
				
				
			}	
		
		
		}

		//if Online: sets up your own pieces first and relays your piece info to the other client
		else{
			for(int i = 0; i < pieces.Length; i++){

				f_Piece p = pieces[i].GetComponent<f_Piece>();

				//fills white and black piece arrays and updates coordinates for only their own piecces.  
				if(myPlayer.isWhite){

					if(p.isWhite){
						coordinates[p.x, p.y] = p.pieceDesignator; // 0 Null; 1 B.Pawn; 2 B.Rook; etc;
						p.startTile = tileCoordinates[p.x, p.y];
						p.StartPosition();
						whitePieces[j] = p;
					//	Debug.Log("Adding to WhitePieces: " + p + ". PieceID : " + p.pieceID);
						//Debug.Log(p);
						j++;
						
						//this.myPhotonView.RPC("UpdatePiece", PhotonTargets.Others, p.transform.position, 
						                   //   p.x, p.y, true, p.pieceID);
						
						
						if(p.isRoyalty){

							//Debug.Log("Adding to White Royalty: " + ". PieceID : " + p.pieceID);
							whiteRoyalties[l] = p.pieceDesignator;
							l++;
						}
						
						
					}
					
					else{
						
						blackPieces[k] = p;
						//Debug.Log("Adding to BlackPieces: " + p + ". PieceID : " + p.pieceID);
						k++;
						if(p.isRoyalty){
							
							blackRoyalties[m] = p.pieceDesignator;
							m++;
						}

					}

				}


				else{

					if(!p.isWhite){
						coordinates[p.x, p.y] = p.pieceDesignator; // 0 Null; 1 B.Pawn; 2 B.Rook; etc;
						p.startTile = tileCoordinates[p.x, p.y];
						p.StartPosition();
						blackPieces[k] = p;
						//Debug.Log("Adding to BlackPieces: " + p + ". PieceID : " + p.pieceID);
						k++;

						//tells other player how to update my pieces.  
						//this.myPhotonView.RPC("UpdatePiece", PhotonTargets.Others, p.transform.position, 
						                   //   p.x, p.y, false, p.pieceID);
						
						if(p.isRoyalty){
							
							blackRoyalties[m] = p.pieceDesignator;
							m++;
						}
						
					}
					
					else{
						
						whitePieces[j] = p;
						//Debug.Log("Adding to WhitePieces: " + p + ". PieceID : " + p.pieceID);
						//Debug.Log(p);
						j++;
						if(p.isRoyalty){
							
							whiteRoyalties[l] = p.pieceDesignator;
							l++;
						}
						
						
					}


				}

			}





		}

		isWaiting = true; 
		myPlayer.isSecondaryReady = true;
		//StartCoroutine ("Game");

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
				
			this.myPhotonView.RPC("UpdateTurn", PhotonTargets.Others);
			return true;
		
		}

		for(int i = 0; i < pieces.Length; i++){
			
			if(pieces[i] != null){
				
				if(!pieces[i].turnTurner){

					return false;

				}
				
			}
			
		}

		this.myPhotonView.RPC("UpdateTurn", PhotonTargets.Others);
		return true;
	
	}

	[RPC]
	void UpdateTurn(){

		Debug.Log ("Updating Turn");
		isTurnOver = true;

	}


	bool isTurnOver;

	IEnumerator Player1Turn(){
		Debug.Log ("Player 1 Turn");
		
		isTurnOver = false;
		isPlayer1Turn = true;
		
		while(!isTurnOver) {

			if(isOffline){

				isTurnOver = IsTurnOver(whitePieces, isTurnPassed);
				yield return null;

			}

			else{

				if(myPlayer.isWhite){

					isTurnOver = IsTurnOver(whitePieces, isTurnPassed);
					yield return null;
				}

				else{

					yield return null;
				}
			}

			
			//yield return null;
		}
		
		ResetTurn (whitePieces);

		if (CheckVictoryConditions ()) {
				
			DisableAllPieceColliders();
		
		}

		if (!isOffline) {
				
			yield return new WaitForSeconds (2.0f);
			UpdateBoardState (coordinates, tileCoordinates);
			yield return new WaitForSeconds (2.0f);
			UpdatePieceSet (whitePieces, true);
			yield return new WaitForSeconds (2.0f);
			UpdatePieceSet (blackPieces, false);
			yield return new WaitForSeconds (2.0f);
		
		}

		//Update pieces and coordinates
		
		yield return null;
		
		
		
	}
	
	IEnumerator Player2Turn(){
		Debug.Log ("Player 2 Turn");
		
		isPlayer1Turn = false;
		isTurnOver = false;
		
		while(!isTurnOver) {
			
			if(isOffline){
				
				isTurnOver = IsTurnOver(blackPieces, isTurnPassed);
				yield return null;
				
			}
			
			else{
				
				if(!myPlayer.isWhite){
					
					isTurnOver = IsTurnOver(blackPieces, isTurnPassed);
					yield return null;
				}
				
				else{

					yield return null;
					
				}

			}
		}
		
		ResetTurn (blackPieces);

		if (CheckVictoryConditions ()) {
				
			DisableAllPieceColliders();
		
		}



		if (!isOffline) {
				
			yield return new WaitForSeconds (2.0f);
			UpdateBoardState (coordinates, tileCoordinates);
			yield return new WaitForSeconds (2.0f);
			UpdatePieceSet (whitePieces, true);
			yield return new WaitForSeconds (2.0f);
			UpdatePieceSet (blackPieces, false);
			yield return new WaitForSeconds (2.0f);
		
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


	/*public void UpdateCoordinates(int oldX, int oldY, int newX, int newY, int pieceID){
		
		coordinates[oldX, oldY] = 0;
		f_Tile t = tileCoordinates [oldX, oldY].GetComponent<f_Tile> ();
		t.isOccupied = false;
		
		coordinates[newX, newY] = pieceID;
		if (pieceID != 0) {
				
			t = tileCoordinates [oldX, oldY].GetComponent<f_Tile> ();
			t.isOccupied = true;
		
		}

		//if (photonView.isMine) {
				
			//photonView.RPC("UpdateCoordinates", PhotonTargets.All, oldX, oldY, newX, newY, pieceID);
		
		//}

	}*/




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

		gameOn = true;
		//first update of pieces and board.  uses updateset overload to update specific coordinates and also updates castle position for
		//opponents version of your castle.
		if (!isOffline) {
				
			//yield return new WaitForSeconds(2f);


			if(myPlayer.isWhite){
				//Debug.Log("Updating");
				UpdatePieceSet (whitePieces, true, true);
				this.myPhotonView.RPC("UpdateCastle", PhotonTargets.Others, whiteCastle.x, whiteCastle.y, 
				                      whiteCastle.isOccupied, whiteCastle.rotation, true);

			}

			//yield return new WaitForSeconds(2f);

			else{
				//Debug.Log("updating");
				UpdatePieceSet (blackPieces, false, true);
				this.myPhotonView.RPC("UpdateCastle", PhotonTargets.Others, blackCastle.x, blackCastle.y, 
				                      blackCastle.isOccupied, blackCastle.rotation, false);

			}

		}




		
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



	// identical to ArePlayersReady() in setupmanager except it uses secondary ready up boolean
	void WaitingForOtherPlayers(){

		Debug.Log ("Waiting for other players");

		int[] playersReady = new int[players.Length];
		
		for (int i = 0; i < players.Length; i++) {

			if(players[i].isSecondaryReady){

				playersReady[i] = 1;


			}
		
		}

		int readyTotal = 0;


		for (int i = 0; i < playersReady.Length; i++) {
				
			readyTotal += playersReady[i];

		}

		Debug.Log ("ready total: " + readyTotal);
		if (readyTotal == players.Length) {
				
			//myPlayer.isSecondaryReady = false;
			isWaiting = false;
			StartCoroutine("Game");
		
		}
    
	}

	//updates coordinates[,] for the opposing client

	void UpdateBoardState(int[,] coordinates, GameObject[,] tiles){

		for(int i = 0; i < 24; i++){
			for(int j = 0; j <24; j++){

				f_Tile t = tiles[i,j].GetComponent<f_Tile>();

				this.myPhotonView.RPC("UpdateCoordinates", PhotonTargets.Others, coordinates[i,j], i, j, t.isOccupied);

			}

		}
		
	}


	//takes piece designator P and passes its value to the recieving coordinate grid at position i,j
	//updates tile occupied coordinates as well
	[RPC]
	void UpdateCoordinates(int p, int x, int y, bool isOccupied){

		coordinates [x, y] = p;
		f_Tile t = tileCoordinates [x, y].GetComponent<f_Tile> ();
		t.isOccupied = isOccupied;
		Debug.Log ("updating coordinates");


	}


	//updates opponents castle and calls replace tiles
	[RPC]
	void UpdateCastle(int x, int y, bool isOccupied, int rotation, bool isCastleWhite){

		if (isCastleWhite) {
				
			whiteCastle.x = x;
			whiteCastle.y = y;
			whiteCastle.isOccupied = isOccupied;
			whiteCastle.rotation = rotation;


			whiteCastle.UpdateCastleGreensPos();
			whiteCastle.ReplaceOccupiedTile (whiteCastle.castleGreens);
			whiteCastle.ReplaceOccupiedTile (whiteCastle);
		
		
		}

		else{

			blackCastle.x = x;
			blackCastle.y = y;
			blackCastle.isOccupied = isOccupied;
			blackCastle.rotation = rotation;

			blackCastle.UpdateCastleGreensPos();
			blackCastle.ReplaceOccupiedTile (blackCastle.castleGreens);
			blackCastle.ReplaceOccupiedTile (blackCastle);

		}
	
	
	
	}




	void UpdatePieceSet(f_Piece[] pieceSet, bool isSetWhite){

		for (int i = 0; i < pieceSet.Length; i++) {
				
			this.myPhotonView.RPC("UpdatePiece", PhotonTargets.Others, pieceSet[i].transform.position, 
				pieceSet[i].x, pieceSet[i].y, isSetWhite, pieceSet[i].pieceID);
		
		}



	}


	void UpdatePieceSet(f_Piece[] pieceSet, bool isSetWhite, bool isFirstUpdate){

	
		if (isFirstUpdate) {
				
			for (int i = 0; i < pieceSet.Length; i++) {
				
				this.myPhotonView.RPC("UpdatePiece", PhotonTargets.Others, pieceSet[i].transform.position, 
				                      pieceSet[i].x, pieceSet[i].y, isSetWhite, pieceSet[i].pieceID);
				this.myPhotonView.RPC ("UpdateCoordinates", PhotonTargets.Others, pieceSet[i].pieceDesignator, 
				                       pieceSet[i].x, pieceSet[i].y, true);
				
			}
		
		
		}

		else{

			for (int i = 0; i < pieceSet.Length; i++) {
				
				this.myPhotonView.RPC("UpdatePiece", PhotonTargets.Others, pieceSet[i].transform.position, 
				                      pieceSet[i].x, pieceSet[i].y, isSetWhite, pieceSet[i].pieceID);
				
			}

		}
	
	
	
	}


	//updates the piece sets for the opposing piece sets. 
	[RPC]
	void UpdatePiece(Vector3 newPosition, int newX, int newY, bool isSetWhite, int pieceID){




		//Debug.Log (gameObject);
		//f_Piece[] pieces = FindObjectsOfType (f_Piece);

		//Debug.Log("piece ID: " + pieceID);

		if (isSetWhite) {
				
			for (int i = 0; i < whitePieces.Length; i++) {

				if(whitePieces[i] == null){
					Debug.Log("White Piece Missing: Null Reference Exception!");

					break;

				}



				else if(whitePieces[i].pieceID == pieceID){

					whitePieces[i].transform.position = newPosition;
					whitePieces[i].x = newX;
					whitePieces[i].y = newY;
					whitePieces[i].occupiedTile = tileCoordinates[newX, newY].GetComponent<f_Tile>();


				}


				else{}
			
			}
		
		}

		else{
			for (int i = 0; i < blackPieces.Length; i++) {


				if(blackPieces[i] == null){
					Debug.Log("Black Piece Missing: Null Reference Exception!");
					break;

				}
				
				else if(blackPieces[i].pieceID == pieceID){
					
					blackPieces[i].transform.position = newPosition;
					blackPieces[i].x = newX;
					blackPieces[i].y = newY;
					blackPieces[i].occupiedTile = tileCoordinates[newX, newY].GetComponent<f_Tile>();
					
				}

				else{}

			}

		}
		
	}







	void Awake () {
		
		tileCoordinates = new GameObject[24,24];
		coordinates = new int[24,24];

		/*

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

		if (!isOffline) {
				
			myPhotonView = GetComponent<PhotonView>();
		
		
		}

		//players = FindObjectsOfType<Player> ();
		//Debug.Log ("Player " + players[0].playerNumber);
		//Debug.Log ("Player " + players [1].playerNumber);
		//FindNetworkManager ();

		//toggleTileGUI = false;
		
		//SetUpBoard();

		//StartCoroutine (Game());
		
		//Debug.Log ("GameOver");
		
	}
	
	// Update is called once per frame
	void Update () {

		if (isWaiting) {
				

			WaitingForOtherPlayers();
		
		
		}
		
		//selectedPiece.occupiedTile.gameObject.transform.renderer.material.color = Color.red;
	
		
		
	}
}

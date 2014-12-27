using UnityEngine;
using System.Collections;


public class f_GameManager : MonoBehaviour {

	
	public f_Piece selectedPiece;
	public f_Piece emptyPiece;
	

	public bool isPlayer1Turn;

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

	public void SetUpBoard(){

		gameOn = true;
		//GameObject[] tiles = new GameObject[576];
		GameObject[] tiles = new GameObject[169];
		tiles = GameObject.FindGameObjectsWithTag("f_Tile");
		
		for(int i = 0; i < tiles.Length; i++){
			
			f_Tile t = tiles[i].GetComponent<f_Tile>();
			
			tileCoordinates[t.x, t.y] = tiles[i];

			if(t.tileType == 5){


				f_Castle c = t.GetComponent<f_Castle>();

				if(c.isWhite){

					whiteCastle = c;


				}
				else{

					blackCastle = c;
				
				}

			}
		}
		
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

		whiteCastle.SetUpCastleGreens (whiteCastle.castleGreens);
		whiteCastle.ReplaceOccupiedTile (whiteCastle);
		blackCastle.SetUpCastleGreens (blackCastle.castleGreens);
		blackCastle.ReplaceOccupiedTile (blackCastle);
		
	}


	IEnumerator Player1Turn(){
		Debug.Log ("Player 1 Turn");
		
		bool isTurnOver = false;
		isPlayer1Turn = true;
		
		while(!isTurnOver) {
			
			isTurnOver = selectedPiece.turnTurner;
			
			yield return null;
		}
		
		selectedPiece.turnTurner = false;

		selectedPiece.HighlightMovementTiles (selectedPiece.MovementTiles, false);
		selectedPiece.MovementTiles.Clear ();

		selectedPiece = emptyPiece;

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
			
			isTurnOver = selectedPiece.turnTurner;
			
			yield return null;
		}
		
		selectedPiece.turnTurner = false;

		selectedPiece.HighlightMovementTiles (selectedPiece.MovementTiles, false);
		selectedPiece.MovementTiles.Clear ();
		
		selectedPiece = emptyPiece;

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
				didWhiteWin = false;
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
			
			toggleTileGUI = GUI.Toggle (new Rect (10, 40, 125, 20), toggleTileGUI, "Toggle Tile GUI");
			
			selectedPieceString = GUI.TextField (new Rect (10, Screen.height - 40, 300, 20), selectedPieceString, 300); 
		
		}

		else {
			//pass
		}

	}
	

	void Awake () {
		
		/*tileCoordinates = new GameObject[24,24];
		coordinates = new int[24,24];
		whitePieces = new f_Piece[13];
		blackPieces = new f_Piece[13];

		tileCoordinates = new GameObject[5,5];
		coordinates = new int[5,5];
		whitePieces = new f_Piece[2];
		blackPieces = new f_Piece[2];

		*/

		tileCoordinates = new GameObject[13,13];
		coordinates = new int[13,13];
		whitePieces = new f_Piece[13];
		blackPieces = new f_Piece[13];
		whiteRoyalties = new int[3];
		blackRoyalties = new int[3];

		gameOn = false;

		//toggleTileGUI = false;
		
		//SetUpBoard();

		//StartCoroutine (Game());
		
		//Debug.Log ("GameOver");
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//selectedPiece.occupiedTile.gameObject.transform.renderer.material.color = Color.red;
	
		
		
	}
}

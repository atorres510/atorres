using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	

	public Piece selectedPiece;
	public Piece emptyPiece;

	public King whiteKing;
	public King blackKing;

	public bool isPlayer1Turn;
	public bool isCheckPieceVulnerable;
	bool isGameOver;
	bool didWhiteWin;

	public int[,] coordinates;
	public GameObject[,] tileCoordinates;

	public Piece[] whitePieces;
	public Piece[] blackPieces;

	public bool toggleTileGUI;




	public void SetUpBoard(){

		GameObject[] tiles = new GameObject[64];

		tiles = GameObject.FindGameObjectsWithTag("Tile");

		for(int i = 0; i < tiles.Length; i++){

			Tile t = tiles[i].GetComponent<Tile>();

			tileCoordinates[t.x, t.y] = tiles[i];

		}

		int j = 0;
		int k = 0;

		GameObject[] pieces = new GameObject[32];

		pieces = GameObject.FindGameObjectsWithTag("Piece");

		for(int i = 0; i < pieces.Length; i++){

			Piece p = pieces[i].GetComponent<Piece>();

			coordinates[p.x, p.y] = p.pieceDesignator; // 0 Null; 1 B.Pawn; 2 B.Rook; etc;
			p.startTile = tileCoordinates[p.x, p.y];
			p.StartPosition();
			if(p.isWhite){
				whitePieces[j] = p;
				j++;
			}

			if(!p.isWhite){
				blackPieces[k] = p;
				k++;
			}

		}

	}

	public void UpdateThreatenedTiles(){

		GameObject[] pieces = new GameObject[32];
		
		pieces = GameObject.FindGameObjectsWithTag("Piece");
		
		for(int i = 0; i < pieces.Length; i++){

			if(pieces[i].transform.GetInstanceID() != selectedPiece.transform.GetInstanceID()){
				Piece p = pieces[i].GetComponent<Piece>();
				
				p.ThreatenTiles(true);
			}

			else{}

		}

	}

	public bool VerifyMove(GameObject target){

		if (selectedPiece.transform.GetInstanceID () == emptyPiece.transform.GetInstanceID ()) {
				
			return false;
		}

		//simulates piece movement
		int oldPieceDesignator = coordinates [selectedPiece.x, selectedPiece.y];
		coordinates [selectedPiece.x, selectedPiece.y] = 0;

		int targetX = 0;
		int targetY = 0;

		if (target.tag == "Tile") {
				
			Tile t = target.GetComponent<Tile>();
			targetX = t.x;
			targetY = t.y;
			coordinates [targetX, targetY] = selectedPiece.pieceDesignator;
		
		}

		if (target.tag == "Piece") {

			Piece p = target.GetComponent<Piece>();
			targetX = p.x;
			targetY = p.y;
			p.ThreatenTiles(false);
			coordinates [targetX, targetY] = selectedPiece.pieceDesignator;
				
		
		}

		GameObject[] pieces = new GameObject[32];
		
		pieces = GameObject.FindGameObjectsWithTag("Piece");
		
		for(int i = 0; i < pieces.Length; i++){
			Piece p = pieces[i].GetComponent<Piece>();

			if(p.x == targetX && p.y == targetY){
				/*pass, this is the target piece*/
			}

			else{
				if(!p.isValidMove(false)){

					//p.isCheckBlocked = false;
					coordinates[selectedPiece.x, selectedPiece.y] = selectedPiece.pieceDesignator;
					coordinates[targetX, targetY] = 0;
					return false;

				}

				else{};
			}

		}

		Debug.Log ("The move was verified");
		coordinates [selectedPiece.x, selectedPiece.y] = selectedPiece.pieceDesignator;
		coordinates [targetX, targetY] = 0;


		coordinates[targetX, targetY] = oldPieceDesignator;
		if (target.tag == "Tile") {

			coordinates[targetX, targetY] = 0;	
		
		}
		if (target.tag == "Piece") {
			
			Piece p = target.GetComponent<Piece>();
			p.ThreatenTiles(true);
	
		}
		
		return true;
	
	}


	public bool isCheckingPieceVulnerable(Piece p){


		//Debug.Log(p.x);
		//Debug.Log(p.y);
		//Debug.Log(p);
		Tile t = tileCoordinates[p.x, p.y].GetComponent<Tile>();

		if(p.isWhite){

			if(t.isThreatened == 2 || t.isThreatened == 3){

				return true;

			}

			if(p.isValidMove(true)){

				return true;

			}

			return false;

		}

		if(!p.isWhite){
			
			if(t.isThreatened == 1 || t.isThreatened == 3){
				
				return true;
				
			}
			
			if(p.isValidMove(true)){
				
				return true;
				
			}
			
			return false;
			
		}
		return false;
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
		UpdateThreatenedTiles ();


		if (blackKing.IsKingInCheck ()) {
			isCheckPieceVulnerable = isCheckingPieceVulnerable(selectedPiece);
			Debug.Log("Black King is in check");
		}
		if (blackKing.IsCheckmate ()) {
			Debug.Log("GAME OVER");
			isGameOver = true;
			didWhiteWin = true;
		}

		selectedPiece = emptyPiece;
		isCheckPieceVulnerable = true;


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
		UpdateThreatenedTiles ();
		//selectedPiece = emptyPiece;
		if (whiteKing.IsKingInCheck ()) {
			isCheckPieceVulnerable = isCheckingPieceVulnerable(selectedPiece);
			Debug.Log("White King is in check");
		}
		if (whiteKing.IsCheckmate ()) {
			Debug.Log("GAME OVER");
			isGameOver = true;
			didWhiteWin = false;
		}

		selectedPiece = emptyPiece;
		isCheckPieceVulnerable = true;

		yield return null;
	
	}
	

	IEnumerator Game(){

		while(!isGameOver){
			
			yield return StartCoroutine(Player1Turn());
			yield return StartCoroutine(Player2Turn());

		}
	
	}
	



	void OnGUI(){

		string player1Turn = " It is the White Turn";
		string player2Turn = " It is the Black Turn";
		string gameOverWhiteWin = " White wins!";
		string gameOverBlackWin = " Black wins!";

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

	}










	void Awake () {

		tileCoordinates = new GameObject[8,8];
		coordinates = new int[8,8];
		whitePieces = new Piece[16];
		blackPieces = new Piece[16];





		SetUpBoard();

		toggleTileGUI = false;


		StartCoroutine (Game());


	
	}
	
	// Update is called once per frame
	void Update () {

		//selectedPiece.occupiedTile.gameObject.transform.renderer.material.color = Color.red;




	
	}
}

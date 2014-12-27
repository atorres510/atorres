using UnityEngine;
using System.Collections;

public class f_EmptyPiece : f_Piece {

	public override void ProjectMovementTiles (){}
	
	//public abstract void ThreatenTiles (bool isUpdate);
	
	public override bool isTileOccupied (f_Tile t){
		return false;
	}
	
	public override bool isValidMove (f_Tile t){
		return false;
	}
	
	//public abstract void Move (GameObject targetTile);



	// Use this for initialization
	void Start () {
	
		InstantiateVariables ();



	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

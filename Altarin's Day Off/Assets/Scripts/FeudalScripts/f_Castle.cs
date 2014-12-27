using UnityEngine;
using System.Collections;

public class f_Castle : f_Tile {
	


	public int rotation;  //0 = greens up;  1 = greens left; 2 = greens down; 3 = greens right;
	public bool isWhite;



	public f_Tile castleGreens;


	public void SetUpCastleGreens(f_Tile g){

		if (rotation == 0) {

			g.x = x;
			g.y = y + 1;
		
		
		}

		else if (rotation == 1) {
			
			g.x = x - 1;
			g.y = y;

		}

		else if (rotation == 2) {
			
			g.x = x;
			g.y = y - 1;
			
			
		}

		else if (rotation == 3) {
			
			g.x = x - 1;
			g.y = y;
			
			
		}

		else {

			Debug.Log("Rotation integer out of scope");
			g.x = x;
			g.y = y;
		}

		ReplaceOccupiedTile (g);


		
	
	}


	public void ReplaceOccupiedTile(f_Tile g){

		f_Tile t = f_gameManager.tileCoordinates [g.x, g.y].GetComponent<f_Tile> ();
		g.transform.position = t.transform.position;
		Destroy (f_gameManager.tileCoordinates [g.x, g.y]);
		f_gameManager.tileCoordinates [g.x, g.y] = g.gameObject;
	
	
	}



	public bool isCaptured(){

		return false;
	
	
	}


	//void RotateCastle(){


	//}
	
	// Use this for initialization
	void Awake () {

		//SetUpCastleGreens (castleGreens);
		//ReplaceOccupiedTile (this);

		//meshCollider = gameObject.GetComponent<MeshCollider> ();
		boxCollider = gameObject.GetComponent<BoxCollider2D>();
		f_gameManagerObject = GameObject.FindGameObjectWithTag ("f_GameManager");
		f_gameManager = f_gameManagerObject.GetComponent<f_GameManager> ();
		mainCameraObject = GameObject.FindGameObjectWithTag ("MainCamera");
		mainCamera = mainCameraObject.GetComponent<Camera> ();

	}
	

}

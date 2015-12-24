using UnityEngine;
using System.Collections;

public class f_Castle : f_Tile {

	public int rotation;  //0 = greens up;  1 = greens left; 2 = greens down; 3 = greens right;
	public bool isWhite;
	public f_Piece.Faction faction;
	public bool isSetup;


	public f_Tile occupiedTile;
	public f_Tile castleGreens;

	#region Set Methods
	//sets variables for castle, including the assignment of the greens
	void SetVariables(bool iswhite, f_Piece.Faction fct, f_Tile cgrns, GameObject startTile){

		isWhite = iswhite;
		faction = fct;
		castleGreens = cgrns;

		occupiedTile = startTile.GetComponent<f_Tile>();
		transform.position = startTile.transform.position;

	}

	//uses player's faction and sprite library to assign sprites to castle and greens
	//also sets sprite to default texture 
	void SetCastleSprites(f_Piece.Faction fct, SpriteLibrary library){

		string key = fct.ToString();
		key = key.Trim();
		
		defaultTexture = library.GetSprite(key, 11);
		highLightedTexture = library.GetSprite(key, 12); 
		highLightedHostileTexture = library.GetSprite(key, 13); 

		castleGreens.defaultTexture = library.GetSprite(key, 14);
		castleGreens.highLightedTexture = library.GetSprite(key, 15); 
		castleGreens.highLightedHostileTexture = library.GetSprite(key, 16); 

		SpriteRenderer castleRenderer = gameObject.GetComponent<SpriteRenderer>();
		castleRenderer.sprite = defaultTexture;

		SpriteRenderer castleGreensRenderer = castleGreens.gameObject.GetComponent<SpriteRenderer>();
		castleGreensRenderer.sprite = castleGreens.defaultTexture;

	}

	//for public use by setupmanager after instantiation.
	public void SetUpCastle(Player player, SpriteLibrary library, f_Tile cgrns, GameObject startTile){

		SetVariables(player.isWhite, player.faction, cgrns, startTile);
		SetCastleSprites(player.faction, library);

	}



	#endregion

	public void ReplaceOccupiedTile(f_Tile g){
		
			f_Tile t = f_gameManager.tileCoordinates [g.x, g.y].GetComponent<f_Tile> ();
			g.transform.position = t.transform.position;
			//Debug.Log (t + " : " + g);
			//Debug.Log(g.x + ", " + g.y);
			if(t.isOccupied){
				g.isOccupied = t.isOccupied;

				
			}
			Destroy (f_gameManager.tileCoordinates [g.x, g.y]);
			f_gameManager.tileCoordinates [g.x, g.y] = g.gameObject;
		
	}


	public void UpdateCastleGreensPos(){

		if(rotation == 0){
			//z used to equal -10.0f
			Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y + 1.06f, this.transform.position.z);
			castleGreens.transform.position = pos;
			castleGreens.x = x;
			castleGreens.y = y + 1;

		}

		else if(rotation == 1){

			Vector3 pos = new Vector3(this.transform.position.x - 1.06f, this.transform.position.y, this.transform.position.z);
			castleGreens.transform.position = pos;
			
			castleGreens.x = x - 1;
			castleGreens.y = y;

			
			
		}

		else if(rotation == 2){
			
			Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y - 1.06f, this.transform.position.z);
			castleGreens.transform.position = pos;
			castleGreens.x = x;
			castleGreens.y = y - 1;

			
		}

		else if(rotation == 3){


			
			//Vector3 pos = new Vector3(this.transform.position.x + 1.06f, this.transform.position.y, -10.0f);

			float normalizedXPosition = ((1.06f * (transform.localScale.x)) - 1.06f);

			Vector3 pos = new Vector3((this.transform.position.x + normalizedXPosition + 1.06f), this.transform.position.y, this.transform.position.z);

			castleGreens.transform.position = pos;
			castleGreens.x = x + 1;
			castleGreens.y = y;

		}

		else{}



	}



	/*public bool isCaptured(){

		return false;
	
	
	}*/





	
	// Use this for initialization
	void Awake () {

		//SetUpCastleGreens (castleGreens);
		//ReplaceOccupiedTile (this);

		//meshCollider = gameObject.GetComponent<MeshCollider> ();
		isSetup = true;
		boxCollider = gameObject.GetComponent<BoxCollider2D>();
		f_gameManagerObject = GameObject.FindGameObjectWithTag ("f_GameManager");
		f_gameManager = f_gameManagerObject.GetComponent<f_GameManager> ();
		//mainCameraObject = GameObject.FindGameObjectWithTag ("MainCamera");
		//mainCamera = mainCameraObject.GetComponent<Camera> ();

	}

	void Update(){


		if(isSetup){

			UpdateCastleGreensPos();

		}




	}
	

}

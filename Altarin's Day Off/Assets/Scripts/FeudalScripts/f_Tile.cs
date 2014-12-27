using UnityEngine;
using System.Collections;

public class f_Tile : MonoBehaviour {

	public int x;
	public int y;
	
	public bool isOccupied;

	// 0 = Empty; 1 = oops forgot to assign; 2 = rough terrain;  3 = Mountain terrain;  4 = castle greens; 5 = castle;
	public int tileType; 


	public Sprite defaultTexture;
	public Sprite highLightedTexture;
	public Sprite highLightedHostileTexture;
	
	public MeshCollider meshCollider;
	
	public GameObject f_gameManagerObject;
	public f_GameManager f_gameManager;
	public GameObject mainCameraObject;
	public Camera mainCamera;

	
	
	void DisableCollider(bool occupied){
		
		if (occupied) {
			meshCollider.enabled = false;
		}
		
		else {
			meshCollider.enabled = true;
		}
	}




	
	void MovePiece(f_Piece p){

		foreach (f_Tile t in p.MovementTiles) {
				
			if(t == this && p.isValidMove(t)){



				p.ChangePosition(t);
				break;

			}

			else{
				//pass
			}
		
		}
	
	
	}




	void OnMouseOver(){

		if (f_gameManager.gameOn) {
				
			if (Input.GetMouseButtonDown (0)) {
				//moves piece if the check is cleared.
				MovePiece(f_gameManager.selectedPiece);
				
			}
		
		}

	}


	void Start () {
		//instantiation
		meshCollider = gameObject.GetComponent<MeshCollider> ();
		f_gameManagerObject = GameObject.FindGameObjectWithTag ("f_GameManager");
		f_gameManager = f_gameManagerObject.GetComponent<f_GameManager> ();
		mainCameraObject = GameObject.FindGameObjectWithTag ("MainCamera");
		mainCamera = mainCameraObject.GetComponent<Camera> ();

		
	}
	

	void Update () {
		
		DisableCollider (isOccupied);
		
		
	}
}

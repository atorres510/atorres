using UnityEngine;
using System.Collections;

public class BannerBehaviour : MonoBehaviour {


	public bool isWhite;
	public float xRatio;
	public float yRatio;
	Rect screenPosition;


	GameObject f_setUpManagerObject;

	f_GameManager f_gameManager;
	f_SetUpManager f_setUpManager;

	SpriteRenderer spriteRenderer;
	Texture2D bannerTexture;




	void UpdateSpriteRenderer(){

		if (isWhite) {
				
			if(!f_gameManager.gameOn){

				if(f_setUpManager.isWhiteSetUp){
					
					spriteRenderer.enabled = true;
					
				}
				
				else{
					
					spriteRenderer.enabled = false;
					
				}

			}

			else {

				if(f_gameManager.isPlayer1Turn){
					
					spriteRenderer.enabled = true;
					
				}
				
				else{
					
					spriteRenderer.enabled = false;
					
				}

			}
		
		}

		else{
			
			if(!f_gameManager.gameOn){
				
				if(!f_setUpManager.isWhiteSetUp){
					
					spriteRenderer.enabled = true;
					
				}
				
				else{
					
					spriteRenderer.enabled = false;
					
				}
	
			}
			
			else {
				
				if(!f_gameManager.isPlayer1Turn){
					
					spriteRenderer.enabled = true;
					
				}
				
				else{
					
					spriteRenderer.enabled = false;
					
				}
				
	
			}
			
			
		}

	
	}



	//void OnGUI(){

	//	GUI.DrawTexture (screenPosition, bannerTexture, ScaleMode.ScaleToFit, true, 0);
	//}



	void Start(){

		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
	//	bannerTexture = spriteRenderer.sprite.texture;
		//Debug.Log (bannerTexture);

		//screenPosition.x = Screen.width * xRatio;
		//screenPosition.y = Screen.height * yRatio;
		//screenPosition.z = 1;


		f_setUpManagerObject = GameObject.FindGameObjectWithTag ("f_SetUpManager");
		f_setUpManager = f_setUpManagerObject.GetComponent<f_SetUpManager> ();
		f_gameManager = f_setUpManager.f_gameManager;
	
	
	
	}


	void Update () {
	
		UpdateSpriteRenderer ();

	}
}

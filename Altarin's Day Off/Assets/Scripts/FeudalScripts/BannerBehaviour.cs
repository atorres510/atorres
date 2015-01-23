using UnityEngine;
using System.Collections;

public class BannerBehaviour : MonoBehaviour {


	public bool isWhite;

	GameObject f_setUpManagerObject;

	f_GameManager f_gameManager;
	f_SetUpManager f_setUpManager;

	SpriteRenderer spriteRenderer;




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




	void Start(){

		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();

		f_setUpManagerObject = GameObject.FindGameObjectWithTag ("f_SetUpManager");
		f_setUpManager = f_setUpManagerObject.GetComponent<f_SetUpManager> ();
		f_gameManager = f_setUpManager.f_gameManager;
	
	
	
	}


	void Update () {
	
		UpdateSpriteRenderer ();

	}
}

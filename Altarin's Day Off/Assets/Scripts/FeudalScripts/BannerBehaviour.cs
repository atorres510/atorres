using UnityEngine;
using System.Collections;

public class BannerBehaviour : MonoBehaviour {

	public Sprite[] bannerSprites;


	GameObject f_setUpManagerObject;
	f_GameManager f_gameManager;
	f_SetUpManager f_setUpManager;

	UnityEngine.UI.Image image;


	void UpdateImage(){

		if (!f_setUpManager.isSetUp) {
				
			ChangeImage(0);
		
		}

		else if (!f_gameManager.gameOn) {
				
			if (f_setUpManager.isWhiteSetUp) {
				
				ChangeImage(1);
				
				
			}
			
			else{
				
				ChangeImage(2);
				
			}
		
		
		}

		else{

			if (f_gameManager.isPlayer1Turn) {
				
				ChangeImage(1);
				
				
			}

			else{

				ChangeImage(2);

			}


		}
	
	
	}









	void ChangeImage(int spriteID){

		image.sprite = bannerSprites [spriteID];
	

	}




	void Start(){
		image = gameObject.GetComponent<UnityEngine.UI.Image> ();
		f_setUpManagerObject = GameObject.FindGameObjectWithTag ("f_SetUpManager");
		f_setUpManager = f_setUpManagerObject.GetComponent<f_SetUpManager> ();
		f_gameManager = f_setUpManager.f_gameManager;
	
	
	
	}


	void Update () {
	
		UpdateImage ();

	}
}

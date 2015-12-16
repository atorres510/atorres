using UnityEngine;
using System.Collections;

public class ScreenBehavior : MonoBehaviour {

	//sets position using anchor tile and which side it will hide
	void SetScreenPosition(f_Tile anchor, bool isWhiteScreen){
	
		Vector3 anchorPos = anchor.transform.position;
	
		Vector3 posAdjustment;

		Vector3 adjustedPos = new Vector3(0,0,0);


		if (isWhiteScreen) {
				
			posAdjustment = new Vector3(0.53f, 7.42f, -9.0f); 

		}

		else {
				
			posAdjustment = new Vector3(0.53f, -7.42f, -9.0f); 
		
		}

		adjustedPos = anchorPos + posAdjustment;
		
		gameObject.transform.position = adjustedPos;

	}

	//retreives spriter renderer on this gameobject and assigns it a new sprite.
	void SetScreenSprite(Sprite s){

		SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();

		spriteRenderer.sprite = s;

	}
	//method to be used by setupmanager
	public void SetUpScreen(f_Tile anchor, bool isWhiteScreen, Sprite sprite){

		SetScreenPosition(anchor, isWhiteScreen);

		SetScreenSprite(sprite);

	}
	


}

using UnityEngine;
using System.Collections;

public class ScreenBehavior : MonoBehaviour {
	

	public Player myPlayer;
	public f_Tile anchorTile;

	SpriteRenderer spriteRenderer;

	public Sprite[] screenSprites;
	




	public void SetUpPosition(f_Tile anchor){

		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();

		Vector3 anchorPos = anchor.transform.position;

		Vector3 adjustedPos = new Vector3(0,0,0);

		if (myPlayer.isWhite) {
				
			Vector3 posAdjustment = new Vector3(0.53f, 7.42f, -9.0f); 
		
			adjustedPos = anchorPos + posAdjustment;

			gameObject.transform.position = adjustedPos;

			spriteRenderer.sprite = screenSprites[0];
		
		} 


		else {
				
			Vector3 posAdjustment = new Vector3(0.53f, -7.42f, 0.0f); 
			
			adjustedPos = anchorPos + posAdjustment;
			
			gameObject.transform.position = adjustedPos;
			
			spriteRenderer.sprite = screenSprites[1];
	
		}



	}
	


}

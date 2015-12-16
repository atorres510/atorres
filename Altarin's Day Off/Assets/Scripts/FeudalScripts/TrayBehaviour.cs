using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class TrayBehaviour : MonoBehaviour {

	//public f_Tile[] slots;

	GridLayoutGroup gridLayoutGroup;

	//changes the spacing.y in the grid layout group of the piece panel to better fit the screen
	void CorrectButtonSpacing(){
		
		gridLayoutGroup = GetComponent<GridLayoutGroup> ();
		
		float spacingX = gridLayoutGroup.spacing.x;
		float spacingY = gridLayoutGroup.spacing.y;
		
		float aspectRatio = Screen.height;
		
		float desiredSpacing = (0.131f * aspectRatio) - 69.76f;
		
		spacingY = desiredSpacing;
		
		
		if (desiredSpacing < 0) {
			
			gridLayoutGroup.spacing = new Vector2(spacingX,0);
			
		}
		
		else{
			
			gridLayoutGroup.spacing = new Vector2(spacingX, spacingY);
			
		}
		
		
	}


	//uses sprite library and player's faction to determine sprites for buttons
	void SetButtonSprites(f_Piece.Faction faction, SpriteLibrary library){

		//find buttons that are children of this tray
		Button[] buttons;
		buttons = GetComponentsInChildren<Button> ();

		//sprite order: 7.7.7.7.6.6.5.4.4.8.2.3.1.castle.castlegreens
		int [] spriteOrder = {7, 7, 7, 7, 6, 6, 5, 4, 4, 8, 2, 3, 1, 0, 0};

		for (int i = 0; i < buttons.Length; i++) {
				
			//set sprite
			buttons[i].image.sprite = library.GetSprite(faction.ToString(), spriteOrder[i]);
		
		}
	
	}

	//public for use by setupmanager.
	public void SetUpTray(f_Piece.Faction playerFaction, SpriteLibrary library){

		CorrectButtonSpacing();
		SetButtonSprites(playerFaction, library);

	}

}

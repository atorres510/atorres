using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class TrayBehaviour : MonoBehaviour {
	
	Button[] buttons;
	int[] buttonIDs; 

	GridLayoutGroup gridLayoutGroup;

	#region Button Methods
	//find buttons that are children of this tray and 
	//assign them to the buttons array. for use by SetUpTray  
	void SetButtons(){

		buttons = GetComponentsInChildren<Button> ();

	}

	//return buttons. for public use.
	public Button[] GetButtons(){

		return buttons;

	}

	#region SetButtonIDs()
	//pikeman x 4, sergeant x 2, mage, knight x 2, rogue, prince, duke, king, castle, castlegreens
	//initializes the piece ID array and fills its elements with the player's pieces' id's in the 
	//designated order.  Used in SetUpTray.
	void SetButtonIDs(Player player){

		f_Piece[] pieces = player.pieceSet;

		buttonIDs = new int[(pieces.Length + 1)]; //extra +1 to accomidate for addition of castle

		int w; //used to adjust the piece designator between white and black
		if(player.isWhite){
			w = 0;
		}

		else{
			w = 1;
		}

		int k = 0; //for use by pieceIDs array
		
		f_Piece[] pikemen = new f_Piece[4];
		int j = 0; //for pikemen, sergeants, and knights arrays.

		//looks for pikemen using the piece designator(normalized for white/black pieces)
		//puts this in the pikemen array to be added to the pieceIDs later
		for(int i = 0; i< pieces.Length; i++){

			if(pieces[i].pieceDesignator == (7 + (8 * w))){

				pikemen[j] = pieces[i];
				j++;

			}

			else{}

		}

		//puts pikemen IDs in the array
		for(int i = 0; i < 4; i++){

			buttonIDs[k] = pikemen[i].pieceID;
			k++;

		}

		f_Piece[] sergeants = new f_Piece[2];
		j = 0;
		//looks for sergeants using the piece designator(normalized for white/black pieces)
		//puts this in the sergeants array to be added to the pieceIDs later
		for(int i = 0; i< pieces.Length; i++){
			
			if(pieces[i].pieceDesignator == (6 + (8 * w))){
				
				sergeants[j] = pieces[i];
				j++;
				
			}
			
			else{}
			
		}

		//puts sergeants' IDs in the array
		for(int i = 0; i < 2; i++){
			
			buttonIDs[k] = sergeants[i].pieceID;
			k++;
			
		}

		//looks for the mage, then addes it to the piece ids;
		for(int i = 0; i < pieces.Length; i++){
			
			if(pieces[i].pieceDesignator == (5 + (8 * w))){
				
				buttonIDs[k] = pieces[i].pieceID;
				k++;
				break;
			}
			
			else{}
			
		}

		f_Piece[] knights = new f_Piece[2];
		j = 0;
		//looks for knights using the piece designator(normalized for white/black pieces)
		//puts this in the knights array to be added to the pieceIDs later
		for(int i = 0; i< pieces.Length; i++){
		
			if(pieces[i].pieceDesignator == (4 + (8 * w))){
				
				knights[j] = pieces[i];
				j++;
				
			}
			
			else{}
			
		}

		//puts knights' IDs in the array
		for(int i = 0; i < 2; i++){
			
			buttonIDs[k] = knights[i].pieceID;
			k++;
			
		}

		//looks for the rogue, then addes it to the piece ids;
		for(int i = 0; i < pieces.Length; i++){
			
			if(pieces[i].pieceDesignator == (8 + (8 * w))){
				
				buttonIDs[k] = pieces[i].pieceID;
				k++;
				break;
			}
			
			else{}
			
		}

		//looks for the prince, then addes it to the piece ids;
		for(int i = 0; i < pieces.Length; i++){
			
			if(pieces[i].pieceDesignator == (2 + (8 * w))){
				
				buttonIDs[k] = pieces[i].pieceID;
				k++;
				break;
			}
			
			else{}
			
		}

		//looks for the duke, then addes it to the piece ids;
		for(int i = 0; i < pieces.Length; i++){
			
			if(pieces[i].pieceDesignator == (3 + (8 * w))){
				
				buttonIDs[k] = pieces[i].pieceID;
				k++;
				break;
			}
			
			else{}
			
		}

		//looks for the king, then addes it to the piece ids;
		for(int i = 0; i < pieces.Length; i++){
			
			if(pieces[i].pieceDesignator == (1 + (8 * w))){
				
				buttonIDs[k] = pieces[i].pieceID;
				k++;
				break;
			}
			
			else{}
			
		}

		//adds an ID for the castle.  assigns it the last ID for the list, then adjusts for # of players to
		//give it a unique ID.  
		buttonIDs[k] = (pieces.Length + 1) + ((player.playerNumber - 1) * (pieces.Length + 1));

	}
	#endregion
	
	public int[] GetButtonIDs(){
		
		return buttonIDs;
		
	}

	#endregion

	#region Tray/button UI setup methods
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
		
		//sprite order: 7.7.7.7.6.6.5.4.4.8.2.3.1.castle.castlegreens,empty
		int [] spriteOrder = {7, 7, 7, 7, 6, 6, 5, 4, 4, 8, 2, 3, 1, 11, 0};

		for (int i = 0; i < buttons.Length; i++) {
				
			//set sprite
			buttons[i].image.sprite = library.GetSprite(faction.ToString(), spriteOrder[i]);
		
		}
	
	}

	void SetTrayColor(f_Piece.Faction playerFaction){

		//get gameobject's image component
		Image image = gameObject.GetComponent<Image>();
		
		f_Piece.Faction[] factions;
		
		//gathers enum values and place them into the array
		factions = (f_Piece.Faction[])System.Enum.GetValues(typeof(f_Piece.Faction));

		//list of different colors for each faction.  new colors need to be added with 
		//the addition of new factions.
		Color32 clanColors = new Color32(153, 110, 110, 219);
		Color32 battalionColors = new Color32(148, 162, 255, 192);
		Color32 blankColors = new Color32(255, 255, 255, 255);

		//colors need to be in the same order as they are listed in the f_Piece enum declaration.  
		Color32[] factionColors = {clanColors, battalionColors, blankColors};

		//find the player's faction in the list and then return the corrisponding color
		for(int i = 0; i < factions.Length; i++){

			if(playerFaction == factions[i]){

				image.color = factionColors[i];
				break;

			}

			else{}

		}

	}

	#endregion
	
	//public for use by setupmanager.
	public void SetUpTray(Player player, SpriteLibrary library){

		f_Piece.Faction playerFaction = player.faction;

		SetButtons();
		CorrectButtonSpacing();
		SetButtonSprites(playerFaction, library);
		SetTrayColor(playerFaction);
		SetButtonIDs(player);

	}



}

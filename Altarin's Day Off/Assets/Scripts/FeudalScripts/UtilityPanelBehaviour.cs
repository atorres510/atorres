using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UtilityPanelBehaviour : MonoBehaviour {

	//identical to SetTrayColor in TrayBehaviour, with the exception of a different set of colors.  
	//for public use by setupmanager.
	public void SetPanelColor(f_Piece.Faction playerFaction){

		Image image = gameObject.GetComponent<Image>();
		
		f_Piece.Faction[] factions;
		
		//gathers enum values and place them into the array
		factions = (f_Piece.Faction[])System.Enum.GetValues(typeof(f_Piece.Faction));
		

		Color32 clanColors = new Color32(165, 61, 61, 255);
		Color32 battalionColors = new Color32(63, 135, 253, 173);;
		Color32 blankColors = new Color32(255, 255, 255, 177);
		
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






}

﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class TrayBehaviour : MonoBehaviour {

	public f_Tile[] slots;



	GridLayoutGroup gridLayoutGroup;
	
	
	//changes the spacing.y in the grid layout group of the piece panel to better fit the screen
	void FixTheFuckingSpacing(){
		
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


	//sprite order: 6.6.6.6.7.7.5.4.4.8.2.3.1.castle.greens
	void FillTheFuckOuttaTheButtons(){

	
		Button[] buttons;

		buttons = GetComponentsInChildren<Button> ();

		int [] spriteOrder = {6, 6, 6, 6, 7, 7, 5, 4, 4, 8, 2, 3, 1};

		Sprite newSprite = Resources.Load<Sprite>("Sprites/FeudalSprites/FactionSprites/Battalion/Altarin");

		Debug.Log (newSprite);

		buttons [0].image.sprite = newSprite;

	
	}



	void Start(){

		FixTheFuckingSpacing();
		FillTheFuckOuttaTheButtons ();


	}

}

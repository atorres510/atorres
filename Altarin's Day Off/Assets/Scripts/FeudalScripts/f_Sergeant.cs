﻿using UnityEngine;
using System.Collections;

public class f_Sergeant : f_Piece {


	int boardLength;

	public override void ProjectMovementTiles (){

		
		if (this.occupiedTile.tileType == 5) {
			
			f_Castle castle = occupiedTile.GetComponent<f_Castle>();
			f_Tile greens = castle.castleGreens;
			
			MovementTiles.Add (greens);
			//Debug.Log("hi");
			
		}
		
		
		else {
			
			int i = x + 1;
			int j = y + 1;
			
			while (i < (x + 13) && j < (y + 13)) {
				if(i < 0 || j < 0 || i >= boardLength || j >= boardLength){
					break;
				}
				
				else{
					
					f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
					
					if(t.tileType == 5){
						
						MovementTiles.Add(t);
						//Debug.Log("hi");
						break;
						
						
					}
					
					else {
						
						if(!isTileOccupied(t)){
							
							MovementTiles.Add(t);
							i++;
							j++;
						}
						
						else{
							
							MovementTiles.Add(t);
							break;
						}
						
						
					}
					
					
				}
				
				
			}
			
			
			
			i = x + 1;
			j = y - 1;
			
			while (i < (x + 13) && j > (y - 13)) {

				//Debug.Log(i + " ," +j);
				if(i < 0 || j < 0 || i >= boardLength || j >= boardLength){
					break;
				}
				
				else{
					
					f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();

					if(t.tileType == 5){
						
						MovementTiles.Add(t);
						//Debug.Log("hi");
						break;
						
						
					}
					
					else {
						
						if(!isTileOccupied(t)){
							
							MovementTiles.Add(t);
							i++;
							j--;
						}
						
						else{
							
							MovementTiles.Add(t);
							break;
						}
						
					}
					
					
					
					
				}
				
				
			}
			
			i = x - 1;
			j = y - 1;
			
			while (i > (x - 13) && j > (y - 13)) {
				if(i < 0 || j < 0 || i >= boardLength || j >= boardLength){
					break;
				}
				
				else{
					
					f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
					
					if(t.tileType == 5){
						
						MovementTiles.Add(t);
						//Debug.Log("hi");
						break;
						
						
					}
					
					else {
						
						if(!isTileOccupied(t)){
							
							MovementTiles.Add(t);
							i--;
							j--;
						}
						
						else{
							
							MovementTiles.Add(t);
							break;
						}
						
					}
					
					
					
				}
				
			}
			
			
			i = x - 1;
			j = y + 1;
			
			while (i > (x - 13) && j < (y + 13)) {
				if(i < 0 || j < 0 || i >= boardLength || j >= boardLength){
					break;
				}
				
				else{
					
					f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
					
					if(t.tileType == 5){
						
						MovementTiles.Add(t);
						//Debug.Log("hi");
						break;
						
						
					}
					
					else {
						
						if(!isTileOccupied(t)){
							
							MovementTiles.Add(t);
							i--;
							j++;
						}
						
						else{
							
							MovementTiles.Add(t);
							break;
						}
						
						
					}
					
					
					
					
				}
			}
			
			
			i = x;
			j = y + 1;
			
			while (j < (y + 2)) {
				if(i < 0 || j < 0 || i >= boardLength || j >= boardLength){
					break;
				}
				
				else{
					
					f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
					
					if(t.tileType == 5){
						
						MovementTiles.Add(t);
						//Debug.Log("hi");
						break;
						
						
					}
					
					else {
						
						if(!isTileOccupied(t)){
							
							MovementTiles.Add(t);
							
							j++;
						}
						
						else{
							
							MovementTiles.Add(t);
							break;
						}
						
					}
					
					
					
					
				}
				
				
			}
			
			
			i = x + 1;
			j = y;
			
			while (i < (x + 2)) {
				//Debug.Log(i);
				if(i < 0 || j < 0 || i >= boardLength || j >= boardLength){
					break;
				}
				
				else{
					
					f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
					
					if(t.tileType == 5){
						
						MovementTiles.Add(t);
						//Debug.Log("hi");
						break;
						
						
					}
					
					else {
						
						if(!isTileOccupied(t)){
							
							MovementTiles.Add(t);
							i++;
							
						}
						
						else{
							
							MovementTiles.Add(t);
							break;
						}
						
					}
					
					
					
				}
				
				
			}
			
			
			i = x;
			j = y - 1;
			
			while (j > (y - 2)) {
				if(i < 0 || j < 0 || i >= boardLength || j >= boardLength){
					break;
				}
				
				else{
					
					f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
					
					if(t.tileType == 5){
						
						MovementTiles.Add(t);
						//Debug.Log("hi");
						break;
						
						
					}
					
					else {
						
						if(!isTileOccupied(t)){
							
							MovementTiles.Add(t);
							
							j--;
						}
						
						else{
							
							MovementTiles.Add(t);
							break;
						}
						
					}
					
					
					
				}
				
				
			}
			
			
			i = x - 1;
			j = y;
			
			while (i > (x - 2)) {
				if(i < 0 || j < 0 || i >= boardLength || j >= boardLength){
					break;
				}
				
				else{
					
					f_Tile t = f_gameManager.tileCoordinates[i,j].GetComponent<f_Tile>();
					
					if(t.tileType == 5){
						
						MovementTiles.Add(t);
						//Debug.Log("hi");
						break;
						
						
					}
					
					else {
						
						if(!isTileOccupied(t)){
							
							MovementTiles.Add(t);
							i--;
							
						}
						
						else{
							
							MovementTiles.Add(t);
							break;
						}
						
					}
					
					
					
				}
				
				
				
			}
			
		}
		



	}

	public override bool isTileOccupied (f_Tile t){
		f_Tile i = f_gameManager.tileCoordinates [t.x, t.y].GetComponent<f_Tile> ();
		f_Tile j = f_gameManager.tileCoordinates [x, y].GetComponent<f_Tile> ();
		//if selected piece occupies the greens
		if (j.tileType == 4) {
			
			if (i.tileType == 3 || i.isOccupied) {
				
				return true;
				
			} 
			else {
				return false;
			}
			
		}
		
		//if selected piece occupies the castle only allow movement to the greens
		else if(j.tileType == 5){
			
			if(i.tileType == 2 || i.tileType == 3 || i.tileType == 0 || i.tileType == 5){
				
				return true;
				
			}
			
			else {
				
				return false;
			}
			
		}
		
		//if not occupying castle or greens
		else{
			
			if (i.tileType == 3 || i.tileType == 4 || i.tileType == 5 ||  i.isOccupied) {
				return true;
			} 
			
			else {
				return false;
			}
			
		}

	}
	
	public override bool isValidMove (f_Tile t){
		f_Tile i = f_gameManager.tileCoordinates [x, y].GetComponent<f_Tile> ();
		
		if (this.isWhite) {
			if (f_gameManager.coordinates[t.x,t.y] > 8 || f_gameManager.coordinates[t.x,t.y] == 0){
				//if this piece occupies the greens
				if(i.tileType == 4){
					
					if(t.tileType == 4 || t.tileType == 5 || t.tileType == 0 || t.tileType == 2) {
						return true;
					}
					
					else{
						return false;
					}
					
				}
				
				//if this piece occupies the castle, only allow movement to the greens
				else if(i.tileType == 5){
					
					if(t.tileType == 4) {
						return true;
					}
					
					else{
						
						return false;
					}
					
				}
				
				
				//if not occupying castle or greens
				else if(t.tileType == 4 || t.tileType == 0 ||t.tileType == 2) {
					return true;
				}
				
				else{
					return false;
				}
			}
			
			else {
				return false;
			}	
		}
		
		
		
		else {
			if (f_gameManager.coordinates[t.x,t.y] < 9 || f_gameManager.coordinates[t.x,t.y] == 0){
				//if this piece occupies the greens
				if(i.tileType == 4){
					
					if(t.tileType == 4 || t.tileType == 5 || t.tileType == 0 || t.tileType == 2) {
						return true;
					}
					
					else{
						return false;
					}
					
				}
				//if this piece occupies the castle, only allow movement to the greens
				else if(i.tileType == 5){
					
					if(t.tileType == 4) {
						return true;
					}
					
					else{
						
						return false;
					}
					
				}
				//if not occupying castle or greens
				else if(t.tileType == 4 || t.tileType == 0 || t.tileType == 2) {
					return true;
				}
				
				else{
					return false;
				}
			}
			
			else {
				return false;
			}	
			
		}

	}
	






	void Start () {

		InstantiateVariables ();
		float l = Mathf.Sqrt (f_gameManager.tileCoordinates.Length);
		boardLength = (int)l;
	
	}
	
	void Update(){
		
		if (f_gameManager.gameOn) {
			TogglePieceCollider (turnTurner);
		}

		
	}

}

using UnityEngine;
using System.Collections;

public class Enemy2D : MonoBehaviour {
	
	//Reference to Game Manager Scrpit
	//public GameManager gameManager;
	
	//Enemy Starting/End Positions
	float startingPosition;
	float endPosition;
	
	//Units Enemy Moves Right
	public int unitsToMove = 5;
	
	//Enemies Movement Speed
	public int moveSpeed = 2;
	
	
	//EnemyMoving Right or left
	bool moveRight = true;
	
	
	void Awake()
	{
		startingPosition = transform.position.x;
		endPosition = startingPosition + unitsToMove;
	
	}
	
	void Update()
	{
		if(moveRight){
			GetComponent<Rigidbody>().position += Vector3.right * moveSpeed * Time.deltaTime;
			}
			
		if(GetComponent<Rigidbody>().position.x >= endPosition){
			moveRight = false;
			}
		
		if(!moveRight){
			GetComponent<Rigidbody>().position -= Vector3.right * moveSpeed * Time.deltaTime;
			
		}
		
		if(GetComponent<Rigidbody>().position.x <= startingPosition){
			moveRight = true;
		}
		
			
	}
	
	
	
	//int damageValue = 1;
	/*
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player"){
			gameManager.SendMessage ("PlayerDamage", damageValue, SendMessageOptions.DontRequireReceiver);
			gameManager.controller2D.SendMessage("TakenDamage", SendMessageOptions.DontRequireReceiver);
			
		}	
	}*/
}
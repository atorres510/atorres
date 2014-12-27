using UnityEngine;
using System.Collections;

public class Controller2D : MonoBehaviour {
	
	//Reference to Character Controller
	CharacterController characterController;
	public float walkSpeed = 5;
	public float jumpHeight = 5;
	
	float takenDamage = 0.2f;
	
	//Controller Player Movement Direction
	Vector4 moveDirection = Vector4.zero;
	float horizontal = 0;
	float vertical = 0;

	// Use this for initialization
	void Start () {
		// Controls Character Controller
		characterController = GetComponent<CharacterController>();
		

	}
	
	// Update is called once per frame
	void Update () {
		
		characterController.Move(moveDirection * Time.deltaTime);
		horizontal = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");
		
		//Move Player Right
		if(horizontal > 0.01){
			moveDirection.x = horizontal * walkSpeed;
		}
		// Move Player Left
		if(horizontal < 0.01){
			moveDirection.x = horizontal * walkSpeed;
		}
		
		// Moves Player Up
		if (vertical > 0.01){
			moveDirection.y = vertical * walkSpeed;
		}
		
		// Moves Player Down
		if (vertical < 0.01){
			moveDirection.y = vertical * walkSpeed;
		}
	}
	
	public IEnumerator TakenDamage(){
		renderer.enabled = false;
		yield return new WaitForSeconds(takenDamage);
		renderer.enabled = true;
		yield return new WaitForSeconds(takenDamage);
		renderer.enabled = false;
		yield return new WaitForSeconds(takenDamage);
		renderer.enabled = true;
		yield return new WaitForSeconds(takenDamage);
		renderer.enabled = false;
		yield return new WaitForSeconds(takenDamage);
		renderer.enabled = true;
		yield return new WaitForSeconds(takenDamage);
	}
}

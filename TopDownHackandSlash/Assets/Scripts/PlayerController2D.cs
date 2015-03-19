using UnityEngine;
using System.Collections;

public class PlayerController2D : MonoBehaviour {

	Animator anim;

	public float playerWalkSpeed;
	public float playerSprintSpeed;

	private float playerSpeed;
	private int direction;


	public int Direction{

		get{
			return direction;
		}

	}


	void Start () {
		
		playerSpeed = playerWalkSpeed;
		anim = GetComponent<Animator> ();
		
	}
	
	void Update () {
		
		PlayerController ();
		
	}



	public void TogglePlayerController(){
		
		//references this component
		PlayerController2D thisPlayerController2D = gameObject.GetComponent<PlayerController2D> ();

		if (thisPlayerController2D.enabled == true) {
			Debug.Log("Controller Disabled");
			thisPlayerController2D.enabled = false;
			anim.SetInteger("direction", 0);
			direction = 0;

		}

		else {
			Debug.Log("Controller enabled");
			thisPlayerController2D.enabled = true;
			
		}
		
		
	}

	//recieves player's input and moves this gameobject
	void PlayerController(){//1-left 2-right 3-up 4-daaawown

		//Player movement controled with WASD
		if (Input.GetKey(KeyCode.W)){
			rigidbody2D.transform.position += Vector3.up * playerSpeed * Time.fixedDeltaTime;
			anim.SetInteger("direction", 3);
			direction = 3;
		}
		if (Input.GetKey(KeyCode.S)){
			rigidbody2D.transform.position += Vector3.down * playerSpeed * Time.deltaTime;
			anim.SetInteger("direction", 4);
			direction = 4;
		}
		if (Input.GetKey(KeyCode.A)){
			rigidbody2D.transform.position += Vector3.left * playerSpeed * Time.deltaTime;
			//anim.SetInteger("direction", 1);
		}
		if (Input.GetKey(KeyCode.D)){
			rigidbody2D.transform.position += Vector3.right * playerSpeed * Time.deltaTime;
			//anim.SetInteger("direction", 2);
		}

		//reset direction back to 0
		if (Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.S) || Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.D)) {
				
			anim.SetInteger("direction", 0);
			direction = 0;
		
		}

		//Sprint Toggle w/ LeftShift
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			playerSpeed = playerSprintSpeed;
		} 
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			playerSpeed = playerWalkSpeed;
		}

	}







}

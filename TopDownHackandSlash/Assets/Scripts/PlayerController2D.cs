using UnityEngine;
using System.Collections;

public class PlayerController2D : MonoBehaviour {

	Animator anim;
	Inventory inv;

	public float playerWalkSpeed;
	public float playerSprintSpeed;


	private float playerSpeed;
	private int direction;
	private float animSpeed;

	public bool primaryAnimComplete;


	
	public int Direction{

		get{
			return direction;
		}

	}

	public float AnimSpeed{

		get{
			return animSpeed;
		}



	}




	void Start () {
		
		playerSpeed = playerWalkSpeed;
		anim = GetComponent<Animator> ();
		anim.SetFloat ("speed", 0.0f);
	
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
			anim.SetFloat("speed", 0.0f);
			direction = 0;
			animSpeed = 0.0f;

		}

		else {
			Debug.Log("Controller enabled");
			thisPlayerController2D.enabled = true;
			
		}
		
		
	}

	//recieves player's input and moves this gameobject
	void PlayerController(){//0-idle 1-left 2-right 3-up 4-daaawown

		//anim.SetInteger("direction", direction);
		//anim.SetFloat("speed", animSpeed);
		


		//Player movement controled with WASD
		if (Input.GetKey(KeyCode.W)){
			rigidbody2D.transform.position += Vector3.up * playerSpeed * Time.fixedDeltaTime;
			anim.SetInteger("direction", 3);
			anim.SetFloat("speed", 1.0f);
			direction = 3;
			animSpeed = 1.0f;
		}
		if (Input.GetKey(KeyCode.S)){
			rigidbody2D.transform.position += Vector3.down * playerSpeed * Time.deltaTime;
			anim.SetInteger("direction", 4);
			anim.SetFloat("speed", 1.0f);
			direction = 4;
			animSpeed = 1.0f;
		}
		if (Input.GetKey(KeyCode.A)){
			rigidbody2D.transform.position += Vector3.left * playerSpeed * Time.deltaTime;
			anim.SetInteger("direction", 1);
			anim.SetFloat("speed", 1.0f);
			direction = 1;
			animSpeed = 1.0f;
		}
		if (Input.GetKey(KeyCode.D)){
			rigidbody2D.transform.position += Vector3.right * playerSpeed * Time.deltaTime;
			anim.SetInteger("direction", 2);
			anim.SetFloat("speed", 1.0f);
			direction = 2;
			animSpeed = 1.0f;
		}

		//reset direction back to 0
		if (Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.S) || Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.D)) {
				
			anim.SetInteger("direction", 0);
			anim.SetFloat("speed", 0.0f);
			direction = 0;
			animSpeed = 0.0f;
		
		}

		//Sprint Toggle w/ LeftShift
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			playerSpeed = playerSprintSpeed;
		} 
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			playerSpeed = playerWalkSpeed;
		}



		if (Input.GetMouseButtonDown (0)) {
				

			SwingPrimaryWeapon();
		

		
		}

		if (primaryAnimComplete) {

			animSpeed = 0.0f;
			anim.SetFloat("speed", 0.0f);
			primaryAnimComplete = false;
		
		
		
		}

	}


	void SwingPrimaryWeapon(){

		anim.SetFloat ("speed", 2.0f);
		animSpeed = 2.0f;
		Debug.Log ("swing");
	


	}








}

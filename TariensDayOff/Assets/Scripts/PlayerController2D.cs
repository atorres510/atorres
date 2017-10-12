using UnityEngine;
using System.Collections;

public class PlayerController2D : MonoBehaviour {


	public float playerWalkSpeed;
	public float playerSprintSpeed;

	private float playerSpeed;
	//private bool isJumping;
	//private bool isAbleToJump;
	private float smooth;

	public Transform waypoint;
	

	void Start () {

		playerSpeed = playerWalkSpeed;
		//isAbleToJump = false;
		//isJumping = false;
		smooth = 2f;

	
	}
	
	void Update () {

		PlayerController ();

	}


	/*void OnTriggerEnter2D(Collider2D other){

		if(other.tag == "VaultPoint"){
			isAbleToJump = true;
			BoxCollider2D boxcollider2D;

			boxcollider2D = other.GetComponent<BoxCollider2D>();
			boxcollider2D.enabled = false;
		}
	}

	void OnTriggerExit2D(Collider2D other){

		if(other.tag == "VaultPoint"){
			isAbleToJump = false;
			BoxCollider2D boxcollider2D;
			
			boxcollider2D = other.GetComponent<BoxCollider2D>();
			boxcollider2D.enabled = true;
		}

	}*/


	void PlayerController(){

		//Player movement controled with WASD
		if (Input.GetKey(KeyCode.W)){
			GetComponent<Rigidbody2D>().transform.position += Vector3.up * playerSpeed * Time.fixedDeltaTime;
		}
		if (Input.GetKey(KeyCode.S)){
			GetComponent<Rigidbody2D>().transform.position += Vector3.down * playerSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.A)){
			GetComponent<Rigidbody2D>().transform.position += Vector3.left * playerSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D)){
			GetComponent<Rigidbody2D>().transform.position += Vector3.right * playerSpeed * Time.deltaTime;
		}

		//Sprint Toggle w/ LeftShift
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			playerSpeed = playerSprintSpeed;
		} 
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			playerSpeed = playerWalkSpeed;
		}

		//if (Input.GetKeyDown (KeyCode.Space)) {
		//	StartCoroutine(PlayerJump());
		//}
	}




	/*IEnumerator PlayerJump(){

		if (isAbleToJump == false) {
			StopCoroutine("PlayerJump");
			yield return null;
		}

		if (isAbleToJump == true) {
			Debug.Log ("jumping!");

			Vector3 newPosition;
	
			newPosition = waypoint.position;

			while (transform.position != newPosition) {

					transform.position = Vector3.Lerp (transform.position, newPosition, (Time.deltaTime * smooth)); 
					yield return null;

			}
	
			Debug.Log ("done jumping");
		}
	}*/


















}

using UnityEngine;
using System.Collections;

public class PlayerController2D : MonoBehaviour {


	public float playerWalkSpeed;
	public float playerSprintSpeed;
    public bool isSprintEnabled;

    Animator playerAnimator;

	private float playerSpeed;
	//private bool isJumping;
	//private bool isAbleToJump;
	private float smooth;

    private int testDirection;
    private bool testMoving;

	public Transform waypoint;
	

	void Start () {

        playerAnimator = GetComponent<Animator>();
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

		//Player movement controled with WASD //forward = 1, back = 2, left =3, right = 4, moving = true/false;

		if (Input.GetKey(KeyCode.W)){
			GetComponent<Rigidbody2D>().transform.position += Vector3.up * playerSpeed * Time.fixedDeltaTime;//back
            playerAnimator.SetInteger("direction", 2);
            playerAnimator.SetBool("moving", true);
            testDirection = 2;
            testMoving = true;

        }

        if (Input.GetKeyUp(KeyCode.W)) {
            playerAnimator.SetInteger("direction", 2);
            playerAnimator.SetBool("moving", false);
            testDirection = 2;
            testMoving = false;
        }

		if (Input.GetKey(KeyCode.S)){
			GetComponent<Rigidbody2D>().transform.position += Vector3.down * playerSpeed * Time.deltaTime; //forward
            playerAnimator.SetInteger("direction", 1);
            playerAnimator.SetBool("moving", true);
            testDirection = 1;
            testMoving = true;
        }

        if (Input.GetKeyUp(KeyCode.S)){
            playerAnimator.SetInteger("direction", 1);
            playerAnimator.SetBool("moving", false);
            testDirection = 1;
            testMoving = false;
        }

		if (Input.GetKey(KeyCode.A)){
			GetComponent<Rigidbody2D>().transform.position += Vector3.left * playerSpeed * Time.deltaTime;//left
            playerAnimator.SetInteger("direction", 3);
            playerAnimator.SetBool("moving", true);
            testDirection = 3;
            testMoving = true;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().transform.position += Vector3.left * playerSpeed * Time.deltaTime;//left
            playerAnimator.SetInteger("direction", 3);
            playerAnimator.SetBool("moving", false);
            testDirection = 3;
            testMoving = false;
        }

        if (Input.GetKey(KeyCode.D)){
            GetComponent<Rigidbody2D>().transform.position += Vector3.right * playerSpeed * Time.deltaTime;//right
            playerAnimator.SetInteger("direction", 4);
            playerAnimator.SetBool("moving", true);
            testDirection = 4;
            testMoving = true;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().transform.position += Vector3.right * playerSpeed * Time.deltaTime;//right
            playerAnimator.SetInteger("direction", 4);
            playerAnimator.SetBool("moving", false);
            testDirection = 4;
            testMoving = false;
        }

        if (isSprintEnabled) {

            //Sprint Toggle w/ LeftShift
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerSpeed = playerSprintSpeed;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                playerSpeed = playerWalkSpeed;
            }


        }

        Debug.Log("Direction: " + testDirection);
        Debug.Log("Moving is: " + testMoving);
    
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

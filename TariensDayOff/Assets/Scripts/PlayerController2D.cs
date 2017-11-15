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
    private Vector3 oldPosition;

	//public Transform waypoint;
	

	void Start () {

        playerAnimator = GetComponent<Animator>();
        playerSpeed = playerWalkSpeed;
		//isAbleToJump = false;
		//isJumping = false;
		smooth = 2f;

	
	}
	
	void FixedUpdate () {
        oldPosition = gameObject.transform.position;
		PlayerController ();
        MovingOverride(oldPosition, gameObject.transform.position);

	}




	void PlayerController(){
        
		//Player movement controled with WASD //forward = 1, back = 2, left =3, right = 4, moving = true/false;

		if (Input.GetKey(KeyCode.W)){
			GetComponent<Rigidbody2D>().transform.position += Vector3.up * playerSpeed * Time.fixedDeltaTime;//back
            playerAnimator.SetInteger("direction", 2);
            playerAnimator.SetBool("moving", true);
          
        }

        if (Input.GetKeyUp(KeyCode.W)) {
            playerAnimator.SetInteger("direction", 2);
            playerAnimator.SetBool("moving", false);
         
        }

		if (Input.GetKey(KeyCode.S)){
			GetComponent<Rigidbody2D>().transform.position += Vector3.down * playerSpeed * Time.deltaTime; //forward
            playerAnimator.SetInteger("direction", 1);
            playerAnimator.SetBool("moving", true);
         
        }

        if (Input.GetKeyUp(KeyCode.S)){
            playerAnimator.SetInteger("direction", 1);
            playerAnimator.SetBool("moving", false);
          
        }

		if (Input.GetKey(KeyCode.A)){
			GetComponent<Rigidbody2D>().transform.position += Vector3.left * playerSpeed * Time.deltaTime;//left
            playerAnimator.SetInteger("direction", 3);
            playerAnimator.SetBool("moving", true);
         
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().transform.position += Vector3.left * playerSpeed * Time.deltaTime;//left
            playerAnimator.SetInteger("direction", 3);
            playerAnimator.SetBool("moving", false);
        
        }

        if (Input.GetKey(KeyCode.D)){
            GetComponent<Rigidbody2D>().transform.position += Vector3.right * playerSpeed * Time.deltaTime;//right
            playerAnimator.SetInteger("direction", 4);
            playerAnimator.SetBool("moving", true);
          
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().transform.position += Vector3.right * playerSpeed * Time.deltaTime;//right
            playerAnimator.SetInteger("direction", 4);
            playerAnimator.SetBool("moving", false);
         
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
        
    }

    //fixes the moving animations getting "stuck".  checks after the player controller in fixed update to ensure that if the player is not
    //in a different position, they are not moving.  It then ensures the moving parameter in the animator is set to false.
    void MovingOverride(Vector3 pos1, Vector3 pos2) {

        if (pos1 == pos2){
            playerAnimator.SetBool("moving", false);
            
        }

        else { }
        
    }
















}

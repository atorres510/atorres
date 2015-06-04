using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	Animator anim;
	KeyCode moveLeft, moveRight, lookUp, lookDown;
	bool ignoreInputLeft, ignoreInputRight, ignoreInputUp, ignoreInputDown, isAgainstWallLeft, isAgainstWallRight;
	Transform cameraTracker;
	float speed;

	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
		foreach(Transform t in transform)
		{
			if(t.name == "CameraTracker")
			{
				cameraTracker = t;
			}
		}

		cameraTracker = transform.transform;
		moveLeft = KeyCode.A;
		moveRight = KeyCode.D;
		lookUp = KeyCode.W;
		lookDown = KeyCode.S;
		ignoreInputLeft = false;
		ignoreInputRight = false;
		ignoreInputUp = false;
		ignoreInputDown = false;
		speed = 1;
	}
	
	// Update is called once per frame
	void Update()
	{
		MoveLeft();
		MoveRight();
		LookUp();
		LookDown();
	}

	void MoveLeft()
	{
		if(anim.GetBool("isFacingLeft") && !ignoreInputLeft)
		{
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerThomasWalkLeft") || anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerDouglasWalkLeft"))
			{
				transform.Translate(Time.deltaTime * -speed, 0 ,0);
				ignoreInputRight = true;
				ignoreInputUp = true;
				ignoreInputDown = true;
				isAgainstWallRight = false;

			}
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerThomasIdleLeft") || anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerDouglasIdleLeft"))
			{
				ignoreInputRight = false;
				ignoreInputUp = false;
				ignoreInputDown = false;
			}
			if(false)
			{
				isAgainstWallLeft = true;
				anim.SetBool("isAgainstWallLeft", isAgainstWallLeft);
			}

			if(Input.GetKey(moveLeft))
			{
				anim.SetInteger("direction", -1);
			}
			if(Input.GetKeyUp(moveLeft))
			{
				anim.SetInteger("direction", 0);
			}
			if(Input.GetKey(moveRight) && !ignoreInputRight)
			{
				anim.SetBool("isFacingLeft", false);
			}
		}
	}

	void MoveRight()
	{
		if(!anim.GetBool("isFacingLeft") && !ignoreInputRight)
		{
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerThomasWalkRight") || anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerDouglasWalkRight"))
			{
				transform.Translate(Time.deltaTime * speed, 0 ,0);
				ignoreInputLeft = true;
				ignoreInputUp = true;
				ignoreInputDown = true;
				isAgainstWallLeft = false;
				anim.SetBool("isAgainstWallLeft", isAgainstWallLeft);
			}
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerThomasIdleRight") || anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerDouglasIdleRight"))
			{
				ignoreInputLeft = false;
				ignoreInputUp = false;
				ignoreInputDown = false;
			}
			if(Input.GetKey(moveRight))
			{
				anim.SetInteger("direction", 1);
			}
			if(Input.GetKeyUp(moveRight))
			{
				anim.SetInteger("direction", 0);
			}
			if(Input.GetKey(moveLeft) && !ignoreInputLeft)
			{
				anim.SetBool("isFacingLeft", true);
			}
		}
	}

	void LookUp()
	{
		if(Input.GetKey(lookUp) && !ignoreInputUp)
		{
			anim.SetInteger("searching", 1);
		}
		if(Input.GetKeyUp(lookUp))
		{
			anim.SetInteger("searching", 0);
		}
		if(anim.GetInteger("searching") == 1)
		{
			ignoreInputLeft = true;
			ignoreInputRight = true;
			ignoreInputDown = true;
		}
		if(anim.GetInteger("searching") == 0 && anim.GetInteger("direction") == 0)
		{
			ignoreInputLeft = false;
			ignoreInputRight = false;
			//ignoreInputDown = false;
		}
	}

	void LookDown()
	{
		if(Input.GetKey(lookDown) && !ignoreInputDown)
		{
			anim.SetInteger("searching", -1);
		}
		if(Input.GetKeyUp(lookDown))
		{
			anim.SetInteger("searching", 0);
		}
		if(anim.GetInteger("searching") == -1)
		{
			ignoreInputLeft = true;
			ignoreInputRight = true;
			ignoreInputUp = true;
		}
		if(anim.GetInteger("searching") == 0 && anim.GetInteger("direction") == 0)
		{
			ignoreInputLeft = false;
			ignoreInputRight = false;
			ignoreInputUp = false;
		}
	}
}

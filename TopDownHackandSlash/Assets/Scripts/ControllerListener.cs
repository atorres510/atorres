using UnityEngine;
using System.Collections;

public class ControllerListener : MonoBehaviour {

	//listens to the player controller and communicates with the individual animators of the player's upper
	//lower, and weapon parts.
	
	public PlayerController2D playerController;

	public bool isAnimComplete;
	
	Animator anim;
	
	int listenerDirection;
	float listenerAnimSpeed;
	int listenerAttack;
	
	
	// Use this for initialization
	void Start () {
		
		anim = GetComponent<Animator> ();
		listenerDirection = playerController.Direction; 
		listenerAnimSpeed = playerController.AnimSpeed;
		
		
	}
	
	// Update is called once per frame
	void Update () {

		if (isAnimComplete) {
				
			playerController.primaryAnimComplete = isAnimComplete;
			isAnimComplete = false;
		
		}


		listenerDirection = playerController.Direction; 
		listenerAnimSpeed = playerController.AnimSpeed;
		listenerAttack = playerController.Attack;
		
		anim.SetInteger ("direction", listenerDirection);
		anim.SetFloat ("speed", listenerAnimSpeed);
		anim.SetInteger ("attack", listenerAttack);
		
	}
}

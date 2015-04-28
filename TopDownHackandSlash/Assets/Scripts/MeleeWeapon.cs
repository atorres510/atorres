using UnityEngine;
using System.Collections;

public class MeleeWeapon : Weapon {



	Animator anim;



	//int swing;
	public bool isAttacking;
	public bool animCompleted; //used in animation

	public float hitColliderWidth;
	public float hitColliderHeight;
	BoxCollider2D hitCollider;
	//ControllerListener controllerListener;



	
	//creates a collider for weapon/enemy interaction with varied size and direction.  needs Destroy(hitcollider) for cleanup.
	BoxCollider2D InstantiateHitCollider(float width, float height, int currentDirection){
		
		BoxCollider2D hitCol = gameObject.AddComponent("BoxCollider2D") as BoxCollider2D;
		
		hitCol.size = new Vector2(width,height);
		hitCol.isTrigger = true;
		return hitCol;
		
	}



	
	//tells Weapon Animator to start animation
	public override void Attack(){
		
		//anim.SetInteger ("swing", swing);
		
		
		if (Input.GetMouseButtonDown(0)) {
			//swing = 1;

			//Destroy(hitCollider);
			if(!isAttacking){

				if(hitCollider != null){

					Debug.Log("hit collider not null");


				}

				else {

					hitCollider = InstantiateHitCollider(hitColliderWidth, hitColliderHeight, anim.GetInteger("direction"));

				}





			}

		}
	






		if (animCompleted) {
			Destroy(hitCollider);
			//swing = 0;
			animCompleted = false;
		}
		
	}















	void Start(){

		anim = GetComponent<Animator> ();
		//swing = 0;
		
	}

	void Update(){
		
		Attack ();

	}

}

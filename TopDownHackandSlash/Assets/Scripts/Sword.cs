using UnityEngine;
using System.Collections;

public class Sword : Weapon {


	Animator anim;



	//int swing;
	public bool animCompleted; //used in animation

	public float hitColliderWidth;
	public float hitColliderHeight;
	BoxCollider2D hitCollider;
	//ControllerListener controllerListener;



	
	//creates a collider for weapon/enemy interaction with varied size and direction.  needs Destroy(hitcollider) for cleanup.
	public BoxCollider2D InstantiateHitCollider(float width, float height, int currentDirection){
		
		BoxCollider2D hitCollider = gameObject.AddComponent("BoxCollider2D") as BoxCollider2D;
		
		hitCollider.size = new Vector2(width,height);
		hitCollider.isTrigger = true;
		return hitCollider;
		
	}
	
	
	//tells Weapon Animator to start animation
	public override void Attack(){
		
		//anim.SetInteger ("swing", swing);
		
		
		if (Input.GetMouseButtonDown(0)) {
			//swing = 1;
			hitCollider = InstantiateHitCollider(hitColliderWidth, hitColliderHeight, anim.GetInteger("direction"));
			
		}
		
		if (animCompleted) {
			Destroy(hitCollider);
			//swing = 0;
			animCompleted = false;
			
		}
		
	}


	/*void setLastDirection(int direction){

		if(direction > 0){

			anim.SetInteger("lastDirection", direction);
			//Debug.Log (lastDirection);
		}





	}*/








	void Start(){

		anim = GetComponent<Animator> ();
		//swing = 0;


	}

	void Update(){

		//setLastDirection(anim.GetInteger("direction"));
		Attack ();

	}

}

using UnityEngine;
using System.Collections;

public class Sword : Weapon {

	Animator anim;

	public int setAnimInteger;
	public bool animCompleted;


	public override void Attack(){

		anim.SetInteger ("swing", setAnimInteger);


		if (Input.GetMouseButtonDown(0)) {
			setAnimInteger = 1;

		}

		if (animCompleted) {
			setAnimInteger = 0;
			animCompleted = false;
		
		
		}

	}





	void Start(){

		//anim = GetComponent<Animator> ();
		//setAnimInteger = 0;


	}

	void Update(){

		//Attack ();


	}

}

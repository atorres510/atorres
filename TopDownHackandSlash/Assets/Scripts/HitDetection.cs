using UnityEngine;
using System.Collections;

public class HitDetection : MonoBehaviour {

	public string colliderTag;



	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == colliderTag) {
			Debug.Log("Hit");
		
		
		}




	}
	

}

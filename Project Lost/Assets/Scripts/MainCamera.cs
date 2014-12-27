using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	GameObject player;
	float smooth;

	
	//keeps the camera trained on the player
	void followPlayer(){
		
		float x = player.transform.position.x;
		float y = player.transform.position.y;
		//passes the player's position while retaining the camera's z axis
		Vector3 newPosition = new Vector3 (x, y, transform.position.z);

		transform.position = Vector3.Lerp (transform.position, newPosition, smooth * Time.deltaTime);
		//Debug.Log (x);
		
	}

	

	
	void Start () {
		//instantiation 
		player = GameObject.FindGameObjectWithTag ("Sally");
		//Debug.Log (player);
		smooth = 3f;
		
	}
	

	void Update () {
		
		followPlayer ();
		
	}
}

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


	public float speed;


	void cameracontrols(){

		if(Input.GetKey(KeyCode.W)){

			transform.position += Vector3.up * speed * Time.fixedDeltaTime;

		}

		if(Input.GetKey(KeyCode.A)){

			transform.position += Vector3.left * speed * Time.fixedDeltaTime;
		}

		if(Input.GetKey(KeyCode.S)){

			transform.position += Vector3.down * speed * Time.fixedDeltaTime;
		}
	
		if(Input.GetKey(KeyCode.D)){

			transform.position += Vector3.right * speed * Time.fixedDeltaTime;
		}

			

	}






	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		cameracontrols();

	}
}

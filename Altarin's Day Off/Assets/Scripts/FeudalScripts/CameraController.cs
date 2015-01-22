using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float cameraSpeed;
	public Transform origin;
	public float borderHeight;
	public float borderWidth;

	float height;
	float width;
	

	void Controller(){
		
		//camera movement controlled with WASD
		if (Input.GetKey(KeyCode.W) && isWithinBorder(transform, 0)){
			rigidbody2D.transform.position += Vector3.up * cameraSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S) && isWithinBorder(transform, 1)){
			rigidbody2D.transform.position += Vector3.down * cameraSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.A)&& isWithinBorder(transform, 2)){
			rigidbody2D.transform.position += Vector3.left * cameraSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D)&& isWithinBorder(transform, 3)){
			rigidbody2D.transform.position += Vector3.right * cameraSpeed * Time.deltaTime;
		}

		//else {}
		

	}

	//Checks if camera is within the defined borders
	bool isWithinBorder(Transform t, int direction){

		float x = t.position.x;
		float y = t.position.y;

		//Up
		if(y >= height && direction == 0){

			return false;

		}

		//Down
		else if(y <= origin.position.y && direction == 1){
			
			return false;
	
		}
	
		//Left
		else if(x <= origin.position.x && direction == 2){
			
			return false;

		}

		//Right
		else if(x >= width && direction == 3){
			
			return false;
	
		}
		
		else{
			return true;
		}
	
	
	}


	
	void Awake(){

		height = origin.position.y + borderHeight;
		width = origin.position.x + borderWidth;

	}




	void Update () {
		Controller ();
	}
}

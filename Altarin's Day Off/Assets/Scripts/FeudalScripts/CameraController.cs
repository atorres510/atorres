using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float cameraSpeed;
	public Transform origin;
	public float borderHeight;
	public float borderWidth;

	Camera thisCamera;

	float height;
	float width;
	float minFov;
	float maxFov;
	float fov;
	float zoomSensitivity;
	

	void Controller(){
		
		//camera movement controlled with WASD
		if (Input.GetKey(KeyCode.W) && isWithinBorder(transform, 0)){
			rigidbody2D.transform.position += Vector3.up * cameraSpeed * Time.fixedDeltaTime;
		}
		if (Input.GetKey(KeyCode.S) && isWithinBorder(transform, 1)){
			rigidbody2D.transform.position += Vector3.down * cameraSpeed * Time.fixedDeltaTime;
		}
		if (Input.GetKey(KeyCode.A)&& isWithinBorder(transform, 2)){
			rigidbody2D.transform.position += Vector3.left * cameraSpeed * Time.fixedDeltaTime;
		}
		if (Input.GetKey(KeyCode.D)&& isWithinBorder(transform, 3)){
			rigidbody2D.transform.position += Vector3.right * cameraSpeed * Time.fixedDeltaTime;
		}

		fov -= Input.GetAxis ("Mouse ScrollWheel") * zoomSensitivity;
		fov = Mathf.Clamp (fov, minFov, maxFov);
		thisCamera.orthographicSize = fov;
		//Debug.Log (thisCamera.fieldOfView);

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

		thisCamera = gameObject.GetComponent<Camera> ();
		height = origin.position.y + borderHeight;
		width = origin.position.x + borderWidth;
		minFov = 5.0f;
		maxFov = 15.0f;
		zoomSensitivity = 5.0f;
		fov = thisCamera.orthographicSize;
	}
	

	void FixedUpdate () {
		Controller ();
	}
}

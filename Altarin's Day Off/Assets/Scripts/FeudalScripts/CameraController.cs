using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float cameraSpeed;
	public Transform origin;
	public int mapSize;

	Camera thisCamera;

	float mapLength;
	float height;
	float width;
	Vector3 centerBoard;


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

		if (fov == maxFov) {

			rigidbody2D.transform.position = centerBoard;
				
		
		
		}

		fov -= Input.GetAxis ("Mouse ScrollWheel") * zoomSensitivity;
		fov = Mathf.Clamp (fov, minFov, maxFov);
		thisCamera.orthographicSize = fov;
		//Debug.Log (thisCamera.fieldOfView);

		//else {}
		

	}

	//corrects the camera's transform to keep the FOV within the boarders of the map
	void CorrectCameraPosition(){

	
	
	}

	//Checks if camera is within the defined borders
	bool isWithinBorder(Transform t, int direction){

		float x = t.position.x;
		float y = t.position.y;

		float xFOVCorrection = ((1.254f * fov) - 0.045f);
		float yFOVCorrection = (fov - 0.5f);

		//Up
		if((y + yFOVCorrection) >= height && direction == 0){

			return false;

		}

		//Down
		else if((y - yFOVCorrection) <= origin.position.y && direction == 1){
			
			return false;
	
		}
	
		//Left
		else if((x - xFOVCorrection) <= origin.position.x && direction == 2){
			
			return false;

		}

		//Right
		else if((x + xFOVCorrection) >= width && direction == 3){
			
			return false;
	
		}
		
		else{
			return true;
		}
	
	
	}


	
	void Start(){

		mapLength = (mapSize * 1.06f); 
		thisCamera = gameObject.GetComponent<Camera> ();
		height = origin.position.y + mapLength;
		width = origin.position.x + mapLength;
		centerBoard = new Vector3 (((mapLength / 2) - 0.53f), ((mapLength / 2) - 0.53f), transform.position.z);
		transform.position = centerBoard;

		minFov = 5.0f;
		maxFov = 13.0f;
		zoomSensitivity = 5.0f;
		fov = thisCamera.orthographicSize;
	}
	

	void FixedUpdate () {
		Controller ();
	}
}

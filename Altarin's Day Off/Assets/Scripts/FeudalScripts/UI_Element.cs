using UnityEngine;
using System.Collections;

public class UI_Element : MonoBehaviour {

	public float xRatio;
	public float yRatio;

	GameObject mainCameraObject;
	Camera mainCamera;

	Vector3 screenPosition;
	Vector3 worldPointPosition;
	public Vector3 dPosition;

	Vector3 oldCameraPosition;
	float cameraFov;
	float oldcameraFov;




	public void SetUpElement(){

		mainCameraObject = GameObject.FindGameObjectWithTag ("MainCamera");
		mainCamera = mainCameraObject.GetComponent<Camera> ();
		cameraFov = mainCamera.orthographicSize;
		oldcameraFov = cameraFov;
		oldCameraPosition = mainCamera.transform.position;
	
		Resize ();
	


	}

	public void Resize(){

		float aspectRatio = cameraFov / oldcameraFov;

		gameObject.transform.localScale = gameObject.transform.localScale * aspectRatio;

		screenPosition.x = Screen.width * xRatio;
		screenPosition.y = Screen.height * yRatio;
		screenPosition.z = 1;
		worldPointPosition = mainCamera.ScreenToWorldPoint(screenPosition);
		transform.position = worldPointPosition;
		


	}



	public void UpdateElementPosition(){

		cameraFov = mainCamera.orthographicSize;

		if (cameraFov != oldcameraFov) {

			Resize();
			//SetUpElement();
			oldcameraFov = cameraFov;
			//Debug.Log("HI");

		}


		else{
			
			dPosition = mainCamera.transform.position - oldCameraPosition;
			
			transform.position += dPosition;
	
			oldCameraPosition = mainCamera.transform.position;

		}


	}


	void Start () {
		//Debug.Log ("UIELEMENT START");
		/*mainCameraObject = GameObject.FindGameObjectWithTag ("MainCamera");
		mainCamera = mainCameraObject.GetComponent<Camera> ();
		cameraFov = mainCamera.orthographicSize;
		oldcameraFov = cameraFov;
		oldCameraPosition = mainCamera.transform.position;*/
		SetUpElement();
	
	}
	

	void FixedUpdate () {


		UpdateElementPosition();
	
	}
}

using UnityEngine;
using System.Collections;

public class UI_Element : MonoBehaviour {

	public float xRatio;
	public float yRatio;

	GameObject mainCameraObject;
	Camera mainCamera;

	Vector3 screenPosition;
	Vector3 worldPointPosition;

	Vector3 oldCameraPosition;
	float cameraFov;
	float oldcameraFov;




	public void SetUpElement(){
	
		screenPosition.x = Screen.width * xRatio;
		screenPosition.y = Screen.height * yRatio;
		screenPosition.z = 1;
		worldPointPosition = mainCamera.ScreenToWorldPoint(screenPosition);
		transform.position = worldPointPosition;
	


	}

	public void Resize(){

		float aspectRatio = cameraFov / oldcameraFov;
		
		gameObject.transform.localScale = gameObject.transform.localScale * aspectRatio;
		

	}



	public void UpdateElementPosition(){

		cameraFov = mainCamera.orthographicSize;

		if (cameraFov != oldcameraFov) {

			Resize();
			SetUpElement();
			oldcameraFov = cameraFov;
			//Debug.Log("HI");

		}


		else{
			
			Vector3 dPosition = mainCamera.transform.position - oldCameraPosition;
			
			transform.position += dPosition;
	
			oldCameraPosition = mainCamera.transform.position;

		}


	}


	void Start () {

		mainCameraObject = GameObject.FindGameObjectWithTag ("MainCamera");
		mainCamera = mainCameraObject.GetComponent<Camera> ();
		cameraFov = mainCamera.orthographicSize;
		oldcameraFov = cameraFov;
		oldCameraPosition = mainCamera.transform.position;
		SetUpElement();
	
	}
	

	void Update () {


		UpdateElementPosition();
	
	}
}

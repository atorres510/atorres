using UnityEngine;
using System.Collections;

public class UI_Element : MonoBehaviour {

	public float xRatio;
	public float yRatio;


	Vector3 screenPosition;
	Vector3 worldPointPosition;

	Vector3 oldCameraPosition;
	float cameraFov;
	float oldcameraFov;



	public void SetUpElement(){
	
		screenPosition.x = Screen.width * xRatio;
		screenPosition.y = Screen.height * yRatio;
		screenPosition.z = 1;
		worldPointPosition = Camera.main.ScreenToWorldPoint(screenPosition);
		transform.position = worldPointPosition;
	


	}

	public void Resize(){

		float aspectRatio = cameraFov / oldcameraFov;
		
		gameObject.transform.localScale = gameObject.transform.localScale * aspectRatio;
		

	}





	public void UpdateElementPosition(){

		cameraFov = Camera.main.orthographicSize;

		if (cameraFov != oldcameraFov) {

			SetUpElement();
			oldcameraFov = cameraFov;
			Debug.Log("HI");

		}


		else{
		
			
			Vector3 dPosition = Camera.main.transform.position - oldCameraPosition;
			
			transform.position += dPosition;
			

			
			oldCameraPosition = Camera.main.transform.position;

		}


	}




	void Start () {

		cameraFov = Camera.main.orthographicSize;
		oldcameraFov = cameraFov;
		oldCameraPosition = Camera.main.transform.position;
		SetUpElement();
	
	}
	

	void Update () {


		UpdateElementPosition();
	
	}
}

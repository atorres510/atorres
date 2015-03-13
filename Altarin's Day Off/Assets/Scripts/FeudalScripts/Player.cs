using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int playerNumber = 0;
	public bool isWhite;
	public bool isReady;
	Camera playerCamera;
	//CameraController playerCameraController;

	void SetUpPlayer(){

		playerCamera = gameObject.GetComponent<Camera> ();
		//playerCamera.enabled = false;
		//playerCameraController = gameObject.GetComponent<CameraController> ();
		//GameObject origin = GameObject.FindGameObjectWithTag ("Origin");
		//playerCameraController.origin = origin.transform;

	}




	void Awake(){

		isWhite = true;
		isReady = false;
		SetUpPlayer ();

	}



}

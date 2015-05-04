using UnityEngine;
using System.Collections;

public class Player : Photon.MonoBehaviour {

	public int playerNumber = 0;
	public bool isMyPlayer;
	public bool isWhite;
	public bool isReady;

	bool correctedIsWhite;
	bool correctedIsReady;
	int correctedPlayerNumber;


	Camera playerCamera;
	CameraController playerCameraController;

	void SetUpPlayer(){

		playerCamera = gameObject.GetComponent<Camera> ();
		playerCameraController = gameObject.GetComponent<CameraController> ();
		GameObject borderOriginObject = GameObject.FindGameObjectWithTag ("Origin");
		Transform origin = borderOriginObject.transform;
		playerCameraController.origin = origin;
		isMyPlayer = photonView.isMine;
		//playerCamera.enabled = false;
		//playerCameraController = gameObject.GetComponent<CameraController> ();
		//GameObject origin = GameObject.FindGameObjectWithTag ("Origin");
		//playerCameraController.origin = origin.transform;

	}




	void Awake(){

		//isWhite = true;
		isReady = false;
		SetUpPlayer ();

	}

	void Update(){

		if (!photonView.isMine) {
				
			isWhite = correctedIsWhite;
			isReady = correctedIsReady;
			playerNumber = correctedPlayerNumber;

		
		}
	
	
	}
	

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){

		if (stream.isWriting) {
				
			stream.SendNext(isWhite);
			stream.SendNext(isReady);
			stream.SendNext(playerNumber);
		
		}


		else{

			this.correctedIsWhite = (bool) stream.ReceiveNext();
			this.correctedIsReady = (bool) stream.ReceiveNext();
			this.correctedPlayerNumber = (int) stream.ReceiveNext();

		}




	}




}

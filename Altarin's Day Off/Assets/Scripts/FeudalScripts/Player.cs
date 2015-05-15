using UnityEngine;
using System.Collections;

public class Player : Photon.MonoBehaviour {

	public int playerNumber = 0;
	public bool isMyPlayer;
	public bool isWhite;
	public bool isReady;
	public bool isSecondaryReady; //for testing purposes.  should be removed.

	bool correctedIsWhite;
	bool correctedIsReady;
	int correctedPlayerNumber;
	bool correctedIsSecondaryReady;


	Camera playerCamera;
	CameraController playerCameraController;

	void SetUpPlayer(){

		isReady = false;
		isSecondaryReady = false;
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
	
		SetUpPlayer ();

	}

	void Update(){

		if (!photonView.isMine) {
				
			isWhite = correctedIsWhite;
			isReady = correctedIsReady;
			playerNumber = correctedPlayerNumber;
			isSecondaryReady = correctedIsSecondaryReady;

		
		}
	
	
	}
	

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){

		if (stream.isWriting) {
				
			stream.SendNext(isWhite);
			stream.SendNext(isReady);
			stream.SendNext(playerNumber);
			stream.SendNext(isSecondaryReady);
		
		}


		else{

			this.correctedIsWhite = (bool) stream.ReceiveNext();
			this.correctedIsReady = (bool) stream.ReceiveNext();
			this.correctedPlayerNumber = (int) stream.ReceiveNext();
			this.correctedIsSecondaryReady = (bool) stream.ReceiveNext();

		}




	}




}

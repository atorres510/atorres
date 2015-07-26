using UnityEngine;
using System.Collections;

public class Player : Photon.MonoBehaviour {

	public int playerNumber = 0;
	public bool isMyPlayer;
	public bool isWhite;
	public bool isReady;
	public bool isSecondaryReady; //for testing purposes.  should be removed.
	public AudioClip music1;
	public AudioClip music2;

	bool correctedIsWhite;
	bool correctedIsReady;
	int correctedPlayerNumber;
	bool correctedIsSecondaryReady;


	Camera playerCamera;
	CameraController playerCameraController;
	AudioListener playerAudioListener;
	AudioSource audioSource;

	void SetUpPlayer(){

		isReady = false;
		isSecondaryReady = false;
		playerCamera = gameObject.GetComponent<Camera> ();
		playerCameraController = gameObject.GetComponent<CameraController> ();
		playerAudioListener = gameObject.GetComponent<AudioListener> ();
		audioSource = FindObjectOfType(typeof(AudioSource)) as AudioSource;
		GameObject borderOriginObject = GameObject.FindGameObjectWithTag ("Origin");
		Transform origin = borderOriginObject.transform;
		playerCameraController.origin = origin;
		isMyPlayer = photonView.isMine;
		//playerCamera.enabled = false;
		//playerCameraController = gameObject.GetComponent<CameraController> ();
		//GameObject origin = GameObject.FindGameObjectWithTag ("Origin");
		//playerCameraController.origin = origin.transform;
		if (!photonView.isMine) {
				
			playerCamera.enabled = false;
			playerCameraController.enabled = false;
			playerAudioListener.enabled = false;

		
		
		
		}
	

	}

	public void SetUpAudio(){

		//AudioClip music;



		if (isWhite) {
				
			//music = Resources.Load("Audio/ShockDrop&Roll") as AudioClip;
			audioSource.clip = music1;
		
		}


		else{


			//music = Resources.Load("Audio/TheBattalionAdvances") as AudioClip;
			audioSource.clip = music2;
		}


		audioSource.enabled = true;
		audioSource.enabled = false;
		audioSource.enabled = true;


	}


	void Awake(){

		//isWhite = true;
	
		SetUpPlayer ();
		//SetUpAudio ();

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

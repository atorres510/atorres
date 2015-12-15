using UnityEngine;
using System.Collections;

public class Player : Photon.MonoBehaviour {

	public int playerNumber = 0;
	public bool isMyPlayer;
	public bool isWhite;
	public f_Piece.Faction faction;
	public int factionID = 0; //ID number to be used in photon seralize view to relay faction more easily.

	public bool isReady;
	public bool isSecondaryReady; //for testing purposes.  should be removed.
	public AudioClip music1;
	public AudioClip music2;

	public f_Piece[] pieceSet; //do we want this?  consider PUN principles

	bool correctedIsWhite;
	bool correctedIsReady;
	int correctedPlayerNumber;
	bool correctedIsSecondaryReady;
	int correctedFactionID;

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

	//assigns faction's music and enables audiosource.
	public void SetUpAudio(){

		AudioClip music;
		
		if (isWhite) {
				
			music = Resources.Load<AudioClip>("Audio/ShockDrop&Roll");

		}


		else{

			music = Resources.Load<AudioClip>("Audio/TheBattalionAdvances");

		}

		audioSource.clip = music;

		audioSource.enabled = true;
		audioSource.enabled = false;
		audioSource.enabled = true;


	}

	//reads through the faction enums, then uses the order of the enum to assign an ID to be
	//read later by ReturnFaction.  
	public void AssignFactionID(){

		f_Piece.Faction[] factions;

		//gathers enum values and place them into the array
		factions = (f_Piece.Faction[])System.Enum.GetValues(typeof(f_Piece.Faction));

		for(int i = 0; i <factions.Length; i++){

			//once your faction is found, assign the ID number.
			if(factions[i] == faction){

				factionID = i;

			}

		}

		for(int i = 0; i <factions.Length; i++){
			
			Debug.Log(factions[i]);

		}
		
	}

	//reads the factionID and returns the correct faction from the array of enums.  
	//required to sync your/your opponents factions across clients.  
	f_Piece.Faction ReturnFaction(int id){

		f_Piece.Faction[] factions;

		//gathers enum values and place them into the array
		factions = (f_Piece.Faction[])System.Enum.GetValues(typeof(f_Piece.Faction));

		return factions[id];

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
			factionID = correctedFactionID;
			faction = ReturnFaction(factionID);
	
		}
	
	
	}
	

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){

		if (stream.isWriting) {
				
			stream.SendNext(isWhite);
			stream.SendNext(isReady);
			stream.SendNext(playerNumber);
			stream.SendNext(isSecondaryReady);
			stream.SendNext(factionID);
		
		}


		else{

			this.correctedIsWhite = (bool) stream.ReceiveNext();
			this.correctedIsReady = (bool) stream.ReceiveNext();
			this.correctedPlayerNumber = (int) stream.ReceiveNext();
			this.correctedIsSecondaryReady = (bool) stream.ReceiveNext();
			this.correctedFactionID = (int) stream.ReceiveNext();

		}




	}




}

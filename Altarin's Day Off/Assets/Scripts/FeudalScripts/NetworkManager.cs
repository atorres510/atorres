using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	bool beginSetup = false;

	void Start(){

		Connect ();
	
	}

	void Update(){

		BeginSetup ();
	
	}


	void BeginSetup(){


		if (PhotonNetwork.otherPlayers.Length == 1 && !beginSetup) {
			Debug.Log("setup begins");
		
			beginSetup = true;
			f_SetUpManager setUpManager = FindObjectOfType<f_SetUpManager> ();
			setUpManager.InitiateSetup ();
		
		}
	
	
	}



	void Connect(){

		PhotonNetwork.ConnectUsingSettings ("ADOv1.0");

	}

	void OnGUI(){


		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());

	}
	
	void OnJoinedLobby(){
		Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom ();

	}

	void OnPhotonRandomJoinFailed(){

		Debug.Log ("OnPhotonRandomJoinFailed()");
		PhotonNetwork.CreateRoom (null);
	
	}




	void OnJoinedRoom(){
		Debug.Log ("OnJoinedRoom");
		Vector3 myVector = new Vector3 (8.71f, 6.59f, -11.0f);
		GameObject myPlayer = PhotonNetwork.Instantiate ("Player", myVector, Quaternion.identity, 0);
		Player p = myPlayer.GetComponent<Player> ();
		PhotonPlayer[] players = PhotonNetwork.otherPlayers;

		if (players.Length > 0) {
				
			Debug.Log("Number of players connected: " + players.Length);
			//p.isMyPlayer = true;
			p.isWhite = false;
			p.playerNumber = PhotonNetwork.playerList.Length;

		}

		else {
			Debug.Log("No other players connected.");
			p.isWhite = true;
			p.playerNumber = PhotonNetwork.playerList.Length + 1;


		}

	}

}

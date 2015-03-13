using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {


	void Start(){

		Connect ();
	
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
		//GameObject myPlayer = PhotonNetwork.Instantiate ("Player", Vector3.zero, Quaternion.identity, 0);
		Player p = FindObjectOfType<Player> ();
		PhotonPlayer[] players = PhotonNetwork.otherPlayers;

		if (players.Length > 0) {
				
			Debug.Log("Number of players connected: " + players.Length);
			p.isWhite = false;
			p.playerNumber = PhotonNetwork.playerList.Length;

		}

		else Debug.Log("No other players connected.");

		f_SetUpManager setUpManager = FindObjectOfType<f_SetUpManager> ();
		setUpManager.InitiateSetup ();

	}

}

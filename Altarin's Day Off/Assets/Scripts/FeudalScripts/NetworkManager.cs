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


	}
	


}

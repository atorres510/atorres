using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public bool isOffline;


	
	void Start () {
		Connect ();
	}

	//connects client to photon cloud. 
	void Connect(){
		Debug.Log ("Connect");

		PhotonNetwork.offlineMode = isOffline;
		PhotonNetwork.ConnectUsingSettings ("1.0.0v"); //requires version string.


	}
	

	void OnGUI(){

		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());

	}

	//called when client connects to photon lobby
	void OnJoinedLobby(){
		Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom ();



	}



	void OnPhotonRandomJoinFailed(){
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom (null);

	}

	void OnJoinedRoom(){
		Debug.Log ("OnJoinedRoom");
		SpawnMyPlayer ();
	}
	

	void SpawnMyPlayer(){


		GameObject player = PhotonNetwork.Instantiate("player2", Vector3.zero, Quaternion.identity, 0);

		//player.GetComponent<PlayerController2D> ().enabled = true;
	}
	


}

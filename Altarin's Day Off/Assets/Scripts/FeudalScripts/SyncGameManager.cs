using UnityEngine;
using System.Collections;

public class SyncGameManager : Photon.MonoBehaviour {

	f_GameManager f_gameManager;

	void Start () {

		f_gameManager = gameObject.GetComponent<f_GameManager> ();
	
	}


	[RPC]
	void SyncGrid(int[,] grid){

		f_gameManager.coordinates = grid;

		if (photonView.isMine) {
				
			photonView.RPC ("SyncGrid", PhotonTargets.All, grid);
		
		}
	
	}
	

	void Update () {

		if (photonView.isMine) {
				
		
		
		}


		else{}


	}


}

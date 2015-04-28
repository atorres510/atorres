using UnityEngine;
using System.Collections;

public class NetworkPiece : Photon.MonoBehaviour{


	Vector3 correctPiecePos;
	//f_Piece piece; 




	// Use this for initialization
	void Start () {



		//piece = this.GetComponent<f_Piece> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (!photonView.isMine) {
				
			//transform.position = correctPiecePos;
		
		}





	
	}



	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){

		if (stream.isWriting){
				
			//stream.SendNext(transform.position);
		
		}


		else{

			//this.correctPiecePos = (Vector3) stream.ReceiveNext();
			
		}


	}


}

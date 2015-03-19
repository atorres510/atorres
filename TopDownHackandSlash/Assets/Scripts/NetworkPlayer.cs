using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour {


	//essentially communicates your player gameobject to other clients about its position, animator, 
	//and other information that needs consistent updates.


	private PlayerController2D playerController;
	private Animator anim;

	private float lastUpdateTime = 0.1f;

	private Vector3 correctPlayerPos;
	private Quaternion correctPlayerRot;
	private int correctDirection; //determines direction for animator recieved from other clients


	void Start(){

		anim = GetComponent<Animator> ();

		if (photonView.isMine) {

			playerController = GetComponent<PlayerController2D> ();
		
		}

	
	}


	void Update(){

		if (!photonView.isMine) {
			//smooths movement
			transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, lastUpdateTime);
			transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, lastUpdateTime);
			//updates animators on other clients about your current animation state
			anim.SetInteger("direction", correctDirection);

			/*if(transform.position != this.correctPlayerPos){

				Debug.Log("Transform.position: " + transform.position);
				Debug.Log("CorrectPlayerPos: " + correctPlayerPos);
				transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, lastUpdateTime);
				
				
				anim.SetInteger("direction", correctDirection);

			}*/

		}
	
	}

	//sends information between clients through the photon stream
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){

		if (stream.isWriting) {
				
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(playerController.Direction);

		}


		else{

			this.correctPlayerPos = (Vector3) stream.ReceiveNext();
			this.correctPlayerRot = (Quaternion) stream.ReceiveNext();
			this.correctDirection = (int) stream.ReceiveNext();

		}

	}


}

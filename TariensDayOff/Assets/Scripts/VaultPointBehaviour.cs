using UnityEngine;
using System.Collections;

public class VaultPointBehaviour : MonoBehaviour {

	public BoxCollider2D vaultpointBoxCollider2D;
	public Transform waypointA;
	public Transform waypointB;
	public float vaultSpeed;

	private GameObject player;
	//private BoxCollider2D boxCollider2D;

	private bool isJumping;


	void Awake(){

		player = GameObject.FindGameObjectWithTag ("Player");
		//boxCollider2D = GetComponent<BoxCollider2D> ();
		isJumping = false;

	}


	void OnTriggerStay2D(Collider2D other){

		if (other.gameObject == player && !isJumping) {
			if(Input.GetKeyDown(KeyCode.Space)){
				//Debug.Log(FindFurthestWaypoint(player.transform, waypointA, waypointB));
				StartCoroutine(MovePlayer(vaultSpeed));



			}
		}





	}

	Transform FindFurthestWaypoint(Transform player, Transform a, Transform b){

		float difA = FindDistance (a, player);
		float difB = FindDistance (b, player);
	

		if (difA > difB) {
			return a;	
		
		}

		if (difA < difB) {
			return b;	
		}

		if (difA == difB) {
			return null;
		
		} 

		else {
			return null;
		}
	}

	float FindDistance(Transform t, Transform u){
		
		float x1 = t.position.x;
		float y1 = t.position.y;

		float x2 = u.position.x;
		float y2 = u.position.y;

		//find difference between axes
		float x = x2 - x1;
		float y = y2 - y1;

		//pythagorean theorem
		float z = Mathf.Sqrt ((x * x) + (y * y));
	
		//Debug.Log (z);
		return z;

	}

	IEnumerator MovePlayer(float speed){

		if (!isJumping) {

			isJumping = true;
			PlayerController2D playerController2D = player.GetComponent<PlayerController2D>();
			playerController2D.enabled = false;
			vaultpointBoxCollider2D.enabled = false;
            

			Transform targetWaypoint = FindFurthestWaypoint (player.transform, waypointA, waypointB);

			Vector3 newPosition = targetWaypoint.position;

			while (FindDistance(targetWaypoint, player.transform) > 0.15f) {

				player.transform.position = Vector3.Lerp (player.transform.position, newPosition, (Time.deltaTime * speed)); 
				yield return null;
				
			}

			if(FindDistance(targetWaypoint, player.transform) <= 0.15f){

				//Debug.Log("done!");
				playerController2D.enabled = true;
				vaultpointBoxCollider2D.enabled = true;
				isJumping = false;

				yield return null;

			}

		}



		if (isJumping) {
			StopCoroutine("PlayerJump");
			yield return null;
		}


	}






	
}

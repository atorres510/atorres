using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySight : MonoBehaviour {


	//enemySight relays information to the enemy patrol about the player when entering the field of view

	public float fieldOfView = 110f;
	public float suspiciousPatrolSpeed;
	public float suspiciousRotationSpeed;
	public float secondsSuspicious;
	public float secondsUntilDetection;
	public GameObject questionmarkPrefab;
	public GameObject exclamationmarkPrefab;

	private Collider2D col;
	private GameObject player;
	private Transform playerLastTransform;
	private Vector3 playerLastPosition;
	//private EnemyBehaviour enemyBehaviour;
	private EnemyPatrol enemyPatrol;

	private bool playerInSight;
	private bool suspicious;
	private bool questionMarkInstantiated;
	private bool exclamationMarkInstantiated;

	private GameObject gameManagerObject;
	private GameManager gameManager;


	void Awake(){

		gameManagerObject = GameObject.FindGameObjectWithTag ("GameManager");
		gameManager = gameManagerObject.GetComponent<GameManager> ();

		col = GetComponent<PolygonCollider2D>();
		//enemyBehaviour = GetComponent<EnemyBehaviour> ();
		enemyPatrol = GetComponent<EnemyPatrol> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		//Debug.Log (player);
		playerLastPosition = new Vector3(0,0,0);
	

	}
	

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject == player) {

			Vector2 direction = other.transform.position - transform.position;
			//float angle = Vector2.Angle(direction, transform.forward);

			//Vector2 simpleDirection = new Vector2(-2,0);
		//	Debug.Log ("Colliding with Player");
			//Debug.DrawRay (transform.position, direction,Color.green);
			//enemyBehaviour.SendMessage ("isPatrolling", false, SendMessageOptions.DontRequireReceiver);

			//if(angle < fieldOfView * 0.5f){

			//	Debug.Log ("Angle of view Achieved");

			RaycastHit2D hit = Physics2D.Raycast(transform.position, direction); 
				//if(Physics2D.Raycast(transform.position, direction.normalized, fieldOfView, out hit)){

				if(hit.collider.gameObject == player){

					if(hit.fraction > 3f){
						
						if(!suspicious){

							playerInSight = true;

							if(!questionMarkInstantiated){
								StartCoroutine(renderQuestionMark());
							}
								
							//Debug.Log ("Enemy is Suspicious");
							enemyPatrol.SendMessage("InteruptPatrol", SendMessageOptions.DontRequireReceiver);
							StartCoroutine(TrackLastPosition());
						}

						else if (!playerInSight && suspicious){

							playerInSight = true;
							Debug.Log(playerInSight);
							StopAllCoroutines();

							if(!questionMarkInstantiated){
								StartCoroutine(renderQuestionMark());
							}

							StartCoroutine(TrackLastPosition());
						}
							

					}

					else if(hit.fraction <= 3f){
						StopAllCoroutines();

						if(!exclamationMarkInstantiated){
							StartCoroutine(renderExclamationMark());
							gameManager.GameOver();
							
						}
					}


				}
		}
	}


	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject == player) {
			playerInSight = false;
			//Debug.Log (playerInSight);
		}
	}
	

	//Looks at a specified target.  Improved from EnemyPatrol LookAt()
	IEnumerator LookAt(Transform target){

		//Determines magnitude of angle between target and self
		Vector3 targetDir = target.position - transform.position;
		Vector3 forward = -transform.right;
		float angle = Vector3.Angle (targetDir, forward);

		//determines left or right handedness of angle
		Vector3 relative = transform.InverseTransformPoint (target.position);
		float degAngle = Mathf.Atan2 (relative.y, relative.x) * Mathf.Rad2Deg;


		//Debug.Log (angle);
		//Debug.Log (degAngle);

		//if the target is to the right
		if (degAngle > 0) {
			while (angle > 5f) {
				transform.Rotate(-Vector3.forward, suspiciousRotationSpeed*Time.deltaTime);
				forward = -transform.right;
				//relative = transform.InverseTransformPoint (target.position);
				angle = Vector3.Angle (targetDir, forward);
				//angle = Mathf.Atan2 (relative.y, relative.x) * Mathf.Rad2Deg;
				//Debug.Log ("I'm looking right!");
				//Debug.Log (angle);
				yield return null;
			}
		}

		//if the target is to the left
		else if (degAngle < 0) {
			while (angle > 5f) {
				transform.Rotate(Vector3.forward, suspiciousRotationSpeed*Time.deltaTime);
				forward = -transform.right;
				//relative = transform.InverseTransformPoint (target.position);
				angle = Vector3.Angle (targetDir, forward);
				//angle = Mathf.Atan2 (relative.y, relative.x) * Mathf.Rad2Deg;
				//Debug.Log("I'm looking left!");
				//Debug.Log (angle);
				yield return null;
			}
		}

	}



	IEnumerator MoveTo(Vector3 target){

		//Debug.Log ("Following target");
		
		while ((transform.position - target).sqrMagnitude > 0.3f) {
			transform.position = Vector3.MoveTowards(transform.position, target, (suspiciousPatrolSpeed * Time.deltaTime));
			yield return null;
		}
	}
				
	IEnumerator TrackLastPosition(){

				suspicious = true;
				
				StopCoroutine ("LookAt");
				StopCoroutine ("MoveTo");


				playerLastPosition = player.transform.position;
				playerLastTransform = player.transform;

				Debug.Log (playerLastPosition);
				
				yield return StartCoroutine (LookAt (playerLastTransform));
				yield return new WaitForSeconds (secondsSuspicious);
				yield return StartCoroutine (MoveTo (playerLastPosition));
				yield return new WaitForSeconds (secondsSuspicious);

				//Debug.Log ("I am not suspicious anymore");

				suspicious = false;

				enemyPatrol.SendMessage ("ContinuePatrol", suspicious, SendMessageOptions.DontRequireReceiver);
				/*Vector3 turntoPlayerLastPosition = new Vector3 (playerLastPosition.x, playerLastPosition.y);
				Vector3 movetoPlayerLastPosition = new Vector3 (-playerLastPosition.x, playerLastPosition.y, transform.position.z);
				transform.Rotate (turntoPlayerLastPosition, Time.deltaTime);
				rigidbody2D.transform.position += movetoPlayerLastPosition * 2 * Time.deltaTime;
				yield return null; */

	}


	 //Displays question mark above enemy	
	IEnumerator renderQuestionMark(){

		//Debug.Log ("instantiating");
		questionMarkInstantiated = true;
		GameObject questionmarkInstance;
		Vector3 questionMarkPosition = new Vector3 (transform.position.x, (transform.position.y + 1.5f), -1f);
		questionmarkInstance = Instantiate (questionmarkPrefab, questionMarkPosition, questionmarkPrefab.transform.rotation) as GameObject;

		yield return new WaitForSeconds(2);

		//Debug.Log ("done instantiating");
		Destroy (questionmarkInstance);
		questionMarkInstantiated = false;

		yield return null;
	
		
	
	}

	//Displays exclamation mark above enemy
	IEnumerator renderExclamationMark(){
		
		//Debug.Log ("instantiating");
		exclamationMarkInstantiated = true;
		GameObject exclamationMarkInstance;
		Vector3 exclamationMarkPosition = new Vector3 (transform.position.x, (transform.position.y + 1.5f), -1f);
		exclamationMarkInstance = Instantiate (exclamationmarkPrefab, exclamationMarkPosition, exclamationmarkPrefab.transform.rotation) as GameObject;
		
		yield return new WaitForSeconds(2);
		
		//Debug.Log ("done instantiating");
		Destroy (exclamationMarkInstance);
		questionMarkInstantiated = false;
		
		yield return null;
		
		
		
	}
	











}

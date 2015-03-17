using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySight : MonoBehaviour {


	//enemySight relays information to the enemy patrol about the player when entering the field of view

	public float fieldOfView = 110f;

	public GameObject questionmarkPrefab;
	public GameObject exclamationmarkPrefab;
	private GameObject questionmarkInstance;

	private Collider2D col;
	private GameObject player;
	
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
	
	

	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject == player) {

			Vector2 direction = other.transform.position - transform.position;

			//	Debug.Log ("Angle of view Achieved");

			RaycastHit2D hit = Physics2D.Raycast(transform.position, direction); 

				if(hit.collider.gameObject == player){

					if(hit.fraction > 3f){
						
						if(!suspicious){

							playerInSight = true;

							if(!questionMarkInstantiated){
								StartCoroutine(renderQuestionMark());
							}
								
							//Debug.Log ("Enemy is Suspicious");
							enemyPatrol.StopAllCoroutines();
							//enemyPatrol.StopCoroutine("TrackLastPosition");
							suspicious = true;
							StartCoroutine(enemyPatrol.TrackLastPosition(player));
							suspicious = enemyPatrol.ReturnSuspicion();
						}

						else if (!playerInSight && suspicious){

							playerInSight = true;
							//Debug.Log(playerInSight);
							//StopAllCoroutines();
							enemyPatrol.StopAllCoroutines();
							//enemyPatrol.StopCoroutine("TrackLastPosition");
							if(!questionMarkInstantiated){
								StartCoroutine(renderQuestionMark());
							}



							suspicious = true;
							StartCoroutine(enemyPatrol.TrackLastPosition(player));
							suspicious = enemyPatrol.ReturnSuspicion();
						}
							

					}

					else if(hit.fraction <= 3f){
						StopAllCoroutines();
						enemyPatrol.StopAllCoroutines();

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


				



	 //Displays question mark above enemy	
	IEnumerator renderQuestionMark(){

		//Debug.Log ("instantiating");
		questionMarkInstantiated = true;
		//GameObject questionmarkInstance;
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



	void Update(){




	}
	











}

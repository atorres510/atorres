using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySight : MonoBehaviour {


	//enemySight relays information to the enemy patrol about the player when entering the field of view

	public float fieldOfView = 110f;

    public LayerMask layermask; //set in Inspector, determines the layers that will be detectable by raycasts

	public GameObject questionmarkPrefab;
	public GameObject exclamationmarkPrefab;
	private GameObject questionmarkInstance;
    private GameObject emoteClone;
    private bool isEmoteInstantiated;
    private bool isQuestionMarkInstantiated;

    private Collider2D col;
	private GameObject player;
	
	private EnemyPatrol enemyPatrol;

	private bool isPlayerInSight;
	private bool isSuspicious;  //
	

	private GameObject gameManagerObject;
	private GameManager gameManager;

    //Instantiate member variables, along with finding game manager and player.
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

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, layermask);
            //Debug.DrawRay(transform.position, direction, Color.red, 1f, false);

            if (hit.collider.gameObject == player)
            {

                //if player is within the enemy's visual range, make the enemy suspicious, stop their current patrol
                //and begin tracking the player's last position.  !isplayerinsight condition keeps the method from repeating unnecessarily.
                if (hit.fraction > 3f && !isPlayerInSight)
                {

                    isPlayerInSight = true;
                    //Debug.Log(playerInSight);
                    //StopAllCoroutines();
                    enemyPatrol.StopAllCoroutines();
                    //enemyPatrol.StopCoroutine("TrackLastPosition");
                    if (!isQuestionMarkInstantiated)
                    {
                        StartCoroutine(InstantiateQuestionMark());
                    }



                    isSuspicious = true;
                    StartCoroutine(enemyPatrol.TrackTargetLastPosition(player));
                    isSuspicious = enemyPatrol.ReturnSuspicion();

                }
                //if player gets too close to the enemy, end the game.  
                else if (hit.fraction <= 3f)
                {

                    FoundPlayer();

                }


            }


        }


        
    }


	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject == player) {
			isPlayerInSight = false;
			//Debug.Log (playerInSight);
		}
	}



    #region Emote Methods
    public void InstantiateEmoteClone(GameObject emotePrefab) {

        if (emoteClone != null){
            Destroy(emoteClone);
        }

        Vector3 emotePosition = new Vector3(transform.position.x, (transform.position.y + 1.5f), -1f);
        emoteClone = Instantiate(emotePrefab, emotePosition, emotePrefab.transform.rotation) as GameObject;
    }

    public void DestroyEmoteClone() {

        if (emoteClone != null){
            Destroy(emoteClone);
        }

    }

    //Displays question mark above enemy.  Question mark is destroyed after 2 seconds.	
    IEnumerator InstantiateQuestionMark(){

		//Debug.Log ("instantiating");
		isQuestionMarkInstantiated = true;
		//GameObject questionmarkInstance;
		Vector3 questionMarkPosition = new Vector3 (transform.position.x, (transform.position.y + 1.5f), -1f);
		questionmarkInstance = Instantiate (questionmarkPrefab, questionMarkPosition, questionmarkPrefab.transform.rotation) as GameObject;

		yield return new WaitForSeconds(2);

		//Debug.Log ("done instantiating");
		Destroy (questionmarkInstance);
		isQuestionMarkInstantiated = false;

		yield return null;
	
	}

	//Displays exclamation mark above enemy when they are caught.  The exclamation mark is not 
    //destroyed.  
	void InstantiateExclamationMark(){
        if (isQuestionMarkInstantiated) {
            Destroy(questionmarkInstance);
        }
		//Debug.Log ("instantiating");
		GameObject exclamationMarkInstance;
		Vector3 exclamationMarkPosition = new Vector3 (transform.position.x, (transform.position.y + 1.5f), -1f);
		exclamationMarkInstance = Instantiate (exclamationmarkPrefab, exclamationMarkPosition, exclamationmarkPrefab.transform.rotation) as GameObject;
		
	}

#endregion

    //called when player has been found, stops coroutines and tells the gamemanager to end the game.
    void FoundPlayer() {

        StopAllCoroutines();
        enemyPatrol.StopAllCoroutines();
        InstantiateExclamationMark();
        gameManager.GameOver();
        
    }
    //For when the enemy should be suspicious, then goes to the position of the object causing suspicion. 
    //Called by OnTriggerStay in this script, or by soundbehaviours.  
    public void LookingForTarget(GameObject target) {

        if (isPlayerInSight){
            Debug.Log("Player spotted!");
        }

        else {
            Debug.Log(gameObject.name + " heard sound!");
        }
        

        enemyPatrol.StopAllCoroutines();
        //enemyPatrol.StopCoroutine("TrackLastPosition");
        /*if (!isQuestionMarkInstantiated){
           StartCoroutine(InstantiateQuestionMark());
        }*/

        InstantiateEmoteClone(questionmarkPrefab);

        isSuspicious = true;
        StartCoroutine(enemyPatrol.TrackTargetLastPosition(target.gameObject));
        isSuspicious = enemyPatrol.ReturnSuspicion();

    }


}

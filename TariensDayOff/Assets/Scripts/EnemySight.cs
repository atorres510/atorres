using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySight : MonoBehaviour {
    //enemySight relays information to the enemy patrol about the player when entering the field of view

    //Public Member Variables
	public float fieldOfView = 110f;
    public LayerMask layermask; //set in Inspector, determines the layers that will be detectable by raycasts

    //Enemy Compnonents, set at Awake()
    private EnemyPatrol enemyPatrol;
    private Collider2D col;

    //Player and GameManger Components, set at Awake()
    private GameObject player;
    private GameObject gameManagerObject;
    private GameManager gameManager;

    //Emote Member Variables
    public GameObject questionmarkPrefab;
	public GameObject exclamationmarkPrefab;

	private GameObject questionmarkInstance;
    private GameObject emoteClone;
    private bool isEmoteInstantiated;
    private bool isQuestionMarkInstantiated;
    
    //Player Interaction Member Variables
    private bool isPlayerInSight; //called in OnTriggerStay
	private bool isSuspicious;  //Dunno what this is for.
	


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

    //Uses colliders enemy and player colliders to determine which Player Interaction Methods to call, and when.
    void OnTriggerStay2D(Collider2D other){

        if (other.gameObject == player) {
            
            Vector2 direction = other.transform.position - transform.position;

            //	Debug.Log ("Angle of view Achieved");

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, layermask);
            //Debug.DrawRay(transform.position, direction, Color.red, 1f, false);

            if (hit.collider.gameObject == player) {

                //if player is within the enemy's visual range, make the enemy suspicious, stop their current patrol
                //and begin tracking the player's last position.  !isplayerinsight condition keeps the method from repeating unnecessarily.
                if (hit.fraction > 3f && !isPlayerInSight){

                    isPlayerInSight = true;
                    LookForTarget(player);

                }
                //if player gets too close to the enemy, end the game.  
                else if (hit.fraction <= 3f){

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


    #endregion

    #region Player Interaction Methods
    //called when player has been found, stops coroutines and tells the gamemanager to end the game.
    void FoundPlayer() {

        StopAllCoroutines();
        enemyPatrol.StopAllCoroutines();
        InstantiateEmoteClone(exclamationmarkPrefab);
        gameManager.GameOver();
        
    }
    //For when the enemy should be suspicious, then goes to the position of the object causing suspicion. 
    //Called by OnTriggerStay in this script, or by soundbehaviours.  
    public void LookForTarget(GameObject target) {

        if (isPlayerInSight){
            Debug.Log(gameObject.name + " is suspicious of player!");
        }

        else {
            Debug.Log(gameObject.name + " heard a sound!");
        }
        
        enemyPatrol.StopAllCoroutines();

        InstantiateEmoteClone(questionmarkPrefab);

        isSuspicious = true;
        StartCoroutine(enemyPatrol.TrackTargetLastPosition(target.gameObject));
        isSuspicious = enemyPatrol.ReturnSuspicion();

    }

#endregion

}

using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	GameObject gameObjectManager;
	GameManager gameManager;



	//PolygonCollider2D polygonCollider2D;

	float startPosition;
	float endPosition;
	public float unitsToMove;
	public float patrolSpeed;
	public bool patrolling = false;

	bool moveRight = true;

	public GameObject losWall; // los = Line of Sight.  This is determines what game objects are in the enemy's line of sight.
	public GameObject losPlayer;
	//bool inCover = false;

	void Patrol(){
		//Patrols Enemy to the Right
		if (moveRight) {
			rigidbody2D.transform.position += Vector3.right * patrolSpeed * Time.deltaTime;
			
		}
		//Checks if Right Patrol is finished
		if (rigidbody2D.transform.position.x >= endPosition) {
			moveRight = false;		
		}
		//Patrols Enemy to the Left
		if (!moveRight) {
			rigidbody2D.transform.position -= Vector3.right * patrolSpeed * Time.deltaTime;
		}
		//Checks if Left Patrol is finished
		if (rigidbody2D.transform.position.x <= startPosition) {
			moveRight = true; 		
		}

	}


	/*void CheckCover(){
		Vector3 wall = losWall.transform.position;  
		Vector3 player = losPlayer.transform.position;
		float distanceFromWall = Vector3.Distance (transform.position, wall);
		float distanceFromPlayer = Vector3.Distance (transform.position, player);

		Debug.Log ("Wall" + distanceFromWall);
		Debug.Log ("Player" + distanceFromPlayer);

		if(Vector3.Distance (transform.position, wall) < Vector3.Distance(transform.position, player)){
			inCover = true; 
		}*/

	void isPatrolling(bool p){

		patrolling = p;

		}

	





	
	void Awake () {

		gameObjectManager = GameObject.FindGameObjectWithTag ("GameManager");
		gameManager = gameObjectManager.GetComponent<GameManager> ();
		//polygonCollider2D = gameObject.GetComponent<PolygonCollider2D> ();


		startPosition = transform.position.x;  
		endPosition = startPosition + unitsToMove;  
		}





	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (patrolling == true) {
			Patrol ();
		}
	
		//CheckCover ();
		//Debug.Log ("InCover is " + inCover);

	}

	void OnCollisionStay2D(Collision2D col){
		/*if (col.gameObject.tag == "Player") {
			col.gameObject = losPlayer;
		

		}

		if (col.gameObject.tag == "Wall") {
			col.gameObject = losWall;

		
		}*/


		}




	//Player Detection
	/*void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			Debug.Log ("I see yoU!");
			gameManager.SendMessage("GameOver", SendMessageOptions.DontRequireReceiver);


		}*/

		/*if (collider.gameObject.tag == "Wall"){
		Debug.Log ("I can't see you!");
		}*/

		


}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPatrol : MonoBehaviour {

	//this script deals with enemy movement, patrols, and player tracking.

	//Public control of Enemy patrolling speeds.
	public float patrolSpeed;
	public float rotationSpeed;
	public bool isPatrollingGuard;




	private float step; // patrollSpeed * deltatime for smoother movement
	private bool moving; //Dunno what this was for.
	private int currentWaypoint; //holds the value of the next waypoint in the list for the patrol.  allows for enemies to continue the patrol without restarting if interupted by TrackLastPosition
	private bool donePatrolling; //to check if a patrol is finished so the patrol can reset

	private bool suspicious;
	public float suspiciousPatrolSpeed;
	public float suspiciousRotationSpeed;
	public float secondsSuspicious;
	public float secondsUntilDetection;
	private Transform playerLastTransform;
	private Vector3 playerLastPosition;
	private int currentPosition; //identical to the functionality of currenwaypoint, but used instead for the enemyLast position list
	

	//Holds a list of waypoints for a patrol
	public List<Transform> waypoints = new List<Transform>();
	List<Vector3> enemyLastPositions = new List<Vector3>();

	
	void Start () {
		
		step = patrolSpeed * Time.fixedDeltaTime;
		donePatrolling = false;
		currentWaypoint = 0;
		playerLastPosition = new Vector3(0,0,0);
		if (isPatrollingGuard) {
			StartCoroutine (Patrol (currentWaypoint, waypoints));
		} 
		else {
			StartCoroutine (StationaryPosition(currentWaypoint));
		}

		
	}

	void Update () {
		
		RestartPatrol ();
		
		
	}




	//Member Functions of Patrol Behavior//

	
	//Rotates about Z axis to look at a specificed target
	IEnumerator LookAt(Transform target, float rotSpeed){
		
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
				transform.Rotate(-Vector3.forward, rotSpeed*Time.deltaTime);
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
				transform.Rotate(Vector3.forward, rotSpeed*Time.deltaTime);
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

	IEnumerator LookAt(Vector3 target, float rotSpeed){
		
		//Determines magnitude of angle between target and self
		Vector3 targetDir = target - transform.position;
		Vector3 forward = -transform.right;
		float angle = Vector3.Angle (targetDir, forward);
		
		//determines left or right handedness of angle
		Vector3 relative = transform.InverseTransformPoint (target);
		float degAngle = Mathf.Atan2 (relative.y, relative.x) * Mathf.Rad2Deg;
		
		
		//Debug.Log (angle);
		Debug.Log (degAngle);
		
		//if the target is to the right
		if (degAngle > 0) {
			while (angle > 5f) {
				transform.Rotate(-Vector3.forward, rotSpeed*Time.deltaTime);
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
				transform.Rotate(Vector3.forward, rotSpeed*Time.deltaTime);
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


	IEnumerator MoveTo(Transform target, float speed){
		
		//Debug.Log ("Following target");
		
		Vector3 targetPosition = target.position;
		
		
		while ((transform.position - targetPosition).sqrMagnitude > 0.3f) {
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, (speed * Time.deltaTime));
			yield return null;
		}
	}




	//Moves to a specified target
	IEnumerator MoveTo(Vector3 target, float speed){
		
		//Debug.Log ("Following target");

		//Vector3 targetPosition = target.position;


		while ((transform.position - target).sqrMagnitude > 0.3f) {
			transform.position = Vector3.MoveTowards(transform.position, target, (speed * Time.deltaTime));
			yield return null;
		}
	}
			

	//Combines LookAt and MoveTo so the enemy looks then moves to a specified waypoint.
	IEnumerator Patrol(int i, List<Transform> waypoints){


		for (i = currentWaypoint; i < waypoints.Count; i++) {
		//	Debug.Log ("patrolling on waypoint " + i);
			 //probably want to make this a property.
			yield return StartCoroutine (LookAt (waypoints[i], rotationSpeed));
			yield return StartCoroutine (MoveTo (waypoints[i], patrolSpeed));
			currentWaypoint++;
			
		}
	//	Debug.Log ("Done Patrolling");
		donePatrolling = true;

	}

	//used to track the last positions of the player backward such that the patrolling guard can return to its original patrol without crashing into walls.
	IEnumerator ReturnToPatrol(int i, List<Vector3> positions){
		
		
		for (i = (currentPosition); i > 0; i--) {
			//	Debug.Log ("patrolling on waypoint " + i);
			//probably want to make this a property.
			int j = i - 1;
			//Debug.Log(j);
			//Debug.Log(positions[j]);
			//yield return StartCoroutine (LookAt (positions[j], rotationSpeed));
			yield return StartCoroutine (MoveTo (positions[j], patrolSpeed));


		}
		Debug.Log ("Returned to original patrol.");
		//donePatrolling = true;
		positions.Clear ();
		
	}






	public IEnumerator TrackLastPosition(GameObject player){

		suspicious = true;
		
		StopCoroutine ("LookAt");
		StopCoroutine ("MoveTo");
		StopCoroutine ("ContinuePatrol");
		
		
		playerLastPosition = player.transform.position;
		playerLastTransform = player.transform;
		enemyLastPositions.Add (transform.position);


		
		Debug.Log ("Player's Last Position: " + playerLastPosition);
		
		yield return StartCoroutine (LookAt (playerLastTransform, suspiciousRotationSpeed));
		yield return new WaitForSeconds (secondsSuspicious);
		yield return StartCoroutine (MoveTo (playerLastPosition, suspiciousPatrolSpeed));
		yield return new WaitForSeconds (secondsSuspicious);
		
		//Debug.Log ("I am not suspicious anymore");
		
		suspicious = false;
		
		StartCoroutine(ContinuePatrol (suspicious));
	
		
	}



	//used in Update to constantly check if patrol needs resetting
	public void RestartPatrol(){

		if (donePatrolling) {

			donePatrolling = false;
			currentWaypoint = 0;

			if(isPatrollingGuard){
				StartCoroutine (Patrol (currentWaypoint, waypoints));
			}

			else{
				StartCoroutine (StationaryPosition(0));
			}


		
		} 
	}





	IEnumerator ContinuePatrol(bool suspicious){

		if (!suspicious) {

			if(isPatrollingGuard){
				currentPosition = enemyLastPositions.Count;
				//Debug.Log("Current Position " + currentPosition);
				yield return StartCoroutine(ReturnToPatrol(currentPosition, enemyLastPositions));
				yield return StartCoroutine (Patrol (currentWaypoint, waypoints));
				Debug.Log("patrol");
			}
			
			else{
				StartCoroutine (StationaryPosition(0));
			}

			yield return null;
		}

		yield return null;
		
		
	}


	IEnumerator StationaryPosition(int i){

		yield return StartCoroutine (LookAt (waypoints [i], rotationSpeed));
		yield return StartCoroutine (MoveTo (waypoints [i], patrolSpeed));
		i++;
		yield return StartCoroutine (LookAt (waypoints [i], rotationSpeed));

			}


	IEnumerator ReturnToLastWayPoint(){
			
		yield return null;





	}

	public bool ReturnSuspicion(){

		return suspicious;

	}




	





	
}

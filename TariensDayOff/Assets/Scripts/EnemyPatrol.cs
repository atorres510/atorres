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

	private bool isSuspicious;
	private bool isTracking;
	private int trackLastCoroutineCounter; // to count the number of coroutines currently in action. used to break any pre-existing coroutines; 
	public float suspiciousPatrolSpeed;
	public float suspiciousRotationSpeed;
	public float secondsSuspicious;
	public float secondsUntilDetection;
	private Transform playerLastTransform;
	private Vector3 playerLastPosition;
	private int currentPosition; //identical to the functionality of currenwaypoint, but used instead for the enemyLast position list
	

	//Holds a list of waypoints for a patrol
	public Transform[] waypoints;
	List<Vector3> waypointPositions = new List<Vector3>();
	List<Vector3> enemyLastPositions = new List<Vector3>();

	
	void Start () {
		
		step = patrolSpeed * Time.fixedDeltaTime;
		donePatrolling = false;
		currentWaypoint = 0;
		playerLastPosition = new Vector3(0,0,0);
		ConvertWaypointsToPositions (waypoints, waypointPositions);
		if (isPatrollingGuard) {
			StartCoroutine (Patrol (currentWaypoint, waypointPositions));
		} 
		else {
			StartCoroutine (StationaryPosition(currentWaypoint));
		}

		
	}

	void Update () {
		
		RestartPatrol ();
		
		
	}




	//Member Functions of Patrol Behavior//


	//conversts the public waypoint array into positions and adds each to a list - to be called in start()
	void ConvertWaypointsToPositions(Transform[] waypointTransformArray, List<Vector3> positionsList){

		for (int i = 0; i < waypointTransformArray.Length; i++) {
				
			Vector3 waypointposition = waypointTransformArray[i].position;

			positionsList.Add(waypointposition);
		
		}
	
	}



	
	//Rotates about Z axis to look at a specificed target - called in patrol and tracklastposition

	IEnumerator LookAt(Vector3 target, float rotSpeed){

		StopCoroutine ("LookAt");

		//Determines magnitude of angle between target and self
		Vector3 targetDir = target - transform.position;
		Vector3 forward = -transform.right;
		float angle = Vector3.Angle (targetDir, forward);
		
		//determines left or right handedness of angle
		Vector3 relative = transform.InverseTransformPoint (target);
		float degAngle = Mathf.Atan2 (relative.y, relative.x) * Mathf.Rad2Deg;
		
		
		//Debug.Log (angle);
		//Debug.Log (degAngle);
		
		//if the target is to the right
		if (degAngle > 0) {
			while (angle > 5f) {
				StopCoroutine("LookAt");
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
				StopCoroutine("LookAt");
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


	//Moves to a specified target position
	IEnumerator MoveTo(Vector3 target, float moveSpeed){

		StopCoroutine ("MoveTo");

		Debug.Log ("move to 1");

		while ((transform.position - target).sqrMagnitude > 0.3f) {
			StopCoroutine("MoveTo");
			transform.position = Vector3.MoveTowards(transform.position, target, (moveSpeed * Time.deltaTime));
			yield return null;
		}
	}

	//overload that takes lookat() parameters and executes lookat() prior to its own code.  
	IEnumerator MoveTo(Vector3 target, float moveSpeed, float rotSpeed){

		StopCoroutine ("MoveTo");

		yield return StartCoroutine (LookAt (target, rotSpeed));

		Debug.Log ("move to 2");
		
		while ((transform.position - target).sqrMagnitude > 0.3f) {
			StopCoroutine ("MoveTo");
			transform.position = Vector3.MoveTowards(transform.position, target, (moveSpeed * Time.deltaTime));
			yield return null;
		}
	}


	//overload that uses lookat() and moveto() in jxn with suspicion.  to be used in tracklastposition
	IEnumerator MoveTo(Vector3 target, float moveSpeed, float rotSpeed, bool suspicion, float waitTimeinSeconds){


		if (suspicion) {
				
			yield return StartCoroutine (LookAt (target, rotSpeed));
			yield return new WaitForSeconds(waitTimeinSeconds);
			
			Debug.Log ("move to 2");
			
			while ((transform.position - target).sqrMagnitude > 0.3f) {
				StopCoroutine ("MoveTo");
				transform.position = Vector3.MoveTowards(transform.position, target, (moveSpeed * Time.deltaTime));
				yield return null;
			}

			yield return new WaitForSeconds(waitTimeinSeconds);

		}

		else{

			yield return StartCoroutine (LookAt (target, rotSpeed));
			
			Debug.Log ("move to 2");
			
			while ((transform.position - target).sqrMagnitude > 0.3f) {
				StopCoroutine ("MoveTo");
				transform.position = Vector3.MoveTowards(transform.position, target, (moveSpeed * Time.deltaTime));
				yield return null;
			}

		}



	}


			

	//Combines LookAt and MoveTo so the enemy looks then moves to a specified waypoint.
	IEnumerator Patrol(int i, List<Vector3> waypoints){


		for (i = currentWaypoint; i < waypoints.Count; i++) {
		//	Debug.Log ("patrolling on waypoint " + i);
			 //probably want to make this a property.
			//yield return StartCoroutine (LookAt (waypoints[i], rotationSpeed));
			yield return StartCoroutine (MoveTo (waypoints[i], patrolSpeed, rotationSpeed));
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

		yield return StartCoroutine (Patrol (currentWaypoint, waypointPositions));
		
	}






	public IEnumerator TrackLastPosition(GameObject player){


		trackLastCoroutineCounter++;
		Debug.Log ("Counter : " + trackLastCoroutineCounter);

		isSuspicious = true;

		StopCoroutine ("LookAt");
		StopCoroutine ("MoveTo");
		StopCoroutine ("Patrol");
		StopCoroutine ("TrackLastPosition");
		StopCoroutine ("ReturnToPatrol");



		playerLastPosition = player.transform.position;
		//playerLastTransform = player.transform;
		enemyLastPositions.Add (transform.position);

		
		Debug.Log ("Player's Last Position: " + playerLastPosition);

		yield return StartCoroutine (LookAt (playerLastPosition, suspiciousRotationSpeed));

		//float secondsWaited = 0.0f;
		/*while (secondsWaited <= secondsSuspicious) {
				
			yield return new WaitForSeconds(0.1f);
			secondsWaited += 0.1f;
			yield return 0;
		
		}*/
		//secondsWaited = 0.0f;


		yield return new WaitForSeconds (secondsSuspicious);

		if (trackLastCoroutineCounter > 1) {
				
			trackLastCoroutineCounter--;
			Debug.Log("Break. Counter : " + trackLastCoroutineCounter);
			yield break;
		
		}

		else{
			trackLastCoroutineCounter--;
		}



		yield return StartCoroutine (MoveTo (playerLastPosition, suspiciousPatrolSpeed));

		//Debug.Log(StartCoroutine(MoveTo(playerLastPosition, suspiciousPatrolSpeed));
		Debug.Log(trackLastCoroutineCounter);

		yield return new WaitForSeconds (secondsSuspicious);
		
		Debug.Log ("I am not suspicious anymore");
		
		isSuspicious = false;

		currentPosition = enemyLastPositions.Count;
		//trackLastCoroutineCounter--;
		if (trackLastCoroutineCounter == 0) {
				
			yield return StartCoroutine(ReturnToPatrol(currentWaypoint, enemyLastPositions));
		
		}

		else{
			yield return null;
		}


	
		
	}

	//used to see if more than one of a certain coroutine is active.  if so, a break is necessary for this coroutine.


	//used in Update to constantly check if patrol needs resetting
	public void RestartPatrol(){

		if (donePatrolling) {

			donePatrolling = false;
			currentWaypoint = 0;

			if(isPatrollingGuard){
				StartCoroutine (Patrol (currentWaypoint, waypointPositions));
			}

			else{
				StartCoroutine (StationaryPosition(0));
			}

		} 
	}





	/*void ContinuePatrol(bool suspicious){

		if (!suspicious) {

			if(isPatrollingGuard){
				currentPosition = enemyLastPositions.Count;
				//Debug.Log("Current Position " + currentPosition);
				StartCoroutine(ReturnToPatrol(currentPosition, enemyLastPositions));
				//yield return StartCoroutine (Patrol (currentWaypoint, waypointPositions));
				Debug.Log("patrol");
			}
			
			else{
				StartCoroutine (StationaryPosition(0));
			}

			yield return null;
		}

		yield return null;
		
		
	}*/


	IEnumerator StationaryPosition(int i){

		yield return StartCoroutine (LookAt (waypointPositions [i], rotationSpeed));
		yield return StartCoroutine (MoveTo (waypointPositions [i], patrolSpeed));
		i++;
		yield return StartCoroutine (LookAt (waypointPositions [i], rotationSpeed));

			}


	IEnumerator ReturnToLastWayPoint(){
			
		yield return null;





	}

	public bool ReturnSuspicion(){

		return isSuspicious;

	}




	





	
}

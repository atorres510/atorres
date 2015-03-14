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
	private int currentWaypoint;
	private bool donePatrolling; //to check if a patrol is finished so the patrol can reset

	private bool suspicious;
	public float suspiciousPatrolSpeed;
	public float suspiciousRotationSpeed;
	public float secondsSuspicious;
	public float secondsUntilDetection;
	private Transform playerLastTransform;
	private Vector3 playerLastPosition;
	

	//Holds a list of waypoints for a patrol
	public List<Transform> waypoints = new List<Transform>();
	List<Transform> playerLastPositions = new List<Transform>();

	
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
	IEnumerator LookAt(Transform target, float rotationSpeed){
		
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
				transform.Rotate(-Vector3.forward, rotationSpeed*Time.deltaTime);
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
				transform.Rotate(Vector3.forward, rotationSpeed*Time.deltaTime);
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

	public IEnumerator TrackLastPosition(GameObject player){

		suspicious = true;
		
		StopCoroutine ("LookAt");
		StopCoroutine ("MoveTo");
		
		
		playerLastPosition = player.transform.position;
		playerLastTransform = player.transform;
		
		Debug.Log ("Player's Last Position: " + playerLastPosition);
		
		yield return StartCoroutine (LookAt (playerLastTransform, suspiciousRotationSpeed));
		yield return new WaitForSeconds (secondsSuspicious);
		yield return StartCoroutine (MoveTo (playerLastPosition, suspiciousPatrolSpeed));
		yield return new WaitForSeconds (secondsSuspicious);
		
		//Debug.Log ("I am not suspicious anymore");
		
		suspicious = false;
		
		ContinuePatrol (suspicious);
	
		
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

	
	public void ContinuePatrol(bool suspicious){

		if (!suspicious) {

			if(isPatrollingGuard){
				StartCoroutine (Patrol (currentWaypoint, waypoints));
			}
			
			else{
				StartCoroutine (StationaryPosition(0));
			}
		}
		
		
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

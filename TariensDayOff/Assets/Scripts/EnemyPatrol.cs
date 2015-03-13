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
	

	//Holds a list of waypoints for a patrol
	public List<Transform> waypoints = new List<Transform>();
	List<Transform> playerLastPositions = new List<Transform>();

	
	void Start () {
		
		step = patrolSpeed * Time.fixedDeltaTime;
		donePatrolling = false;
		currentWaypoint = 0;
		if (isPatrollingGuard) {
			StartCoroutine (Patrol (currentWaypoint));
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
	IEnumerator LookAt(Transform target){

		Vector3 targetDir = target.position - transform.position;
		Vector3 forward = -transform.right;

		float angle = Vector3.Angle (targetDir, forward);

		while (angle > 1f) {
			transform.Rotate(Vector3.forward, rotationSpeed*Time.deltaTime);
			forward = -transform.right;
			angle = Vector3.Angle (targetDir, forward);
			//Debug.Log (angle);
			yield return null;
		}
	}



	//Moves to a specified target
	IEnumerator MoveTo(Transform target){

		while ((transform.position - target.position).sqrMagnitude > 0.3f) {
				transform.position = Vector3.MoveTowards(transform.position, target.position, (step));
				yield return null;
			}
	}
			

	//Combines LookAt and MoveTo so the enemy looks then moves to a specified waypoint.
	IEnumerator Patrol(int i){


		for (i = currentWaypoint; i < waypoints.Count; i++) {
		//	Debug.Log ("patrolling on waypoint " + i);
			 //probably want to make this a property.
			yield return StartCoroutine (LookAt (waypoints[i]));
			yield return StartCoroutine (MoveTo (waypoints[i]));
			currentWaypoint++;
			
		}
	//	Debug.Log ("Done Patrolling");
		donePatrolling = true;

	}


	public void InteruptPatrol(){
		StopAllCoroutines ();

	//	Debug.Log ("Interupting Patrol");
	
	
	}



	//used in Update to constantly check if patrol needs resetting
	public void RestartPatrol(){

		if (donePatrolling) {

			donePatrolling = false;
			currentWaypoint = 0;

			if(isPatrollingGuard){
				StartCoroutine (Patrol (currentWaypoint));
			}

			else{
				StartCoroutine (StationaryPosition(0));
			}


		
		} 
	}

	
	public void ContinuePatrol(bool suspicious){

		if (!suspicious) {

			if(isPatrollingGuard){
				StartCoroutine (Patrol (currentWaypoint));
			}
			
			else{
				StartCoroutine (StationaryPosition(0));
			}
		}
		
		
	}


	IEnumerator StationaryPosition(int i){

		yield return StartCoroutine (LookAt (waypoints [i]));
		yield return StartCoroutine (MoveTo (waypoints [i]));
		i++;
		yield return StartCoroutine (LookAt (waypoints [i]));

			}


	IEnumerator ReturnToLastWayPoint(){
			
		yield return null;





	}




	





	
}

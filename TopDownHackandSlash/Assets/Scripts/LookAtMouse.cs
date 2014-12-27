using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour {

	public float speed;
	
	float anglePreUpdate;
	float anglePostUpdate;
	
	void LookAt(Vector3 target){
		
		//Determines magnitude of angle between target and self
		Vector3 targetDir = target - transform.position;
		Vector3 forward = -transform.right;
		float angle = Vector3.Angle (targetDir, forward);
		//Debug.Log (angle);
		
		//determines left or right handedness of angle
		Vector3 relative = transform.InverseTransformPoint (target);
		float degAngle = Mathf.Atan2 (relative.y, relative.x) * Mathf.Rad2Deg;
		Debug.Log (degAngle);

		//anglePreUpdate = angle;

		//float difference =  Mathf.Abs (anglePostUpdate) - Mathf.Abs (anglePreUpdate);
		//Debug.Log (difference);

		//if (difference <= 1 && difference >= -1) {
			//pass
			//	}
	//	else{

			//if the target is to the right
			if (degAngle > 0) {
					if (angle > 20f) {
							transform.Rotate (-Vector3.forward, speed * Time.deltaTime);
							//forward = -transform.right;
							//relative = transform.InverseTransformPoint (target.position);
							//angle = Vector3.Angle (targetDir, forward);
							//angle = Mathf.Atan2 (relative.y, relative.x) * Mathf.Rad2Deg;
							//Debug.Log ("I'm looking right!");
							//Debug.Log (angle);

					}

			}

			//if the target is to the left
			if (degAngle < 0) {
					if (angle > 20f) {
							transform.Rotate (Vector3.forward, speed * Time.deltaTime);
							//forward = -transform.right;
							//relative = transform.InverseTransformPoint (target.position);
							//angle = Vector3.Angle (targetDir, forward);
							//angle = Mathf.Atan2 (relative.y, relative.x) * Mathf.Rad2Deg;
							//Debug.Log("I'm looking left!");
							//Debug.Log (angle);


					} 

			}

		//}
		//anglePostUpdate = angle;

	}
	






	// Update is called once per frame
	void FixedUpdate () {

		/*Plane playerPlane = new Plane (Vector3.back, transform.position);

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		float rayX = ray.direction.x;
		float rayY = ray.direction.y;

		Vector3 targetPoint = new Vector3 (rayX, rayY, 0);*/

		Vector3 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		//Debug.Log (Input.mousePosition);




		LookAt (mouse);

	

	}
}

using UnityEngine;
using System.Collections;

public class ITrigger : MonoBehaviour {

	BoxCollider2D triggerBoxCollider2D;

	bool isTriggered;

	public bool IsTriggered{

		get{
			return isTriggered;
		}

		set{
			isTriggered = value;
		}

	}

	public BoxCollider2D TriggerBoxCollider2D{

		get{
			return triggerBoxCollider2D;
		}

		set{ 
			triggerBoxCollider2D = value;
		}


	}

	public Vector3 ReturnTriggerPosition(){

		return gameObject.transform.position;
	
	}

	public virtual void ResetTrigger(bool reset){

		if (reset) {
			IsTriggered = false;
				
		}

	
	
	}


	void Awake(){
	
		isTriggered = false;
		TriggerBoxCollider2D = gameObject.GetComponent<BoxCollider2D> ();

	}






}

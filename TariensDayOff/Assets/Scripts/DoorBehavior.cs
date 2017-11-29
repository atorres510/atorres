using UnityEngine;
using System.Collections;

public class DoorBehavior : MonoBehaviour {

	public BoxCollider2D doorBoxCollider2D;

	private GameObject player;
	//private BoxCollider2D boxcollider2D;


	private bool isDoorOpen;







    //Initialize variables
	void Awake() {

		player = GameObject.FindGameObjectWithTag ("Player");
		//boxcollider2D = GetComponent<BoxCollider2D> ();
		doorBoxCollider2D.enabled = true;
		isDoorOpen = false;


	}
	
	void Start () {
	
	}
	

	void Update () {

	
	}

	void OnTriggerStay2D(Collider2D other){
        Debug.Log(other.name);
        if (other.gameObject == player){

            Debug.Log("Player is colliding.");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("GetKeyDown");
                if (!isDoorOpen)
                {

                    isDoorOpen = true;
                    doorBoxCollider2D.enabled = false;
                    Debug.Log("Door is open");

                }
                else if (isDoorOpen)
                {

                    isDoorOpen = false;
                    doorBoxCollider2D.enabled = true;
                    Debug.Log("Door is closed");

                }

            }
            
        }
        
	}
    
}













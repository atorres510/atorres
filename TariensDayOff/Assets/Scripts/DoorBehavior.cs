using UnityEngine;
using System.Collections;

public class DoorBehavior : MonoBehaviour {

    //Components
	public BoxCollider2D doorBoxCollider2D;
    public SpriteRenderer spriteRenderer;
    //Sprite states
    public Sprite openStateSprite;
    public Sprite closedStateSprite;


	private GameObject player;
	//private BoxCollider2D boxcollider2D;

    [SerializeField]
	private bool isDoorOpen;
    

    //Initialize variables
	void Awake() {

		player = GameObject.FindGameObjectWithTag ("Player");
		doorBoxCollider2D.enabled = !isDoorOpen;
		


	}


	void OnTriggerStay2D(Collider2D other){
        //Debug.Log(other.name);
        if (other.gameObject == player){

            //Debug.Log("Player is colliding.");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("GetKeyDown");
                if (!isDoorOpen)
                {

                    isDoorOpen = true;
                    doorBoxCollider2D.enabled = false;
                    spriteRenderer.sprite = openStateSprite;
                    Debug.Log("Door is open");

                }
                else if (isDoorOpen)
                {

                    isDoorOpen = false;
                    doorBoxCollider2D.enabled = true;
                    spriteRenderer.sprite = closedStateSprite;
                    Debug.Log("Door is closed");

                }

            }
            
        }
        
	}
    
}













using UnityEngine;
using System.Collections;

public class DoorBehavior : MonoBehaviour {

    [SerializeField]
    private bool isDoorOpen; //determines open/closed stated. Can be set in the inspector if the door should start in a certain state.
    
    //Components
    public BoxCollider2D doorBoxCollider2D;
    public SpriteRenderer mainSpriteRenderer; //This renderer comes from the sprite renderer on the door's collider.  Should contain the entire sprite
    public SpriteRenderer topSpriteRenderer; //This renderer comes from the parent gameobject.  Should only contain the top of the sprite.

    //Sprite states
    public Sprite openStateSprite; //these two sprites are the toggled sprite states for the main sprite renderer
    public Sprite closedStateSprite;
    public Sprite topOpenStateSprite;//these two sprites are the toggled sprite states for the top sprite renderer
    public Sprite topClosedStateSprite;

    private GameObject player;
    
   

    //Initialize variables
	void Awake() {

		player = GameObject.FindGameObjectWithTag ("Player"); //find player.
        CheckDoorState(); //checks if the door should start open or closed based on inspector variable setting.
		
	}

    //checks if player is colliding with trigger collider, then toggles the 
    //door open boolean if button is pressed.  Then calls CheckDoorState to "open" or "close" the door.  
	void OnTriggerStay2D(Collider2D other){
        
        if (other.gameObject == player){

            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                isDoorOpen = !isDoorOpen;
                CheckDoorState();

            }
            
        }
        
	}

    //Called when the door properties need to be changed.  Uses isDoorOpen.
    void CheckDoorState() {

        if (isDoorOpen) //Open Door State
        {
            
            doorBoxCollider2D.enabled = false;
            mainSpriteRenderer.sprite = openStateSprite;
            topSpriteRenderer.sprite = topOpenStateSprite;
            Debug.Log("Door is open");

        }
        else if (!isDoorOpen) //Closed Door State
        {
            
            doorBoxCollider2D.enabled = true;
            mainSpriteRenderer.sprite = closedStateSprite;
            topSpriteRenderer.sprite = topClosedStateSprite;
            Debug.Log("Door is closed");

        }

    }
    

}













using UnityEngine;
using System.Collections;

public class DoorBehavior : MonoBehaviour {

    //Components
	public BoxCollider2D doorBoxCollider2D;
    public SpriteRenderer mainSpriteRenderer;
    public SpriteRenderer topSpriteRenderer;
    //Sprite states
    public Sprite openStateSprite;
    public Sprite closedStateSprite;
    public Sprite topOpenStateSprite;
    public Sprite topClosedStateSprite;


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
                if (!isDoorOpen) //Open Door State
                {

                    isDoorOpen = true;
                    doorBoxCollider2D.enabled = false;
                    mainSpriteRenderer.sprite = openStateSprite;
                    topSpriteRenderer.sprite = topOpenStateSprite;
                    Debug.Log("Door is open");

                }
                else if (isDoorOpen) //Closed Door State
                {

                    isDoorOpen = false;
                    doorBoxCollider2D.enabled = true;
                    mainSpriteRenderer.sprite = closedStateSprite;
                    topSpriteRenderer.sprite = topClosedStateSprite;
                    Debug.Log("Door is closed");

                }

            }
            
        }
        
	}
    
}













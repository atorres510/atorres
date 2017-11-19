using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script defines the abilities for the main character and the controller components such as key presses.  Does not include Vaultpoint or door behaviours, 
//along with any other behaviours that interact with the environment. Public members should be set in the inspector.
public class PlayerAbilities : MonoBehaviour {

    //General Ability Member Variables
    private bool isUsingAbility;
    private int currentAbility = 1; //should always start at 1.
    private int maxNumAbilities = 2; //should match the number of abilities active (i.e. if distract and shadowstep are active, set max to 2).

 
    //Player Object and Components
    private GameObject player;
    private Animator playerAnimator;
    private PlayerController2D playerController;
    private BoxCollider2D playerCollider;
    private Rigidbody2D playerRigidbody;

    //Shadowstep Member Variables
    public GameObject playerGhostPrefab;
    public int ghostMaxRadius; //sets the limit radius of the shadowstep ability

    private bool isShadowstepping = false; //called in the controller, ensures the ability is not called more than once
    private bool isCompletingShadowstep = false; //^^^
    private GameObject ghostClone = null; //holds the instantiated ghost clone

    //Distract Member Variables
    public GameObject bombPrefab;
    public float distractDistance; //distance that the bomb is instantiated from the player at a given direction
   
    private GameObject bombClone = null; //holds the instantiated bomb clone
    private bool isBombDeployed = false; //keeps from more than one bomb being deployed at a time
  
 
    

    void Awake(){

        InitializePlayerandComponents();
     
    }

    // Update is called once per frame
    void Update () {

        AbilityController();
        AbilitySelector();

        SetIsUsingAbility();
		
	}



    #region General Script Methods
    void InitializePlayerandComponents() {

        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
        playerController = player.GetComponent<PlayerController2D>();
        playerCollider = player.GetComponent<BoxCollider2D>();
        playerRigidbody = player.GetComponent<Rigidbody2D>();

    }
    //called in Update, sets currentAbility with F and toggles between the abilities.
    //maxNumAbilities should be set to the number of abilities available (i.e. if distract and shadowstep are active, then set maxNumbAbilities to 2).
    void AbilityController() {

        if (Input.GetKeyDown(KeyCode.F) && !isUsingAbility) { //keeps from activating if an ability is in progress.

            currentAbility++;

            if (currentAbility > maxNumAbilities) { //cycles back to the first ability if the maxNum was passed.

                currentAbility = 1;

            }
            
        }
       
    }

    //Called in Update, switches the active ability controller on and off.
    void AbilitySelector() {

        switch (currentAbility) {

            case 1: ShadowstepController();
                //Debug.Log("Shadowstep Active.");
                break;

            case 2: DistractController();
                //Debug.Log("Distract Active");
                break;

        }
        
    }


    //called in update, 
    void SetIsUsingAbility() {

        isUsingAbility = isShadowstepping;
        
    }
    //get method for isUsingAbility.  necessary for other scripts to determine if other interactions should be allowed.  
    //currently only called by Vaultpointbehaviour.
    public bool GetIsUsingAbility() {

        return isUsingAbility;
        
    }

    #endregion
    
    // Abilities //

    #region Shadowstep Controller and Methods

    void ShadowstepController() {
        
        if (Input.GetKey(KeyCode.LeftShift) && !isShadowstepping) {

            InitiateShadowstep();
            
        }
        
        if (Input.GetKeyUp(KeyCode.LeftShift) && isShadowstepping && !isCompletingShadowstep) {

            //keeps the clone from moving and sets the proper animations.  this ensures the clone is in TarienForwardIdle, which is the only
            //animation pathway to the shadowstep forward animation.

            isCompletingShadowstep = true; //ensure that the "GetKeyUp" method does not activate more than once.  This is reset in "ResetShadowstep"
         
            ghostClone.GetComponent<PlayerController2D>().enabled = false;
            ghostClone.GetComponent<Animator>().SetInteger("direction", 1);
            ghostClone.GetComponent<Animator>().SetBool("moving", false);
            ghostClone.GetComponent<Animator>().SetBool("shadowstepping", true);

            ghostClone.GetComponent<Animator>().Play("TarienForwardIdle");
             
            playerAnimator.SetBool("shadowstepping", false); //triggers the animation to begin, which will trigger the subsequent methods to continue the ability.
           
        }

    }

    //called in ShadowstepController.  sets the player components to false and sets appropriate animation parameters.
    //also instantiates a ghostClone prefab
    void InitiateShadowstep() {

        isShadowstepping = true;// ensures that the shadowstepController "GetKey" method does not activate more than once. this should be reset in "ResetShadowstep"
        playerAnimator.SetBool("shadowstepping", true);
        playerAnimator.SetInteger("direction", 1);
        playerAnimator.SetBool("moving", false);
        
        playerController.enabled = false;
        playerCollider.isTrigger = true; //does not cause collision but still acts as a trigger for guards
        playerRigidbody.bodyType = RigidbodyType2D.Static; //keeps player from moving with ghost due to joint

        ghostClone = (GameObject)Instantiate(playerGhostPrefab, player.transform.position, player.transform.rotation);
        
    }

    //called in the Animator during the Shadowstep animation as an animation event.  
    //ensure the player is done disapperating before moving them to the ghosts position, then 
    //destroys the ghost.
    public void Shadowstep() {
        
        player.transform.position = ghostClone.transform.position;
        Destroy(ghostClone);

    }

    //called in the Animator at the end of the shadowstep animation.  resets player components and shadowstepping so that the
    //player can't move during the animation and once animation is complete, that shadowstep can be used again.
    public void ResetShadowstep() {

        playerController.enabled = true;
        playerCollider.isTrigger = false;
        playerRigidbody.bodyType = RigidbodyType2D.Dynamic; //resets rigidbody bodytype to allow collision with walls and enemies

        isShadowstepping = false;
        isCompletingShadowstep = false;


    }

    #endregion

    #region Distract Controller and Methods
    //holds key inputs and checks if bomb is still deployed or not.
    void DistractController() {

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isBombDeployed) {

            InstantiateBomb(distractDistance);

        }

        if (bombClone == null)
        {

            isBombDeployed = false;

        }


    }

    void InstantiateBomb(float distance) {

        isBombDeployed = true;

        Vector3 playerPosition = player.transform.position;
        Vector3 instantiatePosition = Vector3.zero;

        int caseSwitch = playerAnimator.GetInteger("direction");

        switch (caseSwitch) {

            case 1: //forward
                instantiatePosition = playerPosition + (new Vector3(0, -distance, 0));
                break;
            case 2: //back
                instantiatePosition = playerPosition + (new Vector3(0, (distance - 0.5f), 0));
                break;
            case 3: //left
                instantiatePosition = playerPosition + (new Vector3(-distance, -0.5f, 0));
                break;
            case 4: //right
                instantiatePosition = playerPosition + (new Vector3(distance, -0.5f, 0));
                break;

        }

        bombClone = Instantiate(bombPrefab, instantiatePosition, player.transform.rotation);
        
    }





















    #endregion







}

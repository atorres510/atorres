using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script defines the abilities for the main character and the controller components such as key presses.  Does not include Vaultpoint behaviours.
public class PlayerAbilities : MonoBehaviour {

    //General Ability Member Variables
    private bool isUsingAbility;
 

    //Player Object and Components
    private GameObject player;
    private Animator playerAnimator;
    private PlayerController2D playerController;
    private BoxCollider2D playerCollider;
    private Rigidbody2D playerRigidbody;

    //Shadowstep Member Variables
    public GameObject playerGhostPrefab;
    public int ghostMaxRadius;

    private bool isShadowstepping = false;
    private bool isCompletingShadowstep = false;
    private GameObject ghostClone;
 
    




    void Awake(){

        InitializePlayerandComponents();
     
    }

    // Update is called once per frame
    void Update () {

        ShadowstepController();
        SetIsUsingAbility();
		
	}

    void InitializePlayerandComponents() {

        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
        playerController = player.GetComponent<PlayerController2D>();
        playerCollider = player.GetComponent<BoxCollider2D>();
        playerRigidbody = player.GetComponent<Rigidbody2D>();

    }

    void SetIsUsingAbility() {

        isUsingAbility = isShadowstepping;
        
    }

    public bool GetIsUsingAbility() {

        return isUsingAbility;
        
    }

   




    #region Shadowstep Controller and Methods

    void ShadowstepController() {
        

        if (Input.GetKey(KeyCode.LeftShift) && !isShadowstepping) {

            InitiateShadowstep();
            
        }
        

        if (Input.GetKeyUp(KeyCode.LeftShift) && isShadowstepping && !isCompletingShadowstep) {

            //keeps the clone from moving and sets the proper animations.  this ensures the clone is in TarienForwardIdle, which is the only
            //animation pathway to the shadowstep forward animation.

            isCompletingShadowstep = true;
         
            ghostClone.GetComponent<PlayerController2D>().enabled = false;
            ghostClone.GetComponent<Animator>().SetInteger("direction", 1);
            ghostClone.GetComponent<Animator>().SetBool("moving", false);
            ghostClone.GetComponent<Animator>().SetBool("shadowstepping", true);

            ghostClone.GetComponent<Animator>().Play("TarienForwardIdle");
             



            playerAnimator.SetBool("shadowstepping", false); //triggers the animation to begin, which will trigger the subsequent methods to continue the ability.
           
        }

    }

    //called in ShadowstepController.  sets the player components to false and sets appropriate animation parameters.
    //also instantiates ghostClone
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

    //called in the Animator.  resets player components and shadowstepping so that the
    //player can't move during the animation and once animation is complete, that shadowstep can be used again.
    public void ResetShadowstep() {

        playerController.enabled = true;
        playerCollider.isTrigger = false;
        playerRigidbody.bodyType = RigidbodyType2D.Dynamic; //resets rigidbody bodytype to allow collision with walls and enemies

        isShadowstepping = false;
        isCompletingShadowstep = false;


    }


   














    #endregion 








}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour {
    //This script primarily sets the variables to ensure the ghost stays within range of the player.  The range is set in the player abilities script on the 
    //player object in the inspector.  Requires a DistanceJoint2D on the ghost object, but its variables need not be set in the inspector.  

    //Fills the parameters of the Ghost's distance joint to give the ability limited range
    GameObject player;
    PlayerAbilities playerAbilities;
  
    DistanceJoint2D joint; //joint connected to the PlayerGhost game object.
    
    void Awake(){
        //Initialized player and its components.
        player = GameObject.FindGameObjectWithTag("Player");
        playerAbilities = player.GetComponent<PlayerAbilities>();

        
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.connectedBody = player.GetComponent<Rigidbody2D>(); //sets the player to the rigidbody
        joint.distance = playerAbilities.ghostMaxRadius; //sets the radius as determined in the PlayerAbilities script on the player.

        
    }


}

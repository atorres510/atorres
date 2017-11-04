using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour {


    //fills the parameters of the Ghost's distance joint to give the ability limited range
    GameObject player;
    PlayerAbilities playerAbilities;
  
    DistanceJoint2D joint; //joint connected to the PlayerGhost game object.
    
    void Awake(){
        //initialized player and its components
        player = GameObject.FindGameObjectWithTag("Player");
        playerAbilities = player.GetComponent<PlayerAbilities>();

        
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.connectedBody = player.GetComponent<Rigidbody2D>(); //sets the player to the rigidbody
        joint.distance = playerAbilities.ghostMaxRadius; //sets the radius as determined in the PlayerAbilities script on the player.

        
    }


}

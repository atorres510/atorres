using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour {
    //On awake, bome will "light" its fuse, which works with the animator to play animation and then detonate.

    //Public Members
    public float fuseSeconds = 3f; //length in seconds before detonation animation is performed. Default 3 seconds.


    //Bomb Object Colliders and Animator
    private BoxCollider2D boxCollider; //for object
    private CircleCollider2D circleCollider; //for sound
    private Animator animator;

    //Get components
    private void Awake(){

        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        animator = gameObject.GetComponent<Animator>();
        StartCoroutine(LightFuse());

    }

    
    //Calls animator to start animation.  Called on awake.
    IEnumerator LightFuse(){

        yield return new WaitForSeconds(fuseSeconds);
        animator.SetBool("isFuseLit", true);
        
    }

    //called in animator, changes colliders from box to circle.
    public void Detonate() {

        boxCollider.enabled = false;
        circleCollider.enabled = true;

    }

    //Destroys the bomb object, called in animator after Detonate anim.
    public void DestroyBomb() {

        Destroy(gameObject);
        
    }





}

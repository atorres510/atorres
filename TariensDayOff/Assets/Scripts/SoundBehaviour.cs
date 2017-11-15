using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBehaviour : MonoBehaviour {

    public LayerMask activeLayers;
    public float radius;
    public CircleCollider2D soundCollider;

    //public List<EnemySight> listeningEnemies = new List<EnemySight>();

    private void Awake()
    {

        soundCollider.radius = radius;

    }






    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log(other.gameObject.GetComponent(typeof(EnemySight)));
        if (other.gameObject.GetComponent(typeof(EnemySight))){
            
            SoundCast(gameObject, other.gameObject);

        }

    }

    //Uses the sound's collider to determine what is in range of the sound, 
    //then raycasts to determine if the target object should be able to hear the sound.
    void SoundCast(GameObject originObject, GameObject targetObject) {

        //Get object positions
        Vector2 origin = originObject.transform.position;
        Vector2 target = targetObject.transform.position;
        
        Vector2 direction = target - origin; //corrects the ray, I have no idea why this works but don't take this out.

        //Mostly for debugging.
        float distance = Vector2.Distance(origin, target);
        //Debug.Log("Distance of " + targetObject.name + " is " + distance + ".");

        //Casts ray, collecting all objects in its path.  If a wall is one of them, then set isWallObstructing to true.
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, distance, activeLayers);
        Debug.DrawRay(origin, direction, Color.red, 1f, false);

        bool isWallobstructing = false;
        bool isInRange = false;
        foreach (RaycastHit2D element in hits) {

            //Debug.Log("Hit: " + element.collider.gameObject.name);
            if (element.collider.gameObject.tag == "Wall")
            {

                isWallobstructing = true;

            }
            
            else if ((element.collider.gameObject.GetComponent(typeof(EnemySight))) && distance <= radius) {

                isInRange = true;

            }
            
        }

        Debug.Log("Wall obstruction: " + isWallobstructing);
        Debug.Log("In range: " + isInRange);

        //if target is an enemy within range and not being obstructed, then call LookForTarget in EnemySight Script of the object.
        if (!isWallobstructing && isInRange && (targetObject.GetComponent(typeof(EnemySight)))) {

            targetObject.GetComponent<EnemySight>().LookForTarget(gameObject);

        }

        
       
    }


}

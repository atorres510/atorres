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






    //cast circle and return all enemies found
    void SoundCast(GameObject originObject, GameObject targetObject) {

        Vector2 origin = originObject.transform.position;
        Vector2 target = targetObject.transform.position;
        
        Vector2 direction = target - origin;
        float distance = Vector2.Distance(origin, target);
        Debug.Log("Distance of " + targetObject.name + " is " + distance + ".");

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

        if (!isWallobstructing && isInRange && (targetObject.GetComponent(typeof(EnemySight)))) {

            targetObject.GetComponent<EnemySight>().LookingForOther(gameObject);

        }

        
       
    }


}

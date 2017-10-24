using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TestScript : MonoBehaviour {

    public float smooth = 2.0F;
    public float tiltAngle = 30.0F;

   // public float angle = 0.0F;
    public Vector3 axis = Vector3.zero;
    IEnumerator Example(Vector3 byAngles, float inSeconds)
    {
        Quaternion fromAngle = transform.rotation;
        Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (float t = 0f; t < 1; t += Time.deltaTime / inSeconds)
        {
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }

        




    }

    // Use this for initialization
    void Start () {

        StartCoroutine(Example(-Vector3.forward * 45, 1.5f));
      
	}
	
	// Update is called once per frame
	void Update () {
       
        
        
}
}

using UnityEngine;
using System.Collections;

public class HitDetection : MonoBehaviour {

	public string colliderTag;
	MeshRenderer meshRenderer;



	void Start(){

		meshRenderer = gameObject.GetComponent<MeshRenderer> ();
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == colliderTag) {
			Debug.Log("Hit");
			StartCoroutine("OnHit");
		
		
		}

	}





	IEnumerator OnHit(){

		meshRenderer.enabled = false;
		yield return new WaitForSeconds (0.1f);
		meshRenderer.enabled = true;
		yield return new WaitForSeconds (0.1f);
		meshRenderer.enabled = false;
		yield return new WaitForSeconds (0.1f);
		meshRenderer.enabled = true;
		yield return new WaitForSeconds (0.1f);
		meshRenderer.enabled = false;
		yield return new WaitForSeconds (0.1f);
		meshRenderer.enabled = true;
		yield return new WaitForSeconds (0.1f);
		meshRenderer.enabled = false;
		yield return new WaitForSeconds (0.1f);
		meshRenderer.enabled = true;
		yield return new WaitForSeconds (0.1f);

	}




}

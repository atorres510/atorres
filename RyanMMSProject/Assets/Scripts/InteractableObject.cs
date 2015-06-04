using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour {


	Light simpleHighlightEffect;
	public Text hoverOverText;

	public string test;





	void OnMouseOver(){
	
	
		simpleHighlightEffect.enabled = true;
	
	
	
	
	}




	void OnMouseExit(){
	
		simpleHighlightEffect.enabled = false;
	
	
	}






	void SetupObject(){
	


	
	
	}






	void Start () {

		simpleHighlightEffect = gameObject.GetComponent<Light> ();
		simpleHighlightEffect.enabled = false;

		int stringLength = test.Length;

		Debug.Log (stringLength);

	
	}
	




	void Update () {
	
	}
}

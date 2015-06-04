using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour {



	public TextAsset text;
	public Textbox textBoxPrefab;

	Light simpleHighlightEffect;



	void OnMouseOver(){
	
	
		simpleHighlightEffect.enabled = true;

		if (Input.GetMouseButton (0)) {
		
		
		
		
		
		
		}
	
	
	
	
	}




	void OnMouseExit(){
	
		simpleHighlightEffect.enabled = false;
	
	
	}



	void InstantiateTextBox(TextAsset textFile){
	
		Canvas canvas = FindObjectOfType <Canvas>();

		Textbox textBox = Instantiate (textBoxPrefab, transform.position, Quaternion.identity) as Textbox;

		textBox.transform.SetParent (gameObject.transform);

		//textBox.transform.position = gameObject.transform.position;

		RectTransform rect = textBox.GetComponent<RectTransform> ();

		rect.anchoredPosition = new Vector2(transform.position.x, transform.position.y);

		textBox.TextFile = textFile;
	
	
	
	}



	void Start () {

		simpleHighlightEffect = gameObject.GetComponent<Light> ();
		simpleHighlightEffect.enabled = false;
		InstantiateTextBox (text);




	
	}
	




	void Update () {
	
	}
}

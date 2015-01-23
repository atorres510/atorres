using UnityEngine;
using System.Collections;

public class UI_Element : MonoBehaviour {
	
	public Vector3 screenPosition;

	Vector3 worldPointPosition;


	void SetUpElement(){

		screenPosition.x = 10;
		screenPosition.y = Screen.height;
		worldPointPosition = Camera.main.ScreenToWorldPoint(screenPosition);



	}




	void UpdateElementPosition(){

		transform.position = worldPointPosition;



	}








	// Use this for initialization
	void Start () {

		SetUpElement();
	
	}
	
	// Update is called once per frame
	void Update () {

		UpdateElementPosition();
	
	}
}

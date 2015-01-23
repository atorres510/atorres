using UnityEngine;
using System.Collections;

public class UI_Element : MonoBehaviour {

	public float xRatio;
	public float yRatio;


	Vector3 screenPosition;
	Vector3 worldPointPosition;

	Vector3 oldCameraPosition;



	public void SetUpElement(){

		screenPosition.x = Screen.width * xRatio;
		screenPosition.y = Screen.height * yRatio;
		screenPosition.z = 1;
		worldPointPosition = Camera.main.ScreenToWorldPoint(screenPosition);
		transform.position = worldPointPosition;
	


	}




	public void UpdateElementPosition(){

		Vector3 dPosition = Camera.main.transform.position - oldCameraPosition;

		transform.position += dPosition;

		//transform.position += Camera.main.transform.position - oldCameraPosition;

		oldCameraPosition = Camera.main.transform.position;


	}






	// Use this for initialization
	void Start () {

		oldCameraPosition = Camera.main.transform.position;
		SetUpElement();
	
	}
	
	// Update is called once per frame
	void Update () {

		UpdateElementPosition();
	
	}
}

using UnityEngine;
using System.Collections;

public class CoinBehaviour : MonoBehaviour {

	GameObject gameObjectManager;
	GameManager gameManager;

	
	void Awake () {


		gameObjectManager = GameObject.FindGameObjectWithTag ("GameManager");
		gameManager = gameObjectManager.GetComponent<GameManager> ();



	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			Debug.Log ("Coin Get!");
			gameManager.SendMessage ("IncreaseScore", 10, SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
		}
	}
}

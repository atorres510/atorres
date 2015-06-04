using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public int sceneToLoad;
	public Fading fade;
	private bool isColliding = false;
	private GameObject player;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");

	}

	void OnMouseOver()
	{
		if(Input.GetMouseButtonUp(0) && isColliding)
		{
			fade.BeginFadeToBlack(sceneToLoad);
		}
	}

	void OnTriggerStay2D(Collider2D bcoll)
	{
		if(bcoll.gameObject == player)
		{
			isColliding = true;
			Debug.Log ("WE HAVE COLLIDED.");
		}
	}

	void OnTriggerExit2D(Collider2D bcoll)
	{
		if(bcoll.gameObject == player)
		{
			isColliding = false;
			Debug.Log("NO LONGER COLLIDING.");
		}
	}


}

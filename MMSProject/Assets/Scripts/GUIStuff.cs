using UnityEngine;
using System.Collections;

public class GUIStuff : MonoBehaviour {
	public Texture2D cursorNormalTexture;
	// Use this for initialization
	void Awake () {
		Object.DontDestroyOnLoad(gameObject);
	}

	void Start () {
		Cursor.SetCursor(cursorNormalTexture, Vector2.zero, CursorMode.Auto);
	}
		// Update is called once per frame
	void Update () {

	}
}

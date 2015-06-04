using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour
{
	public Texture2D fadingTexture;
	public float fadeSpeed = 0.8f;

	private int drawDepth = -1000;
	private float alpha = 1.0f;
	private int fadeDir = -1;
	private int levelToLoad = 0;

	void OnGUI()
	{
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01(alpha);

		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadingTexture);
	}

	public void BeginFadeFromBlack()
	{
		fadeDir = -1;

		//return(fadeSpeed);
	}

	public void BeginFadeToBlack(int newLevelToLoad)
	{
		fadeDir = 1;

		levelToLoad = newLevelToLoad;
		StartCoroutine("Load");
		//return(fadeSpeed);
	}

	IEnumerator Load()
	{
		yield return new WaitForSeconds(1.0f);
		Application.LoadLevel(levelToLoad);
	}

	void OnLevelWasLoaded()
	{
		BeginFadeFromBlack();
	}

	public void QuitProgram()
	{
		Application.Quit();
	}
}

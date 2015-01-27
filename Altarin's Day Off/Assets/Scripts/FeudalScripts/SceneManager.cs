using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	
	public void LoadScene(int sceneNumber){
		
		Application.LoadLevel (sceneNumber);
		
	}


	public void Quitgame(){
		
		Application.Quit ();
		
	}
}

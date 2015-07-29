using UnityEngine;
using System.Collections;

public class TrayBehaviour : MonoBehaviour {

	public f_Tile[] slots;

	void Start(){

		slots = GetComponentsInChildren<f_Tile> ();


	}

}

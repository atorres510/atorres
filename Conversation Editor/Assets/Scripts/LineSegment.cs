using UnityEngine;
using System.Collections;

public class LineSegment : MonoBehaviour {

	LineRenderer lineRenderer;
	public TextBox origin;
	public TextBox destination;




	public void InstantiateLineSegment(float x1, float y1, float x2, float y2){

		Vector3 pos1 = new Vector3 (x1, y1, 1.0f);
		Vector3 pos2 = new Vector3 (x2, y2, 1.0f);

		Debug.Log(pos1 + ", " + pos2);

		lineRenderer.SetWidth (0.2f, 0.2f);
		lineRenderer.SetPosition (0, pos1);
		lineRenderer.SetPosition (1, pos2);



	}










	void Awake(){

		lineRenderer = gameObject.GetComponent<LineRenderer> ();
	
	}



	void Update () {
	
	}
}

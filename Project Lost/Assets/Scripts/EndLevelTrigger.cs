using UnityEngine;
using System.Collections;

public class EndLevelTrigger : ITrigger {
	

	//triggers when the player reaches the end of the level
	void OnTriggerEnter2D(Collider2D col){

		if (col.tag == "Sally") {
				
			IsTriggered = true;

		}

	}

}

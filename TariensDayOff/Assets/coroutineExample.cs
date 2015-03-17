using UnityEngine;
using System.Collections;

public class coroutineExample : MonoBehaviour {
	
	public int number_to_print=1;

	void Start () {
		//StartCoroutine("test");
		//StartCoroutine("wait_then_test");
		StartCoroutine (coroutine1());
		StopCoroutine ("coroutine2");
	}


	IEnumerator coroutine1(){

		Debug.Log ("Started coroutine1");
		yield return StartCoroutine (coroutine2());
		Debug.Log ("coroutine1 finished");
	
	}





	IEnumerator coroutine2(){

		Debug.Log ("started coroutine2");

		while (true) {
				
			yield return 0;
		}
		//yield break;
		Debug.Log ("finished coroutine2");
		yield return 0;
	
	
	}
	
	IEnumerator test(){
		StopCoroutine("test");
		//StopAllCoroutines ();
		StopCoroutine ("wait_then_test");
		int number_to_print_in_test=number_to_print;
		while(true){
			Debug.Log (number_to_print_in_test);
			yield return 0;
		}
	}



	IEnumerator wait_then_test(){
		yield return new WaitForSeconds(5);
		yield return 0;
		Debug.Log ("created 2nd instance of test");
		number_to_print++;
		StartCoroutine("test");
	}

}

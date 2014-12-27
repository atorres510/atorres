using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	GameObject player;
	PlayerController2D playerController2D;

	GameObject[] coins;
	int numberOfCoins;

	public int score;

	//Variables to determine game outcome
	bool gameOver = false;
	bool gameWon = false;



	
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerController2D = player.GetComponent<PlayerController2D> ();
		Time.timeScale = 1.0f;
		
	}
	
	
	void Start(){
		Time.timeScale = 1.0f;
		
		
	}

	

	void Update () {
		//Counts number of coins still in play and checks to see if win condition has been met
		coins = GameObject.FindGameObjectsWithTag ("Coin");
		numberOfCoins = coins.Length;
		//Debug.Log ("There are " + numberOfCoins + "Coins left!");
		
		if (numberOfCoins == 0) {
			GameWon();	
		}
		
	}

	//Member Functions of GameManager//


	//Keeps track of score
	void IncreaseScore(int scorePoints){

		score = score + scorePoints;  
	
	}

	//Condition if player touches an enemy
	void GameOver(){

		if (!gameWon) {

			Destroy (player);
			gameOver = true;
			Debug.Log ("GAME OVER");
		}

	}

	//Condition if all coins are collected
	void GameWon(){

		if (!gameOver) {
			playerController2D.enabled = false; 
			gameWon = true;
			Time.timeScale = 0.0f; //not working?
			Debug.Log ("YOU WON");
		}
	}

	void Restart(){

		Application.LoadLevel(0);


	}

	void OnGUI(){
		//Score Display
		GUI.TextField (new Rect (400, 0, 80, 30), "Score: " + score.ToString());
		//Losing Display
		if (gameOver) {
			GUI.TextField (new Rect (400, 250, 100, 30), "GAME OVER");

			if(GUI.Button(new Rect (400, 290, 100, 30), "Retry?")){
			
				Restart ();
			}
				
		}
		//Winning Display
		if (gameWon) {
			GUI.TextField (new Rect (400, 250, 100, 30), "YOU WIN");
			if(GUI.Button(new Rect (400, 290, 100, 30), "New Game?")){
				//Time.timeScale = Time.realtimeSinceStartup;
				Restart ();
			}
		
		}


	
	}






}

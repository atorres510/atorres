using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {


    private GameObject player;
    private PlayerController2D playerController;





    void Awake(){

        player = GameObject.FindGameObjectWithTag("Player");
        //ShadowstepController();

    }

    // Update is called once per frame
    void Update () {
		
	}

    #region Shadowstep

    void ShadowstepController() {

        //SpriteRenderer playerRenderer = player.GetComponent<SpriteRenderer>();

        //playerRenderer.color = new Color(0.24f, 0.24f, 0.24f, 0.8f);


    }














    #endregion 








}

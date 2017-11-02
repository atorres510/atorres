using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

    public GameObject playerGhostPrefab;


    private GameObject player;
    private PlayerController2D playerController;
    private BoxCollider2D playerCollider;
    private Rigidbody2D playerRigidbody;

    private bool isShadowstepping;






    void Awake(){

        InitializePlayerandComponents();
        isShadowstepping = false;
        

    }

    // Update is called once per frame
    void Update () {

        ShadowstepController();
		
	}

    void InitializePlayerandComponents() {

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController2D>();
        playerCollider = player.GetComponent<BoxCollider2D>();
        playerRigidbody = player.GetComponent<Rigidbody2D>();

    }

    void TogglePlayerComponents(bool isEnabled)
    {

        playerController.enabled = isEnabled;
        playerCollider.enabled = isEnabled;
        


    }





    #region Shadowstep

    void ShadowstepController() {


        if (Input.GetKey(KeyCode.F) && !isShadowstepping) {

            isShadowstepping = true;

            TogglePlayerComponents(false);

            GameObject ghostClone = (GameObject) Instantiate(playerGhostPrefab, player.transform.position, player.transform.rotation);




        }
        

        //SpriteRenderer playerRenderer = player.GetComponent<SpriteRenderer>();

        //playerRenderer.color = new Color(0.24f, 0.24f, 0.24f, 0.8f);


    }

   














    #endregion 








}

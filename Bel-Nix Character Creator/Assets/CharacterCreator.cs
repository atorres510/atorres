﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CharacterCreator : MonoBehaviour {

    public SpriteLibrary spriteLibrary;
    public Renderer activeRenderer;
    public GameObject gridObject;
    public Sprite blankUISprite;
    Button[] buttonGrid;
    int currentbuttonGridLength;

    public GameObject paperDoll;
    Image[] paperDollLayers;

    int activeFeature; // 0 body, 1 long shirt, 2 shirt, 3 vest, 4 hair


    bool isBoy = true;
    int bodyType = 0; // 0 fit, 1 chubby, 2 fat
   
    Renderer vestRenderer;
    Renderer shirtRenderer;
    Renderer longShirtRenderer;
    Renderer accessoryRenderer;

    public ColorPicker picker;



    public void SetSex() {

        isBoy = !isBoy;

    }


    public void SetActiveFeature(int caseSwitch) {

        switch (caseSwitch) {
            case 1: //BodyTypes
                activeFeature = 0;

                if (isBoy)
                {
                    FillButtons("MALE");
                }
                else
                {
                    FillButtons("FEMALE");
                }
                break;
            case 2: //hair
                activeFeature = 4;
                FillButtons("HAIRTYPES");
                break;
            case 3: //clothes
                
                break;
            case 4: //accessory
                
                break;

        }

        picker.CurrentColor = paperDollLayers[activeFeature].color;

    }

    


    #region Paperdoll Methods

    void SetPaperDollLayers() {

        paperDollLayers = paperDoll.transform.GetComponentsInChildren<Image>();

        for (int i = 0; i < paperDollLayers.Length; i++) {

            Debug.Log(paperDollLayers[i].gameObject.name);

        }

        paperDollLayers[1].gameObject.SetActive(false);
        paperDollLayers[3].gameObject.SetActive(false);

    }

    public void SetFeaturetoPaperDoll(Button button)
    {

        Sprite newSprite;
        Image buttonImage = button.transform.GetChild(0).GetComponent<Image>();

        newSprite = buttonImage.sprite;

        paperDollLayers[activeFeature].sprite = newSprite;

        switch (activeFeature) {

            case 0: //body

                for (int i = 0; i < buttonGrid.Length; i++)
                {
                    
                    if (buttonGrid[i] == button) {

                        bodyType = i;
                        break;

                    }
                    
                }
                
                break;

            



        }
        
    }

    public void UpdateActiveFeatureColor() {

        paperDollLayers[activeFeature].color = activeRenderer.material.color;

    }

    void UpdatePaperDoll() {


        if (isBoy)
        {


            paperDollLayers[0].sprite = spriteLibrary.GetSprite("MALE", bodyType);

            switch (bodyType) {

                case 0: //fit
                    paperDollLayers[1].sprite = spriteLibrary.GetSprite("01_M_FitClothes".ToUpper(), 0);  //long
                    paperDollLayers[2].sprite = spriteLibrary.GetSprite("01_M_FitClothes".ToUpper(), 1);  //short
                    paperDollLayers[3].sprite = spriteLibrary.GetSprite("01_M_FitClothes".ToUpper(), 2);  //vest
                    break;
                case 1: //chubs
                    paperDollLayers[1].sprite = spriteLibrary.GetSprite("02_M_ChubbyClothes".ToUpper(), 0);
                    paperDollLayers[2].sprite = spriteLibrary.GetSprite("02_M_ChubbyClothes".ToUpper(), 1);
                    paperDollLayers[3].sprite = spriteLibrary.GetSprite("02_M_ChubbyClothes".ToUpper(), 2);
                    break;
                case 2: //fat
                    paperDollLayers[1].sprite = spriteLibrary.GetSprite("03_M_FatClothes".ToUpper(), 0);
                    paperDollLayers[2].sprite = spriteLibrary.GetSprite("03_M_FatClothes".ToUpper(), 1);
                    paperDollLayers[3].sprite = spriteLibrary.GetSprite("03_M_FatClothes".ToUpper(), 2);
                    break;

            }
            
        }

        else {

            paperDollLayers[0].sprite = spriteLibrary.GetSprite("FEMALE", bodyType);


            switch (bodyType)
            {

                case 0: //fit
                    paperDollLayers[1].sprite = spriteLibrary.GetSprite("01_F_FitClothes".ToUpper(), 0);  //long
                    paperDollLayers[2].sprite = spriteLibrary.GetSprite("01_F_FitClothes".ToUpper(), 1);  //short
                    paperDollLayers[3].sprite = spriteLibrary.GetSprite("01_F_FitClothes".ToUpper(), 2);  //vest
                    break;
                case 1: //chubs
                    paperDollLayers[1].sprite = spriteLibrary.GetSprite("02_F_ChubbyClothes".ToUpper(), 0);  //long
                    paperDollLayers[2].sprite = spriteLibrary.GetSprite("02_F_ChubbyClothes".ToUpper(), 1);  //short
                    paperDollLayers[3].sprite = spriteLibrary.GetSprite("02_F_ChubbyClothes".ToUpper(), 2);  //vest
                    break;
                case 2: //fat
                    paperDollLayers[1].sprite = spriteLibrary.GetSprite("03_F_FatClothes".ToUpper(), 0);  //long
                    paperDollLayers[2].sprite = spriteLibrary.GetSprite("03_F_FatClothes".ToUpper(), 1);  //short
                    paperDollLayers[3].sprite = spriteLibrary.GetSprite("03_F_FatClothes".ToUpper(), 2);  //vest
                    break;

            }

        }






    }



    #endregion
    
    #region Button Grid Methods

    void SetButtons()
    {

        buttonGrid = gridObject.transform.GetComponentsInChildren<Button>();

    }

    void FillButtons(string filePath) {
        
        Sprite[] sprites = spriteLibrary.GetSprites(filePath);

        if (currentbuttonGridLength < sprites.Length) {

            for (int i = currentbuttonGridLength; i < sprites.Length; i++) {

                buttonGrid[i].gameObject.SetActive(true);

            }

        }

        for (int i = 0; i < sprites.Length; i++)
        {

            //set sprite
            Image buttonImage = buttonGrid[i].transform.GetChild(0).GetComponent<Image>();
            
            buttonImage.sprite = sprites[i];

        }

        for (int i = sprites.Length; i < 15; i++)
        {

            //set sprite
            /*Image buttonImage = buttonGrid[i].transform.GetChild(0).GetComponent<Image>();

            buttonImage.sprite = blankUISprite;
            buttonImage.color = Color.white;*/
            buttonGrid[i].gameObject.SetActive(false);

        }

        currentbuttonGridLength = sprites.Length;

        
    }

    void UpdateButtonColor() {

        for (int i = 0; i < currentbuttonGridLength; i++)
        {

            //set sprite
            Image buttonImage = buttonGrid[i].transform.GetChild(0).GetComponent<Image>();

            buttonImage.color = activeRenderer.material.color;

        }


    }



    #endregion

    public void SetActiveColor(Button button) {

        Image buttonImage = button.GetComponent<Image>();

        picker.CurrentColor = buttonImage.color;
        
    }


    // Use this for initialization
    void Start () {

        SetButtons();

        SetPaperDollLayers();

        for (int i = 0; i < buttonGrid.Length; i++) {

            buttonGrid[i].gameObject.SetActive(false);

        }

        picker.CurrentColor = Color.white;

        picker.onValueChanged.AddListener(color =>
        {
            activeRenderer.material.color = color;
        });
    }
	
	// Update is called once per frame
	void Update () {

        UpdateButtonColor();
        UpdateActiveFeatureColor();
        UpdatePaperDoll();
	
	}
}

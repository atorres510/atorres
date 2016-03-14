using UnityEngine;
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

    public int activeFeature; // 0 body, 1 long shirt, 2 shirt, 3 vest, 4 hair, 5 general clothing


    bool isBoy = true;
    int bodyType = 0; // 0 fit, 1 chubby, 2 fat
    int hairType = 0; 

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
                

                for (int i = 0; i < buttonGrid.Length; i++)
                {

                    if (i == bodyType)
                    {
                        
                        buttonGrid[i].image.color = buttonGrid[i].colors.pressedColor;
                    }

                    else {

                        buttonGrid[i].image.color = buttonGrid[i].colors.normalColor;

                    }



                }

                break;


            case 2: //hair
                activeFeature = 4;
                FillButtons("HAIRTYPES");
                for (int i = 0; i < buttonGrid.Length; i++)
                {

                    if (i == hairType)
                    {

                        buttonGrid[i].image.color = buttonGrid[i].colors.pressedColor;
                    }

                    else {

                        buttonGrid[i].image.color = buttonGrid[i].colors.normalColor;

                    }



                }
                break;
            case 3: //clothes
                activeFeature = 5; //no feature selected;
                if (isBoy)
                {

                    switch (bodyType)
                    {
                        case 0: //fit
                            FillButtons("01_M_FitClothes".ToUpper());
                            break;
                        case 1: //chubs
                            FillButtons("02_M_ChubbyClothes".ToUpper());
                            break;
                        case 2: //fat
                            FillButtons("03_M_FatClothes".ToUpper());
                            break;

                    }

                }

                else
                {

                    switch (bodyType)
                    {

                        case 0: //fit
                            FillButtons("01_F_FitClothes".ToUpper());
                            break;
                        case 1: //chubs
                            FillButtons("02_F_ChubbyClothes".ToUpper());
                            break;
                        case 2: //fat
                            FillButtons("03_F_FatClothes".ToUpper());
                            break;

                    }

                }

                for (int i = 1; i < 4; i++) {

                    if (paperDollLayers[i].gameObject.activeSelf)
                    {

                        buttonGrid[i - 1].image.color = buttonGrid[i - 1].colors.pressedColor;

                    }

                    else {

                        buttonGrid[i - 1].image.color = buttonGrid[i - 1].colors.normalColor;
                    }

                }

              


                break;
            case 4: //accessory
                
                break;

        }

        if (activeFeature < paperDollLayers.Length) {
            picker.CurrentColor = paperDollLayers[activeFeature].color;
        }

        
        

    }

    public void SetActiveColor(Button button)
    {

        Image buttonImage = button.GetComponent<Image>();

        picker.CurrentColor = buttonImage.color;

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

        if (activeFeature == 5 || activeFeature == 1 || activeFeature == 2 || activeFeature == 3)
        {

            for (int i = 0; i < buttonGrid.Length; i++)
            {

                if (buttonGrid[i] == button)
                {

                    if (activeFeature == (i + 1))
                    {

                        paperDollLayers[activeFeature].gameObject.SetActive(!paperDollLayers[activeFeature].gameObject.activeSelf);

                        if (paperDollLayers[activeFeature].gameObject.activeSelf)
                        {

                            button.image.color = button.colors.pressedColor;

                        }

                        else {

                            button.image.color = button.colors.normalColor;
                        }

                    }

                    else {

                        
                        activeFeature = (i + 1);
                        if (paperDollLayers[activeFeature].gameObject.activeSelf)
                        {




                        }

                        else {

                            paperDollLayers[activeFeature].gameObject.SetActive(!paperDollLayers[activeFeature].gameObject.activeSelf);
                            if (paperDollLayers[activeFeature].gameObject.activeSelf)
                            {

                                button.image.color = button.colors.pressedColor;

                            }

                            else {

                                button.image.color = button.colors.normalColor;
                            }

                        }
                        

                    }
                    
                    Debug.Log(activeFeature);
                    break;
                }

            }

           
            picker.CurrentColor = paperDollLayers[activeFeature].color;

        }


        else {

            Sprite newSprite;
            Image buttonImage = button.transform.GetChild(0).GetComponent<Image>();

            newSprite = buttonImage.sprite;

            paperDollLayers[activeFeature].sprite = newSprite;

            switch (activeFeature)
            {

                case 0: //body

                    for (int i = 0; i < buttonGrid.Length; i++)
                    {

                        if (buttonGrid[i] == button)
                        {
                            bodyType = i;
                            button.image.color = button.colors.pressedColor;
                        }

                        else {

                            buttonGrid[i].image.color = buttonGrid[i].colors.normalColor;

                        }

                        

                    }

                    break;

                //clothes 
                case 1: //longshirt
                case 2: //shortshirt
                case 3: //vest
                    
                    break;


                case 4:
                    for (int i = 0; i < buttonGrid.Length; i++)
                    {

                        if (buttonGrid[i] == button)
                        {
                            hairType = i;
                            button.image.color = button.colors.pressedColor;
                        }

                        else {

                            buttonGrid[i].image.color = buttonGrid[i].colors.normalColor;

                        }



                    }

                    break;

            }

        }
 
    }

    public void UpdateActiveFeatureColor() {


        if (activeFeature < paperDollLayers.Length) {

            paperDollLayers[activeFeature].color = activeRenderer.material.color;

        }
        

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

        if (activeFeature == 0 || activeFeature == 4)
        { //body and hair colors

            for (int i = 0; i < currentbuttonGridLength; i++)
            {

                //set sprite
                Image buttonImage = buttonGrid[i].transform.GetChild(0).GetComponent<Image>();

                buttonImage.color = activeRenderer.material.color;

            }

        }

        else if (activeFeature == 1 || activeFeature == 2 || activeFeature == 3)
        {

            Image buttonImage = buttonGrid[(activeFeature - 1)].transform.GetChild(0).GetComponent<Image>();

            buttonImage.color = activeRenderer.material.color;

        }

        else if (activeFeature == 5){

            for (int i = 0; i < 3; i++) {

                Image buttonImage = buttonGrid[i].transform.GetChild(0).GetComponent<Image>();

                buttonImage.color = paperDollLayers[(i+1)].color;


            }

        }


    }



    #endregion

    #region PNG Export Methods

    #endregion

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

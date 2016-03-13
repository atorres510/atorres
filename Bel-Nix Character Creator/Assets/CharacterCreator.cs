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

    string activeFeature = " ";


    bool isBoy;
   
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
                activeFeature = "BodyType";

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
                activeFeature = "HairType";
                FillButtons("HAIRTYPES");
                break;
            case 3: //clothes
                
                break;
            case 4: //accessory
                
                break;

        }

    }


    #region Paperdoll Methods

    void SetPaperDollLayers() {

        paperDollLayers = paperDoll.transform.GetComponentsInChildren<Image>();

        for (int i = 0; i < paperDollLayers.Length; i++) {

            Debug.Log(paperDollLayers[i].gameObject.name);

        }
        
    }






    #endregion




    #region Button Grid Methods

    void SetButtons()
    {

        buttonGrid = gridObject.GetComponentsInChildren<Button>();

    }

    void FillButtons(string filePath) {

      
     
        Sprite[] sprites = spriteLibrary.GetSprites(filePath);

        for (int i = 0; i < sprites.Length; i++)
        {

            //set sprite
            Image buttonImage = buttonGrid[i].transform.GetChild(0).GetComponent<Image>();
            
            buttonImage.sprite = sprites[i];

        }

        for (int i = sprites.Length; i < 15; i++)
        {

            //set sprite
            Image buttonImage = buttonGrid[i].transform.GetChild(0).GetComponent<Image>();

            buttonImage.sprite = blankUISprite;
            buttonImage.color = Color.white;

        }

        currentbuttonGridLength = sprites.Length;
        
    }

    void UpdateButtonColor() {

        if (activeFeature == " ")
        {


        }


        else {

            for (int i = 0; i < currentbuttonGridLength; i++)
            {

                //set sprite
                Image buttonImage = buttonGrid[i].transform.GetChild(0).GetComponent<Image>();

                buttonImage.color = activeRenderer.material.color;

            }

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

        picker.onValueChanged.AddListener(color =>
        {
            activeRenderer.material.color = color;
        });
    }
	
	// Update is called once per frame
	void Update () {

        UpdateButtonColor();
	
	}
}

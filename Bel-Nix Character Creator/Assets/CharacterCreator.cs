using UnityEngine;
using System.Collections;

public class CharacterCreator : MonoBehaviour {


    public Renderer activeRenderer;


    Renderer skinRenderer;
    Renderer hairRenderer;
    Renderer vestRenderer;
    Renderer shirtRenderer;
    Renderer longShirtRenderer;
    Renderer accessoryRenderer;

    public ColorPicker picker;




    public void SetActiveRenderer(int caseSwitch) {

        switch (caseSwitch) {
            case 1: //skin
                activeRenderer = skinRenderer;
                break;
            case 2: //hair
                activeRenderer = hairRenderer;
                break;
            case 3: //clothes
                activeRenderer = shirtRenderer;
                break;
            case 4: //accessory
                activeRenderer = accessoryRenderer;
                break;

        }



    }








	// Use this for initialization
	void Start () {

        picker.onValueChanged.AddListener(color =>
        {
            activeRenderer.material.color = color;
        });
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

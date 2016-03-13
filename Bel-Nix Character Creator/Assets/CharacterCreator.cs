using UnityEngine;
using System.Collections;

public class CharacterCreator : MonoBehaviour {


    public Renderer activeRenderer;


    public Renderer skinRenderer;
    public Renderer hairRenderer;
    public Renderer vestRenderer;
    public Renderer shirtRenderer;
    public Renderer longShirtRenderer;
    public Renderer accessoryRenderer;

    public ColorPicker picker;













	// Use this for initialization
	void Start () {

        picker.onValueChanged.AddListener(color =>
        {
            //renderer.material.color = color;
        });
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomGridBrush(false, true, false, "DoorBrush")]
public class DoorBrush : GridBrushBase{

#if UNITY_EDITOR
    [MenuItem("Assets/Create/CustomAssets/DoorBrush", false, 0)]

    //this function is called when you click the menu entry
    private static void CreateDoorBrush() {

        string fileName = "Doorbrush";
        DoorBrush mytb = new DoorBrush();
        mytb.name = fileName + ".asset";
        AssetDatabase.CreateAsset(mytb, "Assets/CustomAssets/Brushes/" + mytb.name + "");


    }


#endif  

}

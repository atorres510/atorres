using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class DoorTile : Tile {

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {

        Transform goTransform = go.GetComponent<Transform>();

        Vector3 goPos = goTransform.position;

        Debug.Log(goPos);
        



        return base.StartUp(position, tilemap, go);
    }







#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/DoorTile")]
    public static void CreateDoorTile() {

        string path = EditorUtility.SaveFilePanelInProject("Save DoorTile", "New DoorTile", "asset", "Save doortile", "Assets");
        if (path == "") {

            return;

        }

        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<DoorTile>(), path);
    }






#endif






}

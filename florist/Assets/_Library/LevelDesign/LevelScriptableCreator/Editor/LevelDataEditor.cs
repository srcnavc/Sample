using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        
        if (GUILayout.Button("CreateAsset"))
        {
            string path = AssetDatabase.GetAssetPath((target as LevelData));
            path = path.Replace("Assets", "");
            path= path.Replace(".asset", "");
            path = path.Replace(target.name, "");


            GameObject masterObject =  (target as LevelData).RecoverObject(null, new Vector3(0, 0, 0));
            BlockSaver bs =  masterObject.AddComponent<BlockSaver>();
            bs.BlockDir =  path;
            bs.BlockName = target.name;

        }

        base.OnInspectorGUI();
    }
}



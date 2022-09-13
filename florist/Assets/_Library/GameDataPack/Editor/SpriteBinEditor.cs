using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(SpriteBin))]
public class SpriteBinEditor : Editor
{
    Sprite sprite;
    public override void OnInspectorGUI()
    {
  
        SpriteBin bin = target as SpriteBin;
        EditorUtility.SetDirty(bin);
        for (int i = 0; i < bin.Sprites.Count; i++)
        {
            string sname = "New Sprite";
            if (bin.Sprites[i] != null)
                sname = bin.Sprites[i].name;
            GUILayout.BeginHorizontal();
            bin.Sprites[i] = (Sprite)EditorGUILayout.ObjectField(sname, bin.Sprites[i], typeof(Sprite), allowSceneObjects: true);
            if (GUILayout.Button("Delete",GUILayout.Width(64),GUILayout.Height(64)))
            {
                
                bin.Sprites.RemoveAt(i);

            }
            GUILayout.EndHorizontal();
        }
        if(GUILayout.Button("ADD SPRITE"))
        {
          
            bin.Sprites.Add(null);
            
        }


    }
}

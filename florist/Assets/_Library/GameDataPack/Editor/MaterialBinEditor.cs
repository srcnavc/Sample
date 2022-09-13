using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(MaterialBin))]
public class MaterialBinEditor : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {

        MaterialBin bin = target as MaterialBin;
        EditorUtility.SetDirty(bin);
        if (bin.materials == null)
            bin.materials = new List<Material>();

        for (int i = 0; i < bin.materials.Count; i++)
        {
            string sname = "New Material";
            if (bin.materials[i] != null)
                sname = bin.materials[i].name;
         


            EditorGUILayout.BeginHorizontal();
            bin.materials[i] = drawMarterialField(bin.materials[i]); //(Material)EditorGUILayout.ObjectField(new GUIContent(" An Object: ", AssetPreview.GetAssetPreview(bin.materials[i])),
             //   bin.materials[i], typeof(Material), true);
            if (GUILayout.Button("Delete", GUILayout.Width(64), GUILayout.Height(64)))
            {
               
                bin.materials.RemoveAt(i);

            }
            EditorGUILayout.EndHorizontal();
        }
        if (GUILayout.Button("ADD Material"))
        {
      
            bin.materials.Add(null);

        }

        

    }
    public Material drawMarterialField(Material mat)
    {
        EditorGUILayout.LabelField("", GUILayout.Width(16)); // This controls how far the text is accross
        Rect rt = GUILayoutUtility.GetLastRect();
        String matName = "New Material";
        if (mat != null)
        {
            Texture2D preview = AssetPreview.GetAssetPreview(mat);
            if(preview!=null)
            EditorGUI.DrawPreviewTexture(new Rect(rt.x + 100, rt.y, 64, 64), preview);
            matName = mat.name;
        }
        

        mat = EditorGUILayout.ObjectField(matName, mat, typeof(Material), true) as Material;
        return mat;
    }
}

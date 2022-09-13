using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemLevelGenerator))]
public class ItemLevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ItemLevelGenerator generator = (ItemLevelGenerator)target;

        if (GUILayout.Button("Generate"))
        {
            generator.Create();
        }

    }
}

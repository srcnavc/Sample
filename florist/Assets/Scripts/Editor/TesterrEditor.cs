using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Testerr))]
public class TesterrEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Testerr generator = (Testerr)target;

        if (GUILayout.Button("Generate"))
        {
            generator.Create();
        }

    }
}

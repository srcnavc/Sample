using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ConfigurableJointSetup))]
public class ConfigurableJointSetupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ConfigurableJointSetup CJS = (ConfigurableJointSetup)target;

        base.OnInspectorGUI();
        if (GUILayout.Button("Yabstir"))
        {

            CJS.AssignJoints(CJS.CopyCatRoot, CJS.OriginalRoot);



        }
        if (GUILayout.Button("ResetAng"))
        {

         //   CJS.set(CJS.CopyCatRoot, CJS.OriginalRoot);



        }

    }


}

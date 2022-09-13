using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TutorialCanvas)), CanEditMultipleObjects]

public class StartCanvasEditor : Editor
    {
        // Start is called before the first frame update
        public override void OnInspectorGUI()
        {/*
        StartScreenCanvas canvas = (target as StartScreenCanvas);
        EditorUtility.SetDirty(canvas);
        DragInfoPannelType oldType = canvas.panelType;

        canvas.panelType = (DragInfoPannelType)EditorGUILayout.EnumPopup("DragType" ,canvas.panelType);

        if (canvas.panelType != oldType)
            canvas.Refresh();*/
        base.OnInspectorGUI();
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}

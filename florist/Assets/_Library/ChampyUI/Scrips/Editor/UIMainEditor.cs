using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UIMain))]
public class UIMainEditor : Editor
{/*
    public override void OnInspectorGUI()
    {
        UIMain main = (target as UIMain);

        GameSettings settings = null;
        EditorUtility.SetDirty(main);
       
        if (main.UIAdapter != null)
        {
            settings = main.adapter.getGameSettings();
            if (settings != null)
            {
                EditorUtility.SetDirty(settings);
                if (main.UIAdapter.GetComponent<IUserInterfaceAdapter>() == null)
                {

                    EditorGUILayout.HelpBox("UIAdapter Has to be extended from IUserInterfaceAdapter", MessageType.Error);
                    GUI.color = Color.red;

                }

                bool debugModeOld = main.DebugMode;
                main.DebugMode = EditorGUILayout.Toggle("Debug Mode:", main.DebugMode);

                bool VideoMode_Old = main.VideoMode;
                main.VideoMode = EditorGUILayout.Toggle("Video Mode:", main.VideoMode);


                DragInfoPannelType oldDragType = settings.DragType;
                settings.DragType = (DragInfoPannelType)EditorGUILayout.EnumPopup("DragType", settings.DragType);


        

                WinCanvasPannelType oldwinLooseType = settings.WinLooseType;
                settings.WinLooseType = (WinCanvasPannelType)EditorGUILayout.EnumPopup("Win&Loose Type", settings.WinLooseType);

              

                settings.ShowMidPannelOnLoose = EditorGUILayout.Toggle("Show On Loose:", settings.ShowMidPannelOnLoose);
                settings.progressBarType = (ProgressBarType)EditorGUILayout.EnumPopup("InGame Progress Type", settings.progressBarType);
                settings.ShowLevelNumbers = EditorGUILayout.Toggle("Show Level Numbers:", settings.ShowLevelNumbers);
            }
        }
        base.OnInspectorGUI();
    }*/
}

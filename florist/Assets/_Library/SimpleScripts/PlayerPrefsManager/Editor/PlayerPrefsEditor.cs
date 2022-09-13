using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
[CustomEditor(typeof(PlayerPrefsManager))]
public class PlayerPrefsEditor : Editor
{
    int selectedModel = -1;
    public override void OnInspectorGUI()
    {

        PlayerPrefsManager manager = (PlayerPrefsManager)target;
        base.OnInspectorGUI();
          foreach(PrefKey strkey in manager.PreferencesKeys)
          {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(strkey.name);
            switch (strkey.keytype)
            {
                case KeyType.INT:
                    try
                    {
                         PlayerPrefs.SetInt(strkey.name, int.Parse(EditorGUILayout.TextField(PlayerPrefs.GetInt(strkey.name).ToString())));
 
                    }
                    catch(Exception e)
                    { }
                    break;

                case KeyType.FLOAT:
                    try
                    {
                        PlayerPrefs.SetFloat(strkey.name, float.Parse(EditorGUILayout.TextField(PlayerPrefs.GetFloat(strkey.name).ToString())));
                    }
                    catch (Exception e)
                    { }
                    break;

                case KeyType.STRING:
                    try
                    {
                        PlayerPrefs.SetString(strkey.name, EditorGUILayout.TextField(PlayerPrefs.GetString(strkey.name)));
                    }
                    catch (Exception e)
                    { }
                    break;

            }
            EditorGUILayout.EndHorizontal();


        }



        //  CutTrigger CT = (target as CutTrigger);

        if (GUILayout.Button("save"))
        {

            PlayerPrefs.Save();
        }
    }


}


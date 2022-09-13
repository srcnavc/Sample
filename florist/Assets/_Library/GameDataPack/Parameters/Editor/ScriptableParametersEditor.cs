using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScriptableAlertingParameterInt))]
public class ScriptableParametersEditorInt : Editor
{
    Sprite sprite;
    public override void OnInspectorGUI()
    {


        base.OnInspectorGUI();  
        ScriptableAlertingParameterInt parameter = target as ScriptableAlertingParameterInt;
        EditorUtility.SetDirty(parameter);
        parameter.PValue = EditorGUILayout.IntField("Value", parameter.PValue);
    }
    
}

[CustomEditor(typeof(ScriptableAlertingParameterFloat))]
public class ScriptableParametersEditorFloat : Editor
{
    Sprite sprite;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ScriptableAlertingParameterFloat parameter = target as ScriptableAlertingParameterFloat;
        EditorUtility.SetDirty(parameter);
        parameter.PValue = EditorGUILayout.FloatField("Value", parameter.PValue);
    }

}


[CustomEditor(typeof(ScriptableAlertingParameterString))]
public class ScriptableParametersEditorString : Editor
{
    Sprite sprite;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ScriptableAlertingParameterString parameter = target as ScriptableAlertingParameterString;
        EditorUtility.SetDirty(parameter);
        parameter.PValue = EditorGUILayout.TextField("Value", parameter.PValue);
    }

}





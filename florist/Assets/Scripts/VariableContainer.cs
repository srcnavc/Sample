using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Variable Container", menuName = "Scriptable/Variable Container")]
public class VariableContainer : ScriptableObject
{
    public string ListTag;
    [HideInInspector] public string Name;
    public List<Variable> List = new List<Variable>();
    float tempFloat;

    [System.Serializable]
    public class Variable
    {
        [HideInInspector] public string name;
        public string tag;
        public float value;
    }
    public float GetFloat(string _tag)
    {
        tempFloat = GetValue(_tag);
        if (!float.IsNaN(tempFloat))
            return tempFloat;
        else
            throw new System.Exception("Variable Container " + ListTag + " does not contain variable tagged with " + _tag);
    }

    public int GetInt(string _tag)
    {
        tempFloat = GetValue(_tag);
        if (!float.IsNaN(tempFloat))
            return (int)tempFloat;
        else
            throw new System.Exception("Variable Container " + ListTag + " does not contain variable tagged with " + _tag);
    }

    private float GetValue(string _tag)
    {
        for (int i = 0; i < List.Count; i++)
        {
            if (_tag == List[i].tag)
                return List[i].value;
        }

        return float.NaN;
    }

    private void OnValidate()
    {
        Name = ListTag;

        for (int i = 0; i < List.Count; i++)
        {
            List[i].name = List[i].tag + " - " + List[i].value;
        }
    }
}

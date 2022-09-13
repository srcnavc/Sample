using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableAlertingParameterFloat", menuName = "Scriptables/Parameters/ScriptableAlertingParameterFloat")]
public class ScriptableAlertingParameterFloat : ScriptableAlertingParameter<float>
{
    

    public override string toString()
    {
        return PValue.ToString();
    }

    protected override float readPrefs()
    {
        return PlayerPrefs.GetFloat("Parameter_"+name, DefaultValue);
    }

    protected override void SavePrefs()
    {
        PlayerPrefs.SetFloat("Parameter_" + name, PValue);
    }
}

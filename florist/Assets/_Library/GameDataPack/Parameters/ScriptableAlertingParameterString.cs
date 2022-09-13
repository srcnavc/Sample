using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableAlertingParameterString", menuName = "Scriptables/Parameters/ScriptableAlertingParameterString")]
public class ScriptableAlertingParameterString : ScriptableAlertingParameter<string>
{
    public override string toString()
    {
        return PValue.ToString();
    }

    protected override string readPrefs()
    {
        return PlayerPrefs.GetString("Parameter_" + name, DefaultValue);
    }

    protected override void SavePrefs()
    {
        PlayerPrefs.SetString("Parameter_" + name, PValue);
    }


}

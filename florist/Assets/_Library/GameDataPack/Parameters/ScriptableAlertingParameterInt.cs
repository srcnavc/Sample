using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableAlertingParameterInt", menuName = "Scriptables/Parameters/ScriptableAlertingParameterInt")]
public class ScriptableAlertingParameterInt : ScriptableAlertingParameter<int>
{
    public override string toString()
    {
        return PValue.ToString();
    }

    protected override int readPrefs()
    {
        return PlayerPrefs.GetInt("Parameter_" + name, DefaultValue);
    }

    protected override void SavePrefs()
    {
        PlayerPrefs.SetInt("Parameter_" + name, PValue);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScriptableParameterList 
{
    public List<ScriptableParameter> parameters;


    public ScriptableParameter getParameter()
    {
        return null;
    }

    public ScriptableParameter findParameter(String parameterName)
    {

        if (parameters != null)
        {
            foreach (ScriptableParameter param in parameters)
            {
                if (param.name.Equals(parameterName))
                    return param;
            }
        }
       return null;
    }

}

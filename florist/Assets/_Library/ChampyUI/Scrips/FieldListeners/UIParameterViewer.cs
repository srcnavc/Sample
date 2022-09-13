using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIParameterViewer : MonoBehaviour
{
    public string parameterName;
    TextMeshProUGUI UIText;
    ScriptableParameter parameter;

    void Start()
    {
        parameter = GameManager.Instance.getInGameParameters().paramList.findParameter(parameterName);
        UIText = GetComponent<TextMeshProUGUI>();
        parameter.OnParameterUpdate += updateField;
        UIText.text = parameter.toString();
    }
   
    void updateField()
    {
        GetComponent<SimpleSizeTween>()?.StartAnimation(10f, .1f);
        UIText.text = "" + parameter.toString();

    }

}

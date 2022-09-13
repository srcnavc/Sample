using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasTextColorizer : MonoBehaviour
{
    [SerializeField] Gradient ColorBehaviour;

    [SerializeField] float valueMax;
    [SerializeField] TextMeshProUGUI ColorText;
    IParameterHolder<float> data;

    void Start()
    {
    data =  GetComponentInParent<IParameterHolder<float>>();
        data.onValueUpdate += updateData;
        updateData(data.getCurrent());
    }

    // Update is called once per frame
    void updateData(float data)
    {
        ColorText.color = ColorBehaviour.Evaluate(data / valueMax);

    }
}



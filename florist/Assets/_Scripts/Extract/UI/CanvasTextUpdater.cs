using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasTextUpdater : MonoBehaviour
{
    [SerializeField]IParameterHolder<float> valueHolder;
    [SerializeField]TextMeshProUGUI TMPLifeText;
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        valueHolder = GetComponentInParent<IParameterHolder<float>>();
        valueHolder.onValueUpdate += updateValue;
        TMPLifeText.text = valueHolder.getCurrent().ToString();
    }

    public void updateValue(float value)
    {
        TMPLifeText.text = value.ToString();
    }

    private void OnDestroy()
    {
        valueHolder.onValueUpdate -= updateValue;
    }

}

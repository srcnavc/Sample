using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpdateInGameUI : MonoBehaviour
{
    public CurrencySC relatedCurrency;
    [SerializeField] TMP_Text text;
    [SerializeField] int max;

    public void SetMax(int value)
    {
        max = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        relatedCurrency.OnValueChanged += OnValueChanged;
        text.text = relatedCurrency.Value + " / " + max;
    }

    private void OnValueChanged(int value)
    {
        text.text = value + " / " + max;
    }

    private void OnDestroy()
    {
        relatedCurrency.OnValueChanged -= OnValueChanged;
    }
}

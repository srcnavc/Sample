using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLevelGenerator : MonoBehaviour
{
    [SerializeField] BaseLevelSC willBeModified;
    [SerializeField] int levelLimit;
    [Header("Cost Properties")]
    [SerializeField] AnimationCurve costCurve;
    [SerializeField] int costStart;
    [SerializeField] float costCurveMultiplier;
    [Header("Value Properties")]
    [SerializeField] AnimationCurve valueCurve;
    [SerializeField] int valueStart;
    [SerializeField] float valueCurveMultiplier;
    ItemLevelData data;
    float tempFloat;
    public void Create()
    {
        willBeModified.Levels.Clear();
        for (int i = 0; i < levelLimit; i++)
        {
            data = new ItemLevelData();
            data.level = i + 1;
            tempFloat = (float)i / (float)levelLimit;

            data.cost = Mathf.RoundToInt(costCurve.Evaluate(tempFloat) * costCurveMultiplier) + costStart;
            data.value = Mathf.RoundToInt(valueCurve.Evaluate(tempFloat) * valueCurveMultiplier) + valueStart;
            willBeModified.Levels.Add(data);
            willBeModified.Validate();
        }
    }
}

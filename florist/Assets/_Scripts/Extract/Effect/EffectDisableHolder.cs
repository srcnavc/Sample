using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDisableHolder : MonoBehaviour,IEffect
{

    [SerializeField] string EffectTag = "default";
    public string _EffectTag => EffectTag;

    public void doEffect(GameObject target)
    {
        gameObject.SetActive(false);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDealDamageInstant : MonoBehaviour,IEffect
{
    [SerializeField] float Value;
    [SerializeField] string EffectTag = "default";
    [SerializeField] EffectTarget effectTarget;
    public string _EffectTag => EffectTag;
    public void doEffect(GameObject target)
    {
        if (effectTarget == EffectTarget.other)
            target?.GetComponent<ILifeHandler>()?.getDamage(Value);
        else if (effectTarget == EffectTarget.self)
            gameObject.GetComponent<ILifeHandler>()?.getDamage(Value);
        else
        {
            target?.GetComponent<ILifeHandler>()?.getDamage(Value);
            gameObject.GetComponent<ILifeHandler>()?.getDamage(Value);
        }
    }


}

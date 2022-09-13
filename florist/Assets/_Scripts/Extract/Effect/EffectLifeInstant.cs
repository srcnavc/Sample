using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLifeInstant : MonoBehaviour, IEffect
{
    
    [SerializeField] float Value;

    [SerializeField] string EffectTag = "default";
    public string _EffectTag => EffectTag;

    public void doEffect(GameObject target)
    {
        
        target.GetComponent<ILifeHandler>()?.addLife(Value);
    }


}

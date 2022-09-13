using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EffectEvent :MonoBehaviour, IEffect
{


    public string effectTag;
    public string _EffectTag => effectTag;
    public UnityEvent EffectTriggerEvent;

    public void doEffect(GameObject target)
    {

        EffectTriggerEvent?.Invoke();
    }
}

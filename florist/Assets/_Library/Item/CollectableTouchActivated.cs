using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EffectTrigger))]
public class CollectableTouchActivated : ItemTouchActivated
{

    public string EffectTriggerTag = "all";
    EffectTrigger _effectTrigger;
    EffectTrigger effectTrigger
    {
        get { 
        if(_effectTrigger==null)
                _effectTrigger = GetComponent<EffectTrigger>();
            return _effectTrigger;
        }
    }
    private void Start()
    {

    }

    public override void activate(GameObject other)
    {
        effectTrigger.trigger(other, EffectTriggerTag);
    }

    public void setEffectTriggerTag(string tag)
    {

        EffectTriggerTag = tag;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ITargetSelector))]
[RequireComponent(typeof(EffectTrigger))]
public class DurationActivatedEffector : DurationActivated
{
    [SerializeField] string EffectTag;
    ITargetSelector selector;
    EffectTrigger effectTrigger;
    [SerializeField] bool MultipleTargets = false;
    private void Start()
    {
        selector = GetComponent<ITargetSelector>();
        effectTrigger = GetComponent<EffectTrigger>(); 
    }
    public override void activate(GameObject other)
    {
        if (selector != null)
            {
            if (!MultipleTargets)
                doSingleTargetEffect(other);
            else
                doMultipleTargetEffect(other);

            }
        else
            effectTrigger.trigger(gameObject, EffectTag);
        
    }
    public void changeTag(string TLBitti)
    {
        EffectTag = TLBitti;
    }

    public void  doSingleTargetEffect(GameObject Other)
    {
        effectTrigger.trigger(selector.getTarget()?.GetGameObject(), EffectTag);
    }

    public void doMultipleTargetEffect(GameObject Other)
    {
        List<ITarget> targets = selector.getTargets();
        if (targets.Count < 1)
        {
            doSingleTargetEffect(Other);
            return;
        }
        for (int i = 0; i < targets.Count; i++)
        {
            effectTrigger.trigger(targets[i].GetGameObject(), EffectTag);
        }
       
    }


}



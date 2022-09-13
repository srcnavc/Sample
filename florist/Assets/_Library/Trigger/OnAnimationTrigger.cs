using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnAnimationTrigger : MonoBehaviour,ITrigger
{
    public string triggerTag;

    public UnityEvent OnAnimationTriggerEvent;
    public UnityEvent _TriggerEvent => OnAnimationTriggerEvent;

    // Start is called before the first frame update
    public void Trigger(string inTag)
    {
        //Debug.Log("AnimTriggerArrived: " + inTag);
        if (inTag.Equals(triggerTag))
        {
            OnAnimationTriggerEvent.Invoke();
        }
    }
}

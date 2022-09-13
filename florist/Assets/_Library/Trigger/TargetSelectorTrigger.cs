using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Dreamteck.Splines;

public class TargetSelectorTrigger : MonoBehaviour,ITrigger<ITarget>
{
    public float TriggerDistance;
    public UnityEvent<ITarget> Trigger;
    public UnityEvent<ITarget> _TriggerEvent => Trigger;

    ITargetSelector targetSelector;
    ITarget CurrentTarget;
    bool _fireTrigger;
     bool fireTrigger
    {
        get { return _fireTrigger; }
        set {

               if (value!= _fireTrigger)
                {

                  _fireTrigger = value;
                  if (value)
                    {
                        Trigger.Invoke(CurrentTarget);
                    }

                }
        
        }

    }

    private void Start()
    {
        targetSelector = GetComponent<TargetSelector4Spline>();
        targetSelector.targetChanged += OnTargetChanged;


    }

    private void LateUpdate()
    {

        if (CurrentTarget != null)
        {
            fireTrigger = Vector3.Distance(CurrentTarget.getObjectPosition(), transform.position) < TriggerDistance;

        }
        else
            fireTrigger = false;

    }
    void OnTargetChanged(ITarget target)
    {
        CurrentTarget = target;

    }


    private void OnDestroy()
    {
        targetSelector.targetChanged -= OnTargetChanged;
    }


}

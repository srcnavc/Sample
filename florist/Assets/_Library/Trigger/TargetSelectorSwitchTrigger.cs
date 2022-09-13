using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Dreamteck.Splines;


public class TargetSelectorSwitchTrigger : MonoBehaviour,ISwitchTrigger<ITarget>
{
    public float TriggerDistance;
    public UnityEvent<ITarget> Trigger;
    public UnityEvent<ITarget> TriggerOff;
    public UnityEvent<ITarget> _TriggerEvent => Trigger;
    public UnityEvent<ITarget> _TriggerOffEvent => TriggerOff;
    Extract.IGameStateHandler _GameStateHandler;
    Extract.IGameStateHandler GameStateHandler
    {
        get {
            if (_GameStateHandler == null)
                _GameStateHandler = GetComponent<Extract.IGameStateHandler>();
            return _GameStateHandler;
        }
    }

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
                else
                    {
                        TriggerOff.Invoke(CurrentTarget);
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
        if (GameStateHandler.isPlaying)
        {

            if (CurrentTarget != null)
            {
                fireTrigger = Vector3.Distance(CurrentTarget.getObjectPosition(), transform.position) < TriggerDistance;

            }
            else
                fireTrigger = false;
        }

    }
    public void resetTrigger()
    {
        fireTrigger = false;
    }
    void OnTargetChanged(ITarget target)
    {
        CurrentTarget = target;

    }


    private void OnDestroy()
    {
        if(targetSelector!=null)
        targetSelector.targetChanged -= OnTargetChanged;
    }

   

}

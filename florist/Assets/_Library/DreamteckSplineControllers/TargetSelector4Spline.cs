using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Dreamteck.Splines;

public class TargetSelector4Spline : MonoBehaviour,ITargetSelector
{
    [SerializeField] SplineFollower follower;
    [SerializeField] float selectRange;
    [SerializeField] Vector3 OriginOffset;
    [SerializeField] LayerMask TargetLayers;
    public event Action<ITarget> targetChanged;
    public float targetDistance;
    Vector3 combinedTarget;
    Vector3 currentForward;

    List<ITarget> AllTargets;
    ITarget _CurrentTarget;
    IPhysicsCaster Caster;
    ITarget CurrentTarget
    {

        get
        {
            return _CurrentTarget;
        }
        set
        {
            if (value != _CurrentTarget)
            {
                _CurrentTarget = value;
                targetChanged?.Invoke(_CurrentTarget);
            }
     
        }
    }
   

    public ITarget getCurrentTarget()
    {
        return CurrentTarget;
    }
    private void Start()
    {
        follower = GetComponent<SplineFollower>();
        connectToCaster();


    }

    List<ITarget> SelectTargets()
    {
        combinedTarget = transform.position + OriginOffset;
        currentForward = follower.result.forward;
        Caster.cast(combinedTarget, currentForward);
        return Caster.getTargets();
    }
    private void FixedUpdate()
    {
        AllTargets = SelectTargets();
        if (AllTargets.Count > 0)
            CurrentTarget = AllTargets[0];
        else
            CurrentTarget = null;
    }

    public ITarget getTarget()
    {
        return CurrentTarget;
    }

    public List<ITarget> getTargets()
    {
        return AllTargets;
    }
    public void connectToCaster()
    {
        IPhysicsCaster[] casters = GetComponents<IPhysicsCaster>();
        for (int i = 0; i < casters.Length; i++)
        {
            if (casters[i].isActive())
            {
                Caster = casters[i];
                return; 


            }
        }
       
    }

   
}



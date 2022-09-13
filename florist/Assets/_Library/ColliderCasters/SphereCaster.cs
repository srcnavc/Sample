using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCaster : MonoBehaviour, IPhysicsCaster
{

    public float selectRange;
    public LayerMask TargetLayers;
    public bool active;


    RaycastHit[] RCH;
    List<ITarget> Targets;
    ITarget TempTarget;

    public void cast(Vector3 Origin, Vector3 Direction)
    {

        if (Targets == null)
            Targets = new List<ITarget>();
        else
            Targets.RemoveRange(0, Targets.Count);
  
        RCH = Physics.SphereCastAll(transform.position, selectRange , Direction, 0.01f, TargetLayers);
        for (int i = 0; i < RCH.Length; i++)
        {
            TempTarget = RCH[i].collider.attachedRigidbody.gameObject.GetComponent<ITarget>();
            if (TempTarget != null&& TempTarget.isValid())
                Targets.Add(TempTarget);
        }
    }

    public List<ITarget> getTargets()
    {
  
        return Targets;
    }

    public bool isActive()
    {
        return active;
    }


}

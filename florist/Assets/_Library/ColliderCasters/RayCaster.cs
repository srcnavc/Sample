using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour,IPhysicsCaster
{


    public float selectRange;
    public LayerMask TargetLayers;
    public bool active;
    ITarget TempTarget;

    public void Activate(bool val)
    {
        active = val;
    }

    Ray ray;
    RaycastHit[] RCH;
    List<ITarget> Targets;
    public void cast(Vector3 Origin, Vector3 Direction  )
    {

        if (Targets == null)
            Targets = new List<ITarget>();
        else
            Targets.RemoveRange(0,Targets.Count);




            ray.direction = Direction;
            ray.origin = Origin;
            RCH  = Physics.RaycastAll(ray, selectRange, TargetLayers);
                for (int i = 0; i < RCH.Length; i++)
                {
                 TempTarget = RCH[i].collider.attachedRigidbody.gameObject.GetComponent<ITarget>();

                    if (TempTarget != null && TempTarget.isValid())
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

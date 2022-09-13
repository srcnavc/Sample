using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCaster : MonoBehaviour,IPhysicsCaster
{


    public float selectRange;
    public float selectwidth;
    public LayerMask TargetLayers;
   
    RaycastHit[] RCH;
    List<ITarget> Targets;
    Vector3 boxScale = new Vector3();
    Vector3 lastOrigin,Last_Direction,lastScale;

    public bool active;
    public void Activate(bool val)
    {
        active = val;
    }

    public void cast(Vector3 Origin, Vector3 Direction  )
    {
        lastOrigin = Origin;
        Last_Direction = Direction;
        
        if (Targets == null)
            Targets = new List<ITarget>();
        else
            Targets.RemoveRange(0,Targets.Count);


        boxScale.x = selectwidth / 2;
        boxScale.y = selectwidth / 2;
        boxScale.z = selectRange / 2;

        lastScale = boxScale;

            RCH  = Physics.BoxCastAll(Origin+(Direction.normalized*selectRange/2),boxScale,Direction,transform.rotation,selectRange, TargetLayers);
    
        for (int i = 0; i < RCH.Length; i++)
                {
                ITarget TempTarget = RCH[i].collider.attachedRigidbody?.gameObject.GetComponent<ITarget>();

                    if (TempTarget != null&& TempTarget.isValid())
                    Targets.Add(TempTarget);

                }
    
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(lastOrigin,)

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

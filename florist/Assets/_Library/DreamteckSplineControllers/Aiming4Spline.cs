using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(Rig))]
public class Aiming4Spline : MonoBehaviour
{
    
    [SerializeField] GameObject AimTarget;
    [SerializeField] TargetSelector4Spline targetSelector;
    [SerializeField] float AimSpeed,smoothing;
    public bool Aiming = false;
    
    public bool aim
    {
        get {
            ITarget target = targetSelector.getCurrentTarget();
            if (target != null)
            {
                //return Aiming && Vector3.Distance(target.getObjectPosition(), transform.position) < AimDistance;
                return Aiming ;

            }
            else
                return false;
        
        }
    }
    Rig rig;
    void Start()
    {
        targetSelector = GetComponentInParent<TargetSelector4Spline>();
        rig = GetComponent<Rig>();
    }

    public void startAiming()
    {
        Aiming = true;
    }

    public void stopAiming()
    {
        Aiming = false;
    }
    void Update()
    {

        if (aim)
        {
            AimTarget.transform.position = targetSelector.getCurrentTarget().getTargetPosition();
            if (rig.weight < 0.99f)
            {
                rig.weight = Mathf.Lerp(rig.weight, 1, AimSpeed * Time.deltaTime);
                rig.weight = Mathf.SmoothDamp(rig.weight, 1, ref smoothing, AimSpeed * Time.deltaTime);
               
            }
        }
        else
            if (rig.weight > 0.01f)
            {
            rig.weight = Mathf.Lerp(rig.weight, 0, AimSpeed * Time.deltaTime);
            }
    }
}

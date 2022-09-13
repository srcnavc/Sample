using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public Vector3 StartPoint, TargetPoint, CurrentPoint;
    public float Speed;
    TargettingType targettingType;
    Transform TargetTransform;
    Vector3 Offset;
    bool action = false;
    public float curveMagnitude;
    public AnimationCurve curvyShot;
    Vector3 CurveDirection = Vector3.up;


    protected float progressPercentage
    {
        get
        {
            return (CurrentPoint - StartPoint).magnitude / (TargetPoint - StartPoint).magnitude;
        }
    }
    protected abstract void Onfire();
    protected abstract void AfterMove();
    protected abstract void OnReach();

    public void fire(Vector3 StartPoint, Vector3 TargetPoint)
    {
        CurveDirection = StartPoint - Player.getCurrentPlayer().transform.position;
        CurveDirection.Normalize();
        CurveDirection.z = 0;
        setup(StartPoint, TargetPoint, Vector3.zero, null, TargettingType.WorldPosition);
        transform.position = StartPoint;
        Onfire();
        action = true;
    }
    public void fireHooming(Vector3 StartPoint, Transform hoomingTarget, Vector3 offset)
    {
        CurveDirection = StartPoint - Player.getCurrentPlayer().transform.position;
       CurveDirection.Normalize();
        CurveDirection.z = 0;
        setup(StartPoint, hoomingTarget.transform.position, offset, hoomingTarget, TargettingType.HoomingStrait);
        Onfire();
        action = true;
    }

    public void fireLocal(Vector3 startOffset, Vector3 Finaloffset)
    {
        setup(startOffset, Finaloffset, Vector3.zero, null, TargettingType.LocalPosition);
        transform.localPosition = startOffset;
        Onfire();
        action = true;
    }
    public void setup(Vector3 startPoint, Vector3 targetPoint, Vector3 offset, Transform targetTransform, TargettingType ttype)
    {
        this.StartPoint = startPoint;
        this.CurrentPoint = startPoint;
        this.TargetTransform = targetTransform;
        this.TargetPoint = targetPoint;
        this.Offset = offset;
        this.targettingType = ttype;
    }

    private void FixedUpdate()
    {
        if (action)
        {

            switch (targettingType)
            {
                case TargettingType.WorldPosition:
                    moveNormal();
                    break;
                case TargettingType.LocalPosition:
                    MoveLocal();
                    break;
                case TargettingType.HoomingStrait:
                    MoveHooming();
                    break;
                case TargettingType.HoomingTracker:
                    MoveHooming();//Tracker is not ready
                    break;
                default:
                    break;
            }
        }
    }
    void moveNormal()
    {
        CurrentPoint = Vector3.MoveTowards(CurrentPoint, TargetPoint, Speed * Time.deltaTime);
        transform.position = CurrentPoint + curveOffset(TargetPoint);
        AfterMove();
        if (CurrentPoint == TargetPoint)
        {
            action = false;
            OnReach();
        }
    }

    void MoveHooming()
    {
        
    

        CurrentPoint = Vector3.MoveTowards(CurrentPoint, TargetTransform.position + Offset, Speed * Time.deltaTime)  ;
        transform.position = CurrentPoint + curveOffset(TargetTransform.position + Offset);
        AfterMove();
        if (CurrentPoint == TargetPoint)
        {
            action = false;
            OnReach();
        }
    }

    public Vector3 curveOffset(Vector3 target)
    {
        Vector3 curveOffset = curvyShot.Evaluate((Vector3.Distance(CurrentPoint, target) / Vector3.Distance(StartPoint, target)))*(CurveDirection);
     //   Debug.Log(CurveDirection);
        return curveOffset * curveMagnitude;
         
    }
    void MoveLocal()
    {
        CurrentPoint = Vector3.MoveTowards(CurrentPoint, TargetPoint, Speed * Time.deltaTime);
        transform.localPosition = CurrentPoint;
        AfterMove();
        if (CurrentPoint == TargetPoint)
        {
            action = false;
            OnReach();
        }
    }
}

public enum TargettingType
{
    WorldPosition,
    LocalPosition,
    HoomingStrait,
    HoomingTracker/// Not ready
}

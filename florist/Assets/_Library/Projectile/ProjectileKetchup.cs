using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileKetchup : Projectile,IPoolObject
{
    [SerializeField] float Offset = 1;
    [SerializeField] AnimationCurve OffsetCurveY;

    public void clearForRelease()
    {
 
    }

    public void OnCreate()
    {
        //throw new System.NotImplementedException();
    }

    public void resetForRotate()
    {
        //throw new System.NotImplementedException();
    }

    protected override void AfterMove()
    {
      
        transform.position += OffsetCurveY.Evaluate(progressPercentage) * Vector3.up * Offset;
    }

    protected override void Onfire()
    {
        //Debug.Log("Faayaaaa");
    }

    protected override void OnReach()
    {
        GetComponent<PoolObject>()?.release();   
    }

   
}

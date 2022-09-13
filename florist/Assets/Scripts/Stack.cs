using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FIMSpace.FTail;

public class Stack : MonoBehaviour, IPoolObject
{
    [SerializeField] StackMovement logMove;
    [SerializeField] TailAnimator2 tailAnim;
    public Vector3Int Location;
    public Vector3 TargetLocalPosition;
    
    public void StartMoving()
    {
        logMove.StartMoving(TargetLocalPosition);
        
    }

    public void StartMovingWithScaling(Vector3 startScale, Vector3 targetScale)
    {
        if(tailAnim != null)
            tailAnim.enabled = true;
        logMove.StartMoving(TargetLocalPosition, startScale, targetScale);

    }

    public void clearForRelease()
    {
        transform.parent = null;
        Location = Vector3Int.zero;
        TargetLocalPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        if (tailAnim != null)
            tailAnim.enabled = false;
    }

    public void OnCreate()
    {
        
    }

    public void resetForRotate()
    {
        transform.parent = null;
        Location = Vector3Int.zero;
        TargetLocalPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }
}

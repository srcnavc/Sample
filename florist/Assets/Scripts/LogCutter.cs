using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LogCutter : MonoBehaviour
{
    public static Action OnLogCutting;
    [SerializeField] BaseLevelSC levelData;
    [SerializeField] UnityEvent<Vector3> OnHit;
    [SerializeField] UnityEvent OnStartAnimation;
    [SerializeField] float radius;
    [SerializeField] float distance;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float playerSpeed;
    [SerializeField] float animationDuration;
    bool hitSomething;
    RaycastHit hit;
    Transform targetTransform;
    float distanceBetweenTarget;
    float timeToReachTarget;
    
    public float Damage => levelData.Levels[WeaponStation.ins.CurrentLevel].value;
    private int WeaponLevel => levelData.Levels[WeaponStation.ins.CurrentLevel].level;
    
    private void FixedUpdate()
    {
        hitSomething = Physics.SphereCast(transform.position, radius, Vector3.forward, out hit, distance, layerMask);
        
        if (hitSomething)
            DecideToHit(hit);
    }
    
    private void DecideToHit(RaycastHit rayHit)
    {
        distanceBetweenTarget = Vector3.Distance(rayHit.transform.position, transform.position);
        timeToReachTarget = distanceBetweenTarget / playerSpeed;
        
        if (timeToReachTarget <= animationDuration)
        {
            targetTransform = rayHit.transform;
            OnStartAnimation?.Invoke();
            OnLogCutting?.Invoke();
        }
    }

    public void ResetParameters()
    {
        targetTransform = null;
    }

    public void DoHit()
    {
        if (targetTransform != null && targetTransform.GetComponent<TreeController>().Damage(Damage))
        {
            OnHit?.Invoke(targetTransform.position);
            FrontStack.ins.RemoveItem(gameObject);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.forward * distance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + (Vector3.forward * distance), radius);
        
    }
}

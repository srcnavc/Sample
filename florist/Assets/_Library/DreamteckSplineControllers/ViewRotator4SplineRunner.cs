using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines; 

public class ViewRotator4SplineRunner : MonoBehaviour
{
    IinputBridge input;
    [SerializeField] ScriptableAlertingParameterFloat ForwardSpeed;
    [SerializeField] ScriptableAlertingParameterFloat MaxRotation;
    [SerializeField] ScriptableAlertingParameterFloat RotationLerp;
    [SerializeField] bool RotateWhenIdle;

    // ISplineFollower followerModel;
    SplineFollower follower;
    float _LerpTarget;
    Vector3 _TempV3 =Vector3.zero;
    void Start()
    {
        input = GetComponent<IinputBridge>();
        follower = GetComponent<SplineFollower>();
        
    }
    void Update()
    {
        if (ForwardSpeed.PValue!=0&&!RotateWhenIdle)
        {
            _TempV3 = follower.motion.rotationOffset;
            _LerpTarget = input.moveInput.x * MaxRotation.PValue;
            _LerpTarget = Mathf.Clamp(_LerpTarget, -MaxRotation.PValue, MaxRotation.PValue);
            _TempV3.y = Mathf.Lerp(_TempV3.y, _LerpTarget, RotationLerp.PValue * Time.deltaTime);
            follower.motion.rotationOffset = _TempV3;
        }
    }
}

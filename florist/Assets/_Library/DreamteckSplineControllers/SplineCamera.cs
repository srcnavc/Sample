using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class SplineCamera : MonoBehaviour
{
    Quaternion currentRotation,TargetRotation;
    public SplineFollower Follow;
    public bool followMoveX;
    public float forwardLookOffset = 0.02f;
    SplineFollower follower;
    Vector2 _TempV2;
    Vector3 _TempV3;

    void Start()
    {
        follower = GetComponent<SplineFollower>();
    }

    // Update is called once per frame
    void Update()
    {
        follower.followSpeed = Follow.followSpeed;
        float target_lookDist = (float)Follow.spline.Project(Follow.transform.position).percent + forwardLookOffset;
        target_lookDist = Mathf.Clamp(target_lookDist, 0, 1);
        _TempV3 = Follow.spline.EvaluatePosition(target_lookDist);
        _TempV3.y = Follow.transform.position.y;


        currentRotation = transform.rotation;
        // transform.LookAt(Follow.transform.position + Follow.transform.forward);
        transform.LookAt(_TempV3);
        TargetRotation = transform.rotation;
        transform.rotation = Quaternion.Lerp(currentRotation, TargetRotation, Time.deltaTime*1.5f );
        if (followMoveX)
        {
            _TempV2 = Follow.motion.offset;
            _TempV2.y = follower.motion.offset.y;
            follower.motion.offset = _TempV2;
        }
    }
}

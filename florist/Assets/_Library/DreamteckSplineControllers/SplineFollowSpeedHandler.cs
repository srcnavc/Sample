using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class SplineFollowSpeedHandler : MonoBehaviour
{
    [SerializeField]ScriptableAlertingParameterFloat speed;
    SplineFollower follower;
    ISplineFollower splineFollower;
    void Start()
    {
        speed.OnParameterUpdate += updateSpeed;
        splineFollower = GetComponent<ISplineFollower>();
        follower = GetComponent<SplineFollower>();
        updateSpeed();
    }
    void updateSpeed()
    {
      

        

        if (speed.PValue < 0)
        {
            follower.direction = Spline.Direction.Backward;
        }
        else
            follower.direction = Spline.Direction.Forward;

            follower.followSpeed = Mathf.Abs(speed.PValue);
    }
    private void OnDestroy()
    {
        speed.OnParameterUpdate -= updateSpeed;
    }


}

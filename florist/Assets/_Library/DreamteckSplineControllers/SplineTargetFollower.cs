using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class SplineTargetFollower : MonoBehaviour
{

    public Vector3 Direction;

    public Vector3 TargetDirection
    {
        get {
            if (Target != null)
            {
                return (Target.transform.position - transform.position).normalized;
            }
            else
                return Vector3.zero;
        }
    }


    [SerializeField] SplineFollower Target;
    SplineFollower Follower;
    [SerializeField] ScriptableAlertingParameterFloat EnemySpeed;

    
    float _LerpTargetX;
    Vector2 _TempV2;

    void Start()
    {
        Follower = GetComponent<SplineFollower>();
        _LerpTargetX = Follower.motion.offset.x;
    }

    void Update()
    {

    
   
        if (Target!=null)
        {

            Direction = TargetDirection;

            float followSpeed = (TargetDirection * EnemySpeed.PValue).z;

            Follower.followSpeed = Mathf.Abs(followSpeed);
            Follower.direction = (Spline.Direction)(followSpeed / Mathf.Abs(followSpeed));
            _TempV2 = Follower.motion.offset;
            _LerpTargetX += (TargetDirection * EnemySpeed.PValue).x*Time.deltaTime ;
            _TempV2.x = Mathf.Lerp(_TempV2.x, _LerpTargetX, .5f);
            Follower.motion.offset = _TempV2;
            transform.LookAt(Target.transform);
        }
    }


}

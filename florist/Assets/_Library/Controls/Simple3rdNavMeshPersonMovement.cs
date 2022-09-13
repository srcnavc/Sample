using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Simple3rdNavMeshPersonMovement : MonoBehaviour
{
    public float speed,targetAngle,currentAngle;
    public Joystick stick;
    Animator _animator;
    Animator animator
    {
        get { 
            if(_animator==null)
                _animator = GetComponentInChildren<Animator>();
            return _animator;

        }
    }
    NavMeshAgent _agent;
    NavMeshAgent agent {
        get {
            if (_agent == null)
                _agent = GetComponentInChildren<NavMeshAgent>();
            return _agent;


        }
    }
    Vector3 directionVector;
    bool oldRun = false,run;
    private bool isPlaying()
    {
        return true;
    }
    private void Update()
    {
        if (isPlaying())
        {
            directionVector.x = stick.Horizontal;
            directionVector.z = stick.Vertical;
            run = directionVector.magnitude > 0;

            if (oldRun != run)
            {
                animator.SetBool("run", run);
                oldRun = run;
            }

            if (directionVector.magnitude == 0)
            {
                return;
            }

            agent.Move(directionVector * Time.deltaTime * speed);
            targetAngle = Mathf.Atan2(directionVector.x, directionVector.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }

    }

    private void Awake()
    {
    }


}

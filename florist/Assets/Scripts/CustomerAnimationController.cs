using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAnimationController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] CustomerController customer;
    [SerializeField] FrontStackUp frontStackUp;
    CustomerState activeState;
    string activeTrigger;
    private void Start()
    {
        customer.OnCustomerStateChanged += OnStateChanged;
    }
    private void SetBool(string name, bool value)
    {
        if (anim.GetBool(name) != value)
            anim.SetBool(name, value);
    }

    private void SetTrigger(string name)
    {
        if(activeTrigger != name)
        {
            anim.ResetTrigger(activeTrigger);
            anim.SetTrigger(name);
            activeTrigger = name;
        }
    }

    private void LateUpdate()
    {
        if (activeState != customer.state)
        {
            activeState = customer.state;
            OnStateChanged(activeState);
        }

        if (frontStackUp.CurrentStackCount <= 0)
            SetBool("HavePot", false);
        else
            SetBool("HavePot", true);

    }
     private void OnStateChanged(CustomerState state)
    {
        switch (state)
        {
            case CustomerState.Idle:
                //SetTrigger("Idle");
                SetBool("RunBool", false);
                break;
            case CustomerState.Running:
                //SetTrigger("Run");
                SetBool("RunBool", true);
                break;
            default:
                break;
        }
    }
}

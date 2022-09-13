using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleJoystickInput : MonoBehaviour,IinputBridge
{
    [SerializeField] Vector3 Last, Current;
    public float minThreshold;
    public Vector3 moveInput => Current.normalized;

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Last = Input.mousePosition;
            Current = Vector3.zero;
        }
        if (Input.GetMouseButton(0))
        {
            if((Input.mousePosition - Last).magnitude > minThreshold)
                Current = Input.mousePosition - Last;
           // Last = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Last = Vector3.zero;
            Current = Vector3.zero;
        }
    }

    public void setMinThreshhold(float value)
    {
        minThreshold = value;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingJoystickBridge : MonoBehaviour,IinputBridge
{

    FloatingJoystick FJ;
    public Vector3 moveInput => FJ.Direction;

    public void setMinThreshhold(float value)
    {
        //throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        FJ = FindObjectOfType<FloatingJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

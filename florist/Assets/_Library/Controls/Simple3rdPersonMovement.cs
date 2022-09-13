using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Simple3rdPersonMovement : MonoBehaviour
{
    public float speed,targetAngle,currentAngle;
    public Joystick stick;

    private void Update()
    {


        float Input_X = stick.Horizontal ;
        float Input_Y = stick.Vertical;
        if (stick.Horizontal == 0 && stick.Vertical == 0)
            return;

            
        transform.position += new Vector3(Input_X, 0, Input_Y)*Time.deltaTime*speed;
        targetAngle = Mathf.Atan2(Input_X, Input_Y) * Mathf.Rad2Deg;

        

        //currentAngle = Mathf.SmoothDamp( currentAngle, targetAngle, ref smoothingRef, 0.2f);
       
        transform.rotation = Quaternion.Euler(0, targetAngle, 0);

    }
    private void LateUpdate()
    {
        
    }
    public float angleAbs(float ang)
    {
        if (ang < 0)
            return ang + 360;
        else
            return ang;
    
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class MarkerSensorBase : MonoBehaviour
{

    public event Action<Vector3, SignallCallback> OnSignalBroadCast;
    public delegate void  SignallCallback(float distance ,Vector3 center , LocalMarker marker);
    public void signal(Vector3 center , SignallCallback callback)
    {
        if (OnSignalBroadCast != null)
            OnSignalBroadCast.Invoke(center, callback);
    }
    

}

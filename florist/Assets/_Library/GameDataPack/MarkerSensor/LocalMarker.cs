using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class LocalMarker : MonoBehaviour
{
    public MarkerSensorBase sensorBase; 
    public Vector3 LocalMarkerOffset;
    public Mesh GizmoMesh;
    public float gizmoSize=1;
    private bool used;

    public abstract void connectToBase();
    private void ConnectToBroadCast()
    {
        sensorBase.OnSignalBroadCast += OnBroadcast;
    }

    public void setUsed()
    {
        used = true;
        sensorBase.OnSignalBroadCast -= OnBroadcast;
    }
    public void SetUnused()
    {
        used = false;
        sensorBase.OnSignalBroadCast += OnBroadcast;
    }
    private void LateUpdate()
    {
        if (sensorBase == null)
        {
            connectToBase();
            ConnectToBroadCast();
        }
    }
    protected void OnBroadcast(Vector3 center, MarkerSensorBase.SignallCallback callback)
    {
       
        if(!used)
        callback(Vector3.Distance(center, transform.position + LocalMarkerOffset), transform.position + LocalMarkerOffset,this);
    }

    private void OnDestroy()
    {
        sensorBase.OnSignalBroadCast -= OnBroadcast;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (GizmoMesh != null)
            Gizmos.DrawMesh(GizmoMesh, transform.position + LocalMarkerOffset,Quaternion.identity,Vector3.one*gizmoSize);

    }
}

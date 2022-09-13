using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleTarget : MonoBehaviour,ITarget
{
    public float YoffsetMultiplier,YoffsetChangeSpeed;
    public float XoffsetMultiplier, XoffsetChangeSpeed;
    [SerializeField] Vector3 targetingOffset;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Vector3 getObjectPosition()
    {
        return transform.position;
    }

    public Vector3 getTargetPosition()
    {
        return transform.position + targetingOffset;
    }

    public bool isValid()
    {
        return true;
    }

    private void Update()
    {
       // targetingOffset.y = YoffsetMultiplier * (Mathf.Sin(Time.time* YoffsetChangeSpeed) + 1) / 2;
        targetingOffset.x = XoffsetMultiplier * (Mathf.Sin(Time.time * XoffsetChangeSpeed)) ;
    }

}

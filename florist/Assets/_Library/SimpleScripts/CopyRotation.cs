using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotation : MonoBehaviour
{
    

    Transform transformToCopy;
    Vector3 eullers;
    public void set(Transform source)
    {

        transformToCopy = source;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transformToCopy != null)
        {
            transform.rotation = Quaternion.identity;
            float Yrotation = Mathf.Rad2Deg * Mathf.Atan2(transformToCopy.forward.x, transformToCopy.forward.z);
            transform.Rotate(0, Yrotation, 0);
        }
    }
}

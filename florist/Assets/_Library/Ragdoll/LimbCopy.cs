using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbCopy : MonoBehaviour
{
    public Transform toCopy;
    Quaternion originalRotation;
    ConfigurableJoint cj;
    public bool inverse;
    public bool LOCKED;
    public bool LOCKEDHEAVY;


    void Start()
    {
        cj = GetComponent<ConfigurableJoint>();
        originalRotation = toCopy.localRotation;

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(LOCKEDHEAVY)
        {
            transform.position = toCopy.transform.position;
            transform.rotation = toCopy.rotation;

        }
        else 
        if (LOCKED)
        {
            transform.localPosition = toCopy.transform.localPosition;
            transform.localRotation = toCopy.localRotation;
        }
        else
        {

            cj.targetRotation = Quaternion.Inverse(toCopy.localRotation) * originalRotation;
            if (inverse)
                cj.targetRotation = Quaternion.Inverse(cj.targetRotation) * cj.targetRotation;
        }
    }
}

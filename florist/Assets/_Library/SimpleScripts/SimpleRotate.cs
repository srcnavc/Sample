using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    public Vector3 rotate;
    public Vector3 finalrotate;
    private void LateUpdate()
    {
        finalrotate = rotate * Time.deltaTime;
        transform.Rotate(finalrotate);
    }


}

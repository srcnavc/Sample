using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float x, y, z;
    void Start()
    {
        transform.Rotate(
            Random.Range(0, x),
            Random.Range(0, y),
            Random.Range(0, z)
            );
    }

}

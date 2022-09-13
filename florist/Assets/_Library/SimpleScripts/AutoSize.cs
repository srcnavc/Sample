using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSize : MonoBehaviour
{
    public float SizeMin, SizeMax;

    void Start()
    {
        transform.localScale = transform.localScale * Random.Range(SizeMin, SizeMax);
    }

}

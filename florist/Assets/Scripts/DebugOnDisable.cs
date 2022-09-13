using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugOnDisable : MonoBehaviour
{
    [SerializeField] string message;

    private void OnDisable()
    {
        Debug.Log(transform.parent.name + "  : " + message);
    }
}

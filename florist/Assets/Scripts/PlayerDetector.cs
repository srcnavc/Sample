using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] UnityEvent<Vector3> OnPlayerEnterTrigger;
    [SerializeField] UnityEvent<Vector3> OnPlayerExitTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnPlayerEnterTrigger?.Invoke(other.transform.position);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            OnPlayerExitTrigger?.Invoke(other.transform.position);
    }
}

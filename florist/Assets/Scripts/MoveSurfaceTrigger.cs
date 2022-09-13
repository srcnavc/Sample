using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSurfaceTrigger : MonoBehaviour
{
    [SerializeField] bool disableAtFirstPassed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (InfiniteRoad.ins.howManyTimesMapMoved > 0 || !disableAtFirstPassed)
                InfiniteRoad.ins.MoveForward();
            else
                disableAtFirstPassed = false;
        }
    }
}

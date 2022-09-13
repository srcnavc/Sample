using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTypeSwitch : MonoBehaviour
{
    [SerializeField] bool runwayMoveOn = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            SwitchType(other.transform);
    }

    private void SwitchType(Transform transform)
    {
        if (runwayMoveOn)
        {
            transform.GetComponent<NavMeshPersonMovement>().enabled = false;
            transform.GetComponent<RunwayMove>().enabled = true;
            transform.GetComponent<PlayerController>().GameType = GameType.Runner;
        }
        else
        {
            transform.GetComponent<NavMeshPersonMovement>().enabled = true;
            transform.GetComponent<RunwayMove>().enabled = false;
            transform.GetComponent<PlayerController>().GameType = GameType.Idle;
        }
    }
}

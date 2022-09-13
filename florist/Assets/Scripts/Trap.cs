using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Axe"))
        {
            FrontStack.ins.Scatter(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            FrontStack.ins.ScatterAll();
            other.GetComponent<RunwayMove>().GoBack();
        }
    }
}

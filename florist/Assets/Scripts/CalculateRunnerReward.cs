using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateRunnerReward : MonoBehaviour
{
    [SerializeField] Catapult catapult;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Axe"))
        {
            FrontStack.ins.RemoveItem(other.gameObject);
            catapult.Throw(other.gameObject);
        }
    }
}

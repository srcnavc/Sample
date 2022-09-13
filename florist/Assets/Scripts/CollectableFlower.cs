using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFlower : MonoBehaviour, IPoolObject
{
    [SerializeField] Rigidbody rb;
    public bool IsAvailable = false;

    public void clearForRelease()
    {
        IsAvailable = false;
        //throw new System.NotImplementedException();
    }

    public void OnCreate()
    {
        //throw new System.NotImplementedException();
    }

    public void resetForRotate()
    {
        IsAvailable = false;
        //throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || (other.CompareTag("Axe") && IsAvailable))
        {
            FrontStack.ins.AddItem(gameObject);
            GetComponent<PoolObject>().release();
        }
    }
}

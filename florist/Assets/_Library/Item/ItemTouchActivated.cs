using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemTouchActivated : Item
{
    [SerializeField] public  LayerMask InterractingLayers;
    GameObject OpposingObject;
    private void OnCollisionEnter(Collision collision)
    {
        OpposingObject = null;
        if (collision.rigidbody != null)
            OpposingObject = collision.rigidbody.gameObject;
        doActivate(OpposingObject);

    }
    private void OnTriggerEnter(Collider other)
    {
        OpposingObject = null;
        if(other.attachedRigidbody!=null)
            OpposingObject= other.attachedRigidbody.gameObject;
        doActivate(OpposingObject);
    }

    void doActivate(GameObject target)
    {
        if (target == null)
            return;

        if ((InterractingLayers.value & 1 << target.layer) == 1 << target.layer)
            activate(target);

    }



}

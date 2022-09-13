using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastDown : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] Rigidbody rb;
    [SerializeField] float pushDownPower;
    [SerializeField] float rayDistance;
    RaycastHit hit;
    bool isGround = false;
    private void FixedUpdate()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, out hit, rayDistance, layerMask);
        if (!isGround)
        {
            if(rb.velocity != Vector3.down * pushDownPower)
                rb.velocity = Vector3.down * pushDownPower;
        }
        else
        {
            if (rb.velocity != Vector3.zero)
                rb.velocity = Vector3.zero;
        }
    }
}

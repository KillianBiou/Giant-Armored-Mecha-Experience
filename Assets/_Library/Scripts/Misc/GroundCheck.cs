using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private float maxDist;
    [SerializeField]
    private float minDist;
    [SerializeField]
    private float gravityPower;

    private RaycastHit hit;
    public Rigidbody mecha;

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit, maxDist, groundMask))
        {
            Debug.DrawRay(transform.position, -transform.up * maxDist);
            if(hit.distance >= minDist && hit.distance <= maxDist)
            {
                Physics.gravity = -hit.normal * gravityPower;
                GetComponentInParent<MechaParts>().isGrounded = true;
            }
            else
            {
                GetComponentInParent<MechaParts>().isGrounded = false;
            }
        }
    }
}

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
    private Quaternion baseRotation;
    private Vector3 targetPos;
    public Rigidbody mecha;

    private void OnTriggerEnter(Collider other)
    {
        if((groundMask.value & (1 << other.gameObject.layer)) > 0)
        {
            GetComponentInParent<MechaParts>().isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((groundMask.value & (1 << other.gameObject.layer)) > 0)
            GetComponentInParent<MechaParts>().isGrounded = false;
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit, maxDist, groundMask))
        {
            if(baseRotation.x == float.NegativeInfinity)
                baseRotation = transform.rotation;

            if(hit.distance >= minDist && hit.distance <= maxDist)
            {
                targetPos = hit.normal;
                Physics.gravity = -hit.normal * gravityPower;

                //Quaternion deltaRotation = Quaternion.Euler(transform.rotation * Quaternion.FromToRotation(transform.up, hit.normal).eulerAngles * Time.fixedDeltaTime);

                //transform.GetComponentInParent<Rigidbody>().MoveRotation(test * transform.rotation);
                //transform.GetComponentInParent<Rigidbody>().MoveRotation(Quaternion.FromToRotation(transform.up, hit.normal) * (transform.rotation));
                //transform.GetComponentInParent<Rigidbody>().MoveRotation(deltaRotation * (transform.rotation));
            }
        }

        /*Vector3 direction = transform.position - targetPos;

        direction.Normalize();

        Vector3 rotateAmount = Vector3.Cross(direction, transform.forward);
        rotateAmount.y = 0f;

        mecha.angularVelocity += rotateAmount / 4;*/
    }
}

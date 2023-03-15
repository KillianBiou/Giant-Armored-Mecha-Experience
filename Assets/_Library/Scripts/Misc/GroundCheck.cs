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

    private RaycastHit hit;
    private Quaternion baseRotation;

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
                float currentPercentage = ((hit.distance - minDist)) / (maxDist - minDist);
                Quaternion test = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.up, hit.normal), currentPercentage);
                Debug.Log(test.ToString());

                //Quaternion deltaRotation = Quaternion.Euler(transform.rotation * Quaternion.FromToRotation(transform.up, hit.normal).eulerAngles * Time.fixedDeltaTime);

                //transform.GetComponentInParent<Rigidbody>().MoveRotation(test * transform.rotation);
                transform.GetComponentInParent<Rigidbody>().MoveRotation(Quaternion.FromToRotation(transform.up, hit.normal) * (transform.rotation));
                //transform.GetComponentInParent<Rigidbody>().MoveRotation(deltaRotation * (transform.rotation));
            }
        }
        else
        {
            baseRotation = new Quaternion(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
        }
    }
}

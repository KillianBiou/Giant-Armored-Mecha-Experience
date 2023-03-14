using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float accelerationFactor;
    [SerializeField]
    private float accelerationUpFactor;
    [SerializeField]
    private float deadzone;

    private MechaParts mecha;
    private Rigidbody rb;

    private void Start()
    {
        mecha = GetComponent<MechaParts>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float leftY = JoystickExpose.instance.LYAxis;
        float leftX = JoystickExpose.instance.RYAxis;
        float downThrust = JoystickExpose.instance.LeftPedaleAxis;
        float upThrust = JoystickExpose.instance.RightPedaleAxis;

        // Want straight forward
        if(Mathf.Abs(leftX - leftY) < deadzone)
        {
            leftX = leftY = (leftX + leftY) / 2;
        }

        if(rb.velocity.magnitude <= maxSpeed) {
            rb.AddForceAtPosition(mecha.leftThruster.transform.forward * leftY * accelerationFactor, mecha.leftThruster.transform.position);
            rb.AddForceAtPosition(mecha.rightThruster.transform.forward * leftX * accelerationFactor, mecha.rightThruster.transform.position);
        }

        rb.AddForce(transform.up * (-downThrust + upThrust) * accelerationUpFactor);
    }
}

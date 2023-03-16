using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : MonoBehaviour
{
    [Header("Parameters")]

    [Header("Acceleration Parameters")]
    [SerializeField]
    private float accelerationFactor;
    [SerializeField]
    private float accelerationUpFactor;
    [SerializeField]
    private float accelerationStrafFactor;
    [SerializeField]
    private float accelerationTorqueFactor;

    [Header("Other Parameters")]

    [SerializeField]
    private float deadzone;
    [SerializeField]
    private float maxSpeed;

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
        HandleRotation();
    }

    private void HandleMovement()
    {
        float leftY = JoystickExpose.instance.LYAxis;
        float rightY = JoystickExpose.instance.RYAxis;
        float leftX = JoystickExpose.instance.LXAxis;

        float verticalThrust = JoystickExpose.instance.Pedals;

        // Want straight forward
        if (Mathf.Abs(rightY - leftY) < deadzone)
        {
            leftY = rightY = (rightY + leftY) / 2;
        }

        if (rb.velocity.magnitude <= maxSpeed)
        {
            rb.AddForceAtPosition(mecha.leftThruster.transform.forward * leftY * accelerationFactor, mecha.leftThruster.transform.position);
            rb.AddForceAtPosition(mecha.rightThruster.transform.forward * rightY * accelerationFactor, mecha.rightThruster.transform.position);

            rb.AddForce(transform.right * leftX * accelerationStrafFactor);
        }

        rb.AddForce(transform.up * verticalThrust * accelerationUpFactor);
    }

    private void HandleRotation()
    {
        float rightX = JoystickExpose.instance.RXAxis;
        float ZTilt = JoystickExpose.instance.ZTilt;

        if (!mecha.isGrounded && Mathf.Abs(rightX) >= deadzone)
        {
            rb.AddTorque(transform.right * accelerationTorqueFactor * rightX);
        }
        if (!mecha.isGrounded && Mathf.Abs(ZTilt) >= deadzone)
        {
            rb.AddTorque(transform.forward * accelerationTorqueFactor * ZTilt);
        }
    }
}
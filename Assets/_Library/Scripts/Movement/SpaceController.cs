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
    private float deadzoneTilt;
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
        float leftY = InputExpose.instance.LYAxis;
        float rightY = InputExpose.instance.RYAxis;
        float leftX = InputExpose.instance.LXAxis;

        float verticalThrust = InputExpose.instance.Pedals;

        if (InputExpose.instance.R2Button)
            rightY = leftY;

        // Want straight forward
        if(Mathf.Abs(rightY - leftY) < deadzone)
        {
            leftY = rightY = (rightY + leftY) / 2;
        }

        if(rb.velocity.magnitude <= maxSpeed) {
            rb.AddForceAtPosition(mecha.leftThruster.transform.forward * leftY * accelerationFactor, mecha.leftThruster.transform.position);
            rb.AddForceAtPosition(mecha.rightThruster.transform.forward * rightY * accelerationFactor, mecha.rightThruster.transform.position);

            rb.AddForce(transform.right * leftX * accelerationStrafFactor);
        }

        rb.AddForce(transform.up * verticalThrust * accelerationUpFactor);
    }

    private void HandleRotation()
    {
        float RightX = InputExpose.instance.RXAxis;
        float RightY = InputExpose.instance.RYAxis;
        bool R2Button = InputExpose.instance.R2Button;

        if(!mecha.isGrounded && Mathf.Abs(RightX) >= deadzoneTilt)
        {
            rb.AddTorque(-transform.forward * accelerationTorqueFactor * RightX);
        }
        if (!mecha.isGrounded && R2Button && Mathf.Abs(RightY) >= deadzoneTilt)
        {
            rb.AddTorque(transform.right * accelerationTorqueFactor * RightY);
        }
    }
}

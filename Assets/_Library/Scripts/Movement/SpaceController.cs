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
    [SerializeField]
    private float maxAngularSpeed;

    private MechaParts mecha;
    private Rigidbody rb;

    private void Awake()
    {
        GetComponent<Rigidbody>().useGravity = false;
    }

    private void Start()
    {
        mecha = GetComponent<MechaParts>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
        HandleMisc();
        GetComponent<Rigidbody>().useGravity = false;
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
        if (Mathf.Abs(rightY - leftY) < deadzone)
        {
            leftY = rightY = (rightY + leftY) / 2;
        }

        rb.AddForceAtPosition(mecha.leftThruster.transform.forward * leftY * accelerationFactor, mecha.leftThruster.transform.position);
        rb.AddForceAtPosition(mecha.rightThruster.transform.forward * rightY * accelerationFactor, mecha.rightThruster.transform.position);

        rb.AddForce(transform.right * leftX * accelerationStrafFactor);

        rb.AddForce(transform.up * verticalThrust * accelerationUpFactor);

        // Limit Velocity to max

        //rb.velocity = new Vector3(Mathf.Min(rb.velocity.x, maxSpeed), Mathf.Min(rb.velocity.y, maxSpeed), Mathf.Min(rb.velocity.z, maxSpeed));
        //rb.angularVelocity = new Vector3(Mathf.Min(rb.angularVelocity.x, maxAngularSpeed), Mathf.Min(rb.angularVelocity.y, maxAngularSpeed), Mathf.Min(rb.angularVelocity.z, maxAngularSpeed));
        
        if (rb.velocity.magnitude >= maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
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

        if (rb.angularVelocity.magnitude >= maxAngularSpeed)
            rb.angularVelocity = rb.angularVelocity.normalized * maxAngularSpeed;
    }

    private void HandleMisc()
    {
        bool combatButton = InputExpose.instance.L4Button || InputExpose.instance.R4Button;

        if (combatButton && GetComponent<WeaponManager>().GetTarget())
        {
            mecha.ChangeControllerType(ControllerType.COMBAT_CONTROLLER);
        }

        if (mecha.isGrounded)
            mecha.ChangeControllerType(ControllerType.GROUND_CONTROLLER);
    }
}
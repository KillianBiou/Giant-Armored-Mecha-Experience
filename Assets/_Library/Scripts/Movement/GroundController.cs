using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
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

    [SerializeField]
    private LayerMask layers;

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
        HandleMisc();

        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up, out hit, 10, layers))
        {
            Debug.Log(Vector3.Distance(transform.position, hit.point));
            if (Vector3.Distance(transform.position, hit.point) <= 2)
            {
                rb.AddForce(transform.up * accelerationFactor);
            }
            else if(Vector3.Distance(transform.position, hit.point) >= 3)
            {
                rb.useGravity = true;
            }
            else
            {
                rb.useGravity = false;
            }
        }
    }

    private void HandleMovement()
    {
        float leftY = InputExpose.instance.LYAxis;
        float leftX = InputExpose.instance.LXAxis;

        float verticalThrust = Mathf.Clamp(InputExpose.instance.Pedals, 0, 1);

        rb.AddForce(transform.up * verticalThrust * accelerationUpFactor);
        rb.AddForce(transform.forward * leftY * accelerationFactor);
        rb.AddForce(transform.right * leftX * accelerationStrafFactor);

        if (rb.velocity.magnitude >= maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    private void HandleRotation()
    {
        float RightX = InputExpose.instance.RXAxis;
        float RightY = InputExpose.instance.RYAxis;
        bool R2Button = InputExpose.instance.R2Button;

        if (Mathf.Abs(RightX) >= deadzoneTilt)
        {
            rb.AddTorque(transform.up * accelerationTorqueFactor * RightX);
        }

        if (rb.angularVelocity.magnitude >= maxAngularSpeed)
            rb.angularVelocity = rb.angularVelocity.normalized * maxAngularSpeed;
    }

    private void HandleMisc()
    {
        bool combatButton = InputExpose.instance.L4Button || InputExpose.instance.R4Button;
        float verticalThrust = InputExpose.instance.Pedals;

        /*if (combatButton && GetComponent<WeaponManager>().GetTarget())
        {
            mecha.ChangeControllerType(ControllerType.COMBAT_CONTROLLER);
        }*/

        if (!mecha.isGrounded && verticalThrust > 0f)
        {
            mecha.ChangeControllerType(ControllerType.SPACE_CONTROLLER);
            rb.useGravity = false;
        }
    }
}
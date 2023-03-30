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

    [Header("Cockpit Tilt Parameters")]
    [SerializeField]
    private GameObject cockpit;
    [SerializeField]
    private float maxAngle;
    [SerializeField]
    private float lerpSpeed;

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
    private float glidStart;
    [SerializeField]
    private float glidRange;
    [SerializeField]
    private float takeOffDistance;
    [SerializeField]
    private bool useGravity;
    [SerializeField]
    private float gravityForce;
    [SerializeField]
    public bool canFly;

    [SerializeField]
    private LayerMask layers;

    private MechaParts mecha;
    private Rigidbody rb;

    private void Start()
    {
        mecha = GetComponent<MechaParts>();
        rb = GetComponent<Rigidbody>();
        Physics.gravity = Physics.gravity * gravityForce;
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
        HandleCockpit();
        HandleMisc();

        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up, out hit, takeOffDistance * 2, layers))
        {
            float distance = Vector3.Distance(transform.position, hit.point);
            if (distance <= glidStart)
            {
                rb.AddForce(transform.up * accelerationFactor);
            }
            else if(distance < glidStart + glidRange)
            {
                mecha.isGrounded = true;
                rb.useGravity = false;
            }
            else if(distance >= glidStart + glidRange)
            {
                if(useGravity)
                    rb.useGravity = true;
            }
            if (distance >= takeOffDistance)
                mecha.isGrounded = false;
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

        mecha.mechaAnim.SetFloat("X", leftX);
        mecha.mechaAnim.SetFloat("Y", leftY);

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
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
    }

    private void HandleCockpit()
    {
        float RightY = InputExpose.instance.RYAxis;

        Vector3 newEuler = cockpit.transform.localRotation.eulerAngles;

        newEuler.x += RightY * lerpSpeed;

        if (newEuler.x > 180)
            newEuler.x = Mathf.Clamp(newEuler.x, 360 - maxAngle, 360);
        else
            newEuler.x = Mathf.Clamp(newEuler.x, 0, maxAngle);

        cockpit.transform.localRotation = Quaternion.Euler(newEuler);
    }

    private void HandleMisc()
    {
        bool combatButton = InputExpose.instance.L4Button || InputExpose.instance.R4Button;
        float verticalThrust = InputExpose.instance.Pedals;

        if (canFly && !mecha.isGrounded && verticalThrust > 0f)
        {
            mecha.ChangeControllerType(ControllerType.SPACE_CONTROLLER);
            rb.useGravity = false;
        }
    }
}
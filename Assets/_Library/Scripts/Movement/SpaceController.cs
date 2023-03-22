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
    [SerializeField]
    private float maxZTilt;
    [SerializeField]
    private float maxXTilt;

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
        float rightX = InputExpose.instance.RXAxis;
        bool R2Button = InputExpose.instance.R2Button;


        float strafVector = leftX;

        if(!R2Button)
            strafVector = (leftX + rightX) / 2;

        float verticalThrust = InputExpose.instance.Pedals;

        if (InputExpose.instance.R2Button)
        {
            rightY = leftY;
        }
        else
        {
            mecha.mechaAnim.SetFloat("X", leftX);
        }

        // Want straight forward
        if (Mathf.Abs(rightY - leftY) < deadzone)
        {
            leftY = rightY = (rightY + leftY) / 2;
        }

        rb.AddForceAtPosition(mecha.leftThruster.transform.forward * leftY * accelerationFactor, mecha.leftThruster.transform.position);
        rb.AddForceAtPosition(mecha.rightThruster.transform.forward * rightY * accelerationFactor, mecha.rightThruster.transform.position);

        rb.AddForce(transform.right * strafVector * accelerationStrafFactor);

        rb.AddForce(transform.up * verticalThrust * accelerationUpFactor);

        mecha.mechaAnim.SetFloat("Y", Mathf.Clamp(leftY + rightY, -1, 1));

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

        transform.Rotate(Vector3.forward * RightX * accelerationTorqueFactor + Vector3.right * RightY * accelerationTorqueFactor);
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Clamp(180 - transform.eulerAngles.z, -maxZTilt + 180, maxZTilt + 180));

        /*if (!mecha.isGrounded && R2Button && Mathf.Abs(RightX) >= deadzoneTilt )// && transform.rotat ion.eulerAngles.x <= maxXTilt)
        {
            //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, ((360 - transform.rotation.eulerAngles.z > 180) ? maxZTilt : -maxZTilt));
            /*if (RightX < 0 && (360 - transform.rotation.eulerAngles.z > 180))
                rb.AddTorque(transform.forward * accelerationTorqueFactor * RightX);
            if (RightX > 0 && (360 - transform.rotation.eulerAngles.z < 180))
                rb.AddTorque(transform.forward * accelerationTorqueFactor * RightX);
            else
            {
                rb.AddTorque(transform.forward * accelerationTorqueFactor * RightX);
            }
            //rb.AddTorque(Vector3.forward * accelerationTorqueFactor * RightX);
        }
        if (!mecha.isGrounded && R2Button && Mathf.Abs(RightY) >= deadzoneTilt)
        {
            if (360 - transform.rotation.eulerAngles.x > maxXTilt && 360 - transform.rotation.eulerAngles.x < 360 - maxXTilt)
            {
                transform.rotation = Quaternion.Euler(new Vector3(((360 - transform.rotation.eulerAngles.x > 180) ? maxXTilt : -maxXTilt), transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
                if(RightY < 0 && (360 - transform.rotation.eulerAngles.x > 180))
                    rb.AddTorque(Vector3.right * accelerationTorqueFactor * RightY);
                if (RightY > 0 && (360 - transform.rotation.eulerAngles.x < 180))
                    rb.AddTorque(Vector3.right * accelerationTorqueFactor * RightY);
            }
            else
            {
                rb.AddTorque(Vector3.right * accelerationTorqueFactor * RightY);
            }
        }*/

        if (rb.angularVelocity.magnitude >= maxAngularSpeed)
            rb.angularVelocity = rb.angularVelocity.normalized * maxAngularSpeed;

        //transform.rotation = Quaternion.Euler(new Vector3(Mathf.Clamp(transform.rotation.eulerAngles.x, -maxXTilt, maxXTilt), transform.rotation.eulerAngles.y, Mathf.Clamp(transform.rotation.eulerAngles.z, -maxZTilt, maxZTilt)));
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
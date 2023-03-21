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
    }

    private void HandleMovement()
    {
        float leftY = InputExpose.instance.LYAxis;
        float rightY = InputExpose.instance.RYAxis;
        float leftX = InputExpose.instance.LXAxis;

        float verticalThrust = InputExpose.instance.Pedals;

        rb.AddForce(transform.up * verticalThrust * accelerationFactor);
    }

    private void HandleRotation()
    {
        float RightX = InputExpose.instance.RXAxis;
        float RightY = InputExpose.instance.RYAxis;
        bool R2Button = InputExpose.instance.R2Button;

    }

    private void HandleMisc()
    {
        bool combatButton = InputExpose.instance.L4Button || InputExpose.instance.R4Button;

        /*if (combatButton && GetComponent<WeaponManager>().GetTarget())
        {
            mecha.ChangeControllerType(ControllerType.COMBAT_CONTROLLER);
        }*/

        if (!mecha.isGrounded)
            mecha.ChangeControllerType(ControllerType.GROUND_CONTROLLER);
    }
}
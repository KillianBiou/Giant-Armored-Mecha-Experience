using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CombatController : MonoBehaviour
{
    [Header("Parameters")]

    [Header("Acceleration Parameters")]
    [SerializeField]
    private float accelerationFactor;
    [SerializeField]
    private float accelerationUpFactor;
    [SerializeField]
    private float accelerationStrafFactor;

    [Header("Other Parameters")]

    [SerializeField]
    private float deadzone;
    [SerializeField]
    private float maxSpeed;

    private MechaParts mecha;
    private Rigidbody rb;
    private WeaponManager wp;

    private void Start()
    {
        mecha = GetComponent<MechaParts>();
        rb = GetComponent<Rigidbody>();
        wp = GetComponent<WeaponManager>();
        mecha.mechaAnim.SetBool("isFlying", true);
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
        float rightX = InputExpose.instance.RXAxis;

        float verticalThrust = InputExpose.instance.Pedals;

        // UP / DOWN orbiting

        float UpDown = 0;
        if(Mathf.Abs(leftY) > MathF.Abs(rightY))
            UpDown = leftY;
        else
            UpDown = rightY;

        if(Mathf.Abs(UpDown) >= deadzone)
            rb.AddForce(transform.up * UpDown * accelerationUpFactor);


        // RIGHT / LEFT orbitin

        float RightLeft = 0;
        if (Mathf.Abs(leftX) > MathF.Abs(rightX))
            RightLeft = leftX;
        else
            RightLeft = rightX;

        if (Mathf.Abs(RightLeft) >= deadzone)
            rb.AddForce(transform.right * RightLeft * accelerationStrafFactor);

        rb.AddForce(transform.forward * verticalThrust * accelerationFactor);

        //rb.AddForceAtPosition(mecha.leftThruster.transform.forward * Mathf.Max(leftX, leftY) * accelerationFactor, mecha.leftThruster.transform.position);
        //rb.AddForceAtPosition(mecha.rightThruster.transform.forward * Mathf.Max(leftX, leftY) * accelerationFactor, mecha.rightThruster.transform.position);
    }

    private void HandleRotation()
    {
    }

    private void HandleMisc()
    {
        bool spaceButton = InputExpose.instance.L4Button || InputExpose.instance.R4Button;

        if (spaceButton)
            mecha.ChangeControllerType(ControllerType.SPACE_CONTROLLER);
    }

    private void Update()
    {
        if (!wp.GetTarget())
            mecha.ChangeControllerType(ControllerType.SPACE_CONTROLLER);

        Vector3 direction = transform.position - wp.GetTarget().transform.position;

        direction.Normalize();

        Vector3 rotateAmount = Vector3.Cross(direction, transform.forward);

        GetComponent<Rigidbody>().angularVelocity += rotateAmount;

        //transform.LookAt(wp.GetTarget().transform.position, transform.up);
    }
}
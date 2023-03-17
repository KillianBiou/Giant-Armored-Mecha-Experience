using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject target;

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

    private void Update()
    {
        transform.LookAt(target.transform.position, transform.up);
    }
}
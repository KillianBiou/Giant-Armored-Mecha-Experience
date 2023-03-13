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
        rb.AddForceAtPosition(mecha.leftThruster.transform.forward * JoystickExpose.instance.LYAxis * accelerationFactor, new Vector3(mecha.leftThruster.transform.position.x, 0, mecha.leftThruster.transform.position.z));
        rb.AddForceAtPosition(mecha.rightThruster.transform.forward * JoystickExpose.instance.LYAxis * accelerationFactor, new Vector3(mecha.rightThruster.transform.position.x, 0, mecha.leftThruster.transform.position.z));
    }
}

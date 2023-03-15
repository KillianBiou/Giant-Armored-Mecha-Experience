using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickExpose : MonoBehaviour
{
    [Header("Left Joystick")]
    [Tooltip("X Axis of the left joystick (-1, 1)")]
    public float LXAxis;
    [Tooltip("Y Axis of the left joystick (-1, 1)")]
    public float LYAxis;

    [Header("Right Joystick")]
    [Tooltip("X Axis of the right joystick (-1, 1)")]
    public float RXAxis;
    [Tooltip("Y Axis of the right joystick (-1, 1)")]
    public float RYAxis;

    [Header("Pedals")]
    [Tooltip("The right pedal (0, 1)")]
    public float RightPedaleAxis;
    [Tooltip("The left pedal (0, 1)")]
    public float LeftPedaleAxis;

    [Header("Special")]
    [Tooltip("Button for Z tilt")]
    public float ZTilt;


    private PlayerInput input;

    public static JoystickExpose instance;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        instance = this;
    }

    public void OnLX()
    {
        LXAxis = input.actions["LX"].ReadValue<float>();
    }

    public void OnLY()
    {
        LYAxis = input.actions["LY"].ReadValue<float>();
    }

    public void OnRX()
    {
        RXAxis = input.actions["RX"].ReadValue<float>();
    }

    public void OnRY()
    {
        RYAxis = input.actions["RY"].ReadValue<float>();
    }

    public void OnRPedal()
    {
        //RightPedaleAxis = input.actions["RPedal"].ReadValue<float>();
        if(input.actions["RPedal"].ReadValue<float>() <= -0.1)
            RightPedaleAxis = Mathf.Abs(input.actions["RPedal"].ReadValue<float>());
        else if (input.actions["RPedal"].ReadValue<float>() >= 0.1)
            LeftPedaleAxis = input.actions["RPedal"].ReadValue<float>();
        else
            RightPedaleAxis = LeftPedaleAxis = 0;
    }

    public void OnLPedal()
    {
        //LeftPedaleAxis = input.actions["LPedal"].ReadValue<float>();
        if (input.actions["RPedal"].ReadValue<float>() <= -0.1)
            RightPedaleAxis = Mathf.Abs(input.actions["RPedal"].ReadValue<float>());
        else if (input.actions["RPedal"].ReadValue<float>() >= 0.1)
            LeftPedaleAxis = input.actions["RPedal"].ReadValue<float>();
        else
            RightPedaleAxis = LeftPedaleAxis = 0;
    }

    public void OnZTilt()
    {
        ZTilt = input.actions["ZTilt"].ReadValue<float>();
    }
}

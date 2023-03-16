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
    [Tooltip("The pedals value (Left -1, Right 1)")]
    public float Pedals;

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

    public void OnPedals()
    {
        Pedals = input.actions["Pedals"].ReadValue<float>();
    }

    public void OnZTilt()
    {
        ZTilt = input.actions["ZTilt"].ReadValue<float>();
    }
}
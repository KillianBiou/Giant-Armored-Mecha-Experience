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
}

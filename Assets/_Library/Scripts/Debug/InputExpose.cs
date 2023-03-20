using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputExpose : MonoBehaviour
{
    [Header("Left Joystick")]
    [Tooltip("X Axis of the left joystick (-1, 1)")]
    public float LXAxis;
    [Tooltip("Y Axis of the left joystick (-1, 1)")]
    public float LYAxis;

    public bool L2Button;
    public bool L3Button;
    public bool L4Button;
    public bool L5Button;
    public bool LTrigger;

    [Header("Right Joystick")]
    [Tooltip("X Axis of the right joystick (-1, 1)")]
    public float RXAxis;
    [Tooltip("Y Axis of the right joystick (-1, 1)")]
    public float RYAxis;

    public bool R2Button;
    public bool R3Button;
    public bool R4Button;
    public bool R5Button;
    public bool RTrigger;

    [Header("Pedals")]
    [Tooltip("The pedals value (Left -1, Right 1)")]
    public float Pedals;

    public static InputExpose instance;

    private void Start()
    {
        instance = this;
    }
}

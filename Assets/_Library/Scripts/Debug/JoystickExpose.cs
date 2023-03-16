using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickExpose : MonoBehaviour
{
    [Header("Left Joystick")]
    [Tooltip("X Axis (-1, 1)")]
    public float XAxis;
    [Tooltip("Y Axis (-1, 1)")]
    public float YAxis;

    [Header("Buttons")]
    public bool ButtonTwo;

    public float Pedals;

    private PlayerInput input;

    private bool playerOne;

    public static JoystickExpose instance;



    private void Start()
    {
        input = GetComponent<PlayerInput>();
        instance = this;
        playerOne = PlayerInputManager.instance.playerCount == 1 ? true : false;

        if (playerOne)
            GetComponent<JoystickMove>().side = Side.LEFT;
        else
            GetComponent<JoystickMove>().side = Side.RIGHT;
    }

    public void OnX()
    {
        XAxis = input.actions["X"].ReadValue<float>();
        SynchronizeValue();
    }

    public void OnY()
    {
        YAxis = input.actions["Y"].ReadValue<float>();
        SynchronizeValue();
    }

    public void OnPedals()
    {
        Pedals = input.actions["Pedals"].ReadValue<float>();
        SynchronizeValue();
    }

    public void On_2()
    {
        ButtonTwo = input.actions["2"].ReadValue<float>() == 1 ? true : false;
        SynchronizeValue();
    }

    public void OnChangePlayer()
    {
        if(PlayerInputManager.instance.playerCount == 2) {
            foreach (JoystickExpose jp in GameObject.FindObjectsOfType<JoystickExpose>())
            {
                jp.ChangePlayer();
            }
        }
    }

    private void SynchronizeValue()
    {
        if(playerOne)
        {
            InputExpose.instance.LXAxis = XAxis;
            InputExpose.instance.LYAxis = YAxis;
            InputExpose.instance.Pedals = Pedals;
            InputExpose.instance.L2Button = ButtonTwo;
        }
        else
        {
            InputExpose.instance.RXAxis = XAxis;
            InputExpose.instance.RYAxis = YAxis;
            InputExpose.instance.Pedals = Pedals;
            InputExpose.instance.R2Button = ButtonTwo;
        }
    }

    public void ChangePlayer()
    {
        playerOne = !playerOne;

        if (GetComponent<JoystickMove>().side == Side.LEFT)
            GetComponent<JoystickMove>().side = Side.RIGHT;
        else
            GetComponent<JoystickMove>().side = Side.LEFT;
    }
}

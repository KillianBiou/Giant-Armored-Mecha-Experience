using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadExpose : MonoBehaviour
{
    [Header("Left Joystick")]
    [Tooltip("X Axis (-1, 1)")]
    public float LXAxis;
    [Tooltip("Y Axis (-1, 1)")]
    public float LYAxis;

    [Header("Right Joystick")]
    [Tooltip("X Axis (-1, 1)")]
    public float RXAxis;
    [Tooltip("Y Axis (-1, 1)")]
    public float RYAxis;
    [Tooltip("up pedal Axis (-1, 1)")]
    public float UpAxis;

    [Header("Buttons")]
    public bool LButtonTwo, RButtonTwo;
    public bool LButtonThree, RButtonThree;
    public bool LButtonFour, RButtonFour;
    public bool LButtonFive, RButtonFive;
    public bool LTrigger, RTrigger;

    [Header("Buttons")]
    public bool Switch;
    public bool P2;
    public bool P3;
    public bool P4;
    public bool P5;
    public bool P6;
    public bool P7;

    public float Pedals;

    private PlayerInput input;

    private bool playerOne;

    public static GamepadExpose instance;



    private void Start()
    {
        input = GetComponent<PlayerInput>();
        instance = this;
        playerOne = PlayerInputManager.instance.playerCount == 1 ? true : false;
    }

    public void OnLSX()
    {
        LXAxis = input.actions["LSX"].ReadValue<float>();
        SynchronizeValue();
    }

    public void OnLSY()
    {
        LYAxis = input.actions["LSY"].ReadValue<float>();
        SynchronizeValue();
    }


    public void OnRSX()
    {
        RXAxis = input.actions["RSX"].ReadValue<float>();
        SynchronizeValue();
    }

    public void OnRSY()
    {
        RYAxis = input.actions["RSY"].ReadValue<float>();
        SynchronizeValue();
    }


    public void OnPedals()
    {
        Pedals = input.actions["Pedals"].ReadValue<float>();
        SynchronizeValue();
    }

    public void OnLD()
    {
        LButtonTwo = input.actions["LD"].ReadValue<float>() == 1 ? true : false;
        SynchronizeValue();
    }
    public void OnRD()
    {
        RButtonTwo = input.actions["RD"].ReadValue<float>() == 1 ? true : false;
        SynchronizeValue();
    }

    public void OnLU()
    {
        LButtonThree = input.actions["LU"].ReadValue<float>() == 1 ? true : false;
        SynchronizeValue();
    }
    public void OnRU()
    {
        RButtonThree = input.actions["RU"].ReadValue<float>() == 1 ? true : false;
        SynchronizeValue();
    }

    public void OnLL()
    {
        LButtonFour = input.actions["LL"].ReadValue<float>() == 1 ? true : false;
        SynchronizeValue();
    }
    public void OnRL()
    {
        RButtonFour = input.actions["RL"].ReadValue<float>() == 1 ? true : false;
        SynchronizeValue();
    }

    public void OnLR()
    {
        LButtonFive = input.actions["LR"].ReadValue<float>() == 1 ? true : false;
        SynchronizeValue();
    }
    public void OnRR()
    {
        RButtonFive = input.actions["RR"].ReadValue<float>() == 1 ? true : false;
        SynchronizeValue();
    }

    public void OnLTrigger()
    {
        LTrigger = input.actions["LTrigger"].ReadValue<float>() == 1 ? true : false;
        SynchronizeValue();
    }

    public void OnRTrigger()
    {
        RTrigger = input.actions["RTrigger"].ReadValue<float>() == 1 ? true : false;
        SynchronizeValue();
    }


    public void OnChangePlayer()
    {
        if (PlayerInputManager.instance.playerCount == 2)
        {
            foreach (JoystickExpose jp in GameObject.FindObjectsOfType<JoystickExpose>())
            {
                jp.ChangePlayer();
            }
        }
    }

    private void SynchronizeValue()
    {
        InputExpose.instance.LXAxis = LXAxis;
        InputExpose.instance.LYAxis = LYAxis;
        InputExpose.instance.L2Button = LButtonTwo;
        InputExpose.instance.L3Button = LButtonThree;
        InputExpose.instance.L4Button = LButtonFour;
        InputExpose.instance.L5Button = LButtonFive;
        InputExpose.instance.LTrigger = LTrigger;

        InputExpose.instance.RXAxis = RXAxis;
        InputExpose.instance.RYAxis = RYAxis;
        InputExpose.instance.R2Button = RButtonTwo;
        InputExpose.instance.R3Button = RButtonThree;
        InputExpose.instance.R4Button = RButtonFour;
        InputExpose.instance.R5Button = RButtonFive;
        InputExpose.instance.RTrigger = RTrigger;

        InputExpose.instance.Pedals = Pedals;
    }

    public void ChangePlayer()
    {
        /*playerOne = !playerOne;

        if (GetComponent<JoystickMove>().side == Side.LEFT)
            return;
        //GetComponent<JoystickMove>().side = Side.RIGHT;
        else
            return;
            //GetComponent<JoystickMove>().side = Side.LEFT;*/
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side {
    RIGHT = 0, LEFT = 1
};

public class JoystickMove : MonoBehaviour
{
    [SerializeField]
    private float maxAngle;

    [SerializeField]
    private GameObject handle;

    [SerializeField]
    private Side side;

    // Update is called once per frame
    void Update()
    {
        if(side == Side.LEFT)
        {
            handle.transform.rotation = Quaternion.Euler(Vector3.left * JoystickExpose.instance.LYAxis * maxAngle + Vector3.forward * JoystickExpose.instance.LXAxis * maxAngle);
        }
        if (side == Side.RIGHT)
        {
            handle.transform.rotation = Quaternion.Euler(Vector3.left * JoystickExpose.instance.RYAxis * maxAngle + Vector3.forward * JoystickExpose.instance.RXAxis * maxAngle);
        }
    }
}

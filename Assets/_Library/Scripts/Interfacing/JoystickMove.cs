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
    public Side side;

    // Update is called once per frame
    void Update()
    {
        if(side == Side.LEFT)
        {
            handle.transform.rotation = Quaternion.Euler(Vector3.left * InputExpose.instance.LYAxis * maxAngle + Vector3.forward * InputExpose.instance.LXAxis * maxAngle);
        }
        if (side == Side.RIGHT)
        {
            handle.transform.rotation = Quaternion.Euler(Vector3.left * InputExpose.instance.RYAxis * maxAngle + Vector3.forward * InputExpose.instance.RXAxis * maxAngle);
        }
    }
}

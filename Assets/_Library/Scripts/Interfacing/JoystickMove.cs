using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side
{
    RIGHT = 0, LEFT = 1
};


public enum But
{
    HANDLE = 0,
    BUT01 = 1,
    BUT02 = 2,
    BUT03 = 3,
    BUT04 = 4,
    BUT05 = 5,
    BUT06 = 6,
    BUT07 = 7,
    BUT08 = 8,
    BUT09 = 9,
    BUT10 = 10,
    BUT11 = 11,
    TRIGG = 12,
    LPEDA = 13,
    RPEDA = 14
};

[System.Serializable]
public struct Butt
{
    public But leBouton;
    public GameObject leGameobj;
}

public class JoystickMove : MonoBehaviour
{
    [SerializeField]
    private float maxAngle;

    [SerializeField]
    private GameObject handle;

    [SerializeField]
    private GameObject Lpedal, Rpedal;

    [SerializeField]
    public Side side;

    [SerializeField]
    public List<Butt> buttonList;

    // Update is called once per frame
    void Update()
    {
        if (Rpedal != null)
        {
            if (InputExpose.instance.Pedals > 0)
            {
                Rpedal.transform.localRotation = Quaternion.Euler(Vector3.forward * InputExpose.instance.Pedals * maxAngle + new Vector3(0, 30, -90));
            }
            else if (InputExpose.instance.Pedals < 0)
            {
                Lpedal.transform.localRotation = Quaternion.Euler(Vector3.forward * Mathf.Abs(InputExpose.instance.Pedals) * maxAngle + new Vector3(0, 30, -90));
            }
        }

        if (side == Side.LEFT)
        {

            //handle.transform.localRotation = Quaternion.Euler(Vector3.left * InputExpose.instance.LYAxis * maxAngle + Vector3.forward * InputExpose.instance.LXAxis * maxAngle);
            handle.transform.localRotation = Quaternion.Euler(Vector3.left * InputExpose.instance.LYAxis * maxAngle + -Vector3.forward * InputExpose.instance.LXAxis * maxAngle);
        }
        if (side == Side.RIGHT)
        {
            handle.transform.localRotation = Quaternion.Euler(Vector3.left * InputExpose.instance.RYAxis * maxAngle + Vector3.forward * InputExpose.instance.RXAxis * maxAngle);
        }

        /*if(side == Side.LEFT)
        {
            //handle.transform.rotation = Quaternion.AngleAxis(InputExpose.instance.LXAxis * maxAngle, transform.forward) * transform.up;
            //handle.transform.rotation = Quaternion.AngleAxis(InputExpose.instance.LYAxis * maxAngle, transform.right) * transform.up;
            //handle.transform.localRotation = Quaternion.Euler(Vector3.right * InputExpose.instance.LYAxis * maxAngle + Vector3.forward * InputExpose.instance.LXAxis * maxAngle);
        }
        if (side == Side.RIGHT)
        {
            handle.transform.localRotation = Quaternion.Euler(Vector3.right * InputExpose.instance.RYAxis * maxAngle + Vector3.forward * InputExpose.instance.RXAxis * maxAngle);
        }*/
    }

    public GameObject GetButtByBut(But b)
    {
        foreach(Butt bb in buttonList)
        {
            if (b == bb.leBouton)
                return bb.leGameobj;
        }

        return null;
    }
}

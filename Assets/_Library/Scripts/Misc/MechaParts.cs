using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaParts : MonoBehaviour
{
    [Header("Thrusters")]
    public GameObject leftThruster;
    public GameObject rightThruster;

    [Header("Other")]
    [SerializeField]
    private bool debugIsGrounded;
    
    public bool isGrounded {
        get { return debugIsGrounded; }
        set { 
            debugIsGrounded = value;
            if (value)
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            else
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}

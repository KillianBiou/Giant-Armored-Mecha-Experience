using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    public void ProcessDamage(GameObject target, Armament armament)
    {
        foreach (Transform child in transform)
        {
            if(child.gameObject == target)
            {
                switch (armament)
                {
                    case Armament.GATLING:
                        child.GetComponent<BodyPart>().TakeBullet();
                        break;
                    case Armament.MISSILE:
                        child.GetComponent<BodyPart>().TakeMissile();
                        break;
                    case Armament.RAILGUN:
                        child.GetComponent<BodyPart>().TakeRailgun();
                        break;
                }
            }
        }
    }
}

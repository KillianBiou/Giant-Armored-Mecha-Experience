using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerHandTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.transform.CompareTag("Hand"))
        {
            Debug.Log("Hand");
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Hand"))
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
}

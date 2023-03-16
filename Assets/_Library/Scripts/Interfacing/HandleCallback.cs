using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCallback : MonoBehaviour
{
    [SerializeField]
    private bool enter;

    private void OnTriggerEnter(Collider other)
    {
        if(enter && other.transform.CompareTag("Hand"))
            transform.parent.GetComponentInParent<DebugPrint2>().EnterTrigger(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if(!enter && other.transform.CompareTag("Hand"))
            transform.parent.GetComponentInParent<DebugPrint2>().ExitTrigger(other);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCallback : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<DebugPrint2>().EnterTrigger(other);
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponentInParent<DebugPrint2>().ExitTrigger(other);
    }
}

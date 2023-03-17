using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigg : MonoBehaviour
{
    [SerializeField]
    private UnityEvent ev;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        ev.Invoke();
    }
}

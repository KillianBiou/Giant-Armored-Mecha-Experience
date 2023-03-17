using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AIGolemBehaviour : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(GetComponent<AIData>().transform.position, transform.up);
    }
}

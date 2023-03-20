using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AIGolemBehaviour : MonoBehaviour
{
    private AIData aiData;

    private void Start()
    {
        aiData = GetComponent<AIData>();
    }

    void Update()
    {
        if(aiData.target)
            transform.LookAt(aiData.player.transform, transform.up);
    }
}

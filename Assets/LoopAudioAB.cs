using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopAudioAB : MonoBehaviour
{

    [SerializeField]
    private float A, B;
    
    [SerializeField]
    private AudioSource AS;


    // Update is called once per frame
    void Update()
    {
        if (AS.time >= B)
            AS.time = A;
    }

}

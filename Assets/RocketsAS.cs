using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketsAS : MonoBehaviour
{

    [SerializeField]
    private AudioSource AS;

    private InputExpose IEx;

    [SerializeField]
    private LoopAudioAB abloop;

    [SerializeField]
    private float audioOff;

    private void Start()
    {
        IEx = InputExpose.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!AS.isPlaying && audioOff <= Mathf.Abs(IEx.Pedals))
        {
            abloop.enabled = false;
            AS.Play();
        }
        
        if(AS.isPlaying)
            AS.volume = Mathf.Lerp(0.2f, 1f, Mathf.Abs(IEx.Pedals));

        if (AS.isPlaying && Mathf.Abs(IEx.Pedals) < audioOff)
            abloop.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterAS : MonoBehaviour
{

    [SerializeField]
    private AudioSource AS;

    private InputExpose IEx;

    [SerializeField]
    private LoopAudioAB abloop;

    [SerializeField]
    private Side side;

    [SerializeField]
    private float audioOff;

    private void Start()
    {
        IEx = InputExpose.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(side == Side.LEFT)
        {
            if (!AS.isPlaying && Mathf.Abs(IEx.LYAxis) > 0.0f)
            {
                Debug.Log("Started Left");
                abloop.enabled = true;
                AS.Play();
            }

            if (AS.isPlaying)
                AS.volume = Mathf.Lerp(0f, 0.5f, Mathf.Abs(IEx.LYAxis));

            if (AS.isPlaying && Mathf.Abs(IEx.LYAxis) < audioOff)
            {
                Debug.Log("Ended Left");
                abloop.enabled = false;
            }
        }
        else
        {
            if (!AS.isPlaying && Mathf.Abs(IEx.RYAxis) > 0.0f)
            {
                abloop.enabled = true;
                AS.Play();
            }

            if (AS.isPlaying)
                AS.volume = Mathf.Lerp(0f, 0.8f, Mathf.Abs(IEx.RYAxis));

            if (AS.isPlaying && Mathf.Abs(IEx.RYAxis) < audioOff)
                abloop.enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterAS : MonoBehaviour
{

    [SerializeField]
    private AudioSource AS;

    [SerializeField]
    private JoystickExpose JEx;

    [SerializeField]
    private LoopAudioAB abloop;

    [SerializeField]
    private float audioOff;

    // Update is called once per frame
    void Update()
    {
        if (!AS.isPlaying && JEx.YAxis > 0.0f)
        {
            abloop.enabled = false;
            AS.Play();
        }

        if (AS.isPlaying)
            AS.volume = Mathf.Lerp(0.2f, 1.4f, JEx.YAxis);

        if (-audioOff < JEx.YAxis && JEx.YAxis < audioOff)
            abloop.enabled = false;
    }
}

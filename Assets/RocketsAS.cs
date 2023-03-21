using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketsAS : MonoBehaviour
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
        if (!AS.isPlaying && audioOff <= Mathf.Abs(JEx.Pedals))
        {
            abloop.enabled = false;
            AS.Play();
        }
        
        if(AS.isPlaying)
            AS.volume = Mathf.Lerp(0.2f, 0.5f, Mathf.Abs(JEx.Pedals));

        if (AS.isPlaying && Mathf.Abs(JEx.Pedals) < audioOff)
            abloop.enabled = false;
    }
}

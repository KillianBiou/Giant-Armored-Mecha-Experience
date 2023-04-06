using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VibraSubPack : MonoBehaviour
{
    public static VibraSubPack instance;

    [SerializeField]
    private AudioSource AS;

    [SerializeField]
    private AudioClip[] freqs;

    void Start()
    {
        instance = this;
    }

    public void Vibrate(float intensity, float duration)
    {
        if(intensity == 10.0f)
        {
            AS.clip = freqs[0];
            AS.Play();
            StartCoroutine(Buzz(duration));
        }
    }

    IEnumerator Buzz(float duree)
    {
        while(duree > 0.0f)
        {
            duree -= Time.deltaTime;
            yield return null;
        }
    }
}

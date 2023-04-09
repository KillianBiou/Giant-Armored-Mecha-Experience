using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VibraSubPack : MonoBehaviour
{
    public static VibraSubPack instance;


    [SerializeField]
    private AudioClip[] freqs;

    void Start()
    {
        instance = this;
        VibraSubPack.instance.Vibrate(3.0f, 0);
    }

    public void Vibrate(float duration, int intensity)
    {
        StartCoroutine(Buzz(duration, intensity));
    }

    IEnumerator Buzz(float duree, int freq)
    {
        AudioSource nAS = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        nAS.loop = true;
        nAS.clip = freqs[freq];
        nAS.Play();

        while (duree > 0.0f)
        {
            duree -= Time.deltaTime;
            yield return null;
        }

        Destroy(nAS);
    }
}

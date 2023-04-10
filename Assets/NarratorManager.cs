using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorManager : MonoBehaviour
{
    public static NarratorManager instance;


    [SerializeField]
    private AudioSource AS;


    void Start()
    {
        instance = this;
    }

    public void SayThis(AudioClip AC)
    {
        AS.clip = AC;
        AS.Play();
    }
}

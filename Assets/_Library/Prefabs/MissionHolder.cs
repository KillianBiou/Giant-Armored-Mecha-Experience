using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissionHolder : ScriptableObject
{
    public AudioClip AC;
    public GameObject target;




    public void OnValidate()
    {
        MissionReaderApp.instance.Speaker.clip = AC;
        MissionReaderApp.instance.target = target;
    }
}

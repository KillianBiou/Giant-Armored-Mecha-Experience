using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Mission Holder 0", menuName = "ScriptableObjects/Mission Holder", order = 1)]
public class MissionHolder : ScriptableObject
{
    public AudioClip AC;
    public GameObject target;




    public void OnValidate()
    {
        if(MissionReaderApp.instance != null)
        {
            if (AC != null)
            {
                MissionReaderApp.instance.Speaker.clip = AC;
                MissionReaderApp.instance.Speaker.Play();
            }
            if (target != null)
                MissionReaderApp.instance.target = target;
        }
        
    }
}
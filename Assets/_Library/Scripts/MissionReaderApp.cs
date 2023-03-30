using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionReaderApp : MonoBehaviour
{
    public static MissionReaderApp instance;


    public AudioSource Speaker;

    public GameObject target;

    void Start()
    {
        instance = this;
    }


    public void SetTarget()
    {
        GpsTridi.instance.SetTarget(target);
    }

    public void UnsetTarget()
    {
        GpsTridi.instance.SetTarget(null);
    }
}

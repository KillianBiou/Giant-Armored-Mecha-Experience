using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionReaderApp : MonoBehaviour
{
    public static MissionReaderApp instance;

    [SerializeField]
    public AudioSource Speaker;

    public GameObject target;

    void Start()
    {
        instance = this;
    }






    void SetTarget()
    {
        GpsTridi.instance.SetTarget(target);
    }

    void UnsetTarget()
    {
        GpsTridi.instance.SetTarget(null);
    }
}

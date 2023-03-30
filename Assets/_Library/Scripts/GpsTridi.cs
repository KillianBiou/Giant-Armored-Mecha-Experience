using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GpsTridi : MonoBehaviour
{
    public static GpsTridi instance;

    [SerializeField]
    private LookAtGps gps;

    [SerializeField]
    private GameObject fleche;


    void Start()
    {
        instance = this;
    }

    public void SetTarget(GameObject go)
    {
        if(go != null)
        {
            fleche.SetActive(true);
            gps.target = go.transform;
            gps.enabled = true;
        }
        else
        {
            gps.enabled = false;
            fleche.SetActive(false);
        }

    }


}

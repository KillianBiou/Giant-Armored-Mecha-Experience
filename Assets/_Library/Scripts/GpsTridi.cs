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
        HideGPS();
    }

    public void SetTarget(GameObject go)
    {
        if(go != null)
        {
            ShowGPS();
            gps.target = go.transform;
        }
        else
        {
            HideGPS();
        }
    }

    public void ShowGPS()
    {
        fleche.SetActive(true);
    }

    public void HideGPS()
    {
        fleche.SetActive(false);
    }
}

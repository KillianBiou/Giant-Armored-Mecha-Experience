using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]

public class camaspect : MonoBehaviour
{

    public Camera c;
    public float aspectr;

    // Start is called before the first frame update
    void Start()
    {
        c.aspect = aspectr;
    }

    // Update is called once per frame
    void Update()
    {
        c.aspect = aspectr;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRot : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = Camera.main.transform.rotation;
    }
}

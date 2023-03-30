using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[ExecuteAlways]
public class RenderCameraToCubemap : MonoBehaviour
{
    public RenderTexture rt;
    void LateUpdate()
    {
        GetComponent<Camera>().RenderToCubemap(rt);
    }
}

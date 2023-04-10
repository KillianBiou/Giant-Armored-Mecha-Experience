using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Camera currentCamera;

    private void Start()
    {
        currentCamera.enabled = true;
    }

    public void ChangeCamera(Camera camera)
    {
        currentCamera.enabled = false;

        currentCamera = camera;
        currentCamera.enabled = true;
    }
}

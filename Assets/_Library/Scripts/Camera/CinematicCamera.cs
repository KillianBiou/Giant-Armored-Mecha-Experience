using UnityEngine;

public class CinematicCamera : MonoBehaviour
{
    public void TriggerCamera()
    {
        GetComponentInParent<CameraManager>().ChangeCamera(GetComponentInChildren<Camera>());
    }
}

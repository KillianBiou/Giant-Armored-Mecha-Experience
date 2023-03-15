using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPrint : MonoBehaviour
{
    [SerializeField]
    private HandGrabPose hgb;
    [SerializeField]
    private HandPose handPose;
    [SerializeField]
    private Pose pose;
    [SerializeField]
    private Transform relative;

    private void Update()
    {
        hgb.InjectAllHandGrabPose(relative);
    }
}

using Oculus.Interaction.HandGrab;
using Oculus.Interaction.Input;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class DebugPrint2: MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer handLPlaced;
    [SerializeField]
    private SkinnedMeshRenderer handRPlaced;

    [SerializeField]
    private SkinnedMeshRenderer handReference;

    private bool leftHand = false;

    private void Start()
    {
        handLPlaced.GetComponent<SyntheticHand>().LockWristPosition(handLPlaced.transform.position, 1);
        handLPlaced.GetComponent<SyntheticHand>().LockFingerAtCurrent(HandFinger.Index);
        handLPlaced.GetComponent<SyntheticHand>().LockFingerAtCurrent(HandFinger.Ring);
        handLPlaced.GetComponent<SyntheticHand>().LockFingerAtCurrent(HandFinger.Pinky);
        handLPlaced.GetComponent<SyntheticHand>().LockFingerAtCurrent(HandFinger.Max);
        handLPlaced.GetComponent<SyntheticHand>().SetFingerFreedom(HandFinger.Thumb, JointFreedom.Free);

        handRPlaced.GetComponent<SyntheticHand>().LockWristPosition(handRPlaced.transform.position, 1);
        handRPlaced.GetComponent<SyntheticHand>().LockFingerAtCurrent(HandFinger.Index);
        handRPlaced.GetComponent<SyntheticHand>().LockFingerAtCurrent(HandFinger.Ring);
        handRPlaced.GetComponent<SyntheticHand>().LockFingerAtCurrent(HandFinger.Pinky);
        handRPlaced.GetComponent<SyntheticHand>().LockFingerAtCurrent(HandFinger.Max);
        handRPlaced.GetComponent<SyntheticHand>().SetFingerFreedom(HandFinger.Thumb, JointFreedom.Free);
    }

    public void EnterTrigger(Collider other)
    {
        Debug.Log("Entered");
        if (!handReference)
        {
            Transform hand = other.transform.parent.parent.parent.parent.parent;
            if(hand.name == "RightHand")
            {
                SyntheticHand handComponent = hand.parent.Find("RightHandSynthetic").GetComponent<SyntheticHand>();
                if (handComponent)
                {
                    Debug.Log("RightHand");
                    handReference = handComponent.transform.GetChild(0).GetChild(0).Find("r_handMeshNode").GetComponent<SkinnedMeshRenderer>();
                    handReference.material.SetFloat("_Opacity", 0f);
                    handRPlaced.enabled = true;
                    leftHand = false;
                }
            }
            else
            {
                SyntheticHand handComponent = hand.parent.Find("LeftHandSynthetic").GetComponent<SyntheticHand>();
                if (handComponent)
                {
                    handReference = handComponent.transform.GetChild(0).GetChild(0).Find("l_handMeshNode").GetComponent<SkinnedMeshRenderer>();
                    handReference.material.SetFloat("_Opacity", 0f);
                    handLPlaced.enabled = true;
                    leftHand = true;
                }
            }
        }
    }

    public void ExitTrigger(Collider other)
    {
        if (handReference)
        {
            handReference.material.SetFloat("_Opacity", 0.8f);
            handReference = null;
            if (leftHand)
                handLPlaced.enabled = false;
            else
                handRPlaced.enabled = false;
        }
    }
}
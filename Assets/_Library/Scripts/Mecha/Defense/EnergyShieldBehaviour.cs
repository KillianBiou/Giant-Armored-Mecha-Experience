using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShieldBehaviour : MonoBehaviour
{
    [SerializeField]
    private float protectionOffset;

    public void Initialize(float protectionOffset)
    {
        this.protectionOffset = protectionOffset;
    }

    public float GetProtectionOffset()
    {
        return protectionOffset;
    }
}

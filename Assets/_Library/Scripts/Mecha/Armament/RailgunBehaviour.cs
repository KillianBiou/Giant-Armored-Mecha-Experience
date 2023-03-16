using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RailgunBehaviour : MonoBehaviour
{

    private VisualEffect fx;

    public void Initialize(VisualEffect fx)
    {
        this.fx = fx;
    }

    public void Fire()
    {
        Debug.Log("Railgun is firing");
    }
}

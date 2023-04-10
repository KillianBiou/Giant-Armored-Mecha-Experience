using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaStep : MonoBehaviour
{
    public void StepEv(int a)
    {
        VibraSubPack.instance.Vibrate(0.95f, 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderValue : MonoBehaviour
{
    [SerializeField]
    private string value;

    private Material m;

    private void Start()
    {
        m = GetComponent<Renderer>().material;
    }

    public void Actualize(float val)
    {
        m.SetFloat(value, val);
    }
}

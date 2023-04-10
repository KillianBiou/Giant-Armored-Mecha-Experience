using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class troisD_GUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] elems;


    void Start()
    {
        ShowIt();
    }

    public void ShowIt()
    {
        foreach (GameObject go in elems)
            go.SetActive(true);
    }

    public void HideIt()
    {
        foreach (GameObject go in elems)
            go.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenChanger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Screens;

    private int i = 0;



    void Start()
    {
        foreach (GameObject go in Screens)
            go.SetActive(false);
    }


    public void SwitchScreen(int a)
    {
        if(a >= 0 && a <= Screens.Length && a != i)
        {
            Screens[i].SetActive(false);
            Screens[a].SetActive(true);
            i = a;
        }
    }



}

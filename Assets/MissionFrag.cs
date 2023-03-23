using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissionFrag : MonoBehaviour
{
    public MissionPoints dady;
    public MissionElement[] objs;
    public GameObject walls;
    private int checks;

    void Start()
    {
        checks = 0;
        foreach (MissionElement ME in objs)
        {
            ME.dady = this;
        }
        walls.SetActive(false);
    }

    public void ChkEnd()
    {
        checks++;
        if (checks >= objs.Length)
            dady.ChkEnd();
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissionFrag : MonoBehaviour
{

    public MissionPoints dady;
    public MissionElement[] objs;
    public AudioClip AC;
    private int checks;

    void Start()
    {
        checks = 0;
        foreach (MissionElement ME in objs)
        {
            ME.dady = this;
        }
    }

    public void StartFrag()
    {
        foreach (MissionElement melem in objs)
            melem.gameObject.SetActive(true);



        if (AC != null)
            dady.getMH().AC = AC;
        else
            dady.getMH().AC = null;


        foreach (MissionElement ME  in objs)
        {
            if (ME.gameObject.activeSelf)
            {
                GpsTridi.instance.SetTarget(ME.gameObject);
                //dady.getMH().target = ME.gameObject;
                break;
            }
        }

        dady.getMH().OnValidate();
    }

    public void ChkEnd()
    {
        checks++;
        if (checks >= objs.Length) // fin d'une sequence
            dady.ChkEnd();
    }
}
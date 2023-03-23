using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissionPoints : MonoBehaviour
{
    [SerializeField]
    private bool activateFlymode;
    [SerializeField]
    private bool activateWeapons;

    [SerializeField]
    private MissionHolder MH;

    [SerializeField]
    private AudioClip AC;

    public Mission dady;
    public MissionFrag[] conditions;
    public GameObject walls;
    private int checks;

    void Start()
    {
        checks = 0;
    }

    public void StartPoint()
    {
        conditions[0].StartFrag();
        walls.SetActive(true);
    }


    public void ChkEnd()
    {
        foreach (MissionElement ME in conditions[checks].objs)
        {
            ME.gameObject.SetActive(false);
        }

        checks++;

        if (checks >= conditions.Length)
        {
            if (activateFlymode)
                Activator.instance.ActivateFlymode();
            if (activateWeapons)
                Activator.instance.ActivateWeapon();

            dady.ChkEnd();
            if(walls != null)
                walls.SetActive(false);
        }
        else
        {
            foreach (MissionElement ME in conditions[checks].objs)
            {
                ME.gameObject.SetActive(true);
            }
        }
    }
}

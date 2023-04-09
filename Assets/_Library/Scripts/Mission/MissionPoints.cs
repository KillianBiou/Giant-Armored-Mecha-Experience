using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissionPoints : MonoBehaviour
{
    [SerializeField]
    private bool activateFlymode;
    [SerializeField]
    private bool activateWeapons;

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


    public void ChkEnd() // fin d'une suite
    {
        foreach (MissionElement ME in conditions[checks].objs) // dez la sequence
        {
            if(ME)
                ME.gameObject.SetActive(false);
        }

        checks++; // seq suivante

        if (checks >= conditions.Length) // fin d'une suite ---->
        {
            if (activateFlymode)
                Activator.instance.ActivateFlymode();
            if (activateWeapons)
                Activator.instance.ActivateWeapon();

            dady.ChkEnd();
            if(walls != null)
                walls.SetActive(false);
        }
        else // sequence suivante start
        {
            conditions[checks].StartFrag();
        }
    }

    public MissionHolder getMH()
    {
        return dady.getMH();
    }
}

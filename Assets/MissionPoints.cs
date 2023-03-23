using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissionPoints : MonoBehaviour
{
    [SerializeField]
    private Mission dady;
    public MissionFrag[] conditions;
    private int checks;

    void Start()
    {
        checks = 0;
    }

    public void ChkEnd()
    {
        foreach (MissionElement ME in conditions[checks].objs)
        {
            ME.gameObject.SetActive(false);
        }

        checks++;

        if (checks >= conditions.Length)
            dady.ChkEnd();
        else
        {
            foreach (MissionElement ME in conditions[checks].objs)
            {
                ME.gameObject.SetActive(true);
            }
            conditions[checks].walls.SetActive(true);
        }
    }
}

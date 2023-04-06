using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionElement : MonoBehaviour
{
    [SerializeField]
    public MissionFrag dady;

    [SerializeField]
    private MissionType type;

    private bool check;



    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            dady.ChkEnd();
            this.enabled = false;
        }
    }*/

    public void CallDady()
    {
        if (!check && dady != null)
        {
            check = true;
            dady.ChkEnd();
            this.enabled = false;
        }
    }

    public void ImDead()
    {
        if(!check && dady != null)
        {
            check = true;
            dady.ChkEnd();
            this.enabled = false;
        }
    }
}

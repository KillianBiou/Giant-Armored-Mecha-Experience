using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionElement : MonoBehaviour
{
    [SerializeField]
    public Mission dady;

    [SerializeField]
    private MissionType type;




    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            dady.ChkEnd();
            this.enabled = false;
        }
    }

    public void ImDead()
    {
        dady.ChkEnd();
        this.enabled = false;
    }
}

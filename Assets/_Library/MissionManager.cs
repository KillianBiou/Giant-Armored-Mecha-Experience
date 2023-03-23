using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MissionType
{
    PATH = 0,
    DEFEAT = 1,
    RETRIEVE = 2
}



public class MissionManager : MonoBehaviour
{

    [SerializeField]
    private Mission currentMission;

    [SerializeField]
    private MissionHolder MH;



    void Start()
    {
        currentMission.MissMana = this;
        currentMission.checks = 0;

        foreach (MissionPoints mp in currentMission.chapters)//dez tout les enfants
        {
            if(mp.walls != null)
                mp.walls.SetActive(false);

            foreach (MissionFrag mf in mp.conditions)
            {
                mf.dady = mp;
                foreach(MissionElement go in mf.objs)
                {
                    go.gameObject.SetActive(false);
                }
            }
        }
        /*
        foreach (MissionElement ME in currentMission.chapters[0].conditions[0].objs)
            ME.gameObject.SetActive(true);
        */
        currentMission.chapters[0].StartPoint();//start mission 0
    }




    public void QuestFinish()
    {
        Debug.Log("\n- questline terminado - YOUPI\n");
    }

    public MissionHolder getMH()
    {
        return MH;
    }

}

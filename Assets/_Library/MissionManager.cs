using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MissionType
{
    PATH = 0,
    DEFEAT = 1,
    RETRIEVE = 2
}

public struct MissionPoints
{
    public MissionElement[] conditions;
}

[System.Serializable]
public struct Mission
{
    public MissionManager MissMana;
    public MissionElement[] objectives;
    public int checks;

    public void ChkEnd()
    {
        checks++;
        if (checks >= objectives.Length)
            MissMana.QuestFinish();
    }
}

public class MissionManager : MonoBehaviour
{


    //[SerializeField]
    //private Mission[] missions;

    [SerializeField]
    private Mission currentMission;


    void Start()
    {
        currentMission.MissMana = this;
        currentMission.checks = 0;

        foreach (MissionElement me in currentMission.objectives)
            me.dady = currentMission;
    }

    public void QuestFinish()
    {
        Debug.Log("\n- quete fini - YOUPI\n");
    }

}

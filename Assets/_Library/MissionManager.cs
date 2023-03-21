using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MissionType
{
    PATH = 0,
    DEFEAT = 1,
    RETRIEVE = 2
}

public struct MissionPoint
{
    public MissionElement[] conditions;
}

public struct Mission
{
    public MissionPoint[] objectives;
    public MissionType type;
}

public class MissionManager : MonoBehaviour
{


    [SerializeField]
    private Mission[] missions;

    [SerializeField]
    private Mission currentMission;

}

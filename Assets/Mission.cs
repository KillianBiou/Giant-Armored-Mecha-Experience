using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Mission : MonoBehaviour
{
    [SerializeField]
    public MissionManager MissMana;
    [SerializeField]
    public MissionPoints[] chapters;
    [SerializeField]
    public int checks;



    void Start()
    {
        checks = 0;
    }

    public void ChkEnd() // fin de missions
    {
        checks++;
        if (checks >= chapters.Length)
            MissMana.QuestFinish();
        else
            chapters[checks].StartPoint();
    }

    public MissionHolder getMH()
    {
        return MissMana.getMH();
    }
}
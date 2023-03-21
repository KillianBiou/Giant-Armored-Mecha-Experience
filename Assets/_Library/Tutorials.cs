using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

[System.Serializable]
public struct Tuto
{
    [SerializeField]
    public But[] usableButtonsL;

    [SerializeField]
    public But[] usableButtonsR;

    [SerializeField]
    public string title;

    [SerializeField]
    public VideoClip vid;

    [SerializeField]
    public AudioClip snd;
}

[CreateAssetMenu(fileName = "Tutorial List 0", menuName = "ScriptableObjects/Tutorial list", order = 1)]
public class Tutorials : ScriptableObject
{

    int i = 0; 

    [SerializeField]
    public Tuto[] tutorials;

    public Tuto GetCurrentTuto()
    {
        return tutorials[i];
    }

    public void PrevTuto()
    {
        i--;
        if (i < 0)
            i = tutorials.Length - 1;
    }

    public void NextTuto()
    {
        i++;
        if (i > tutorials.Length-1)
            i = 0;
    }
}

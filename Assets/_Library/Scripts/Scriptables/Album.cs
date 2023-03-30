using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct Single
{
    [SerializeField]
    public string titleTxt;

    [SerializeField]
    public Sprite pochette;

    [SerializeField]
    public AudioClip snd;
}


[CreateAssetMenu(fileName = "Album list 0", menuName = "ScriptableObjects/Albums", order = 1)]
public class Album : ScriptableObject
{

    [SerializeField]
    private Single[] singles;

    private int i = 0;

    public Single GetCurrentSingle()
    {
        return singles[i];
    }

    public void PrevSingle()
    {
        i--;
        if (i < 0)
            i = singles.Length - 1;
    }

    public void NextSingle()
    {
        i++;
        if (i > singles.Length - 1)
            i = 0;
    }


}

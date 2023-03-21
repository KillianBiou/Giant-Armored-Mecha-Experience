using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "BGM list", menuName = "ScriptableObjects/Tutorial list", order = 1)]
public class PlayList : ScriptableObject
{
    [System.Serializable]
    public struct Album
    {
        [SerializeField]
        private string title;

        [SerializeField]
        private Image pochette;

        [SerializeField]
        private AudioClip snd;
    }

    [SerializeField]
    private int i;

    [SerializeField]
    private Album[] laList;



    public Album getNextBGM()
    {
        i++;
        if (i > laList.Length-1)
            i = 0;

        return laList[i];
    }

    public Album getPrevBGM()
    {
        i--;
        if (i < 0)
            i = laList.Length - 1;
        return laList[i];
    }

}

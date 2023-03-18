using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgApp : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sp;

    [SerializeField]
    private Image bg;

    private int i = 0;

    public void prevBG()
    {
        i--;
        if (i < 0)
            i = sp.Length-1;
        bg.sprite = sp[i];
    }

    public void nextBG()
    {
        i++;
        if (i > sp.Length)
            i = 0;
        bg.sprite = sp[i];
    }
}

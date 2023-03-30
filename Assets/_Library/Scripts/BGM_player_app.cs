using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BGM_player_app : MonoBehaviour
{


    [SerializeField]
    private TMP_Text titleTxt;

    [SerializeField]
    private Image jaquette;

    [SerializeField]
    private AudioSource speaker;

    [SerializeField]
    private Album bgms;

    private Single playing;


    private void Start()
    {
        playing = bgms.GetCurrentSingle();
        Actualize();
    }


    public void prevBgm()
    {
        bgms.PrevSingle();
        playing = bgms.GetCurrentSingle();
        Actualize();
    }

    public void nextBgm()
    {
        bgms.NextSingle();
        playing = bgms.GetCurrentSingle();
        Actualize();
    }


    public void playBgm()
    {
        speaker.Play();
    }

    public void pauseBgm()
    {
        speaker.Pause();
    }

    private void Actualize()
    {
        titleTxt.text = playing.titleTxt;
        jaquette.sprite = playing.pochette;
        speaker.clip = playing.snd;

        playBgm();
    }

}

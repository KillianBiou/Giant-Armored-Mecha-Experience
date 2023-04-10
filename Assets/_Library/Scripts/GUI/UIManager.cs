using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Missile")]
    [SerializeField]
    private GameObject missileSlider;

    [Header("Railgun")]
    [SerializeField]
    private GameObject railgunSlider;

    [Header("Misc")]
    [SerializeField]
    private GameObject scoreHolder;
    [SerializeField]
    private TMP_Text timerTxt;
    [SerializeField]
    private Slider timerSlider;

    private float remainingTime;


    void Start()
    {
        instance = this;
        timerTxt.gameObject.transform.parent.gameObject.SetActive(false);
    }



    public void UpdateMissileNumber(int missileLeft)
    {
        missileSlider.transform.Find("ammo text").GetComponent<TextMeshProUGUI>().text = missileLeft.ToString() + "/12";
    }

    public void UpdateMissileCD(float reloadAdvancement)
    {
        missileSlider.GetComponent<Slider>().value = reloadAdvancement;
    }

    public void UpdateRailgun(float reloadAdvancement)
    {
        railgunSlider.GetComponent<Slider>().value = reloadAdvancement;
    }

    public void UpdateScore(float newScore)
    {
        string scoreTxt = "";
        for(int i=0; i < 6 - newScore.ToString().Length; i++)
        {
            scoreTxt += "0";
        }
        scoreTxt += newScore.ToString();
        scoreHolder.transform.Find("Content").GetComponent<TextMeshProUGUI>().text = scoreTxt;
    }


    public void ShowTimer()
    {
        timerTxt.gameObject.transform.parent.gameObject.SetActive(true);
    }

    public void UpdateTimer(int newTime)
    {
        remainingTime = newTime;
        if(newTime % 60 < 10)
            timerTxt.text = "0" + ((newTime - (newTime % 60))/60).ToString() + ":0" + (newTime % 60).ToString();
        else
            timerTxt.text = "0" + ((newTime - (newTime % 60)) / 60).ToString() + ":" + (newTime % 60).ToString();

        timerSlider.value = (float)newTime/150;
    }


    public int GetRemainingTime()
    {
        return (int)remainingTime;
    }
}

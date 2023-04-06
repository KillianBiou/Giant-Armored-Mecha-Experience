using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("Missile")]
    [SerializeField]
    private GameObject missileSlider;

    [Header("Railgun")]
    [SerializeField]
    private GameObject railgunSlider;

    [Header("Misc")]
    [SerializeField]
    private GameObject scoreHolder;

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
        scoreHolder.transform.Find("Content").GetComponent<TextMeshProUGUI>().text = newScore.ToString();
    }
}
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

    public void UpdateMissile(int missileLeft)
    {
        missileSlider.GetComponent<Slider>().value = (float)missileLeft / 12f;
        missileSlider.transform.Find("ammo text").GetComponent<TextMeshProUGUI>().text = missileLeft.ToString() + "/12";
    }

    public void UpdateRailgun(float reloadAdvancement)
    {
        railgunSlider.GetComponent<Slider>().value = reloadAdvancement;
    }
}

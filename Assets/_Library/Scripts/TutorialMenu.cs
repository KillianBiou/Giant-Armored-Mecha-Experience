using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{

    public GameObject[] tutoControll;
    public GameObject[] tutoButtons;

    private int i = 0;

    

    private void OnEnable()
    {
        if(tutoControll.Length > 0)
            tutoButtons[i].GetComponent<Renderer>().materials[0].SetFloat("_Blinking", 1.0f);
    }

    private void OnDisable()
    {
        if (tutoControll.Length > 0)
            tutoButtons[i].GetComponent<Renderer>().materials[0].SetFloat("_Blinking", 0.0f);
    }








    public void PrevTuto()
    {
        tutoControll[i].SetActive(false);
        tutoButtons[i].GetComponent<Renderer>().materials[0].SetFloat("_Blinking", 0.0f);

        i--;
        if (i < 0)
            i = tutoControll.Length-1;

        tutoControll[i].SetActive(true);
        tutoButtons[i].GetComponent<Renderer>().materials[0].SetFloat("_Blinking", 1.0f);
    }


    public void NextTuto()
    {
        tutoControll[i].SetActive(false);
        tutoButtons[i].GetComponent<Renderer>().materials[0].SetFloat("_Blinking", 0.0f);

        i++;
        if (i >= tutoControll.Length)
            i = 0;

        tutoControll[i].SetActive(true);
        tutoButtons[i].GetComponent<Renderer>().materials[0].SetFloat("_Blinking", 1.0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class TutorialMenu : MonoBehaviour
{
    [SerializeField]
    private Tutorials tutos;


    [SerializeField]
    private TMP_Text titleTxt;

    [SerializeField]
    private Image img;

    [SerializeField]
    private AudioSource speaker;


    [SerializeField]
    private JoystickMove LeftController;

    [SerializeField]
    private JoystickMove RightController;
    
    private Tuto currentTuto;
    

    private void OnEnable()
    {
        currentTuto = tutos.GetCurrentTuto();
        Actualize();
    }

    private void OnDisable()
    {
        HideTuto();
    }


    public void PrevTuto()
    {
        HideTuto();

        tutos.PrevTuto();
        currentTuto = tutos.GetCurrentTuto();

        Actualize();
    }


    public void NextTuto()
    {
        HideTuto();

        tutos.NextTuto();
        currentTuto = tutos.GetCurrentTuto();

        Actualize();
    }

    private void HideTuto()
    {
        foreach (But b in currentTuto.usableButtonsL)
            LeftController.GetButtByBut(b).GetComponent<Renderer>().materials[0].SetFloat("_Blinking", 0.0f);
        foreach (But b in currentTuto.usableButtonsR)
            RightController.GetButtByBut(b).GetComponent<Renderer>().materials[0].SetFloat("_Blinking", 0.0f);
    }

    private void Actualize()
    {
        Debug.Log("\n-- j'ai recu mon pote -- \n");
        titleTxt.text = currentTuto.title;
        if (currentTuto.img)
        {
            img.gameObject.SetActive(true);
            img.sprite = currentTuto.img;
        }
        else
            img.gameObject.SetActive(false);

        speaker.clip = currentTuto.snd;
        speaker.Play();

        foreach (But b in currentTuto.usableButtonsL)
            LeftController.GetButtByBut(b).GetComponent<Renderer>().materials[0].SetFloat("_Blinking", 1.0f);
        foreach (But b in currentTuto.usableButtonsR)
            RightController.GetButtByBut(b).GetComponent<Renderer>().materials[0].SetFloat("_Blinking", 1.0f);
    }
}

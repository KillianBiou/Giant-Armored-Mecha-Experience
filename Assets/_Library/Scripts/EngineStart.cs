using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EngineStart : MonoBehaviour
{

    [SerializeField]
    private GameObject bootScreen;
    [SerializeField]
    private TMP_Text bootText;

    [SerializeField]
    private GameObject OsStart;
    [SerializeField]
    private Slider initbar;

    [SerializeField]
    private GameObject Menu;

    [SerializeField]
    private GameObject TpsScreen;
    private Material m_TpsScreen;
    private float fadein = 1;

    [SerializeField]
    private GameObject FieldScreen;
    private Material m_FieldScreen;


    // Start is called before the first frame update
    void Start()
    {
        m_TpsScreen = TpsScreen.GetComponent<Renderer>().materials[0];
        m_FieldScreen = FieldScreen.GetComponent<Renderer>().materials[0];
        StartCoroutine(BootSec());
    }





    IEnumerator BootSec()
    {
        yield return BootScrolling();

        bootScreen.SetActive(false);
        OsStart.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        yield return LoadOS();

        OsStart.SetActive(false);
        Menu.SetActive(true);


        yield return new WaitForSeconds(0.4f);
        fadein = 1.1f;
        yield return DrawGrid();
        yield return new WaitForSeconds(0.2f);
        yield return FadeInTPS();
        yield return new WaitForSeconds(0.5f);



        fadein = 1;
        yield return FadeIn();
        fadein = 0;
        yield return FadeCompensate();



        yield return null;
    }

    IEnumerator BootScrolling()
    {
        bootText.text += "\n Starting the G.A.M.E System";
        yield return new WaitForSeconds(0.5f);
        bootText.text += "\n Init Arms Engine...";
        yield return new WaitForSeconds(0.1f);
        bootText.text += "\n > 100%";
        bootText.text += "\n Init Legs Engine...";
        yield return new WaitForSeconds(0.5f);
        bootText.text += "\n > 100%";
        bootText.text += "\n ---------";
        yield return new WaitForSeconds(0.8f);
        bootText.text += "\n Energy plug System Loading...";
        yield return new WaitForSeconds(0.1f);
        bootText.text += "\n [===";
        yield return new WaitForSeconds(0.1f);
        bootText.text += "==";
        yield return new WaitForSeconds(0.1f);
        bootText.text += "=";
        yield return new WaitForSeconds(0.1f);
        bootText.text += "=";
        yield return new WaitForSeconds(0.2f);
        bootText.text += "=";
        yield return new WaitForSeconds(0.1f);
        bootText.text += "=";
        yield return new WaitForSeconds(0.1f);
        bootText.text += "=";
        yield return new WaitForSeconds(0.1f);
        bootText.text += "=";
        yield return new WaitForSeconds(0.1f);
        bootText.text += "=";
        yield return new WaitForSeconds(0.4f);
        bootText.text += "====]";
        yield return new WaitForSeconds(0.6f);
        bootText.text += "\n";
        yield return new WaitForSeconds(0.2f);
        bootText.text += "\n\nCharging OS sys LVCE_Ver.10.2.46";
        yield return new WaitForSeconds(1.0f);
        bootText.text += "\n\n\n\n\n\n\n\n";
        yield return new WaitForSeconds(0.3f);

        yield return null;
    }


    IEnumerator LoadOS()
    {
        while(initbar.value < 1)
        {
            initbar.value += Time.deltaTime * 0.3f;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }



    IEnumerator DrawGrid()
    {
        while (fadein > 0)
        {
            fadein -= Time.deltaTime * 1f;
            m_TpsScreen.SetFloat("_Grid", fadein);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    IEnumerator FadeInTPS()
    {
        while (fadein < 1)
        {
            fadein += Time.deltaTime * 0.5f;
            m_TpsScreen.SetFloat("_Scanner", fadein);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }







    IEnumerator FadeIn()
    {
        while (fadein > -1)
        {
            fadein -= Time.deltaTime * 1f;
            m_FieldScreen.SetFloat("_down", fadein);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    IEnumerator FadeCompensate()
    {
        while (fadein < 0.5f)
        {
            fadein += Time.deltaTime * 0.5f;
            m_FieldScreen.SetFloat("_compensation", fadein);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}

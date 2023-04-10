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
    private GameObject content;
    [SerializeField]
    private TMP_Text bootText;

    [SerializeField]
    private GameObject OsStart;
    [SerializeField]
    private Slider initbar;

    [SerializeField]
    private Image bg;

    [SerializeField]
    private GameObject Menu;


    [SerializeField]
    private InitializingControlls initctrl;


    // Start is called before the first frame update
    void Start()
    {
        bg.color = new Color(0.0f, 0.0f, 0.0f);
        content.SetActive(false);
    }

    public void enginego()
    {
        StartCoroutine(BootSec());
    }





    IEnumerator BootSec()
    {
        bootScreen.SetActive(true);
        yield return BootScrolling();

        bootScreen.SetActive(false);
        OsStart.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        yield return LoadOS();

        OsStart.SetActive(false);

        yield return FadeBG();

        yield return new WaitForSeconds(0.4f);

        Menu.SetActive(true);

        if(initctrl != null)
            initctrl.BeginStartup();

        yield return null;
    }

    IEnumerator BootScrolling()
    {
        content.SetActive(true);
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

    IEnumerator FadeBG()
    {
        while (bg.color.r < 1)
        {
            bg.color = new Color(bg.color.r + 0.05f, bg.color.r + 0.05f, bg.color.r + 0.05f);
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }

}

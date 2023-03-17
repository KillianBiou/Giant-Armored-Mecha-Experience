using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializingControlls : MonoBehaviour
{

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
        StartCoroutine(InitSeq());
    }


    IEnumerator InitSeq()
    {
        fadein = 1.1f;
        yield return DrawGrid();
        yield return new WaitForSeconds(0.2f);
        yield return FadeInTPS();
        yield return new WaitForSeconds(0.5f);


        fadein = 1;
        yield return FadeIn();
        fadein = 0;
        yield return FadeCompensate();

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

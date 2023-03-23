using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InitializingControlls : MonoBehaviour
{
    [SerializeField]
    private Light mainLight;


    [SerializeField]
    private GameObject TpsScreen;
    private Material m_TpsScreen;
    private float fadein = 1;

    [SerializeField]
    private GameObject FieldScreen;
    private Material m_FieldScreen;



    [SerializeField]
    private PlayerInputManager PIM;
    [SerializeField]
    private PlayerInput PI;

    [SerializeField]
    private EngineStart SoftStartL;
    [SerializeField]
    private EngineStart SoftStartR;

    private bool Estarted = false;



    // Start is called before the first frame update
    void Start()
    {
        m_TpsScreen = TpsScreen.GetComponent<Renderer>().materials[0];
        m_TpsScreen.SetFloat("_Grid", 1.1f);
        m_TpsScreen.SetFloat("_Scanner", 0);
        m_FieldScreen = FieldScreen.GetComponent<Renderer>().materials[0];
        m_FieldScreen.SetFloat("_down", 1);
        m_FieldScreen.SetFloat("_compensation", 0);

        mainLight.intensity = 0;
    }

    void Update()
    {
        if(PIM.playerCount >= 2 && (GameObject.FindObjectsOfType<PlayerInput>()[0].actions["Trigger"].ReadValue<float>() == 1 ? true : false) && !Estarted)
        {
            StartCoroutine(OnLight());
            StartCoroutine(InitSeq());
            Estarted = true;
            SoftStartL.enginego();
            SoftStartR.enginego();
        }
    }



    public void BeginStartup()
    {
        StartCoroutine(OnLight());
    }








    IEnumerator OnLight()
    {
        if (mainLight.intensity < 1.0f)
        {
            mainLight.intensity += 2.0f * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
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

        this.enabled = false;
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
        while (fadein < 0.2f)
        {
            fadein += Time.deltaTime * 0.2f;
            m_FieldScreen.SetFloat("_compensation", fadein);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}

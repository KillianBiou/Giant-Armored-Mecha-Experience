using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBossBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<SkinnedMeshRenderer> desctructibles;
    [SerializeField]
    private Renderer mainBody;
    [SerializeField]
    private float secondPhaseThreshold;
    [SerializeField]
    private Color secondPhaseColor;

    private AIBodyPartsManager m_BodyPartsManager;
    private AIData aiData;
    private bool isEnraged = false;

    private void Start()
    {
        m_BodyPartsManager = GetComponent<AIBodyPartsManager>();
        aiData = GetComponent<AIData>();
    }

    private void Update()
    {
        ProcessInput();
        ProcessPhase();
    }

    private void ProcessPhase()
    {
        float currentFraction = (float)aiData.hp / (float)aiData.maxHP;
        int toIndex = (int)(desctructibles.Count * (1 - currentFraction));
        for(int i = 0; i < toIndex; i++)
        {
            if (desctructibles[i].enabled)
            {
                desctructibles[i].enabled = false;
            }
        }
        
        if(!isEnraged && currentFraction <= secondPhaseThreshold)
        {
            StartCoroutine(Enrage());
        }
    }

    private IEnumerator Enrage()
    {
        Debug.Log("Enrage");
        isEnraged = true;
        for(float i = 0; i < 1; i+=0.01f)
        {
            foreach (Renderer renderer in desctructibles)
            {
                renderer.materials[0].color = Color.Lerp(Color.white, secondPhaseColor, i);
            }
            mainBody.materials[0].color = Color.Lerp(Color.white, secondPhaseColor, i);
            yield return new WaitForEndOfFrame();
        }
        foreach (Renderer renderer in desctructibles)
        {
            renderer.materials[0].color = Color.Lerp(Color.white, secondPhaseColor, 1);
        }
        mainBody.materials[0].color = Color.Lerp(Color.white, secondPhaseColor, 1);
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            UseGatling();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            UseMissile();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            UseRailgun();
        }
    }

    private void UseGatling()
    {
        StartCoroutine(m_BodyPartsManager.ManualFireGatling());
    }

    private void UseMissile()
    {
        m_BodyPartsManager.ManualFireMissile();
    }

    private void UseRailgun()
    {
        m_BodyPartsManager.ManualFireRailgun();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AIBossBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<SkinnedMeshRenderer> desctructibles;
    [SerializeField]
    private Renderer mainBody;
    [SerializeField]
    private Renderer leftArm;
    [SerializeField]
    private Renderer rightArm;
    [SerializeField]
    private float secondPhaseThreshold;
    [SerializeField]
    private float ThirdPhaseThreshold;
    [SerializeField]
    private Color secondPhaseColor;

    private AIBodyPartsManager m_BodyPartsManager;
    private AIData aiData;
    private bool isSecondPhase = false;
    private bool isEnraged = false;
    private Animator animator;

    private bool canFireRailgun = true;

    private void Start()
    {
        m_BodyPartsManager = GetComponent<AIBodyPartsManager>();
        animator = GetComponent<Animator>();
        aiData = GetComponent<AIData>();
    }

    private void Update()
    {
        ProcessInput();
        ProcessPhase();

        if (aiData.target)
        {
            transform.LookAt(aiData.player.transform.position);
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
        }
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

        if (!isSecondPhase && currentFraction <= secondPhaseThreshold)
        {
            SecondPhase();
        }

        if (!isEnraged && currentFraction <= ThirdPhaseThreshold)
        {
            StartCoroutine(Enrage());
        }
    }

    private void SecondPhase()
    {
        isSecondPhase = true;
        leftArm.enabled = false;
    }

    private IEnumerator Enrage()
    {
        isEnraged = true;
        rightArm.enabled = false;
        for (float i = 0; i < 1; i+=0.01f)
        {
            foreach (Renderer renderer in desctructibles)
            {
                renderer.materials[0].color = Color.Lerp(Color.white, secondPhaseColor, i);
            }
            mainBody.materials[0].color = Color.Lerp(Color.white, secondPhaseColor, i);
            leftArm.materials[0].color = Color.Lerp(Color.white, secondPhaseColor, i);
            rightArm.materials[0].color = Color.Lerp(Color.white, secondPhaseColor, i);
            yield return new WaitForEndOfFrame();
        }
        foreach (Renderer renderer in desctructibles)
        {
            renderer.materials[0].color = Color.Lerp(Color.white, secondPhaseColor, 1);
        }
        mainBody.materials[0].color = Color.Lerp(Color.white, secondPhaseColor, 1);
        leftArm.materials[0].color = Color.Lerp(Color.white, secondPhaseColor, 1);
        rightArm.materials[0].color = Color.Lerp(Color.white, secondPhaseColor, 1);
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
            animator.SetTrigger("Fire");
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

    public void UseRailgun()
    {
        if (canFireRailgun)
        {
            canFireRailgun = false;
            m_BodyPartsManager.ManualFireRailgun();
            StartCoroutine(RailgunTimeout());
        }
        else
        {
            canFireRailgun = true;
        }
    }

    private IEnumerator RailgunTimeout()
    {
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("EndFire");
    }
}

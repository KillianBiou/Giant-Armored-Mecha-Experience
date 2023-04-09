using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEditor.Animations;
using UnityEngine;

public class AIBossBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<SkinnedMeshRenderer> desctructibles;
    [SerializeField]
    private List<Transform> desctructiblesExplosionPosition;

    [SerializeField]
    private Renderer mainBody;
    [SerializeField]
    private Renderer leftArm;
    [SerializeField]
    private Renderer rightArm;

    [Header("Threshold")]
    [SerializeField]
    private float secondPhaseThreshold;
    [SerializeField]
    private float ThirdPhaseThreshold;

    [Header("Blink Parameters")]
    [SerializeField]
    private Color blinkColor;
    [SerializeField]
    private float maxBlinkSpeed;
    [SerializeField]
    private float maxBlinkForce;

    [Header("Sounds")]
    [SerializeField]
    private AudioClip partLoose;
    [SerializeField]
    private AudioClip armorShred;

    [Header("Explosion")]
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private GameObject explosionBigPrefab;
    [SerializeField]
    private GameObject explosionFinalPrefab;

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

        foreach (Renderer renderer in desctructibles)
        {
            renderer.materials[0].SetColor("_Fresnel_Color", blinkColor);
        }

        mainBody.materials[0].SetColor("_Fresnel_Color", blinkColor);

        leftArm.materials[0].SetColor("_Fresnel_Color", blinkColor);

        rightArm.materials[0].SetColor("_Fresnel_Color", blinkColor);
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
                StartCoroutine(PlaySound(armorShred, 0.0f));

                 GameObject temp = Instantiate(explosionPrefab, desctructiblesExplosionPosition[i]);
                var shape = temp.GetComponent<ParticleSystem>().shape;
                shape.skinnedMeshRenderer = desctructibles[i];
            }
        }

        if (!isSecondPhase && currentFraction <= secondPhaseThreshold)
        {
            SecondPhase();
        }

        if (!isEnraged && currentFraction <= ThirdPhaseThreshold)
        {
            Enrage();
        }

        foreach (Renderer renderer in desctructibles)
        {
            renderer.materials[0].SetColor("_Fresnel_Color", blinkColor);
        }

        mainBody.materials[0].SetColor("_Fresnel_Color", blinkColor);

        leftArm.materials[0].SetColor("_Fresnel_Color", blinkColor);

        rightArm.materials[0].SetColor("_Fresnel_Color", blinkColor);

        UpdateBlink(currentFraction);
    }

    private void UpdateBlink(float percentage)
    {
        foreach (Renderer renderer in desctructibles)
        {
            renderer.materials[0].SetFloat("_Speed", (1 - percentage) * maxBlinkSpeed);
            renderer.materials[0].SetFloat("_Force", (1 - percentage) * maxBlinkForce);
        }
        mainBody.materials[0].SetFloat("_Speed", (1 - percentage) * maxBlinkSpeed);
        mainBody.materials[0].SetFloat("_Force", (1 - percentage) * maxBlinkForce);

        leftArm.materials[0].SetFloat("_Speed", (1 - percentage) * maxBlinkSpeed);
        leftArm.materials[0].SetFloat("_Force", (1 - percentage) * maxBlinkForce);

        rightArm.materials[0].SetFloat("_Speed", (1 - percentage) * maxBlinkSpeed);
        rightArm.materials[0].SetFloat("_Force", (1 - percentage) * maxBlinkForce);
    }

    private void SecondPhase()
    {
        isSecondPhase = true;
        leftArm.enabled = false;
        animator.SetTrigger("PartDown");

        GameObject temp = Instantiate(explosionBigPrefab);
        var shape = temp.GetComponent<ParticleSystem>().shape;
        shape.skinnedMeshRenderer = leftArm.GetComponent<SkinnedMeshRenderer>();

        StartCoroutine(PlaySound(partLoose, 0.5f));
    }

    private void Enrage()
    {
        isEnraged = true;
        rightArm.enabled = false;
        animator.SetTrigger("PartDown");

        GameObject temp = Instantiate(explosionBigPrefab);
        var shape = temp.GetComponent<ParticleSystem>().shape;
        shape.skinnedMeshRenderer = rightArm.GetComponent<SkinnedMeshRenderer>();

        StartCoroutine(PlaySound(partLoose, 0.5f));
    }

    private IEnumerator PlaySound(AudioClip clip, float volume)
    {
        AudioSource tempClip = gameObject.AddComponent<AudioSource>();
        tempClip.clip = clip;
        tempClip.maxDistance = 10;
        tempClip.volume = volume;
        tempClip.spatialBlend = 1f;
        tempClip.playOnAwake = false;
        tempClip.Play();
        yield return new WaitForSeconds(clip.length);
        Destroy(tempClip);
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

    private void OnDestroy()
    {
        Instantiate(explosionFinalPrefab, transform.position, Quaternion.identity);

    }
}

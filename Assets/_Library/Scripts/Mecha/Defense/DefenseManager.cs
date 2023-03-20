using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DefenseType
{
    ENERGY_SHIELD = 0,
    POINT_DEFENSE = 1,
    NONE = -1,
}

public class DefenseManager : MonoBehaviour
{
    [Header("General Parameters")]
    [SerializeField]
    private DefenseType currentDefense;

    [SerializeField]
    private float swapCooldown;

    [Header("Point Defense Parameters")]
    [SerializeField]
    private float detectionRadius;
    [SerializeField]
    private GameObject laserBeam;
    [SerializeField]
    private Transform laserStart;
    [SerializeField]
    private float laserDuration;

    [Header("EnergyShield Parameters")]
    [SerializeField]
    private float protectionOffset;

    private DefenseType nextDefense = DefenseType.NONE;
    private float currentCooldown;

    private void Start()
    {
        ChangeDefense(DefenseType.ENERGY_SHIELD);
        currentDefense = DefenseType.ENERGY_SHIELD;
    }

    private void ScheduleChange(DefenseType newDefense)
    {
        if (newDefense == currentDefense || nextDefense != DefenseType.NONE)
            return;

        nextDefense = newDefense;
        currentCooldown = swapCooldown;

        switch (currentDefense)
        {
            case DefenseType.ENERGY_SHIELD:
                Destroy(GetComponent<EnergyShieldBehaviour>());
                break;
            case DefenseType.POINT_DEFENSE:
                Destroy(GetComponent<PointDefenseBehaviour>());
                break;
        }
    }

    private void ChangeDefense(DefenseType newDefense) {
        switch (newDefense)
        {
            case DefenseType.ENERGY_SHIELD:
                EnergyShieldBehaviour rShield = gameObject.AddComponent<EnergyShieldBehaviour>();
                rShield.Initialize(protectionOffset);
                break;
            case DefenseType.POINT_DEFENSE:
                PointDefenseBehaviour rPoint = gameObject.AddComponent<PointDefenseBehaviour>();
                rPoint.Initialize(detectionRadius, laserBeam, laserStart, laserDuration);
                break;
        }

        currentDefense = nextDefense;
        nextDefense = DefenseType.NONE;
    }

    private void Update()
    {
        bool changeDefense = InputExpose.instance.R5Button || InputExpose.instance.L5Button;

        if (changeDefense && currentDefense == DefenseType.ENERGY_SHIELD){
            ScheduleChange(DefenseType.POINT_DEFENSE);
        }
        if (changeDefense && currentDefense == DefenseType.POINT_DEFENSE)
        {
            ScheduleChange(DefenseType.ENERGY_SHIELD);
        }

        if (nextDefense != DefenseType.NONE && currentCooldown >= 0f)
        {
            currentCooldown -= Time.deltaTime;

            if(currentCooldown <= 0f)
            {
                ChangeDefense(nextDefense);
            }
        }
    }
}

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

    private DefenseType nextDefense = DefenseType.NONE;
    private float currentCooldown;

    private void Start()
    {
        ChangeDefense(DefenseType.POINT_DEFENSE);
        currentDefense = DefenseType.POINT_DEFENSE;
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
                break;
            case DefenseType.POINT_DEFENSE:
                PointDefenseBehaviour rPoint = gameObject.AddComponent<PointDefenseBehaviour>();
                rPoint.Initialize(detectionRadius);
                break;
        }

        currentDefense = nextDefense;
        nextDefense = DefenseType.NONE;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)){
            ScheduleChange(DefenseType.ENERGY_SHIELD);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ScheduleChange(DefenseType.POINT_DEFENSE);
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

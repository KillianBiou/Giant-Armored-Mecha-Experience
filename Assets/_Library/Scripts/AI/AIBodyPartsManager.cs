using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBodyPartsManager : MonoBehaviour
{
    [Header("Armament Inventory")]
    [SerializeField]
    private List<MissileBehaviour> missiles = new List<MissileBehaviour>();

    [SerializeField]
    private List<RailgunBehaviour> railguns = new List<RailgunBehaviour>();

    [SerializeField]
    private List<GatlingBehaviour> gatlings = new List<GatlingBehaviour>();


    [Header("Weapon Parameters")]
    [Header("Missile")]
    [SerializeField]
    private float missileThrustFactor;
    [SerializeField]
    private float missileCooldown;
    [SerializeField]
    private int missileDamage;
    [SerializeField]
    private int missileArmorShred;
    [SerializeField]
    private GameObject missile;
    [SerializeField]
    private GameObject missileTrail;
    [SerializeField]
    private GameObject missileExplosion;

    [Header("Bullet")]
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private int bulletSeconds;
    [SerializeField]
    private float burstDuration;
    [SerializeField]
    private float burstCooldown;
    [SerializeField]
    private int bulletDamage;
    [SerializeField]
    private GameObject bullet;

    [Header("Railgun")]
    [SerializeField]
    private float railgunCooldown;
    [SerializeField]
    private int railgunDamage;
    [SerializeField]
    private GameObject railgunVFX;

    [Header("Debug Parameters")]
    [SerializeField]
    private GameObject target;

    private int nbCouroutine;

    private float burstStarted;

    public void Register(BodyPart bodyPart)
    {
        switch (bodyPart.data.weapon)
        {
            case Armament.RAILGUN:
                RailgunBehaviour railgunBehaviour = bodyPart.GetComponent<RailgunBehaviour>();
                railgunBehaviour.Initialize(railgunVFX, railgunDamage);
                railguns.Add(railgunBehaviour);
                break;
            case Armament.MISSILE:
                MissileBehaviour missileBehaviour = bodyPart.GetComponent<MissileBehaviour>();
                missileBehaviour.Initialize(missile, missileThrustFactor, missileExplosion, missileDamage, missileArmorShred, false);
                missiles.Add(missileBehaviour);
                break;
            case Armament.GATLING:
                GatlingBehaviour gatlingBehaviour = bodyPart.GetComponent<GatlingBehaviour>();
                gatlingBehaviour.Initialize(bullet, bulletSpeed, bulletSeconds, bulletDamage);
                gatlings.Add(gatlingBehaviour);
                break;
        }
    }

    private void Start()
    {
        if (gatlings.Count > 0)
            StartCoroutine(FireGatling());
        if (missiles.Count > 0)
            StartCoroutine(FireMissile());
        if (railguns.Count > 0)
            StartCoroutine(FireRailgun());
    }

    private void Update()
    {
        if(target && nbCouroutine == 0)
        {
            if (gatlings.Count > 0)
                StartCoroutine(FireGatling());
            if (missiles.Count > 0)
                StartCoroutine(FireMissile());
            if (railguns.Count > 0)
                StartCoroutine(FireRailgun());
        }
    }

    private IEnumerator FireGatling()
    {
        nbCouroutine++;
        burstStarted = Time.time;
        Debug.Log("AI Fire gatling burst");

        while (Time.time < burstStarted + burstDuration) { 
            foreach(GatlingBehaviour gatlingBehaviour in gatlings)
            {
                if (target)
                    gatlingBehaviour.Fire(target);
            }
            yield return new WaitForSeconds(1 / bulletSeconds);
        }

        yield return new WaitForSeconds(burstCooldown);

        nbCouroutine--;
        if(target)
            StartCoroutine(FireGatling());
    }

    private IEnumerator FireMissile()
    {
        nbCouroutine++;
        Debug.Log("AI Fire missile");
        foreach(MissileBehaviour missileBehaviour in missiles)
        {
            if (target)
                missileBehaviour.Fire(target);
        }

        yield return new WaitForSeconds(missileCooldown);

        nbCouroutine--;
        if (target)
            StartCoroutine(FireMissile());
    }

    private IEnumerator FireRailgun()
    {
        nbCouroutine++;
        Debug.Log("AI Fire railgun");
        foreach(RailgunBehaviour railgunBehaviour in railguns)
        {
            if (target)
                railgunBehaviour.Fire(target);
        }

        yield return new WaitForSeconds(railgunCooldown);

        nbCouroutine--;
        if (target)
            StartCoroutine(FireRailgun());
    }
}

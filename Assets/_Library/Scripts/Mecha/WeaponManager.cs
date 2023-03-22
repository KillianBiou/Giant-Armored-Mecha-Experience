using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponManager : MonoBehaviour
{
    [Header("Armament Inventory")]
    [SerializeField]
    private List<MissileBehaviour> missiles = new List<MissileBehaviour>();

    [SerializeField]
    private List<RailgunBehaviour> railguns = new List<RailgunBehaviour>();

    [SerializeField]
    private List<GatlingBehaviour> gatlings = new List<GatlingBehaviour>();

    [Header("Missile Parameters")]
    [SerializeField]
    private float missileThrustFactor;
    [SerializeField]
    private GameObject missileTrail;
    [SerializeField]
    private GameObject missileExplosion;
    [SerializeField]
    private int missileDamage;
    [SerializeField]
    private int missileArmorShred;

    [Header("Bullet Parameters")]
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private int bulletPerSecond;
    [SerializeField]
    private int bulletDamage;

    [Header("Railgun Parameters")]
    [SerializeField]
    private GameObject railgunVFX;
    [SerializeField]
    private float railgunCooldown;
    [SerializeField]
    private int railgunDamage;

    [Header("Projectile Prefab")]
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject missile;
    [SerializeField]
    private float missileCooldown;

    [Header("Debug Parameters")]
    [SerializeField]
    private GameObject target;

    private int missileIndex = 0;
    private bool canMissile = true;
    private bool canRailgun = true;
    private bool gatlingMemory = false;
    private GameObject dummyTarget;

    static int ClampLoop(int min, int max, int value)
    {
        if (value < min)
            return min;
        if (value > max)
            return ClampLoop(min, max, value - (max + 1));
        return value;
    }

    private void Start()
    {
        dummyTarget = transform.Find("DummyTarget").gameObject;
    }

    public void RegisterArmament(BodyPart armament) {
        switch (armament.data.weapon)
        {
            case Armament.RAILGUN:
                RailgunBehaviour railgunBehaviour = armament.GetComponent<RailgunBehaviour>();
                railgunBehaviour.Initialize(railgunVFX, railgunDamage);
                railguns.Add(railgunBehaviour);
                break;
            case Armament.MISSILE:
                MissileBehaviour missileBehaviour = armament.GetComponent<MissileBehaviour>();
                missileBehaviour.Initialize(missile, missileThrustFactor, missileExplosion, missileDamage, missileArmorShred);
                missiles.Add(missileBehaviour);
                break;
            case Armament.GATLING:
                GatlingBehaviour gatlingBehaviour = armament.GetComponent<GatlingBehaviour>();
                gatlingBehaviour.Initialize(bullet, bulletSpeed, bulletPerSecond, bulletDamage);
                gatlings.Add(gatlingBehaviour);
                break;
        }
        GetComponent<MechaParts>().Register(armament);
    }

    public void UnregisterArmament(BodyPart armament)
    {
        switch (armament.data.weapon)
        {
            case Armament.RAILGUN:
                railguns.Remove(armament.GetComponent<RailgunBehaviour>());
                break;
            case Armament.MISSILE:
                missiles.Remove(armament.GetComponent<MissileBehaviour>());
                break;
            case Armament.GATLING:
                gatlings.Remove(armament.GetComponent<GatlingBehaviour>());
                break;
        }
        GetComponent<MechaParts>().Unregister(armament);
    }

    public void FireGatling()
    {
        GameObject tar;
        if (!target)
            tar = dummyTarget;
        else
            tar = target;

        foreach(GatlingBehaviour bodyPart in gatlings)
        {
            bodyPart.Fire(tar);
        }
    }

    public void FireMissile()
    {
        missileIndex = WeaponManager.ClampLoop(0, missiles.Count - 1, missileIndex + 1);

        GameObject tar;
        if (!target)
            tar = dummyTarget;
        else
            tar = target;

        try
        {
            missiles[missileIndex].Fire(tar);
        }
        catch
        {
            FireMissile();
        }
    }

    public void FireRailgun()
    {
        GameObject tar;
        if (!target)
            tar = dummyTarget;
        else
            tar = target;

        foreach (RailgunBehaviour bodyPart in railguns)
        {
            bodyPart.Fire(tar);
        }
    }

    private void Update()
    {
        bool gatling = InputExpose.instance.LTrigger || InputExpose.instance.RTrigger;
        bool missile = InputExpose.instance.L3Button;
        bool railgun = InputExpose.instance.R3Button;

        if (missile && canMissile)
        {
            FireMissile();
            canMissile = false;
            StartCoroutine(RefreshMissile());
        }
        if (gatling)
        {
            FireGatling();
            if(!gatlingMemory)
            {
                gatlingMemory = true;
                Boureau.instance.SetGatling(true);
            }
        }
        if (railgun && canRailgun)
        {
            FireRailgun();
            canRailgun = false;
            StartCoroutine(RefreshRailgun());
        }

        if (!gatling && gatlingMemory)
        {
            gatlingMemory = false;
            Boureau.instance.SetGatling(false);
        }
    }

    private IEnumerator RefreshMissile()
    {
        yield return new WaitForSeconds(missileCooldown);
        canMissile = true;
    }

    private IEnumerator RefreshRailgun()
    {
        yield return new WaitForSeconds(railgunCooldown);
        canRailgun = true;
    }

    public void SetTarget(GameObject target)
    {
        if(!(GetComponent<MechaParts>().controllerType == ControllerType.COMBAT_CONTROLLER))
            this.target = target;
    }

    public GameObject GetTarget()
    {
        return target;
    }
}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    [Header("Bullet Parameters")]
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private int bulletPerSecond;

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

    static int ClampLoop(int min, int max, int value)
    {
        if (value < min)
            return min;
        if (value > max)
            return ClampLoop(min, max, value - (max + 1));
        return value;
    }

    public void RegisterArmament(BodyPart armament) {
        switch (armament.data.weapon)
        {
            case Armament.RAILGUN:
                RailgunBehaviour railgunBehaviour = armament.GetComponent<RailgunBehaviour>();
                railgunBehaviour.Initialize(null);
                railguns.Add(railgunBehaviour);
                break;
            case Armament.MISSILE:
                MissileBehaviour missileBehaviour = armament.GetComponent<MissileBehaviour>();
                missileBehaviour.Initialize(missile, missileThrustFactor, missileExplosion);
                missiles.Add(missileBehaviour);
                break;
            case Armament.GATLING:
                GatlingBehaviour gatlingBehaviour = armament.GetComponent<GatlingBehaviour>();
                gatlingBehaviour.Initialize(bullet, bulletSpeed, bulletPerSecond);
                gatlings.Add(gatlingBehaviour);
                break;
        }
    }

    public void FireGatling()
    {
        foreach(GatlingBehaviour bodyPart in gatlings)
        {
            bodyPart.Fire(target);
        }
    }

    public void FireMissile()
    {
        missiles[missileIndex].Fire(target);
        missileIndex = WeaponManager.ClampLoop(0, missiles.Count - 1, missileIndex+1);
    }

    public void FireRailgun()
    {
        foreach (RailgunBehaviour bodyPart in railguns)
        {
            bodyPart.Fire();
        }
    }

    private void Update()
    {
        bool gatling = InputExpose.instance.LTrigger;
        bool missile = InputExpose.instance.L3Button;

        if (missile && canMissile)
        {
            FireMissile();
            canMissile = false;
            StartCoroutine(RefreshMissile());
        }
        if (gatling)
        {
            FireGatling();
        }
    }

    private IEnumerator RefreshMissile()
    {
        yield return new WaitForSeconds(missileCooldown);
        canMissile = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{

    private GameObject missile;

    private int missileLeft;

    private float thrustFactor;

    private GameObject explosionEffect;

    private bool depleteMunition = true;

    private int damage;
    private int armorShred;
    private int maxMissile;

    public void Initialize(GameObject missile, float thrustFactor, GameObject explosionEffect, int damage, int armorShred, bool depleteMunition = true, int maxMissile = 6)
    {
        this.missile = missile;
        this.thrustFactor = thrustFactor;
        this.explosionEffect = explosionEffect;
        this.depleteMunition = depleteMunition;
        this.damage = damage;
        this.armorShred = armorShred;
        this.maxMissile = maxMissile;
        missileLeft = maxMissile;
    }

    public void Fire(GameObject target)
    {
        if(missileLeft > 0)
        {
            GameObject IMissile = Instantiate(missile, transform.Find((maxMissile - missileLeft).ToString()).transform.position, transform.Find((maxMissile - missileLeft).ToString()).transform.rotation);
            IMissile.GetComponent<MissileLogic>().InitilizeMissile(target, thrustFactor, explosionEffect, damage, armorShred);
            missileLeft--;
            if (!depleteMunition && missileLeft == 0)
                missileLeft = maxMissile;
        }
    }
}

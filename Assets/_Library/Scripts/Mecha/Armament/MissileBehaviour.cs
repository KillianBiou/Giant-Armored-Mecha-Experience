using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{

    private GameObject missile;

    private int missileLeft = 6;

    private float thrustFactor;

    private GameObject explosionEffect;

    private bool depleteMunition = true;

    private int damage;
    private int armorShred;

    public void Initialize(GameObject missile, float thrustFactor, GameObject explosionEffect, int damage, int armorShred , bool depleteMunition = true)
    {
        this.missile = missile;
        this.thrustFactor = thrustFactor;
        this.explosionEffect = explosionEffect;
        this.depleteMunition = depleteMunition;
        this.damage = damage;
        this.armorShred = armorShred;
    }

    public void Fire(GameObject target)
    {
        if(missileLeft > 0)
        {
            GameObject IMissile = Instantiate(missile, transform.Find((6 - missileLeft).ToString()).transform.position, transform.Find((6 - missileLeft).ToString()).transform.rotation);
            IMissile.GetComponent<MissileLogic>().InitilizeMissile(target, thrustFactor, explosionEffect, damage, armorShred);
            if(depleteMunition)
                missileLeft--;
        }
    }
}

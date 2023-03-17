using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{

    private GameObject missile;

    private int missileLeft = 6;

    private float thrustFactor;

    private GameObject explosionEffect;

    public void Initialize(GameObject missile, float thrustFactor, GameObject explosionEffect)
    {
        this.missile = missile;
        this.thrustFactor = thrustFactor;
        this.explosionEffect = explosionEffect;
    }

    public void Fire(GameObject target)
    {
        if(missileLeft > 0)
        {
            Debug.Log("Missile is firing");
            GameObject IMissile = Instantiate(missile, transform.Find((6 - missileLeft).ToString()).transform.position, missile.transform.localRotation);
            IMissile.GetComponent<MissileLogic>().InitilizeMissile(target, thrustFactor, explosionEffect);
            missileLeft--;
        }
    }
}

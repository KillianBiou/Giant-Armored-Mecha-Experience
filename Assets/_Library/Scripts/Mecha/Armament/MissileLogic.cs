using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLogic : MonoBehaviour
{
    private GameObject target;
    private float accelerationForce;

    private GameObject explosionEffect;

    private int damage;
    private int armorShred;

    public void InitilizeMissile(GameObject target, float accelerationForce, GameObject explosionEffect, int damage, int armorShred)
    {
        this.target = target;
        this.accelerationForce = accelerationForce;
        this.explosionEffect = explosionEffect;
        this.damage = damage;
        this.armorShred = armorShred;

        StartCoroutine(ArmProjectile());
        StartCoroutine(OutOfFuel());
    }

    private IEnumerator ArmProjectile()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.layer = LayerMask.NameToLayer("ArmedProjectile");
    }

    private void FixedUpdate()
    {
        if (!target)
            DestroyMissile();
        else
        {
            Vector3 direction = transform.position - target.transform.position;

            direction.Normalize();

            Vector3 rotateAmount = Vector3.Cross(direction, transform.forward);

            GetComponent<Rigidbody>().angularVelocity = rotateAmount * 15;
            GetComponent<Rigidbody>().velocity = transform.forward * accelerationForce;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<BoxCollider>().enabled = false;

        MechaParts player = collision.gameObject.GetComponent<MechaParts>();
        if (player)
        {
            Debug.Log("feziojiojiojiojiojiojiojiojiojiojiojiojiojiojiojiojiojioj");
            player.ProcessDamage(target, Armament.MISSILE, damage, armorShred);
        }

        AIData ai = collision.gameObject.GetComponent<AIData>();
        if (ai)
        {
            ai.TakeMissile(damage, armorShred);
        }

        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator OutOfFuel()
    {
        yield return new WaitForSeconds(10f);
        DestroyMissile();
    }

    public void DestroyMissile()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

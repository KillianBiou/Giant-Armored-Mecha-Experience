using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private GameObject target;
    private float accelerationForce;

    private bool collided = false;
    private int damage;

    public void InitilizeBullet(GameObject target, float accelerationForce, int damage)
    {
        this.target = target;
        this.accelerationForce = accelerationForce;
        this.damage = damage;
        StartCoroutine(ArmProjectile());
        StartCoroutine(OutOfFuel());
    }

    private IEnumerator ArmProjectile()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.layer = LayerMask.NameToLayer("ArmedProjectile");
    }

    private void FixedUpdate()
    {
        if (!target)
            DestroyBullet();
        else
        {
            transform.LookAt(target.transform.position);
            GetComponent<Rigidbody>().velocity = transform.forward * accelerationForce;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collided)
        {
            Debug.Log("BULLET COLLIDE");
            accelerationForce = 0;
            collided = true;

            GetComponent<BoxCollider>().enabled = false;

            MechaParts player = collision.gameObject.GetComponent<MechaParts>();
            if (player)
            {
                player.ProcessDamage(target, Armament.GATLING, damage);
            }

            AIData ai = collision.gameObject.GetComponent<AIData>();
            if (ai)
            {
                ai.TakeBullet(damage);
            }

            Destroy(gameObject);
        }
    }

    private IEnumerator OutOfFuel()
    {
        yield return new WaitForSeconds(5f);
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject, 1f);
    }
}

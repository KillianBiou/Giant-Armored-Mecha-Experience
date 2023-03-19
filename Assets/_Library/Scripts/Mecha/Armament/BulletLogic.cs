using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private GameObject target;
    private float accelerationForce;

    private bool collided = false;

    public void InitilizeBullet(GameObject target, float accelerationForce)
    {
        this.target = target;
        this.accelerationForce = accelerationForce;
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
            accelerationForce = 0;
            collided = true;

            GetComponent<BoxCollider>().enabled = false;

            MechaParts player = collision.gameObject.GetComponent<MechaParts>();
            if (player)
            {
                player.ProcessDamage(target, Armament.GATLING);
            }

            AIData ai = collision.gameObject.GetComponent<AIData>();
            if (ai)
            {
                ai.TakeBullet();
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

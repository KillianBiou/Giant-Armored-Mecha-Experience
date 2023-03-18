using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLogic : MonoBehaviour
{
    private GameObject target;
    private float accelerationForce;

    private GameObject explosionEffect;

    public void InitilizeMissile(GameObject target, float accelerationForce, GameObject explosionEffect)
    {
        this.target = target;
        this.accelerationForce = accelerationForce;
        this.explosionEffect = explosionEffect;

        StartCoroutine(ArmProjectile());
    }

    private IEnumerator ArmProjectile()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.layer = LayerMask.NameToLayer("ArmedProjectile");
    }

    private void FixedUpdate()
    {
        Vector3 direction = transform.position - target.transform.position;

        direction.Normalize();

        Vector3 rotateAmount = Vector3.Cross(direction, transform.forward);

        GetComponent<Rigidbody>().angularVelocity = rotateAmount * 10;
        GetComponent<Rigidbody>().velocity = transform.forward * accelerationForce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void DestroyMissile()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

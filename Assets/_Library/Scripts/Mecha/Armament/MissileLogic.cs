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
    }

    private void FixedUpdate()
    {
        Vector3 direction = transform.position - target.transform.position;

        direction.Normalize();

        Vector3 rotateAmount = Vector3.Cross(direction, transform.forward);

        GetComponent<Rigidbody>().angularVelocity = rotateAmount * 5;
        GetComponent<Rigidbody>().velocity = transform.forward * accelerationForce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

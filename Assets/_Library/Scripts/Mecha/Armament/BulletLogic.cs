using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private GameObject target;
    private float accelerationForce;

    public void InitilizeBullet(GameObject target, float accelerationForce)
    {
        this.target = target;
        this.accelerationForce = accelerationForce;
    }

    private void FixedUpdate()
    {
        transform.LookAt(target.transform.position);
        GetComponent<Rigidbody>().velocity = transform.forward * accelerationForce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        accelerationForce = 0;
        Destroy(gameObject, GetComponentInChildren<TrailRenderer>().time);
    }
}

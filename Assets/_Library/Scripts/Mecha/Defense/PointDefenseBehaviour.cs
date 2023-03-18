using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDefenseBehaviour : MonoBehaviour
{
    private float detectionRadius;
    
    public void Initialize(float detectionRadius)
    {
        this.detectionRadius = detectionRadius;
    }

    private void Update()
    {
        Debug.Log(LayerMask.NameToLayer("ArmedProjectile"));
        foreach(Collider collider in Physics.OverlapSphere(transform.position, detectionRadius))
        {
            if(collider.gameObject.layer == LayerMask.NameToLayer("ArmedProjectile"))
            {
                collider.GetComponent<MissileLogic>().DestroyMissile();
                Destroy(collider.gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }
}

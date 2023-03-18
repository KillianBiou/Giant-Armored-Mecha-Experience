using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDefenseBehaviour : MonoBehaviour
{
    private float detectionRadius;
    private GameObject laserBeam;
    private Transform laserStart;
    private float laserDuration;

    public void Initialize(float detectionRadius, GameObject laserBeam, Transform laserStart, float laserDuration)
    {
        this.detectionRadius = detectionRadius;
        this.laserBeam = laserBeam;
        this.laserStart = laserStart;
        this.laserDuration = laserDuration;
    }

    private void Update()
    {
        foreach(Collider collider in Physics.OverlapSphere(transform.position, detectionRadius))
        {
            if(collider.gameObject.layer == LayerMask.NameToLayer("ArmedProjectile"))
            {
                
                GameObject beam = Instantiate(laserBeam);
                StartCoroutine(beam.GetComponent<LineSetup>().Initialize(laserStart, collider.transform.position));
                Destroy(beam, laserDuration);
                collider.GetComponent<MissileLogic>().DestroyMissile();
                Destroy(collider.gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawSphere(transform.position, detectionRadius);
    }
}

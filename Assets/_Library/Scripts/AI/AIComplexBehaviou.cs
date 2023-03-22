using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AIComplexBehaviou : MonoBehaviour
{

    [SerializeField]
    private float movementRadius;
    [SerializeField]
    private float minPatrolDistance;
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;


    private bool fetchNew = false;
    private bool wait;
    private Vector3 initialPosition;
    private Vector3 targetPos;
    private AIData aiData;

    private void Start()
    {
        initialPosition = transform.position;
        targetPos = transform.position;
        aiData = GetComponent<AIData>();
    }

    private void Update()
    {
        if (!aiData.target)
        {
            if (!fetchNew && Vector3.Distance(targetPos, transform.position) <= 5)
            {
                StartCoroutine(FetchCooldown());
                fetchNew = true;
            }
            else if (Vector3.Distance(targetPos, transform.position) >= 5 && !wait)
            {
                fetchNew = false;
                Vector3 direction = transform.position - targetPos;

                direction.Normalize();

                Vector3 rotateAmount = Vector3.Cross(direction, transform.forward);

                GetComponent<Rigidbody>().angularVelocity = rotateAmount * rotationSpeed;

                if (rotateAmount.magnitude <= 0.5)
                {
                    GetComponent<Rigidbody>().AddForce(-direction * speed);
                }
            }
        }
        else
        {

        }
    }

    private IEnumerator FetchCooldown()
    {
        wait = true;
        yield return new WaitForSeconds(waitTime);
        Vector3 newPos;
        do
        {
            newPos = GenerateNewTargetPatrol();
        } while (Vector3.Distance(transform.position, newPos) <= minPatrolDistance);
        targetPos = newPos;
        wait = false;
    }

    private Vector3 GenerateNewTargetPatrol()
    {
        return initialPosition + Random.insideUnitSphere * movementRadius;
    }
}

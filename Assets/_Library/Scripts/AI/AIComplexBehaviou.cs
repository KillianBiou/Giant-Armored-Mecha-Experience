using System.Collections;
using UnityEngine;

public class AIComplexBehaviou : MonoBehaviour
{

    /*[SerializeField]
    private float movementRadius;
    [SerializeField]
    private float minPatrolDistance;
    [SerializeField]
    private float waitTime;*/
    [SerializeField]
    private float speed;
    /*[SerializeField]
    private float rotationSpeed;*/

    [SerializeField]
    private float preferedDistance;

    private float currentX;
    private float currentY;

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
        currentX = 0;
        currentY = 0;
    }

    private void Update()
    {
        transform.LookAt(aiData.player.transform.position, transform.up);
        if(Vector3.Distance(aiData.player.transform.position, transform.position) >= preferedDistance)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

            currentY += 0.05f;
            currentY = Mathf.Clamp(currentY, -1, 1);
            GetComponent<Animator>().SetFloat("Y", currentY);
        }
        else if(Vector3.Distance(aiData.player.transform.position, transform.position) <= preferedDistance * 0.8f)
        {
            transform.Translate(-transform.forward * speed * Time.deltaTime, Space.World);
            
            currentY -= 0.05f;
            currentY = Mathf.Clamp(currentY, -1, 1);
            GetComponent<Animator>().SetFloat("Y", currentY);
        }
        else
        {
            if(currentY > 0)
            {
                currentY -= 0.05f;
                currentY = Mathf.Clamp(currentY, -1, 1);
            }
            else if(currentY < 0)
            {
                currentY += 0.05f;
                currentY = Mathf.Clamp(currentY, -1, 1);
            }
            if (Mathf.Abs(currentY) <= 0.1)
                currentY = 0;
            GetComponent<Animator>().SetFloat("Y", currentY);
        }

        /*if (!aiData.target)
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

        }*/


    }

    /*private IEnumerator FetchCooldown()
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
    }*/
}

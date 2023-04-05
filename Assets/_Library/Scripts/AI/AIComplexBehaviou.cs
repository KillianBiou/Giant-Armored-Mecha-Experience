using System.Collections;
using UnityEngine;

public class AIComplexBehaviou : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float preferedDistance;

    [SerializeField]
    private float glidStart;
    [SerializeField]
    private float glidRange;
    [SerializeField]
    private LayerMask layers;
    [SerializeField]
    private float gravityFactor;

    private float currentX;
    private float currentY;

    private bool fetchNew = false;
    private bool wait;
    private Vector3 initialPosition;
    private Vector3 targetPos;
    private AIData aiData;
    private Rigidbody rb;

    private void Start()
    {
        initialPosition = transform.position;
        targetPos = transform.position;
        aiData = GetComponent<AIData>();
        rb = GetComponent<Rigidbody>();
        currentX = 0;
        currentY = 0;
    }

    private void Update()
    {
        GravityPull();
        Movement();
    }

    private void Movement()
    {
        transform.LookAt(aiData.player.transform.position, transform.up);

        Vector3 playerPosition = aiData.player.transform.position;
        playerPosition.y = 0;
        Vector3 currentPosition = transform.position;
        currentPosition.y = 0;

        float distance = Vector3.Distance(playerPosition, currentPosition);

        if (distance >= aiData.detectionRange)
        {
            return;
        }

        if (distance >= preferedDistance)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

            currentY += 0.05f;
            currentY = Mathf.Clamp(currentY, -1, 1);
            GetComponent<Animator>().SetFloat("Y", currentY);
        }
        else if (distance <= preferedDistance * 0.8f)
        {
            transform.Translate(-transform.forward * speed * Time.deltaTime, Space.World);

            currentY -= 0.05f;
            currentY = Mathf.Clamp(currentY, -1, 1);
            GetComponent<Animator>().SetFloat("Y", currentY);
        }
        else
        {
            if (currentY > 0)
            {
                currentY -= 0.05f;
                currentY = Mathf.Clamp(currentY, -1, 1);
            }
            else if (currentY < 0)
            {
                currentY += 0.05f;
                currentY = Mathf.Clamp(currentY, -1, 1);
            }
            if (Mathf.Abs(currentY) <= 0.1)
                currentY = 0;
            GetComponent<Animator>().SetFloat("Y", currentY);
        }

        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0));
    }

    private void GravityPull()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, -Vector3.up * 100);
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity, layers))
        {
            float distance = Vector3.Distance(transform.position, hit.point);
            Debug.Log(distance);

            if (distance <= glidStart)
            {
                rb.AddForce(transform.up * gravityFactor);
            }
            else if (distance < glidStart + glidRange)
            {
                rb.useGravity = false;
            }
            else
            {
                rb.useGravity = true;
            }
        }
    }
}

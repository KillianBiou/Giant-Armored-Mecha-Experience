using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class HeadTracking : MonoBehaviour
{
    [SerializeField]
    private float radius;

    [SerializeField]
    private float distance;

    [SerializeField]
    private float offset;

    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private LayerMask targetLayer;

    [SerializeField]
    private Vector2 YClamp;

    [SerializeField]
    private Vector2 XClamp;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private GameObject lockTarget;

    [SerializeField]
    private float maxAngle;

    [SerializeField]
    private WeaponManager weaponManager;

    private GameObject targetGO;

    private void Start()
    {
        Debug.Log(target.transform.GetChild(0).name);
        target.transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(radius * 2, radius * 2, radius * 2);
    }

    private void Update()
    {
        if (IsInBound())
        {
            target.SetActive(true);
            List<Collider> hit = Sort(Physics.OverlapSphere(transform.position, distance, layerMask));

            if (hit.Count > 0)
            {
                bool stop = false;
                for(int i = 0; i < hit.Count && !stop; i++)
                {
                    /*if (TargetInBound(Quaternion.FromToRotation(transform.forward, hit[i].transform.position - transform.position)))
                    {*/

                        RaycastHit castHit;
                        if (Physics.Raycast(transform.position, hit[i].transform.position - transform.position, out castHit, distance, targetLayer))
                        {
                            if (castHit.transform.gameObject.layer == LayerMask.NameToLayer("UI"))
                            {
                                OnTargetFound(hit[i].gameObject);

                                lockTarget.SetActive(true);
                                lockTarget.transform.position = castHit.point;
                                stop = true;
                            }
                            else
                            {
                                SetTarget(null);
                                lockTarget.SetActive(false);
                            }
                        }
                        else
                        {
                            lockTarget.SetActive(false);
                            SetTarget(null);
                        }
                        Debug.DrawRay(transform.position, (hit[i].transform.position - transform.position) * 10);
                    /*}
                    else
                    {
                        lockTarget.SetActive(false);
                        SetTarget(null);
                    }*/
                }
            }
            else
            {
                lockTarget.SetActive(false);
                SetTarget(null);
            }
        }
        else
        {
            lockTarget.SetActive(false);
            target.SetActive(false);
            SetTarget(null);
        }
    }

    private void OnTargetFound(GameObject target)
    {
        SetTarget(target);
    }

    private bool TargetInBound(Quaternion rawAngle)
    {
        if (rawAngle.eulerAngles.y <= maxAngle * radius)
        {
            if (rawAngle.eulerAngles.x <= maxAngle * radius)
                return true;
            if (rawAngle.eulerAngles.x - 360 >= -maxAngle * radius)
                return true;
            return false;
        }
        if (rawAngle.eulerAngles.y - 360 >= -maxAngle * radius)
        {
            if (rawAngle.eulerAngles.x <= maxAngle * radius)
                return true;
            if (rawAngle.eulerAngles.x - 360 >= -maxAngle * radius)
                return true;
            return false;
        }
        return false;
    }

    private bool IsInBound()
    {/*
        if(transform.parent.localRotation.eulerAngles.y <= YClamp.y)
        {
            return true;
        }
        if (transform.parent.localRotation.eulerAngles.y - 360 >= YClamp.x)
        {
            return true;
        }
        if (transform.parent.localRotation.eulerAngles.x <= YClamp.x)
        {
            return true;
        }
        if (transform.parent.localRotation.eulerAngles.x - 360 >= YClamp.y)
        {
            return true;
        }*/
        return true;
    }

    private void SetTarget(GameObject target)
    {
        if(!target && !targetGO)
        {
            return;
        }
        if(target != targetGO)
        {
            targetGO = target;
            weaponManager.SetTarget(targetGO);
        }

        if (targetGO)
        {
            if (targetGO.GetComponent<AIData>())
                lockTarget.transform.GetChild(0).GetComponentInChildren<Slider>().value = (float)targetGO.GetComponent<AIData>().hp / (float)targetGO.GetComponent<AIData>().maxHP;
            if(targetGO.GetComponent<ManagerReference>())
                lockTarget.transform.GetChild(0).GetComponentInChildren<Slider>().value = (float)targetGO.GetComponent<ManagerReference>().data.GetComponent<AIData>().hp / (float)targetGO.GetComponent<ManagerReference>().data.GetComponent<AIData>().maxHP;
        }
    }

    public List<Collider> Sort(Collider[] items)
    {
        List<(Collider, float)> distances = new List<(Collider, float)>();
        foreach (Collider item in items)
        {
            float distance = Vector3.Distance(item.transform.position, transform.position);
            distances.Add((item, distance));
        }

        // Sort the list of tuples by distance.
        distances.Sort((a, b) => a.Item2.CompareTo(b.Item2));

        List<Collider> sortedItems = new List<Collider>();
        foreach ((Collider, float) distance in distances)
        {
            sortedItems.Add(distance.Item1);
        }

        return sortedItems;
    }
}

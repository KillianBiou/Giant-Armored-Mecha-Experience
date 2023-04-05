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
            Collider[] hit = Physics.OverlapSphere(transform.position, distance, layerMask);
            if (hit.Length > 0)
            {
                bool stop = false;
                for(int i = 0; i < hit.Length && !stop; i++)
                {
                    /*if (TargetInBound(Quaternion.FromToRotation(transform.forward, hit[i].transform.position - transform.position)))
                    {*/

                        RaycastHit castHit;
                        if (Physics.Raycast(transform.position, hit[i].transform.position - transform.position, out castHit, distance))
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
    {
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
        }
        return false;
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
        lockTarget.transform.GetChild(0).GetComponentInChildren<Slider>().value = (float)target.GetComponent<AIData>().hp / (float)target.GetComponent<AIData>().maxHP;
    }
}

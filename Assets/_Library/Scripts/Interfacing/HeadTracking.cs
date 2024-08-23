using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using static OVRPlugin;

public class HeadTracking : MonoBehaviour
{
    [SerializeField]
    private float aimRadius;

    [SerializeField]
    private float distance;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Vector2 YClamp;

    [SerializeField]
    private Vector2 XClamp;

    [SerializeField]
    private GameObject visorUI;

    [SerializeField]
    private GameObject lockTarget;

    [SerializeField]
    private float maxAngle;

    [SerializeField]
    private WeaponManager weaponManager;

    private GameObject targetGO;
    /*
    private void Start()
    {
        Debug.Log(visorUI.transform.GetChild(0).name);
        visorUI.transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(aimRadius * 2, aimRadius * 2, aimRadius * 2);
    }*/

    private void Update()
    {
        if (!IsGazeInBound())
        {
            visorUI.SetActive(false);
            SetTarget(null);
            return;
        }

        visorUI.SetActive(true);
        Collider[] hit = Physics.OverlapSphere(transform.position, distance, layerMask);
        if (hit.Length < 1)
        {
            SetTarget(null);
            return;
        }

        GameObject bestTarget = null;
        float bestDot = -1;

        for (int i = 0; i < hit.Length; i++)
        {
            float res = Vector3.Dot(transform.forward, (hit[i].transform.position - transform.position).normalized);
            if (res > 1-(aimRadius/90) && res > bestDot)
            {
                bestTarget = hit[i].gameObject;
                bestDot = res;
            }
        }
        SetTarget(bestTarget);
    }

    private void OnTargetFound(GameObject target)
    {
        SetTarget(target);
    }

    private bool IsGazeInBound()
    {
        Vector3 rot = transform.parent.localEulerAngles;
        if (rot.x > XClamp.y && rot.x < 360 - XClamp.x)
        {
            return false;
        }
        if (rot.y > YClamp.y && rot.y < 360 - YClamp.x)
        {
            return false;
        }
        return true;
    }

    private void SetTarget(GameObject newTarget)
    {
        if(newTarget == null)
        {
            lockTarget.SetActive(false);
            return;
        }

        weaponManager.SetTarget(newTarget);

        lockTarget.SetActive(true);
        lockTarget.transform.LookAt(newTarget.transform);


        AIData targetData = newTarget.transform.GetComponentInChildren<AIData>();

        if (targetData != null)
            lockTarget.transform.GetChild(0).GetComponentInChildren<Slider>().value = (float)targetData.hp / (float)targetData.maxHP;
    }
}

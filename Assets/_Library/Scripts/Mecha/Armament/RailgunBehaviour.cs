using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

public class RailgunBehaviour : MonoBehaviour
{

    private GameObject fx;
    private int railgunDamage;

    public void Initialize(GameObject fx, int railgunDamage)
    {
        this.fx = fx;
        this.railgunDamage = railgunDamage;
    }

    public void Fire(GameObject target)
    {
        GameObject railgunEffect = Instantiate(fx, transform.GetChild(0).position, Quaternion.identity, transform);

        EnergyShieldBehaviour es = null;

        GameObject t = null;

        if (target.GetComponent<BodyPart>())
        {
            if (target.GetComponent<BodyPart>().weaponManager)
                target.GetComponent<BodyPart>().weaponManager.TryGetComponent<EnergyShieldBehaviour>(out es);
        }
        else if(target.GetComponent<AIData>())
        {
            target.TryGetComponent<EnergyShieldBehaviour>(out es);
        }

        if(!es)
        {
            Debug.Log("Fire");
            railgunEffect.transform.GetChild(0).transform.position = target.transform.position;

            BodyPart player;
            if (target.TryGetComponent<BodyPart>(out player))
            {
                player.TakeRailgun(railgunDamage);
            }

            AIData ai;
            if (target.TryGetComponent<AIData>(out ai))
            {
                ai.TakeRailgun(railgunDamage);
            }
        }
        StartCoroutine(UpdatePos(target, railgunEffect, es));
    }

    private IEnumerator UpdatePos(GameObject target, GameObject effect, EnergyShieldBehaviour es = null)
    {
        while (effect)
        {
            effect.transform.GetChild(0).transform.position = transform.GetChild(0).position + (target.transform.position - transform.position).normalized * (Vector3.Distance(transform.position, target.transform.position) - (es ? es.GetProtectionOffset() : 0));
            yield return new WaitForEndOfFrame();
        }
    }
}

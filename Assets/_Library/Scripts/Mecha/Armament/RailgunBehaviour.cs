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
    private AudioSource fireSound;

    public void Initialize(GameObject fx, int railgunDamage, AudioClip fireSound)
    {
        this.fx = fx;
        this.railgunDamage = railgunDamage;
        this.fireSound = gameObject.AddComponent<AudioSource>();
        this.fireSound.clip = fireSound;
        this.fireSound.volume = 3;
    }

    public void Fire(GameObject target)
    {
        GameObject railgunEffect = Instantiate(fx, transform.GetChild(0).position, Quaternion.identity);

        fireSound.Play();

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
            //railgunEffect.transform.GetChild(0).transform.position = target.transform.position;

            BodyPart player;
            if (target.TryGetComponent<BodyPart>(out player))
            {
                player.weaponManager.GetComponent<MechaParts>().ProcessDamage(target, Armament.RAILGUN, railgunDamage);
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
            //effect.transform.GetChild(0).transform.position = transform.GetChild(0).position + (target.transform.position - transform.position).normalized * (Vector3.Distance(transform.position, target.transform.position) - (es ? es.GetProtectionOffset() : 0));
            if(target)
            {
                effect.transform.LookAt(target.transform);
                effect.transform.localScale = Vector3.one + Vector3.forward * Vector3.Distance(target.transform.position, effect.transform.position) / 15;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}

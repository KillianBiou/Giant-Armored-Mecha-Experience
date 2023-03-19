using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RailgunBehaviour : MonoBehaviour
{

    private GameObject fx;

    public void Initialize(GameObject fx)
    {
        this.fx = fx;
    }

    public void Fire(GameObject target)
    {
        Debug.Log("Railgun is firing");

        GameObject railgunEffect = Instantiate(fx, transform.GetChild(0).position, Quaternion.identity);
        railgunEffect.transform.GetChild(0).transform.position = target.transform.position;

        BodyPart player;
        if (target.TryGetComponent<BodyPart>(out player))
        {
            player.TakeRailgun();
        }

        AIData ai;
        if (target.TryGetComponent<AIData>(out ai))
        {
            ai.TakeRailgun();
        }
    }
}

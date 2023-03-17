using System.Collections;
using UnityEngine;

public class MissileAI : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    public GameObject missile;

    [SerializeField]
    private Transform missileLuncher;

    [SerializeField]
    private float thrustFactor;

    [SerializeField]
    private float cooldown;

    [SerializeField]
    public GameObject explosionEffect;



    private void Start()
    {
        StartCoroutine(Fire());
    }

    public IEnumerator Fire()
    {
        GameObject IMissile = Instantiate(missile, missileLuncher.position, missileLuncher.localRotation);
        IMissile.GetComponent<MissileLogic>().InitilizeMissile(target, thrustFactor, explosionEffect);
        yield return new WaitForSeconds(cooldown);
        StartCoroutine(Fire());
    }
}

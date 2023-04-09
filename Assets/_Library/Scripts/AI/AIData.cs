using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIData : MonoBehaviour
{
    public int maxHP;
    [SerializeField]
    private float armor;

    [SerializeField]
    private int scoreOnDeath;

    public int detectionRange;
    public GameObject player;
    public GameObject target;

    [SerializeField]
    private GameObject destoyedVFX;

    [DoNotSerialize]
    public int hp;

    private void Start()
    {
        hp = maxHP;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void TakeBullet(int damage)
    {
        int amount = (int)((100f - armor) / 100f * damage);
        SubstractHP(amount);
    }

    public void TakeMissile(int damage, int armorShred)
    {
        SubstractArmor(armorShred);
        SubstractHP(damage);
    }
    public void TakeRailgun(int damage)
    {
        Debug.Log("Took railgiun AI");
        SubstractHP(damage);
    }

    private void SubstractArmor(int amount)
    {
        armor = Mathf.Clamp(armor - amount, 0, 100);
    }

    private void SubstractHP(int amount)
    {
        hp = Mathf.Clamp(hp - amount, 0, maxHP);
        if (hp <= 0)
            DestroyEnemy();
    }

    private void DestroyEnemy()
    {
        player.GetComponent<MechaParts>().AddScore(scoreOnDeath);
        if(destoyedVFX != null)
            Instantiate(destoyedVFX, transform.position, transform.rotation);
        MissionElement me;
        if ((me = gameObject.GetComponent<MissionElement>()) != null)
            me.ImDead();
        Destroy(gameObject);
    }

}

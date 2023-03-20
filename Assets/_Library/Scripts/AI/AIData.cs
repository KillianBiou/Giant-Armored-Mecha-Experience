using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIData : MonoBehaviour
{
    [SerializeField]
    private int maxHP;
    [SerializeField]
    private float armor;

    private int hp;

    private void Start()
    {
        hp = maxHP;
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
        Destroy(gameObject);
    }

}

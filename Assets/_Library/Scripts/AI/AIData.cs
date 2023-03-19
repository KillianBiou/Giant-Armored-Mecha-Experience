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

    public void TakeBullet()
    {
        int amount = (int)((100f - armor) / 100f * 10);
        hp -= amount;
    }

    public void TakeMissile()
    {

    }
    public void TakeRailgun()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MemberPart
{
    HEAD = 0,
    TORSO = 1,
    ARM = 2,
    LEG = 3,
}

public enum Armament
{
    RAILGUN = 0,
    GATLING = 1,
    MISSILE = 2,
    NONE = -1,
}

[System.Serializable]
public struct MemberData
{
    public MemberPart part;
    public Armament weapon;
    public float maxHp;
    public float hp;
    public float maxArmor;
    public float armor;
}

public class BodyPart : MonoBehaviour
{
    [SerializeField]
    public MemberData data;

    private void Start()
    {
        data.hp = data.maxHp;
        data.armor = data.maxArmor;

        switch (data.weapon)
        {
            case Armament.RAILGUN:
                gameObject.AddComponent<RailgunBehaviour>();
                break;
            case Armament.MISSILE:
                gameObject.AddComponent<MissileBehaviour>();
                break;
            case Armament.GATLING:
                gameObject.AddComponent<GatlingBehaviour>();
                break;
        }

        if (data.weapon != Armament.NONE)
        {
            WeaponManager weaponManager;
            transform.parent.TryGetComponent<WeaponManager>(out weaponManager);
            if (weaponManager)
                weaponManager.RegisterArmament(this);
            else
            {
                AIBodyPartsManager aiManager;
                transform.parent.TryGetComponent<AIBodyPartsManager>(out aiManager);
                if (aiManager)
                    aiManager.Register(this);
            }
        }
    }

    public void TakeBullet()
    {
        int amount = (int)((100f - data.armor) / 100f * 5);
        SubstractHP(amount);
    }

    public void TakeMissile()
    {
        data.armor = Mathf.Clamp(data.armor - 10, 0, data.maxArmor);
        SubstractHP(20);
    }
    public void TakeRailgun()
    {
        SubstractHP(50);
    }

    private void SubstractHP(int amount)
    {
        data.hp = Mathf.Clamp(data.hp - amount, 0, data.maxHp);
        if(data.hp <= 0)
            DisablePart();
    }

    private void DisablePart()
    {
        WeaponManager weaponManager;
        transform.parent.TryGetComponent<WeaponManager>(out weaponManager);
        if (weaponManager)
            weaponManager.UnregisterArmament(this);
        gameObject.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MemberPart
{
    TORSO = 0,
    LEFT_ARM = 1,
    RIGHT_ARM = 2,
    LEFT_LEG = 3,
    RIGHT_LEG = 4,
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
    public MemberData data;
    public WeaponManager weaponManager;
    public AIBodyPartsManager aiManager;

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
            if (weaponManager)
                weaponManager.RegisterArmament(this);
            if(aiManager)
                aiManager.Register(this);
        }
    }

    public void TakeBullet(int damage)
    {
        int amount = (int)((100f - data.armor) / 100f * damage);
        SubstractHP(amount);
    }

    public void TakeMissile(int damage, int armorShred)
    {
        SubstractArmor(armorShred);
        SubstractHP(damage);
    }
    public void TakeRailgun(int damage)
    {
        SubstractHP(damage);
    }

    private void SubstractArmor(int amount)
    {
        data.armor = Mathf.Clamp(data.armor - amount, 0, data.maxArmor);
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

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
    public float maxHP;
}

public class BodyPart : MonoBehaviour
{
    [SerializeField]
    public MemberData data;

    [SerializeField]
    private float currentHP;

    private void Start()
    {
        currentHP = data.maxHP;

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
            GetComponentInParent<WeaponManager>().RegisterArmament(this);
    }

}

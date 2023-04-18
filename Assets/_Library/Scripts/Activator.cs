using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public static Activator instance;



    [SerializeField]
    private GroundController GndCtrl;
    [SerializeField]
    private SpaceController SpCtrl;
    [SerializeField]
    private WeaponManager WpnManager;
    [SerializeField]
    private DefenseManager DefManager;


    void Start()
    {
        instance = this;
        WpnManager.enabled = false;
        GndCtrl.enabled = false;
    }

    public void ActivateMove()
    {
        GndCtrl.enabled = true;
    }

    public void ActivateFlymode()
    {
        GndCtrl.canFly = true;
    }


    public void ActivateWeapon()
    {
        WpnManager.enabled = true;
    }

    public void ActivateDefennse()
    {
        DefManager.enabled = true;
    }


}

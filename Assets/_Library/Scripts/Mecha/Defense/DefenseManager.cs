using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DefenseType
{
    ENERGY_SHIELD = 0,
    POINT_DEFENSE = 1,
}

public class DefenseManager : MonoBehaviour
{
    [SerializeField]
    private DefenseType currentDefense;

    [SerializeField]
    private float swapCooldown;

    private IEnumerator DefenseChange(DefenseType newDefense)
    {
        yield return new WaitForSeconds(swapCooldown);

        switch(currentDefense)
        {
            case DefenseType.ENERGY_SHIELD:
                break;
            case DefenseType.POINT_DEFENSE:
                break;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown()
    }
}

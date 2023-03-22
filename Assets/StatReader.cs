using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class StatReader : MonoBehaviour
{


    [SerializeField]
    private Slider S_body, S_L_arm, S_R_arm, S_L_leg, S_R_leg;
    [SerializeField]
    private Slider SA_body, SA_L_arm, SA_R_arm, SA_L_leg, SA_R_leg;

    [SerializeField]
    private MechaParts MechParts;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Actualize(MemberPart mp, float hp, float armor)
    {
        switch (mp)
        {
            case MemberPart.TORSO:
                S_body.value = hp;
                SA_body.value = armor;
                break;
            case MemberPart.LEFT_ARM:
                S_L_arm.value = hp;
                SA_L_arm.value = armor;
                break;
            case MemberPart.RIGHT_ARM:
                S_R_arm.value = hp;
                SA_R_arm.value = armor;
                break;
            case MemberPart.LEFT_LEG:
                S_L_leg.value = hp;
                SA_L_leg.value = armor;
                break;
            case MemberPart.RIGHT_LEG:
                S_R_leg.value = hp;
                SA_R_leg.value = armor;
                break;
        }
    }
}

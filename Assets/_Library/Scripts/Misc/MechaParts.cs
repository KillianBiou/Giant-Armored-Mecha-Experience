using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum ControllerType
{
    SPACE_CONTROLLER = 0,
    COMBAT_CONTROLLER = 1,
    GROUND_CONTROLLER = 2,
};

public class MechaParts : MonoBehaviour
{
    [Header("Pilot Parameters")]
    public string name;

    [Header("Thrusters")]
    public GameObject leftThruster;
    public GameObject rightThruster;

    [Header("Other")]
    [SerializeField]
    private bool debugIsGrounded;
    public ControllerType controllerType;
    [SerializeField]
    private UIManager UIManager;

    public Animator mechaAnim;

    public List<BodyPart> bodyParts = new List<BodyPart>();

    private int score;
    public static MechaParts instance;
    private bool changeCooldown = false;

    private void Awake()
    {
        instance = this;
    }

    public bool isGrounded {
        get { return debugIsGrounded; }
        set {
            debugIsGrounded = value;
            if (value)
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            else
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }

    public void Register(BodyPart part)
    {
        bodyParts.Add(part);
    }

    public void Unregister(BodyPart part)
    {
        bodyParts.Remove(part);
    }

    public void ProcessDamage(GameObject target, Armament armament, int damage, int armorShred = 0)
    {
        foreach (BodyPart child in bodyParts)
        {
            if (child.gameObject == target)
            {
                switch (armament)
                {
                    case Armament.GATLING:
                        child.GetComponent<BodyPart>().TakeBullet(damage);
                        break;
                    case Armament.MISSILE:
                        child.GetComponent<BodyPart>().TakeMissile(damage, armorShred);
                        //Boureau.instance.RegisterViber(20, 500);
                        //Boureau.instance.Airblow(20);
                        break;
                    case Armament.RAILGUN:
                        child.GetComponent<BodyPart>().TakeRailgun(damage);
                        //Boureau.instance.RegisterViber(30, 1000);
                        //Boureau.instance.Airblow(80);
                        break;
                }
            }
        }
    }

    public void ChangeControllerType(ControllerType newType)
    {
        if (newType == controllerType || changeCooldown)
            return;

        switch (newType)
        {
            case ControllerType.SPACE_CONTROLLER:
                gameObject.GetComponent<SpaceController>().enabled = true;
                GetComponent<Rigidbody>().useGravity = false;
                break;
            case ControllerType.COMBAT_CONTROLLER:
                CombatController controller = gameObject.GetComponent<CombatController>();
                controller.enabled = true;
                GetComponent<Rigidbody>().useGravity = false;
                break;
            case ControllerType.GROUND_CONTROLLER:
                GroundController controllerG = gameObject.GetComponent<GroundController>();
                controllerG.enabled = true;
                GetComponent<Rigidbody>().useGravity = true;
                break;
        }

        DisableOldController();

        controllerType = newType;
        changeCooldown = true;
        StartCoroutine(ChangeControllerCooldown());
    }

    private void DisableOldController()
    {
        switch (controllerType)
        {
            case ControllerType.SPACE_CONTROLLER:
                GetComponent<SpaceController>().enabled = false;
                break;
            case ControllerType.COMBAT_CONTROLLER:
                GetComponent<CombatController>().enabled = false;
                break;
            case ControllerType.GROUND_CONTROLLER:
                GetComponent<GroundController>().enabled = false;
                break;
        }
    }

    private IEnumerator ChangeControllerCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        changeCooldown = false;
    }

    public void AddScore(int toAdd)
    {
        score += toAdd;
        if(UIManager.Instance)
            UIManager.Instance.UpdateScore(score);
    }

    public void ProcessScore(float mul)
    {
        score = (int)((float)score * mul);
        ScoreManager.instance.AddScore(name, score);
    }

    public int GetScore() { return score; }
}
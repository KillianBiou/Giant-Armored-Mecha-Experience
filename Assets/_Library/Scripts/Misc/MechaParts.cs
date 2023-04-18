using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public static MechaParts instance;

    [Header("Pilot Parameters")]
    public string name;
    private int score;

    [Header("Thrusters")]
    public GameObject leftThruster;
    public GameObject rightThruster;

    [Header("Other")]
    [SerializeField]
    private AudioClip deathClip;
    [SerializeField]
    private bool debugIsGrounded;
    public ControllerType controllerType;
    [SerializeField]
    private UIManager UIManager;

    public Animator mechaAnim;

    public List<BodyPart> bodyParts = new List<BodyPart>();

    private bool changeCooldown = false;

    private void Awake()
    {
        instance = this;
        Debug.Log(Application.persistentDataPath);
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
                if(child.data.part == MemberPart.TORSO)
                {
                    if (child.data.hp <= 0)
                    {
                        //death
                        ProcessScore(0.8f);
                        if(deathClip)
                            NarratorManager.instance.SayThis(deathClip);
                    }
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
                mechaAnim.SetBool("isFlying", true);
                break;
            case ControllerType.COMBAT_CONTROLLER:
                CombatController controller = gameObject.GetComponent<CombatController>();
                controller.enabled = true;
                GetComponent<Rigidbody>().useGravity = false;
                mechaAnim.SetBool("isFlying", false);
                break;
            case ControllerType.GROUND_CONTROLLER:
                GroundController controllerG = gameObject.GetComponent<GroundController>();
                controllerG.enabled = true;
                GetComponent<Rigidbody>().useGravity = true;
                mechaAnim.SetBool("isFlying", false);
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
                GetComponent<Rigidbody>().useGravity = false;
                break;
            case ControllerType.COMBAT_CONTROLLER:
                GetComponent<CombatController>().enabled = false;
                GetComponent<Rigidbody>().useGravity = false;
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
        UIManager.UpdateScore(score);
    }

    private bool registered = false;
    public void ProcessScore(float mul)
    {
<<<<<<< Updated upstream
        score = (int)((float)score * mul);
        score += 10 * UIManager.GetRemainingTime();
        ScoreManager.instance.AddScore(name, score);
=======
        if(!registered)
        {
            registered = true;
            score = (int)((float)score * mul);
            score += 10 * UIManager.GetRemainingTime();
            ScoreManager.instance.AddScore(name, score);
        }
>>>>>>> Stashed changes
    }

    public int GetScore() { return score; }
}
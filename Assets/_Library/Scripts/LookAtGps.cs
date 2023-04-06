using UnityEngine;
using UnityEngine.Assertions;



public class LookAtGps : MonoBehaviour
{
    [SerializeField]
    private Transform toRotate;

    [SerializeField]
    public Transform target;


    void Update()
    {
        if(target != null)
        {
            toRotate.LookAt(target);
        }
    }
}


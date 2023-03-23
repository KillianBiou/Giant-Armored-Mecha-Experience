using UnityEngine;
using UnityEngine.Assertions;




public class LookAtGps : MonoBehaviour
{
    [SerializeField]
    private Transform _toRotate;

    [SerializeField]
    public Transform target;


    void Update()
    {
        if(target != null)
        {
            Vector3 dirToTarget = (target.position - _toRotate.position).normalized;
            _toRotate.LookAt(_toRotate.position - dirToTarget, Vector3.up);
        }
    }
}


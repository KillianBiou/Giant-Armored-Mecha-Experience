using UnityEngine;

public class AIGolemBehaviour : MonoBehaviour
{
    private AIData aiData;

    [SerializeField]
    private GameObject YRotationObject;
    [SerializeField]
    private GameObject XRotationObject;

    private void Start()
    {
        aiData = GetComponent<AIData>();
    }

    void Update()
    {

        if (aiData.target)
        {

            Vector3 yTarget = aiData.target.transform.position;
            Vector3 xTarget = aiData.target.transform.position;

            YRotationObject.transform.LookAt(yTarget, Vector3.up);
            YRotationObject.transform.localRotation = Quaternion.Euler(new Vector3(0, YRotationObject.transform.localRotation.eulerAngles.y, 0));

            XRotationObject.transform.LookAt(xTarget, Vector3.up);
            XRotationObject.transform.localRotation = Quaternion.Euler(new Vector3(XRotationObject.transform.localRotation.eulerAngles.x, 0, 0));
        }
    }
}

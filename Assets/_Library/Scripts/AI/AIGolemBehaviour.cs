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
            Vector3 lookPositionY = aiData.target.transform.position - YRotationObject.transform.position;
            Vector3 lookPositionX = aiData.target.transform.position - XRotationObject.transform.position;

            lookPositionY.y = 0;
            YRotationObject.transform.rotation = Quaternion.LookRotation(lookPositionY, transform.up);
            //YRotationObject.transform.LookAt(lpy);
            //YRotationObject.transform.rotation = Quaternion.Euler(new Vector3(0, YRotationObject.transform.rotation.y, 0));

            Debug.DrawRay(XRotationObject.transform.position, XRotationObject.transform.forward * 100);

            XRotationObject.transform.rotation = Quaternion.LookRotation(lookPositionX, transform.up);
            //XRotationObject.transform.rotation = Quaternion.Euler(new Vector3(XRotationObject.transform.rotation.x, 0, 0));
        }
    }
}

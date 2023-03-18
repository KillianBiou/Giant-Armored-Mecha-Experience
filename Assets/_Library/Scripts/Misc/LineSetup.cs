using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSetup : MonoBehaviour
{
    public IEnumerator Initialize(Transform startPosition, Vector3 endPosition)
    {
        GetComponent<LineRenderer>().SetPosition(0, startPosition.position);
        GetComponent<LineRenderer>().SetPosition(1, endPosition);

        yield return new WaitForEndOfFrame();

        StartCoroutine(Initialize(startPosition, endPosition));
    }


}

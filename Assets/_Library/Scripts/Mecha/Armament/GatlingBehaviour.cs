using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingBehaviour : MonoBehaviour
{

    private GameObject bullet;

    public void Initialize(GameObject bullet)
    {
        this.bullet = bullet;
    }

    public void Fire()
    {
        Debug.Log("Gatling is firing");
    }
}

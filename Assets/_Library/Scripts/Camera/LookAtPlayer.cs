using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(player);
    }
}
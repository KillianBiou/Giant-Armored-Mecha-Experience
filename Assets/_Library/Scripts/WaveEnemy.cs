using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnemy : MonoBehaviour
{
    public EnemySpawner ES;

    void OnDestroy()
    {
        ES.UnregisterEnemy(gameObject);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	[SerializeField]
	private GameObject[] spawnPoses;
	[SerializeField]
	private GameObject[] enemyPrefabs;
	[SerializeField]
	private GameObject bossPrefab;
	[SerializeField]
	private GameObject bossPos;




	private List<GameObject> enemyList = new List<GameObject>();
	
	private int waveCount;
	
	
	private void Start()
	{
		waveCount = 0;
		StartCoroutine(EnemyWave(3));
	}
	
	
	
	//play a wave
	IEnumerator EnemyWave(int n)
	{
		while(n != 0)
		{
			SpawnRandomEnemy();
			n--;
			yield return new WaitForSeconds(0.5f);
		}
	}
	
	
	//spawn 1 random enemy
	private bool SpawnRandomEnemy()
	{
		RegisterEnemy(Instantiate(enemyPrefabs[(int)Random.Range(0, enemyPrefabs.Length-1)], SpawnPos()));
		return true;
	}
		
	
	//get a pos to spawn
	private Transform SpawnPos()
	{
		return spawnPoses[(int)Random.Range(0.0f, spawnPoses.Length)].transform;
	}
	
	
	public void RegisterEnemy(GameObject spawned)
	{
		WaveEnemy we = spawned.AddComponent(typeof(WaveEnemy)) as WaveEnemy;
		we.ES = this;
		enemyList.Add(spawned);
	}
	
	public void UnregisterEnemy(GameObject en)
	{
		enemyList.Remove(en);
		if(enemyList.Count == 0)
		{
			waveCount++;
			if(waveCount == 6)
				CallBoss();
			else if (waveCount == 7)
			{
				//fin du game !
			}
			else
				StartCoroutine(EnemyWave((int)Random.Range(2.0f, 3.0f)));
		}
	}
	
	
	public void CallBoss()
	{
		//RegisterEnemy(Instantiate(bossPrefab, bossPos));
	}

}
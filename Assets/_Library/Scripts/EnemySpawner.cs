using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	[SerializeField]
	private GameObject[] spawnPoses, flyPos;
	[SerializeField]
	private GameObject[] enemyPrefabs;
	[SerializeField]
	private GameObject bossPrefab;
	[SerializeField]
	private GameObject bossPos;

	[SerializeField]
	private AudioClip timeoutedClip, winClip;

	[SerializeField]
	private int nbWaves;

	private GameObject player, nearest;


	private List<GameObject> enemyList = new List<GameObject>();

	private float countDown;
	private int waveCount;
	
	
	private void Start()
	{
		waveCount = 0;
		StartCoroutine(EnemyWave(6));
		player = GameObject.FindGameObjectsWithTag("Player")[0];

		countDown = 150;
		UIManager.instance.ShowTimer();
	}

	void Update()
	{
		countDown -= Time.deltaTime;
		UIManager.instance.UpdateTimer((int)countDown);

		if (countDown <= 0.0f) //time out
		{
			MechaParts.instance.ProcessScore(1);
			if (timeoutedClip)
				NarratorManager.instance.SayThis(timeoutedClip);
		}

		if (enemyList.Count > 1)
		{
			nearest = enemyList[0];
			for(int i=1; i<enemyList.Count; i++ )
            {
				if (Vector3.Distance(player.transform.position, enemyList[i].transform.position) < Vector3.Distance(player.transform.position, nearest.transform.position))
					nearest = enemyList[i];
			}
			GpsTridi.instance.SetTarget(nearest);
		}
		else if(enemyList.Count > 0)
        {
			GpsTridi.instance.SetTarget(enemyList[0]);
		}
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
			if(waveCount == nbWaves)
				CallBoss();
			else if (waveCount == nbWaves+1)
			{
				//fin du game !
				if (winClip)
					NarratorManager.instance.SayThis(winClip);
				MechaParts.instance.ProcessScore(1);
			}
			else if(waveCount%3 == 0)
            {
				RegisterEnemy(Instantiate(enemyPrefabs[3], flyPos[0].transform));
				RegisterEnemy(Instantiate(enemyPrefabs[3], flyPos[1].transform));
				RegisterEnemy(Instantiate(enemyPrefabs[3], flyPos[2].transform));
				StartCoroutine(EnemyWave((int)Random.Range(2.0f, 3.0f)));
			}
			else
				StartCoroutine(EnemyWave((int)Random.Range(2.0f, 3.0f)));
		}
	}
	
	
	private void CallBoss()
	{
		RegisterEnemy(Instantiate(bossPrefab, bossPos.transform));
	}
}
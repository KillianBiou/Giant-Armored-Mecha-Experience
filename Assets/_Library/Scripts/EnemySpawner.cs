using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	[SerializeField]
	private GameObject[] enemyPrefabs;
	[SerializeField]
	private GameObject bossPrefab;
	
	
	private List<GameObject> enemyList = new List<GameObject>();
	
	private int waveCount;
	
	
	private void Start()
	{
		waveCount = 0;
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
		Vector3 pos;/*
		if(pos = SpawnPos() != new Vector3(0, 0, 0))
		{
			//RegisterEnemy(Instanciate(enemyPrefabs[(int)Random.Range(0, enemyPrefabs.Lenght-1)], pos));
			return true;
		}*/
		return false;
	}
		
	
	//get a pos to spawn
	private Vector3 SpawnPos()
	{
		Vector3 direction = Random.insideUnitCircle * Random.Range(5.0f, 10.0f);
		
		UnityEngine.AI.NavMeshHit hit;
		if (UnityEngine.AI.NavMesh.SamplePosition(direction, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas))
		{
			return hit.position;
		}
		return new Vector3(0, 0, 0);
	}
	
	
	public void RegisterEnemy(GameObject en)
	{
		enemyList.Add(en);
	}
	
	public void UnregisterEnemy(GameObject en)
	{
		enemyList.Remove(en);
		if(enemyList.Count == 0)
		{
			waveCount++;
			if(waveCount == 10)
				CallBoss();
			else
				StartCoroutine(EnemyWave((int)Random.Range(2.0f, 5.0f)));
		}
	}
	
	
	public void CallBoss()
	{
	
		//RegisterEnemy();
	}
	
}
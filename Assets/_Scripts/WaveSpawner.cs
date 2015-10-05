using UnityEngine;
using System.Collections;

[System.Serializable]
public class Wave
{
	public string Name;
	public GameObject[] enemys;
	public int enemyCount;
	public float spawnRate;
}

public enum SpawnState{Spawning, Waiting, Counting};

public class WaveSpawner : MonoBehaviour 
{
	public Transform[] spawnPoints;
	public float spawnRang;
	public Wave[] waves;
	private int nextWave = 0;

	public float timeBetweenWaves = 5.0f;
	public float waveCountDown;

	private float searchWait = 1.0f;

	private SpawnState state = SpawnState.Counting;

	void start()
	{
		waveCountDown = timeBetweenWaves;

	}

	void Update()
	{
		if (state == SpawnState.Waiting)
		{
			if(!IsEnemyAlive())
			{
				WaveCompleted();
			}
			else
			{
				return;
			}

		}

		if (waveCountDown <= 0)
		{
			if (state != SpawnState.Spawning)
			{
				StartCoroutine(SpawnWave(waves[nextWave]));
			}
		}
		else
		{
			waveCountDown -= Time.deltaTime;

		}
	}

	void WaveCompleted()
	{
		Debug.Log("Wave Completed!");
		
		state = SpawnState.Counting;
		waveCountDown = timeBetweenWaves;
		
		if (nextWave + 1 > waves.Length - 1)
		{
			nextWave = 0;
			Debug.Log("ALL WAVES COMPLETE! Looping...");
		}
		else
		{
			nextWave++;
		}
	}

	bool IsEnemyAlive()
	{
		searchWait -= Time.deltaTime;

		if(searchWait <= 0.0f)
		{
			searchWait = 1.0f;
			if(GameObject.FindGameObjectWithTag("Enemy") == null)
			{
				return false;
			}
		}
		return true; 
	}

	IEnumerator SpawnWave(Wave _wave)
	{
		Debug.Log("Spawning Wave: " + _wave.Name);
		state = SpawnState.Spawning;

		for (int i = 0; i < _wave.enemyCount; i++)
		{
			SpawnEnemy(_wave.enemys[Random.Range(0,_wave.enemys.Length)]);
			yield return new WaitForSeconds(1.0f / _wave.spawnRate);
		}

		state = SpawnState.Waiting;
		yield break;
	}

	void SpawnEnemy(GameObject enemy)
	{
		Debug.Log("Spawning Enemy: " + enemy.name);
		Transform tr = spawnPoints[Random.Range(0, spawnPoints.Length)];
		Instantiate(enemy, tr.position + Vector3.right * Random.Range(-spawnRang, spawnRang), tr.rotation);
	}
}

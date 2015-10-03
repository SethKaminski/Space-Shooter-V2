using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{

	public GameObject[] hazard;
	public GameObject Star;
	public Vector2 spawnValues;
	public int hazardCount;
	public float hazardSpawnWait;
	public float StarSpawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	//public Text restartText;
	//public Text gameOverText;

	private bool gameOver;
	private bool restart;
	private int score;

	void Start()
	{
		gameOver = false;
		restart = false;
		//gameOverText.text = "";
		//restartText.text = "";
		score = 0;
		UpdateScore();
		StartCoroutine(SpawnWaves());
		StartCoroutine(SpawnStars());
	}

	void Update()
	{
		if(restart)
		{
			if(Input.GetKeyDown(KeyCode.R))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds(startWait);
		while (!gameOver)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector2 spawnPosition = new Vector2(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y);
				Quaternion spawnRotation = Quaternion.identity;
				int Roll = Mathf.CeilToInt(Random.Range(1, 11) / 10);
				Instantiate(hazard[Roll], spawnPosition, spawnRotation);
				yield return new WaitForSeconds(hazardSpawnWait);
			}
			yield return new WaitForSeconds(waveWait);
		}

		//restartText.text = "Press 'R' to Restart";
		restart = true;
	}

	IEnumerator SpawnStars()
	{
		while(!gameOver)
		{
			Vector2 spawnPosition = new Vector2(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate(Star, spawnPosition, spawnRotation);
			yield return new WaitForSeconds(StarSpawnWait);
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}

	void UpdateScore()
	{
		scoreText.text = "" + score;
	}

	public void GameOver()
	{
		gameOver = true;
		//gameOverText.text = "Game Over";
	}
}

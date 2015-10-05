using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
	public GameObject Star;
	public Vector2 spawnValues;
	public float StarSpawnWait;

	public Text scoreText;
	public Text otherScoreText;
	public Text gameOverText;

	private bool gameOver;
	private int score;

	void Start()
	{
		gameOver = false;
		gameOverText.text = "";
		otherScoreText.text = "";
		score = 0;
		UpdateScore();
		StartCoroutine(SpawnStars());
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	IEnumerator SpawnStars()
	{
		while(true)
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

	private void UpdateScore()
	{
		if (!gameOver)
		{
			scoreText.text = "" + score;
		}
	}

	public void GameOver()
	{
		gameOver = true;
		gameOverText.text = "Game Over";
		scoreText.text = "";
		otherScoreText.text = "Score: " + score;

	}
}

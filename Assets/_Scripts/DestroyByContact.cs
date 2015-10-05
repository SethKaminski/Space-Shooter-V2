using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public int damge;
	public int scoreValue;

	public GameObject[] powerUps;

	private GameController gameController;
	private PlayerController playerController;
	void Start()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		else
		{
			Debug.Log("Cannot find 'GameController' script");
		}

		GameObject playerControllerObject = GameObject.FindGameObjectWithTag("Player");
		if (gameControllerObject != null)
		{
			playerController = playerControllerObject.GetComponent<PlayerController>();
		}
		else
		{
			Debug.Log("Cannot find 'PlayerController' script");
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag != "Boundary" && other.tag != "Star" && other.tag != "PowerUp")
		{
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy(this.gameObject);

			if (other.tag == "Player")
			{
				playerController.AddDamge(damge);
			}
			else
			{
				if (other.tag != "ShotPers")
				{
					Destroy(other.gameObject);
				}
				gameController.AddScore(scoreValue);
			}

			if (Random.Range(1,20) == 9)
			{
				Instantiate(powerUps[Random.Range(0,powerUps.Length -1)], transform.position, Quaternion.identity);
			}
		}
	}
}

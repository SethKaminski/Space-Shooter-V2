using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;

	private GameController gameController;
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
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag != "Boundary" && other.tag != "Star")
		{
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy(this.gameObject);
			Destroy(other.gameObject);

			/*if (other.tag == "Player"){
				Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			}*/
			gameController.AddScore(scoreValue);
		}
	}
}

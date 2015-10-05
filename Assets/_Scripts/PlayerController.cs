using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {
	
	public float speed;
	public Boundary boundary;
	public int Damge;
	public int MaxDamge;

	public GameObject[] DamgeMarks;
	
	public GameObject[] shots;
	public int shotting;
	public GameObject[] PowerUps;
	public Transform shotSpawn;
	
	public float fireRate = 0.5F;
	
	private Rigidbody2D rb;
	private float nextFire = 0.0F;
	
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

		rb = GetComponent<Rigidbody2D>();
		UpdateShot();
	}
	
	void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shots[shotting], shotSpawn.position, shotSpawn.rotation);
			nextFire = Time.time + fireRate;
		}
	}
	
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		
		Vector2 movement = new Vector2(moveHorizontal, moveVertical);
		rb.velocity = movement * speed;
		
		rb.position = new Vector2
			(
				Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
				Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax)
			);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "PowerUp")
		{
			if (shotting < shots.Length - 1)
			{
				shotting++;
				UpdateShot();
				GetComponents<AudioSource>()[0].Play();
				Destroy(other.gameObject);
			}
		}
	}

	void UpdateShot()
	{
		for (int i = 0; i < PowerUps.Length; i++)
		{
			if (PowerUps[i].activeSelf)
			{
				PowerUps[i].SetActive(false);
			}
		}
		PowerUps[shotting].SetActive(true);
	}

	public void AddDamge(int add)
	{
		GetComponents<AudioSource>()[1].Play();
		if (Damge + add >= MaxDamge)
		{
			Destroy(this.gameObject);
			gameController.GameOver();
		}
		else
		{
			Damge += add;
			UpdateDamge();
		}

	}

	void UpdateDamge ()
	{
		for (int i = 0; i < DamgeMarks.Length; i++)
		{
			if (DamgeMarks[i].activeSelf)
			{
				DamgeMarks[i].SetActive(false);
			}
		}

		if (Damge != 0)
		{
			DamgeMarks[Damge - 1 ].SetActive(true);
		}
	}
}

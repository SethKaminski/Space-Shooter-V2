using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {
	
	public float speed;
	public float tilt;
	public Boundary boundary;
	
	public GameObject shot;
	public Transform shotSpawn;
	
	public float fireRate = 0.5F;
	
	private Rigidbody2D rb;
	private float nextFire = 0.0F;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
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
}

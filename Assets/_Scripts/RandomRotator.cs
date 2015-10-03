using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

	private Rigidbody2D rb;
	public float spin;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.angularVelocity = Random.Range(-spin, spin);
	}
}

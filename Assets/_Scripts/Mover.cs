﻿using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	private Rigidbody2D rb;
	public float speed;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.up * speed;
	}
}

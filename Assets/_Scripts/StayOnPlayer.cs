using UnityEngine;
using System.Collections;

public class StayOnPlayer : MonoBehaviour {

	private Transform tr;
	private GameObject player;
	void Start()
	{
		tr = GetComponent<Transform>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		tr.position = player.transform.position;
	}
}

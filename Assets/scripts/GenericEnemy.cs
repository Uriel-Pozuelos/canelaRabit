using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemy : MonoBehaviour
{
	public int vida = 3;
	public GameObject player;
	public int comportamiento = 1;
	public float speed;

	private void Start()
	{
		player = GameObject.Find("Player");
	}

	private void Update()
	{
		if (comportamiento == 1)
		{
			transform.LookAt(player.transform);
			GetComponent<Rigidbody>().velocity = transform.forward * speed * 2;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Bala")
		{
			Destroy(collision.collider.gameObject);
			vida = vida - 1;
			if (vida == 0)
			{
				Destroy(gameObject);
			}
		}

		if (collision.collider.tag == "Player")
		{
			FindObjectOfType<PlayerHealth>().RecibirGolpe();
		}
	}
}

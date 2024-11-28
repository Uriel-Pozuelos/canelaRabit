using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemy : MonoBehaviour
{
	public int vida = 3;
	public GameObject player;
	public GameObject deathParticlesPrefab;
	public int comportamiento = 1;
	public float speed;
	void Start()
    {
		comportamiento = Random.Range(1, 3);
		player = GameObject.Find("Player");
		InvokeRepeating("CambiarDireccion", 3, 3);
	}

	void Update()
	{
		if (comportamiento == 1)
		{
			transform.LookAt(player.transform);
			GetComponent<Rigidbody>().velocity = transform.forward * speed * 2;
		}
		else
		{
			GetComponent<Rigidbody>().velocity = transform.forward * speed;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Bala")
		{
			Destroy(collision.collider.gameObject);
			vida = vida - 1;
			if (vida <= 0)
			{
				GameObject particles = Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
				Destroy(particles, 1.0f);
				Destroy(gameObject);
			}
		}

		if (collision.collider.tag == "Player")
		{
			//SceneManager.LoadScene(1);
			FindObjectOfType<PlayerHealth>().RecibirGolpe();
		}
	}

	public void CambiarDireccion()
	{
		transform.Rotate(0, Random.Range(0, 360), 0);
	}
}

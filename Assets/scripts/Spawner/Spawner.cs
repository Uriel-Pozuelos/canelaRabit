using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject prefabEnemigo;
	public int maximoEnemigos;
	public int numeroEnemigos = 0;
	// Start is called before the first frame update
	void Start()
	{
		InvokeRepeating("GenerarEnemigo", 0, 3);
	}

	void GenerarEnemigo()
	{
		if (numeroEnemigos < maximoEnemigos)
		{
			Instantiate(prefabEnemigo, transform.position, Quaternion.identity);
			numeroEnemigos++;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}

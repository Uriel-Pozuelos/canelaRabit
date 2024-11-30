using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabEnemigo;
    public int maximoEnemigos;
    public int numeroEnemigos = 0;

    private List<GenericEnemy> enemigosEnLaZona = new List<GenericEnemy>();
    private bool jugadorEnRango = false;

    void Start()
    {
        InvokeRepeating("GenerarEnemigo", 0, 1);
    }

    void GenerarEnemigo()
    {
        if (numeroEnemigos < maximoEnemigos)
        {
            GameObject enemigoObj = Instantiate(prefabEnemigo, transform.position, Quaternion.identity);
            numeroEnemigos++;

            GenericEnemy enemigo = enemigoObj.GetComponent<GenericEnemy>();
            if (enemigo != null)
            {
                enemigosEnLaZona.Add(enemigo);

                // Si el jugador está en rango, activa el movimiento del enemigo
                enemigo.ActivarMovimiento(jugadorEnRango);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = true;
            NotificarEnemigos(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;
            NotificarEnemigos(false);
        }
    }

    void NotificarEnemigos(bool activar)
    {
        foreach (var enemigo in enemigosEnLaZona)
        {
            if (enemigo != null)
            {
                enemigo.ActivarMovimiento(activar);
            }
        }
    }
}

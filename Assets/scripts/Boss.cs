using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int vida = 5;
    public float velocidad = 3f;
    public float rangoDeteccion = 10f;

    public Transform player;
    public Animator animator;
    private Rigidbody rb;
    private bool enRango = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        // Busca automáticamente al Player si no está asignado
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        // Obtén el componente Animator si no se asignó en el Inspector
        //if (animator == null)
        //{
        //    animator = GetComponent<Animator>();
        //}

        CambiarEstado("Idle");
    }

    void Update()
    {
        // Calcular la distancia al jugador
        float distancia = Vector3.Distance(transform.position, player.position);

        if (distancia <= rangoDeteccion)
        {
            if (!enRango)
            {
                enRango = true;
                CambiarEstado("Follow");
            }

            // Moverse hacia el jugador
            Vector3 direccion = (player.position - transform.position).normalized;
            transform.position += direccion * velocidad * Time.deltaTime;

            // Asegurarse de que el Boss mire al jugador
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
        else
        {
            if (enRango)
            {
                enRango = false;
                CambiarEstado("Idle");
            }
        }
    }

    public void RecibirDaño(int cantidad)
    {
        vida -= cantidad;

        if (vida <= 0)
        {
            Morir();
        }
    }

    private void CambiarEstado(string nuevoEstado)
    {
        if (animator != null)
        {
            animator.SetTrigger(nuevoEstado);
        }
    }

    private void Morir()
    {
        // Aquí puedes agregar efectos de muerte, partículas, etc.
        Destroy(gameObject);
    }
}

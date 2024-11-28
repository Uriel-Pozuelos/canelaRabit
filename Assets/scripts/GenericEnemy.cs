using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemy : MonoBehaviour
{
    public int vida = 3;
    public GameObject deathParticlesPrefab;
    public float speed = 1f;

    private Transform playerTransform; // Referencia al jugador
    private Rigidbody rb; // Referencia al Rigidbody del enemigo
    private Animator animator; // Controlador de animaciones
    private int comportamiento; // Comportamiento del enemigo (1: perseguir, 2: patrullar)
    private float tiempoCambioDireccion = 3f; // Tiempo entre cambios de dirección
    private float siguienteCambioDireccion = 0f; // Tiempo para el próximo cambio

    private bool puedeMoverse = true; // Controla si el enemigo puede moverse

    void Start()
    {
        // Asigna referencias necesarias
        playerTransform = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Asigna comportamiento aleatorio
        comportamiento = UnityEngine.Random.Range(1, 2);

        // Configura la animación inicial
        CambiarAnimacion("Idle");
    }

    void Update()
    {
        if (puedeMoverse)
        {
            if (comportamiento == 1)
            {
                PerseguirJugador();
            }
            //else if (comportamiento == 2)
            //{
            //    Patrullar();
            //}
            //else
            //{
            //    // Cambia a la animación de caminar
            //    CambiarAnimacion("Walk");
            //}

        }
        else
        {
            // Si no puede moverse, queda en idle
            CambiarAnimacion("Idle");
            rb.velocity = Vector3.zero; // Detén cualquier movimiento
        }
    }

    void PerseguirJugador()
    {
        Vector3 direccion = (playerTransform.position - transform.position).normalized;
        Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, Time.deltaTime * 2);

        rb.velocity = transform.forward * speed;
        CambiarAnimacion("Walk");
    }

    void Patrullar()
    {
        if (Time.time > siguienteCambioDireccion)
        {
            CambiarDireccion();
            siguienteCambioDireccion = Time.time + tiempoCambioDireccion;
        }

        rb.velocity = transform.forward * speed;
    }

    public void ActivarMovimiento(bool estado)
    {
        puedeMoverse = estado;
    }

    private void CambiarAnimacion(string estado)
    {
        if (animator != null)
        {
            animator.Play(estado);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.CompareTag("Bala"))
        //{
        //    if (collision.collider.GetComponent<Bala>() != null)
        //    {
        //        Destroy(collision.collider.gameObject);
        //        RecibirDaño(1);
        //    }
        //}

        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<PlayerHealth>()?.RecibirGolpe();
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

    private void Morir()
    {
        if (deathParticlesPrefab != null)
        {
            GameObject particles = Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
            Destroy(particles, 1.0f);
        }

        Destroy(gameObject);
    }

    public void CambiarDireccion()
    {
        float nuevaRotacionY = UnityEngine.Random.Range(0, 360);
        Quaternion nuevaRotacion = Quaternion.Euler(0, nuevaRotacionY, 0);
        transform.rotation = nuevaRotacion;
    }

    // Funcional clase
    //public int vida = 3;
    //public GameObject player;
    //public GameObject deathParticlesPrefab;
    //public int comportamiento = 1;
    //public float speed;
    //void Start()
    //   {
    //	comportamiento = Random.Range(1, 3);
    //	player = GameObject.Find("Player");
    //	InvokeRepeating("CambiarDireccion", 3, 3);
    //}

    //void Update()
    //{
    //	if (comportamiento == 1)
    //	{
    //		transform.LookAt(player.transform);
    //		GetComponent<Rigidbody>().velocity = transform.forward * speed * 2;
    //	}
    //	else
    //	{
    //		GetComponent<Rigidbody>().velocity = transform.forward * speed;
    //	}
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //	if (collision.collider.tag == "Bala")
    //	{
    //		Destroy(collision.collider.gameObject);
    //		vida = vida - 1;
    //		if (vida <= 0)
    //		{
    //			GameObject particles = Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
    //			Destroy(particles, 1.0f);
    //			Destroy(gameObject);
    //		}
    //	}

    //	if (collision.collider.tag == "Player")
    //	{
    //		//SceneManager.LoadScene(1);
    //		FindObjectOfType<PlayerHealth>().RecibirGolpe();
    //	}
    //}

    //public void CambiarDireccion()
    //{
    //	transform.Rotate(0, Random.Range(0, 360), 0);
    //}
}

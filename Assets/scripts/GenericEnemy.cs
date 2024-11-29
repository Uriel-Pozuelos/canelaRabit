using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenericEnemy : MonoBehaviour
{
    public int vida = 3;
    public GameObject player;
    public GameObject deathParticlesPrefab;
    public float speed;

    private int comportamiento = 0; // 0 = Idle, 1 = Follow Player
    private Animator animator;
    private Rigidbody rb;

    private void Start()
    {
        //comportamiento = 1;
        //player = GameObject.Find("Player");
        //InvokeRepeating("CambiarDireccion", 3, 3);

        comportamiento = 0; // Inicia en Idle
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        CambiarAnimacion(false); // Setea animación Idle
    }

    void Update()
    {
        //if (comportamiento == 1)
        //{
        //    transform.LookAt(player.transform);
        //    GetComponent<Rigidbody>().velocity = transform.forward * speed * 2;
        //}
        //else
        //{
        //    GetComponent<Rigidbody>().velocity = transform.forward * speed;
        //}

        if (comportamiento == 1) // Follow Player
        {
            transform.LookAt(player.transform);
            rb.velocity = transform.forward * speed;
        }
        else // Idle
        {
            rb.velocity = Vector3.zero;
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
    }

    public void ActivarMovimiento(bool activar)
    {
        comportamiento = activar ? 1 : 0; // Cambia entre Idle (0) y Follow (1)
        CambiarAnimacion(activar);
    }

    private void CambiarAnimacion(bool caminando)
    {
        if (animator != null)
        {
            animator.SetBool("WalkParam", caminando);
        }
    }
}

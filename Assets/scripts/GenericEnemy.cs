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

    public AudioClip stepSound; // Sonido de pasos
    public AudioClip damageSound; // Sonido de daño
    public AudioClip deathSound; // Sonido de muerte

    private int comportamiento = 0; // 0 = Idle, 1 = Follow Player
    private Animator animator;
    private Rigidbody rb;
    public  AudioSource audioSource;

    private void Start()
    {
        comportamiento = 0; // Inicia en Idle
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        CambiarAnimacion(false); // Setea animación Idle
    }

    void Update()
    {
        if (comportamiento == 1) // Follow Player
        {
            transform.LookAt(player.transform);
            rb.velocity = transform.forward * speed;
            if (!audioSource.isPlaying) // Reproduce pasos si no está ya sonando
            {
                audioSource.clip = stepSound;
                audioSource.loop = true; // Repite mientras camina
                audioSource.Play();
            }
        }
        else // Idle
        {
            rb.velocity = Vector3.zero;
            if (audioSource.isPlaying && audioSource.clip == stepSound)
            {
                audioSource.Stop(); // Detén sonido de pasos
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bala")
        {
            Destroy(collision.collider.gameObject);
            vida = vida - 1;

            if (audioSource != null && damageSound != null)
            {
                audioSource.PlayOneShot(damageSound); // Reproduce sonido de daño
            }

            if (vida <= 0)
            {
                GameObject particles = Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
                Destroy(particles, 1.0f);

                if (audioSource != null && deathSound != null)
                {
                    audioSource.PlayOneShot(deathSound); // Reproduce sonido de muerte
                }

                Destroy(gameObject);
            }
        }

        if (collision.collider.tag == "Player")
        {
            FindAnyObjectByType<PlayerHelth>().TakeDamage();
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

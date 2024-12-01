using System.Collections;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int maxVida = 400;
    private int vida;
    public GameObject player;
    public GameObject deathParticlesPrefab;
    public float speed = 20f;
    public float enragedSpeedMultiplier = 3f;
    public int damage = 20;
    public int enragedDamage = 50;

    private bool isEnraged = false;
    private Animator animator;
    private Rigidbody rb;

    private void Start()
    {
        vida = maxVida;
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        CambiarAnimacion(true); // Inicializa en Walk
        FindAnyObjectByType<PlayerHelth>().acviteBoss();
    }

    void Update()
    {
        MovimientoBoss();
    }

    private void MovimientoBoss()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            rb.velocity = direction * speed;
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bala")
        {
            Destroy(collision.collider.gameObject);
            FindAnyObjectByType<PlayerHelth>().TakeDamageBoss(20, "GameOver");
            vida -= 1;
            VerificarEstadoEnraged();

            if (vida <= 0)
            {
                Muerte();
            }
        }

        if (collision.collider.tag == "Player")
        {
            FindAnyObjectByType<PlayerHelth>().TakeDamage();
        }
    }

    private void VerificarEstadoEnraged()
    {
        if (!isEnraged && vida <= maxVida / 2)
        {
            isEnraged = true;
            speed *= enragedSpeedMultiplier;
            Debug.Log("Boss is enraged!");
        }
    }

    private void Muerte()
    {
        if (deathParticlesPrefab != null)
        {
            GameObject particles = Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
            Destroy(particles, 1.0f);
        }
        Destroy(gameObject);
    }

    private void CambiarAnimacion(bool caminando)
    {
        if (animator != null)
        {
            animator.SetBool("WalkParam", caminando);
        }
    }
}

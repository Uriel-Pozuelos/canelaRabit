using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public float rotationSpeed = 3f; // Velocidad de rotación de la cámara
    public float movementSpeed = 1f; // Velocidad de movimiento del personaje
    public float jumpPower = 200f;
    public Transform groundCheckPosition;
    public float groundCheckRadius = 0.3f;
    public LayerMask groundLayer;

    private PlayerAnimations playerAnim;
    private Rigidbody rb;
    private float rotateY;
    private float horizontal, vertical;
    private bool isGrounded;
	public float turbo = 2f;
	public GameObject prefabBala;
	void Awake()
    {
        playerAnim = GetComponent<PlayerAnimations>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
        HandleCameraRotation();
        AnimatePlayer();
        HandleAttack();

		if (Input.GetButtonDown("Fire1"))
		{
			GameObject balaAux = Instantiate(prefabBala, transform.position + transform.forward * 2, Quaternion.identity);
			balaAux.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
			Destroy(balaAux, 2);
		}

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			movementSpeed = movementSpeed * turbo;
		}

		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			movementSpeed = movementSpeed / turbo;
		}
	}

    private void HandleMovement()
    {
        // Movimiento del personaje con teclas WASD
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 direction = transform.forward * vertical + transform.right * horizontal;
        direction.y = 0; // Evita movimiento vertical inesperado

        // Ajustar velocidad de movimiento y hacerlo dependiente del tiempo
        transform.position += direction.normalized * movementSpeed * Time.deltaTime;
    }

    private void HandleCameraRotation()
    {
        // Rotar al personaje con el mouse en el eje Y
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;

        // Rotar el personaje sobre el eje Y
        transform.Rotate(0, mouseX, 0);
    }

    private void AnimatePlayer()
    {
        // Usar un umbral para ignorar pequeños valores residuales
        float movementThreshold = 0.1f;

        if (Mathf.Abs(horizontal) > movementThreshold || Mathf.Abs(vertical) > movementThreshold)
        {
            playerAnim.PLayerWalk(true); // Activar animación de caminar
        }
        else
        {
            playerAnim.PLayerWalk(false); // Activar animación de idle
        }
    }

    private void HandleAttack()
    {
        // Ataque con clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            playerAnim.PlayerAttack();
        }
    }
}

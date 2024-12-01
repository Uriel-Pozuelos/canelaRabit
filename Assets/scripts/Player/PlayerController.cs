using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;
    public CharacterController player;
    public Vector3 movePlayer;

    private Vector3 playerInput;

    public Camera mainCamera;
    private Vector3 cameraForward;
    private Vector3 cameraRight;

    // Gravedad
    public float gravity = 9.8f;
    private float verticalVelocity; // Para controlar la gravedad y el salto
    private bool isGrounded;

    private void Start()
    {
        player = GetComponent<CharacterController>();

        // Verifica si GameStateManager está inicializado
        if (GameStateManager.Instance == null)
        {
            Debug.LogError("GameStateManager no está inicializado. Asegúrate de que el objeto esté en la escena.");
            return;
        }
    }

    private void Update()
    {
        // Obtener la velocidad del jugador del GameStateManager
        float playerSpeed = GameStateManager.Instance.GetPlayerSpeed();

        horizontalMove = Input.GetAxis("Horizontal") * playerSpeed;
        verticalMove = Input.GetAxis("Vertical") * playerSpeed;

        // Detectar si el jugador está en el suelo
        isGrounded = player.isGrounded;

        // Saltar con la tecla Espacio
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = GameStateManager.Instance.jumpForce; // Asignar fuerza de salto del GameStateManager
            GameStateManager.Instance.OnJump(); // Reproducir sonido de salto
        }

        // Correr con la tecla Shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed *= 1.5f; // Incrementar velocidad al correr
        }

        // Reproducir sonido de caminar si hay movimiento
        if (playerInput.magnitude > 0 && isGrounded)
        {
            GameStateManager.Instance.OnWalk();
        }

        // Detectar disparo con clic izquierdo del ratón
        if (Input.GetMouseButtonDown(0))
        {
            GameStateManager.Instance.OnFire(); // Reproducir sonido de disparo
        }
    }

    private void FixedUpdate()
    {
        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * cameraRight + playerInput.z * cameraForward;

        // Hacer que el jugador mire hacia la dirección del movimiento
        if (movePlayer != Vector3.zero)
        {
            player.transform.LookAt(player.transform.position + movePlayer);
        }

        setGravity();

        // Mover al jugador con gravedad aplicada
        player.Move(movePlayer * GameStateManager.Instance.playerSpeed * Time.deltaTime + new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
    }

    void camDirection()
    {
        cameraForward = mainCamera.transform.forward;
        cameraRight = mainCamera.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;
    }

    void setGravity()
    {
        // Obtener la gravedad del GameStateManager
        float gravity = GameStateManager.Instance.gravity;

        // Aplicar gravedad si el jugador no está en el suelo
        if (!isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity = 0; // Restablecer la velocidad vertical al estar en el suelo
        }
    }

    // Detectar colisiones con el suelo
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Player is grounded");
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

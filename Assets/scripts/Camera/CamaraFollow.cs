using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // La posición del jugador
    public Vector3 offset;    // Distancia entre la cámara y el jugador

    void Start()
    {
        // Asegúrate de que la cámara está en la posición correcta al inicio
        if (player == null)
        {
            Debug.LogError("La cámara necesita un jugador para seguir. Arrastra el jugador al campo 'player'.");
            return;
        }


        //asegurar que existe un GameStateManager
        if (GameStateManager.Instance == null)
        {
            Debug.LogError("GameStateManager no está inicializado. Asegúrate de que el objeto esté en la escena.");
            return;
        }
        // Coloca la cámara en la posición inicial deseada
        transform.position = player.position + offset;
        transform.LookAt(player);  // Hacer que la cámara mire hacia el jugador
    }

    void LateUpdate()
    {
        // Verifica si el jugador está asignado y GameStateManager está inicializado
        if (player == null || GameStateManager.Instance == null) return;

        // Obtener los valores de followSpeed y rotationSpeed del GameStateManager
        float followSpeed = GameStateManager.Instance.followSpeed;
        float rotationSpeed = GameStateManager.Instance.rotationSpeed;

        // Actualiza la posición de la cámara suavemente para seguir al jugador
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Rotar la cámara alrededor del jugador
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}

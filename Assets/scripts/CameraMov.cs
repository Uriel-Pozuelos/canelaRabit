using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
	float rotacionX = 0f;
	public float velocidad = 100f;
	public Transform Jugador;

	void Start()
	{
		// Bloquear el cursor para una mejor experiencia en FPS
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update()
	{
		// Capturar movimiento del ratón
		float mouseX = Input.GetAxis("Mouse X") * velocidad * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * velocidad * Time.deltaTime;

		// Actualizar la rotación en el eje X (vertical)
		rotacionX -= mouseY;
		rotacionX = Mathf.Clamp(rotacionX, -90f, 90f); // Limitar la inclinación

		// Aplicar la rotación vertical a la cámara
		transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);

		// Rotar el jugador en el eje Y (horizontal)
		Jugador.Rotate(Vector3.up * mouseX);
	}
}

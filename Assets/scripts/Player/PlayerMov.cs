using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
	public float speed;
	public float horizontalMove;
	public float verticalMove;
	private Rigidbody rb;
	public bool grounded = true;
	public float turbo = 2f;

	public GameObject prefabBala;
	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		grounded = true;
	}

	// Update is called once per frame
	void Update()
    {
		horizontalMove = Input.GetAxisRaw("Horizontal");
		verticalMove = Input.GetAxisRaw("Vertical");
		transform.Rotate(0, Input.GetAxisRaw("Mouse X"), 0);

		// Movimiento del jugador
		Vector3 movement = new Vector3(horizontalMove, 0, verticalMove);
		rb.AddForce(movement * speed);

		if (Input.GetButtonDown("Fire1"))
		{
			GameObject balaAux = Instantiate(prefabBala, transform.position + transform.forward * 2, Quaternion.identity);
			balaAux.GetComponent<Rigidbody>().AddForce(transform.forward * 800);
			Destroy(balaAux, 2);
		}

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			speed = speed * turbo;
		}

		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			speed = speed / turbo;
		}
	}
}

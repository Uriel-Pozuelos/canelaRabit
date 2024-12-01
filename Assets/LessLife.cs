using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessLife : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindAnyObjectByType<PlayerHelth>().TakeDamage();
            Destroy(gameObject);
        }
    }
}


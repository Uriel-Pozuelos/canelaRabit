using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindAnyObjectByType<PlayerHelth>().ItemConseguido();
            Destroy(gameObject);
        }
    }
}

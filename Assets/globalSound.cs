using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalSound : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip BackgroudSound;

    void Start()
    {
        audioSource.clip = BackgroudSound;
        //poner en loop
        audioSource.loop = true;
        audioSource.Play();

    }

    
}

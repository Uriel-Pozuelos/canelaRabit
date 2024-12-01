using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHelth : MonoBehaviour
{
    // Start is called before the first frame update
    public Image barraVida;
    public TextMeshProUGUI textoItems;
    public float vida;
    public int items;
    public int maxItems = 5;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip OnDamage;
    public AudioClip OnItem;
    public AudioClip OnAddLife;
    public AudioClip OnReaload;

    [Header("Loadings")]
    public Image Loading;
    public bool isLoading = false;




    void Start()
    {

        vida = 100;
        barraVida.fillAmount = 1;

        textoItems.text = 0.ToString() + " de " + maxItems.ToString() + " de Items";
        //ocultar el loading
        Loading.gameObject.SetActive(false);

    }


    public void visbleLoading()
    {
        //mostrar el loading
        Loading.gameObject.SetActive(true);
        isLoading = true;
    }

    public void hideLoading()
    {
        //ocultar el loading
        Loading.gameObject.SetActive(false);
        isLoading = false;
    }



    public void TakeDamage()
    {
        vida -= 5;
        barraVida.fillAmount = vida / 100;

        if (vida <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }



    public void AddLife()
    {
        vida += 10;
        if (vida > 100)
        {
            vida = 100;
        }
        barraVida.fillAmount = vida / 100;
    }


    public void ItemConseguido()
    {
        items++;

        textoItems.text = items.ToString() + " de " + items.ToString() + " de Items";

        if (items >= maxItems)
        {
            SceneManager.LoadScene(2);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}

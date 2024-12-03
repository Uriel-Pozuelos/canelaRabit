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

    [Header("Boos")]
    public int bossLife = 400;
    public Image barraBoss;
    public Image jefe;




    void Start()
    {

        vida = 100;
        barraVida.fillAmount = 1;
        barraBoss.fillAmount = 1;

        textoItems.text = 0.ToString() + " de " + maxItems.ToString() + " de Items";
        //ocultar el loading
        Loading.gameObject.SetActive(false);
        textoItems.gameObject.SetActive(false);
        barraBoss.gameObject.SetActive(false);
        jefe.gameObject.SetActive(false);


    }


    public void acviteBoss()
    {
        barraBoss.gameObject.SetActive(true);
        jefe.gameObject.SetActive(true);
    }


    public void TakeDamageBoss(int damage, string scena)
    {
        bossLife -= damage;
        barraBoss.fillAmount = (float)bossLife / 100; // Conversión explícita a float
        audioSource.clip = OnDamage;
        audioSource.Play();

        if (bossLife <= 0)
        {
            SceneManager.LoadScene("Winner");
        }
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
            SceneManager.LoadScene("GameOver");
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
        // Incrementar los ítems internamente multiplicando por 6
        items += 6;
        if (items > 1)
        {
            //hacer visible el texto de items
            textoItems.gameObject.SetActive(true);
        }

        // Variable para mostrar en la interfaz (como si solo sumara 1)
        int itemsVisual = items / 6;

        // Actualizar el texto del contador de ítems
        textoItems.text = itemsVisual.ToString() + " de " + maxItems.ToString() + " de Items";

        // Comprobar si se alcanzó el máximo de ítems
        if (itemsVisual >= maxItems)
        {
            SceneManager.LoadScene("OpenWorld");
        }
    }

}

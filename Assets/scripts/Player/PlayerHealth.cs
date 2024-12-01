using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	public Image barraVida;
	public TextMeshProUGUI textoItems;
	public float vida;
	public int items;
	public int maxItems;
	// Start is called before the first frame update
	void Start()
	{
		vida = 100;
		barraVida.fillAmount = 1;

		textoItems.text = 0.ToString() + "de " + maxItems.ToString();
	}



	public void RecibirGolpe()
	{
		vida = vida - 5;
		barraVida.fillAmount = vida / 100;

		if (vida <= 0)
		{
			SceneManager.LoadScene(0);
		}
	}

	public void ItemConseguido()
	{
		items++;

		textoItems.text = items.ToString() + "de " + maxItems.ToString();

		if (items == maxItems)
		{
			SceneManager.LoadScene(2);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}

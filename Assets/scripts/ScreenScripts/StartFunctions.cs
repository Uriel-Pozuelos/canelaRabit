using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartFunctions : MonoBehaviour
{
	// Funcion para iniciar el juego, ir a la escena de juego
	public void StartGame()
	{
		SceneManager.LoadScene("Game");
	}

	// Funcion para ir a la escena de instrucciones
	public void Instructions()
	{
		SceneManager.LoadScene("Instructions");
	}

	// Funcion para salir del juego
	public void ExitGame()
	{
		Application.Quit();
	}
}

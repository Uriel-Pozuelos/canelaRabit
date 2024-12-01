using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
	public void Sta()
	{
		SceneManager.LoadScene("Caves");
	}

	public void Exit()
	{
		Application.Quit();
	}
}

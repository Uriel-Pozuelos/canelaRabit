using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinFunctions : MonoBehaviour
{
    public void Exit()
    {
		Application.Quit();
	}

	public void Index()
	{
		SceneManager.LoadScene("Start");
	}
}

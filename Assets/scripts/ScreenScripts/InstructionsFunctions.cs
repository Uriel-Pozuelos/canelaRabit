using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsFunctions : MonoBehaviour
{
    public void Index()
    {
		SceneManager.LoadScene("Start");
	}

	public void Start()
	{
		SceneManager.LoadScene("Wolrd");
	}
}

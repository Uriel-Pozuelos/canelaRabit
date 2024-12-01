using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
   public void GoToStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
    }

    public void GotoInstructions()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Instructions");
    }

    public void GoToWinner()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Winner");
    }

    public void GoToGameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

    public void GoTocaves()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("caves");
    }

    public void GoToOpenWorld()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("OpenWorld");
    }

    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

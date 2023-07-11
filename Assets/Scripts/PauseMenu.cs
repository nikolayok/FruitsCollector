using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _pauseCanvas;

    public void TogglePause()
    {
        if (IsPaused()) // if paused then continue
        {
            ResumeGame();
            _pauseCanvas.SetActive(false);
        }
        else // if resume then pause
        {
            PauseGame();
            _pauseCanvas.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private bool IsPaused()
    {
        if (Time.timeScale == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

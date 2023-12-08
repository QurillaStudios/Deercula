using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void ContinueButton(GameObject panel)
    {
        panel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void EndGame()
    {
        Application.Quit();
    }
}

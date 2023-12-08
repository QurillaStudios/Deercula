using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static bool isGameRunning = true;
    public static bool isGameOver = false;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private TMP_Text gameOverText;

    [SerializeField] private ButterFly[] butterFlies;

    private void Start()
    {
        Time.timeScale = 1.0f;
        isGameRunning = true;
        isGameOver = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameRunning)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        if (!isGameRunning)
        {
            if(isGameOver)
            {
                gameOverText.text = "You Lose!";
            }
            else
            {
                gameOverText.text = "You Win!";
            }

            gameOverMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
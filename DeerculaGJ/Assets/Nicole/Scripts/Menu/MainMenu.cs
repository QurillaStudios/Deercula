using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    [SerializeField] private GameObject videoImage;

    public void StartGame()
    {
        StartCoroutine(StartVideoThenGame());
        
    }


    private IEnumerator StartVideoThenGame()
    {
        videoImage.SetActive(true);
        videoPlayer.Play();

        yield return new WaitForSeconds(6.5f);

        SceneManager.LoadScene("Game");
    }

    public void EndGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;

    private bool endGame,slowedTime;
    public void EndGame()
    {
        endGame = true;
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (!endGame) return;

        if(!slowedTime)
        {
            Time.timeScale -= Time.deltaTime * 2;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }

        if (Time.timeScale <= 0.1)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;

            slowedTime = true;
        }
    }
}

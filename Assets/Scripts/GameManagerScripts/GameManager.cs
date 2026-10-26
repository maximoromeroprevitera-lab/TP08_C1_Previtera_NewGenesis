using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public GameObject GameOverPanel;
    public float deathDelay = 2f;
    private bool gameEnded;
    private void Start()
    {
        GameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void GameOver()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            AudioManager.Instance.PlayGameOver(); // frena la música y suena el game over al morir
            StartCoroutine(ShowGameOver());
        }
    }
    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(deathDelay);

        GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
}
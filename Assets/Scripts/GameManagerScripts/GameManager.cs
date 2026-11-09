using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public GameObject GameOverPanel;
    [SerializeField] private GameObject healthBar;
    public float deathDelay = 2f;
    [Header("Score")]
    [SerializeField] private int currentScore;
    [SerializeField] private int highScore;
    private bool gameEnded;
    private void Start()
    {
        GameOverPanel.SetActive(false);
        healthBar.SetActive(true);

        currentScore = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        Time.timeScale = 1f;
    }
    public void AddScore(int points)
    {
        currentScore += points;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        Debug.Log("Score: " + currentScore);
        Debug.Log("High Score: " + highScore);
    }
    public void GameOver()
    {
        if (!gameEnded)
        {
            gameEnded = true;

            HideHealthBar();

            AudioManager.Instance.PlayGameOver();

            StartCoroutine(ShowGameOver());
        }
    }
    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(deathDelay);

        GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void HideHealthBar()
    {
        healthBar.SetActive(false);
    }
    public void ShowHealthBar()
    {
        healthBar.SetActive(true);
    }
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
}
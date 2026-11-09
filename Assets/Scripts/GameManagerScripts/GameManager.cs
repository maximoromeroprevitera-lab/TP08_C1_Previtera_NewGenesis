using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
public class GameManager : MonoBehaviour
{
    public GameObject GameOverPanel;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject score;
    [SerializeField] private TMP_Text currentScoreText;
    [SerializeField] private TMP_Text highScoreText;
    public float deathDelay = 2f;
    [Header("Score")]
    [SerializeField] private int currentScore;
    [SerializeField] private int highScore;
    private bool gameEnded;
    private void Start()
    {
        GameOverPanel.SetActive(false);
        healthBar.SetActive(true);
        score.SetActive(true);

        currentScore = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        UpdateScoreUI();

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

        UpdateScoreUI();

        Debug.Log("Score: " + currentScore);
        Debug.Log("High Score: " + highScore);
    }
    private void UpdateScoreUI()
    {
        currentScoreText.text = "Score: " + currentScore;
        highScoreText.text = "High Score: " + highScore;
    }
    public void HideScoreUI()
    {
        score.SetActive(false);
    }
    public void ShowScoreUI()
    {
        score.SetActive(true);
        UpdateScoreUI();
    }
    public void GameOver()
    {
        if (!gameEnded)
        {
            gameEnded = true;

            HideHealthBar();
            HideScoreUI();

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
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class VictoryManager : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private Button playAgainButton;
    private void Start()
    {
        victoryPanel.SetActive(false);
        playAgainButton.onClick.AddListener(PlayAgain);
    }
    public void Win()
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    private void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
}
using UnityEngine;
public class CreditsMenu : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject mainPanel;
    public void OpenCredits()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}

using UnityEngine;
public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject mainPanel;
    public void OpenSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}

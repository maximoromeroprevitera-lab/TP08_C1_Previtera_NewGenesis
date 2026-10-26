using UnityEngine;
public class Pausemenu : MonoBehaviour
{
    public GameObject Pausepanel;
    public GameObject Settingspanel;
    public GameObject Creditspanel;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pausepanel.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        Pausepanel.SetActive(true);
        Settingspanel.SetActive(false);
        Creditspanel.SetActive(false);

        Time.timeScale = 0f;
    }
    public void Resume()
    {
        Pausepanel.SetActive(false);
        Settingspanel.SetActive(false);
        Creditspanel.SetActive(false);

        Time.timeScale = 1f;
    }
    public void OpenSettings()
    {
        Pausepanel.SetActive(false);
        Settingspanel.SetActive(true);
        Creditspanel.SetActive(false);
    }
    public void OpenCredits()
    {
        Pausepanel.SetActive(false);
        Settingspanel.SetActive(false);
        Creditspanel.SetActive(true);
    }
    public void Back()
    {
        Pausepanel.SetActive(true);
        Settingspanel.SetActive(false);
        Creditspanel.SetActive(false);
    }
}

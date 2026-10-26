using UnityEngine;
using UnityEngine.UI;
public class ButtonSound : MonoBehaviour
{
    private void Start()
    {
        Button button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(PlaySound);
        }
    }
    private void PlaySound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayButton();
        }
    }
}

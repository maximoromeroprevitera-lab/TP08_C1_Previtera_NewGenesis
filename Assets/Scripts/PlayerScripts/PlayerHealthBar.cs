using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private PlayerLife playerLife;
    [SerializeField] private Slider healthSlider;
    private void Start()
    {
        if (playerLife == null || healthSlider == null)
        {
            Debug.LogError("PlayerHealthBar: faltan referencias.", this);
            return;
        }

        playerLife.OnHealthChanged += UpdateHealthBar;

        UpdateHealthBar(
            playerLife.CurrentHealth,
            playerLife.MaxHealth
        );
    }
    private void OnDestroy()
    {
        if (playerLife != null)
            playerLife.OnHealthChanged -= UpdateHealthBar;
    }
    private void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}
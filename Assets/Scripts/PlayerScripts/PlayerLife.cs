using System;
using UnityEngine;
public class PlayerLife : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] private PlayerDataSO playerData;
    private int currentHealth;
    private Animator animator;
    private bool isDead;
    public bool IsDead => isDead;
    public int CurrentHealth => currentHealth;
    public int MaxHealth => playerData != null ? playerData.maxHealth : 0;
    public event Action<int, int> OnHealthChanged;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        if (playerData == null)
        {
            Debug.LogError("PlayerLife: falta asignar PlayerDataSO.", this);
            return;
        }

        currentHealth = playerData.maxHealth;
        OnHealthChanged?.Invoke(currentHealth, MaxHealth);
    }
    public void TakeDamage(int damage)
    {
        if (isDead || damage <= 0)
            return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
            return;
        }

        if (animator != null)
            animator.SetTrigger("Hurt");

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayPlayerHit();

        OnHealthChanged?.Invoke(currentHealth, MaxHealth);

    }
    public void Heal(int amount)
    {
        if (isDead || playerData == null || amount <= 0)
            return;

        currentHealth += amount;

        if (currentHealth > MaxHealth)
            currentHealth = MaxHealth;

        OnHealthChanged?.Invoke(currentHealth, MaxHealth);

        Debug.Log("Player health: " + currentHealth);
    }
        private void Die()
    {
        isDead = true;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayDeath();

        if (animator != null)
            animator.SetTrigger("Death");

        if (gameManager != null)
            gameManager.GameOver();
        else
            Debug.LogError("PlayerLife: falta asignar GameManager.", this);

        Debug.Log("Player defeated");
    }

}
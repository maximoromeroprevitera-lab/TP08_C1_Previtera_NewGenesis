using UnityEngine;
public class PlayerLife : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] private PlayerDataSO playerData;
    private int currentHealth;
    private Animator animator;
    private bool isDead;
    public bool IsDead => isDead;
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
    }
    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }
        currentHealth -= damage;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayPlayerHit();
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }
    }
    public void Heal(int amount)
    {
        if (isDead || playerData == null)
        {
            return;
        }

        currentHealth += amount;

        if (currentHealth > playerData.maxHealth)
        {
            currentHealth = playerData.maxHealth;
        }

        Debug.Log("Player health: " + currentHealth);
    }
    private void Die()
    {
        isDead = true;

        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        if (gameManager != null)
        {
            gameManager.GameOver();
        }
        else
        {
            Debug.LogError("PlayerLife: falta asignar GameManager.", this);
        }

        Debug.Log("Player defeated");
    }
}
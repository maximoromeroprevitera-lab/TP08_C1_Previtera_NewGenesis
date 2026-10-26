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
        currentHealth = playerData.maxHealth;
    }
    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= damage;
        AudioManager.Instance.PlayPlayerHit();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            animator.SetTrigger("Hurt");
        }
    }
    public void Heal(int amount)
    {
        if (isDead)
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

        animator.SetTrigger("Death");

        gameManager.GameOver();

        Debug.Log("Player defeated");
    }
}

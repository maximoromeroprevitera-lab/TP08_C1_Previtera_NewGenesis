using UnityEngine;
using System.Collections;
public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private EnemyHealthBar healthBar;
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private GameObject ammoBoxPrefab;
    [SerializeField] private float dropOffsetY = -0.75f;
    [SerializeField] private float deathAnimationTime = 1f;
    private int currentHealth;
    private Animator animator;
    private bool isDead;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        currentHealth = maxHealth;

        healthBar.SetHealth(currentHealth, maxHealth);
    }
    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            healthBar.SetHealth(currentHealth, maxHealth);

            Die();
        }
        else
        {
            healthBar.SetHealth(currentHealth, maxHealth);
            animator.SetTrigger("Hurt");
        }
    }
    private void Die()
    {
        isDead = true;

        animator.SetTrigger("Death");

        EnemyShoot enemyShoot = GetComponent<EnemyShoot>();
        if (enemyShoot != null)
        {
            enemyShoot.enabled = false;
        }

        EnemyFollow enemyFollow = GetComponent<EnemyFollow>();
        if (enemyFollow != null)
        {
            enemyFollow.enabled = false;
        }

        EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.enabled = false;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        StartCoroutine(DeathSequence());
    }
    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(deathAnimationTime);

        Instantiate(
            deathParticles,
            transform.position,
            Quaternion.identity
        );

        Instantiate(
            ammoBoxPrefab,
            new Vector2(
                transform.position.x,
                transform.position.y + dropOffsetY
            ),
            Quaternion.identity
        );

        Destroy(gameObject);
    }
}

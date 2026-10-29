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
    private EnemyStateMachine stateMachine;
    private bool isDead;
    public bool IsDead => isDead;
    private void Awake()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
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
            return;
        }

        healthBar.SetHealth(currentHealth, maxHealth);
    }
    private void Die()

    {
        Debug.Log("EL ENEMIGO MURIO: " + gameObject.name, this);

        isDead = true;
        stateMachine.Die();

        isDead = true;

        stateMachine.Die();

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

        EnemyMeleeAttack enemyMeleeAtaack = GetComponent<EnemyMeleeAttack>();
        if (enemyMeleeAtaack != null)
        {
            enemyMeleeAtaack.enabled = false;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();

        foreach (Collider2D enemyCollider in colliders)
        {
            enemyCollider.enabled = false;
        }

        StartCoroutine(DeathSequence());
    }
    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(deathAnimationTime);

        if (deathParticles != null)
        {
            Instantiate(
                deathParticles,
                transform.position,
                Quaternion.identity
            );
        }
        if (ammoBoxPrefab != null)
        {
            Instantiate(
                ammoBoxPrefab,
                new Vector2(
                    transform.position.x,
                    transform.position.y + dropOffsetY
                ),
                Quaternion.identity
            );
        }
        Debug.Log("DeathSequence terminó para: " + gameObject.name, this);
    }
    private void OnDisable()
    {
        Debug.LogWarning(
            "EnemyLife se desactivó: " + gameObject.name,
            this
        );
    }

    private void OnDestroy()
    {
        Debug.LogWarning(
            "EnemyLife fue destruido: " + gameObject.name,
            this
        );
    }
}
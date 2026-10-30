using UnityEngine;
using System.Collections;
public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
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
        if (isDead) return;

        currentHealth = Mathf.Max(0, currentHealth - damage);
        healthBar.SetHealth(currentHealth, maxHealth);

        if (currentHealth == 0)
            Die();
    }
    private void Die()
    {
        isDead = true;
        stateMachine.Die();

        EnemyFollow follow = GetComponent<EnemyFollow>();
        if (follow != null) follow.enabled = false;

        EnemyMeleeAttack attack = GetComponent<EnemyMeleeAttack>();
        if (attack != null) attack.enabled = false;

        EnemyMovement movement = GetComponent<EnemyMovement>();
        if (movement != null) movement.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
        if (capsule != null)
        {
            capsule.size = new Vector2(0.8f, 0.5f);
            capsule.offset = new Vector2(0f, -0.5f);
        }

        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box != null)
        {
            box.size = new Vector2(0.8f, 0.5f);
            box.offset = new Vector2(0f, -0.5f);
        }

        StartCoroutine(DeathSequence());
    }
    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(deathAnimationTime);

        if (deathParticles != null)
            Instantiate(deathParticles, transform.position, Quaternion.identity);

        if (ammoBoxPrefab != null)
            Instantiate(ammoBoxPrefab, transform.position + Vector3.up * dropOffsetY, Quaternion.identity);

        yield return new WaitForSeconds(5f);

        Destroy(gameObject);
    }
}
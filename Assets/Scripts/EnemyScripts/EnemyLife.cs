using System.Collections;
using UnityEngine;
public class EnemyLife : MonoBehaviour
{
    [Header("Enemy Data")]
    [SerializeField] private EnemyDataSO enemyData;
    [SerializeField] private EnemyHealthBar healthBar;
    [Header("Death")]
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private float deathAnimationTime = 1f;
    [Header("Key")]
    [SerializeField] private bool dropKey;
    [SerializeField] private GameObject keyPrefab;
    [Header("Pickables")]
    private PickablePool pickablePool;
    [SerializeField] private PickableDataSO pickableData;
    private int currentHealth;
    private EnemyStateMachine stateMachine;
    private EnemyFlash enemyFlash;
    private bool isDead;
    public bool IsDead => isDead;
    public event System.Action OnDied;
    private void Awake()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        enemyFlash = GetComponent<EnemyFlash>();
        pickablePool = FindFirstObjectByType<PickablePool>();
    }
    private void Start()
    {
        currentHealth = enemyData.maxHealth;
        healthBar.SetHealth(currentHealth, enemyData.maxHealth);
    }
    public void TakeDamage(int damage)
    {
        if (isDead || damage <= 0)
            return;

        currentHealth = Mathf.Max(0, currentHealth - damage);
        healthBar.SetHealth(currentHealth, enemyData.maxHealth);

        if (currentHealth > 0 && enemyFlash != null)
            enemyFlash.Flash();

        if (currentHealth == 0)
            Die();
    }
    private void Die()
    {
        isDead = true;
        OnDied?.Invoke();

        GameManager gameManager = FindFirstObjectByType<GameManager>();

        if (gameManager != null)
            gameManager.AddScore(enemyData.scoreReward);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayDeathEnemy();
        BoxCollider2D box = GetComponent<BoxCollider2D>();

        if (box != null)
        {
            box.size = new Vector2(0.22f, 0.22f);
            box.offset = new Vector2(0f, -0.4f);
        }

        if (box != null)
        {
            float oldHeight = box.size.y;

            box.size *= 0.5f;
            box.offset = new Vector2(
                box.offset.x,
                box.offset.y - oldHeight * 0.25f
            );
        }

        stateMachine.Die();

        if (TryGetComponent(out EnemyFollow follow))
            follow.enabled = false;

        if (TryGetComponent(out EnemyMeleeAttack attack))
            attack.enabled = false;

        if (TryGetComponent(out EnemyMovement movement))
            movement.enabled = false;

        if (TryGetComponent(out Rigidbody2D rb))
            rb.linearVelocity = Vector2.zero;

        StartCoroutine(DeathSequence());
    }
    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(deathAnimationTime);

        if (deathParticles != null)
            Instantiate(deathParticles, transform.position, Quaternion.identity);

        if (dropKey && keyPrefab != null)
            Instantiate(keyPrefab, transform.position, Quaternion.identity);

        DropPickable();

        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    private void DropPickable()
    {
        if (pickablePool == null || pickableData == null)
            return;

        if (Random.value > pickableData.dropChance)
            return;

        int randomType = Random.Range(0, 3);
        pickablePool.GetPickable(randomType, transform.position);
    }
}
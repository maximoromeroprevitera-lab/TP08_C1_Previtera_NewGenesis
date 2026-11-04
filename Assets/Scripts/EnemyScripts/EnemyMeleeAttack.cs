using System.Collections;
using UnityEngine;
public class EnemyMeleeAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject hitboxPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [Header("Attack")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackCooldown = 1.5f;
    [Header("Attack Timing")]
    [SerializeField] private float firstHitDelay = 0.25f;
    [SerializeField] private float firstAttackDuration = 0.65f;
    [SerializeField] private float secondHitDelay = 0.25f;
    [SerializeField] private float secondAttackDuration = 0.65f;
    [Header("Knockback")]
    [SerializeField] private float knockbackForce = 8f;
    [SerializeField] private float knockbackUpForce = 2f;
    private EnemyStateMachine stateMachine;
    private EnemyLife enemyLife;
    private EnemyFollow enemyFollow;
    private PlayerLife playerLife;
    private float cooldownTimer;
    private bool isAttacking;
    private void Awake()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        enemyLife = GetComponent<EnemyLife>();
        enemyFollow = GetComponent<EnemyFollow>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        playerLife = FindFirstObjectByType<PlayerLife>();
        cooldownTimer = attackCooldown;
    }
    private void Update()
    {
        if (enemyLife.IsDead || playerLife == null ||
            playerLife.IsDead || isAttacking)
            return;

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f && enemyFollow.PlayerSeen)
        {
            StartCoroutine(AttackCombo());
            cooldownTimer = attackCooldown;
        }
    }
    private IEnumerator AttackCombo()
    {
        isAttacking = true;
        stateMachine.PlayAttack1();

        yield return new WaitForSeconds(firstHitDelay);
        SpawnHitbox();

        yield return new WaitForSeconds(firstAttackDuration - firstHitDelay);

        if (enemyLife.IsDead) yield break;

        stateMachine.PlayAttack2();

        yield return new WaitForSeconds(secondHitDelay);
        SpawnHitbox();

        yield return new WaitForSeconds(secondAttackDuration - secondHitDelay);

        if (!enemyLife.IsDead)
            stateMachine.FinishAttack();

        isAttacking = false;
    }
    private void SpawnHitbox()
    {
        if (hitboxPrefab == null || shootPoint == null)
            return;

        GameObject hitbox = Instantiate(
            hitboxPrefab,
            shootPoint.position,
            shootPoint.rotation
        );

        BoxCollider2D box = hitbox.GetComponent<BoxCollider2D>();

        if (box != null && spriteRenderer != null)
        {
            Vector2 offset = box.offset;
            offset.x = spriteRenderer.flipX
                ? -Mathf.Abs(offset.x)
                : Mathf.Abs(offset.x);

            box.offset = offset;
        }
        EnemyHitbox hitboxScript = hitbox.GetComponent<EnemyHitbox>();

        if (hitboxScript != null)
            hitboxScript.Setup(transform, damage, knockbackForce, knockbackUpForce);
    }
}
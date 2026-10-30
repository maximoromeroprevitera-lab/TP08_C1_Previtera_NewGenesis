using System.Collections;
using UnityEngine;
public class EnemyMeleeAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject hitboxPrefab;
    [SerializeField] private Transform shootPoint;
    [Header("Attack")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackRange = 1.5f;
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
    private PlayerLife playerLife;
    private float cooldownTimer;
    private bool isAttacking;
    private void Awake()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        enemyLife = GetComponent<EnemyLife>();
    }
    private void Start()
    {
        playerLife = FindFirstObjectByType<PlayerLife>();
        cooldownTimer = attackCooldown;
    }
    private void Update()
    {
        if (enemyLife.IsDead || playerLife == null || playerLife.IsDead ||
            isAttacking)
        {
            return;
        }

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f && IsPlayerInRange())
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
        if (IsPlayerInRange()) SpawnHitbox();

        yield return new WaitForSeconds(
            Mathf.Max(0f, firstAttackDuration - firstHitDelay)
        );

        if (enemyLife.IsDead)
        {
            isAttacking = false;
            yield break;
        }

        if (IsPlayerInRange())
        {
            stateMachine.PlayAttack2();
            yield return new WaitForSeconds(secondHitDelay);
            if (IsPlayerInRange()) SpawnHitbox();

            yield return new WaitForSeconds(
                Mathf.Max(0f, secondAttackDuration - secondHitDelay)
            );
        }

        if (!enemyLife.IsDead)
        {
            stateMachine.FinishAttack();
        }

        isAttacking = false;
    }
    private bool IsPlayerInRange()
    {
        return playerLife != null &&
               !playerLife.IsDead &&
               Mathf.Abs(playerLife.transform.position.x - transform.position.x)
               <= attackRange;
    }
    private void SpawnHitbox()
    {
        if (hitboxPrefab == null || shootPoint == null) return;

        GameObject hitbox = Instantiate(
            hitboxPrefab, shootPoint.position, shootPoint.rotation
        );

        EnemyHitbox hitboxScript = hitbox.GetComponent<EnemyHitbox>();

        if (hitboxScript != null)
        {
            hitboxScript.Setup(
                transform, damage, knockbackForce, knockbackUpForce
            );
        }
    }
}
using UnityEngine;
public class EnemyFollow : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float detectionDistance = 10f;
    [SerializeField] private float stopDistance = 1.5f;
    [Header("Facing")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform shootPoint;
    private Rigidbody2D rb;
    private Transform player;
    private EnemyStateMachine stateMachine;
    private EnemyLife enemyLife;
    private Vector3 originalShootPointPosition;
    public bool PlayerSeen => player != null && Mathf.Abs(player.position.x - transform.position.x) <= detectionDistance;
    public bool PlayerInShootRange { get; private set; }
    public Transform Player => player;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stateMachine = GetComponent<EnemyStateMachine>();
        enemyLife = GetComponent<EnemyLife>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (shootPoint != null)
            originalShootPointPosition = shootPoint.localPosition;
    }
    private void Start()
    {
        PlayerLife playerLife = FindFirstObjectByType<PlayerLife>();

        if (playerLife != null)
            player = playerLife.transform;
    }
    private void FixedUpdate()
    {
        if (enemyLife != null && enemyLife.IsDead)
            return;
        if (stateMachine.IsAttacking)
        {
            Stop();
            return;
        }

        if (!PlayerSeen)
        {
            PlayerInShootRange = false;
            Stop();
            stateMachine.SetMovement(false);
            return;
        }
        UpdateFacing();

        float distance = Mathf.Abs(player.position.x - transform.position.x);

        PlayerInShootRange = distance <= stopDistance;

        if (PlayerInShootRange)
        {
            Stop();
            stateMachine.SetMovement(false);
        }
        else
        {
            Chase();
            stateMachine.SetMovement(true);
        }
    }
    private void UpdateFacing()
    {
        float direction = Mathf.Sign(player.position.x - transform.position.x);

        if (direction == 0f) return;

        spriteRenderer.flipX = direction < 0f;

        if (shootPoint != null)
        {
            shootPoint.localPosition = new Vector3(
                Mathf.Abs(originalShootPointPosition.x) * direction,
                originalShootPointPosition.y,
                originalShootPointPosition.z
            );
        }
    }
    private void Chase()
    {
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }
    private void Stop()
    {
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
    }
}
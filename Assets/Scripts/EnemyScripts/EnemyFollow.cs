using UnityEngine;
public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float detectionDistance = 10f;
    [SerializeField] private float stopDistance = 1.5f;
    private Rigidbody2D rb;
    private Transform player;
    private EnemyStateMachine stateMachine;
    private EnemyLife enemyLife;
    public bool PlayerSeen
    {
        get
        {
            return player != null &&
                   Mathf.Abs(player.position.x - transform.position.x)
                   <= detectionDistance;
        }
    }
    public bool PlayerInShootRange { get; private set; }
    public Transform Player => player;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stateMachine = GetComponent<EnemyStateMachine>();
        enemyLife = GetComponent<EnemyLife>();
    }
    private void Start()
    {
        PlayerLife playerLife = FindFirstObjectByType<PlayerLife>();

        if (playerLife != null)
        {
            player = playerLife.transform;
        }
    }
    private void FixedUpdate()
    {
        if (enemyLife != null && enemyLife.IsDead)
        {
            return;
        }

        if (stateMachine.IsAttacking)
        {
            Stop();
            return;
        }

        if (player == null || !PlayerSeen)
        {
            PlayerInShootRange = false;
            Stop();
            stateMachine.SetMovement(false);
            return;
        }

        float distance = Mathf.Abs(
            player.position.x - transform.position.x
        );

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
    private void Chase()
    {
        float direction = Mathf.Sign(
            player.position.x - transform.position.x
        );

        rb.linearVelocity = new Vector2(
            direction * speed,
            rb.linearVelocity.y
        );
    }
    private void Stop()
    {
        rb.linearVelocity = new Vector2(
            0f,
            rb.linearVelocity.y
        );
    }
}
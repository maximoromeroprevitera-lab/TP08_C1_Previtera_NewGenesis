using UnityEngine;
public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float detectionDistance = 10f;
    [SerializeField] private float stopDistance = 5f;

    private Rigidbody2D rb;
    private Transform player;
    private bool blockedByBarrel;
    public bool PlayerSeen
    {
        get
        {
            return player != null &&
                   Mathf.Abs(player.position.x - transform.position.x) <= detectionDistance;
        }
    }
    public bool PlayerInShootRange { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (!PlayerSeen)
        {
            PlayerInShootRange = false;
            blockedByBarrel = false;
            return;
        }
        float distance = Mathf.Abs(player.position.x - transform.position.x);
        bool blocked = blockedByBarrel;
        blockedByBarrel = false;
        PlayerInShootRange = distance <= stopDistance || blocked;

        if (PlayerInShootRange)
        {
            Stop();
        }
        else
        {
            Chase();
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckBarrel(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckBarrel(collision);
    }
    private void CheckBarrel(Collision2D collision)
    {
        if (collision.collider.GetComponentInParent<Barrel>() == null) return;

        if (Mathf.Abs(collision.GetContact(0).normal.x) > 0.5f)
        {
            blockedByBarrel = true;
        }
    }
}
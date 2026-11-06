using UnityEngine;
public class FireballProjectile : MonoBehaviour
{
    [Header("Fireball Settings")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float maxDistance = 12f;
    private Rigidbody2D rb;
    private Vector2 startPosition;
    private bool hasHit;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        startPosition = transform.position;
        rb.linearVelocity = Vector2.right * speed;
    }
    private void Update()
    {
        float distance = Vector2.Distance(startPosition, transform.position);

        if (distance >= maxDistance)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit)
            return;

        PlayerLife playerLife =
            collision.GetComponentInParent<PlayerLife>();

        if (playerLife == null)
            return;

        hasHit = true;

        playerLife.TakeDamage(damage);

        Destroy(gameObject);
    }
}
using UnityEngine;
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float movementDistance = 3f;
    private Rigidbody2D rb;
    private EnemyFollow follow;
    private Animator animator;
    private float startX;
    private bool movingRight = true;
    private bool following;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        follow = GetComponent<EnemyFollow>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        startX = transform.position.x;
    }
    private void FixedUpdate()
    {
        if (follow != null && follow.PlayerSeen)
        {
            following = true;
            UpdateAnimation();
            return;
        }

        if (following)
        {
            following = false;
            startX = transform.position.x;
        }

        Patrol();
        UpdateAnimation();
    }
    private void Patrol()
    {
        if (movingRight && transform.position.x >= startX + movementDistance)
        {
            movingRight = false;
        }
        else if (!movingRight && transform.position.x <= startX - movementDistance)
        {
            movingRight = true;
        }

        float direction = movingRight ? 1f : -1f;

        rb.linearVelocity = new Vector2(
            direction * speed,
            rb.linearVelocity.y
        );
    }
    private void UpdateAnimation()
    {
        if (animator == null)
        {
            return;
        }

        float horizontalSpeed = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("Speed", horizontalSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Patrullando, si choca con un barril se da vuelta.
        if (!following && collision.collider.GetComponentInParent<Barrel>() != null)
        {
            movingRight = !movingRight;
        }
    }
}

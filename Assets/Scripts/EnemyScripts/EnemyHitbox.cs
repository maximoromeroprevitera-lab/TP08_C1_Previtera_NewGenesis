using UnityEngine;
public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] private float lifetime = 0.15f;
    private int damage;
    private float knockbackForce;
    private float knockbackUpForce;
    private Transform owner;
    private bool hasHit;
    private bool isInitialized;
    public void Setup(
        Transform attackOwner,
        int attackDamage,
        float force,
        float upForce)
    {
        owner = attackOwner;
        damage = attackDamage;
        knockbackForce = force;
        knockbackUpForce = upForce;
        isInitialized = true;
    }
    private void Start()
    {
        if (!isInitialized)
        {
            Debug.LogWarning("EnemyHitbox apareció sin Setup.", this);
            Destroy(gameObject);
            return;
        }
        Destroy(gameObject, lifetime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isInitialized || hasHit || owner == null)
        {
            return;
        }

        if (collision.transform == owner ||
            collision.transform.IsChildOf(owner))
        {
            return;
        }

        PlayerLife playerLife =
            collision.GetComponentInParent<PlayerLife>();

        if (playerLife == null || playerLife.IsDead)
        {
            return;
        }

        hasHit = true;

        playerLife.TakeDamage(damage);

        PlayerKnockback playerKnockback =
            playerLife.GetComponent<PlayerKnockback>();

        float direction = Mathf.Sign(
            playerLife.transform.position.x - owner.position.x
        );

        if (direction == 0f)
        {
            direction = 1f;
        }

        Vector2 force = new Vector2(
            direction * knockbackForce,
            knockbackUpForce
        );

        if (playerKnockback != null)
        {
            playerKnockback.ApplyKnockback(force);
        }
        else
        {
            Debug.LogWarning(
                "El jugador no tiene el componente PlayerKnockback.",
                playerLife
            );
        }
        Destroy(gameObject);
    }
}
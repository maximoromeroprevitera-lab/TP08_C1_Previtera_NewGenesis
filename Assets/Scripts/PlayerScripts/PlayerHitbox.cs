using UnityEngine;
public class PlayerHitbox : MonoBehaviour
{
    [SerializeField] private float lifetime = 0.15f;
    private int damage;
    private bool hasHit;
    private bool isInitialized;
    private PlayerPickableEffects pickableEffects;
    public void Setup(int attackDamage)
    {
        damage = attackDamage;
        isInitialized = true;
    }
    private void Start()
    {
        if (!isInitialized)
        {
            Destroy(gameObject);
            return;
        }

        pickableEffects = FindFirstObjectByType<PlayerPickableEffects>();

        Destroy(gameObject, lifetime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isInitialized || hasHit)
            return;

        EnemyLife enemyLife =
            collision.GetComponentInParent<EnemyLife>();

        if (enemyLife == null || enemyLife.IsDead)
            return;

        hasHit = true;

        int finalDamage = damage;

        if (pickableEffects != null)
            finalDamage += pickableEffects.BonusDamage;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayHit();

        enemyLife.TakeDamage(finalDamage);

        Destroy(gameObject);
    }
}
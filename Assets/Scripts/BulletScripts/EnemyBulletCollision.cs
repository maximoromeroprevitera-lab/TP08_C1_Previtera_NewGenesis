using UnityEngine;
public class EnemyBulletCollision : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bala enemiga tocó: " + collision.name); // TEMPORAL, borrar después de probar

        Barrel barrel = collision.GetComponentInParent<Barrel>();

        if (barrel != null)
        {
            barrel.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }
        PlayerLife player = collision.GetComponentInParent<PlayerLife>();

        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
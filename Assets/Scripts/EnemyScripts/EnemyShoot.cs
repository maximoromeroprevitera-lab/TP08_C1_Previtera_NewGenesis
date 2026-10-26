using UnityEngine;
public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float bulletLifetime = 2f;
    [SerializeField] private float bulletSpeed = 8f;
    [SerializeField] private float shootCooldown = 1.5f;
    private EnemyFollow follow;
    private EnemyAmmo ammo;
    private Animator animator;
    private Transform player;
    private float cooldownTimer;
    private void Awake()
    {
        follow = GetComponent<EnemyFollow>();
        ammo = GetComponent<EnemyAmmo>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        PlayerMovement playerMovement = FindFirstObjectByType<PlayerMovement>();

        if (playerMovement != null)
        {
            player = playerMovement.transform;
        }

        cooldownTimer = shootCooldown;
    }
    private void Update()
    {
        if (player == null)
        {
            return;
        }

        if (!follow.PlayerInShootRange || !ammo.CanShoot)
        {
            return;
        }

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (cooldownTimer <= 0)
        {
            animator.SetTrigger("Shoot");

            Shoot();
            ammo.UseBullet();

            cooldownTimer = shootCooldown;
        }
    }
    private void Shoot()
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            shootPoint.position,
            shootPoint.rotation
        );

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = player.position - shootPoint.position;
        direction.Normalize();

        bulletRb.linearVelocity = direction * bulletSpeed;

        Destroy(bullet, bulletLifetime);
    }
}

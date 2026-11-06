using System.Collections.Generic;
using UnityEngine;
public class TrapLauncher : MonoBehaviour
{
    [Header("Trap Settings")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireInterval = 2f;
    private bool isActive = true;
    private List<FireballProjectile> activeFireballs = new List<FireballProjectile>();
    private void Start()
    {
        InvokeRepeating(nameof(Shoot), fireInterval, fireInterval);
    }
    private void Shoot()
    {
        if (!isActive || fireballPrefab == null)
            return;

        activeFireballs.RemoveAll(fireball => fireball == null);

        Vector3 spawnPosition = firePoint != null
            ? firePoint.position
            : transform.position;

        GameObject newFireball = Instantiate(
            fireballPrefab,
            spawnPosition,
            Quaternion.identity
        );

        FireballProjectile projectile =
            newFireball.GetComponent<FireballProjectile>();

        if (projectile != null)
            activeFireballs.Add(projectile);
    }
    public void SetTrapActive(bool active)
    {
        isActive = active;

        if (active)
        {
            InvokeRepeating(
                nameof(Shoot),
                fireInterval,
                fireInterval
            );
        }
        else
        {
            CancelInvoke(nameof(Shoot));

            foreach (FireballProjectile fireball in activeFireballs)
            {
                if (fireball != null)
                    Destroy(fireball.gameObject);
            }

            activeFireballs.Clear();
        }
    }
}
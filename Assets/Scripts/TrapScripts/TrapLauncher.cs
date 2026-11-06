using UnityEngine;
public class TrapLauncher : MonoBehaviour
{
    [Header("Trap Settings")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireInterval = 2f;
    private bool isActive = true;
    private void Start()
    {
        InvokeRepeating(nameof(Shoot), fireInterval, fireInterval);
    }
    private void Shoot()
    {
        if (!isActive || fireballPrefab == null)
            return;

        Vector3 spawnPosition = firePoint != null
            ? firePoint.position
            : transform.position;

        Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
    }
    public void SetTrapActive(bool active)
    {
        isActive = active;

        if (active)
            InvokeRepeating(nameof(Shoot), fireInterval, fireInterval);
        else
            CancelInvoke(nameof(Shoot));
    }
}
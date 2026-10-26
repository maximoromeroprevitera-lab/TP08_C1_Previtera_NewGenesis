using UnityEngine;
public class EnemyAmmo : MonoBehaviour
{
    [SerializeField] private int magazineSize = 4;
    [SerializeField] private float reloadTime = 4f;
    private int currentBullets;
    private bool reloading;
    private float reloadTimer;
    public bool CanShoot => currentBullets > 0 && !reloading;
    private void Start()
    {
        currentBullets = magazineSize;
    }
    private void Update()
    {
        if (!reloading) return;

        reloadTimer -= Time.deltaTime;

        if (reloadTimer <= 0)
        {
            reloading = false;
            currentBullets = magazineSize;
        }
    }
    public void UseBullet()
    {
        currentBullets--;

        if (currentBullets <= 0)
        {
            reloading = true;
            reloadTimer = reloadTime;
        }
    }
}
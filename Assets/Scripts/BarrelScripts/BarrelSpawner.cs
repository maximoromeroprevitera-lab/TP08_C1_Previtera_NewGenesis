using UnityEngine;
public class BarrelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject barrelPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnY = -3f;
    [SerializeField] private float spawnTime = 15f;
    [SerializeField] private int maxBarrels = 2;
    [SerializeField] private float minDistanceFromPlayer = 3f;
    [SerializeField] private float maxDistanceFromPlayer = 9f;
    [SerializeField] private float clearance = 1.5f;
    [SerializeField] private float despawnDistance = 25f;
    private float timer;
    private void Start()
    {
        SpawnBarrel();
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTime)
        {
            timer = 0f;
            SpawnBarrel();
        }
    }
    private void SpawnBarrel()
    {
        if (player == null) return;

        if (CountBarrelsNearPlayer() >= maxBarrels) return;

        for (int i = 0; i < 10; i++)
        {
            float x = player.position.x + Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);

            if (IsFree(x))
            {
                Instantiate(barrelPrefab, new Vector2(x, spawnY), Quaternion.identity);
                return;
            }
        }
    }
    private int CountBarrelsNearPlayer()
    {
        int count = 0;

        foreach (Barrel barrel in FindObjectsByType<Barrel>(FindObjectsSortMode.None))
        {
            if (Mathf.Abs(barrel.transform.position.x - player.position.x) > despawnDistance)
            {
                Destroy(barrel.gameObject);
            }
            else
            {
                count++;
            }
        }
        return count;
    }
    private bool IsFree(float x)
    {
        foreach (Barrel barrel in FindObjectsByType<Barrel>(FindObjectsSortMode.None))
        {
            if (Mathf.Abs(barrel.transform.position.x - x) < clearance)
            {
                return false;
            }
        }
        foreach (EnemyLife enemy in FindObjectsByType<EnemyLife>(FindObjectsSortMode.None))
        {
            if (Mathf.Abs(enemy.transform.position.x - x) < clearance)
            {
                return false;
            }
        }
        return true;
    }
}
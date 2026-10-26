using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnY = -2.94f;
    [SerializeField] private float spawnTime = 5f;
    [SerializeField] private int maxEnemies = 2;
    [SerializeField] private float minDistanceFromPlayer = 6f;
    [SerializeField] private float maxDistanceFromPlayer = 13f;
    [SerializeField] private float minDistance = 7f;
    [SerializeField] private float barrelClearance = 1.5f;
    [SerializeField] private float despawnDistance = 25f;
    private float timer;
    private void Start()
    {
        SpawnEnemies();
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTime)
        {
            timer = 0f;
            SpawnEnemies();
        }
    }
    private void SpawnEnemies()
    {
        if (player == null) return;

        int free = maxEnemies - CountEnemiesNearPlayer();

        if (free <= 0) return;

        // A veces 1 y a veces 2, sin pasar del máximo.
        int amount = Random.Range(1, free + 1);

        for (int i = 0; i < amount; i++)
        {
            SpawnEnemy();
        }
    }
    private int CountEnemiesNearPlayer()
    {
        int count = 0;

        foreach (EnemyLife enemy in FindObjectsByType<EnemyLife>(FindObjectsSortMode.None))
        {
            if (Mathf.Abs(enemy.transform.position.x - player.position.x) > despawnDistance)
            {
                Destroy(enemy.gameObject);
            }
            else
            {
                count++;
            }
        }

        return count;
    }
    private void SpawnEnemy()
    {
        for (int i = 0; i < 10; i++)
        {
            float x = player.position.x + Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);

            if (IsFree(x))
            {
                Instantiate(enemyPrefab, new Vector2(x, spawnY), Quaternion.identity);
                return;
            }
        }
    }
    private bool IsFree(float x)
    {
        foreach (EnemyLife enemy in FindObjectsByType<EnemyLife>(FindObjectsSortMode.None))
        {
            if (Mathf.Abs(enemy.transform.position.x - x) < minDistance)
            {
                return false;
            }
        }
        foreach (Barrel barrel in FindObjectsByType<Barrel>(FindObjectsSortMode.None))
        {
            if (Mathf.Abs(barrel.transform.position.x - x) < barrelClearance)
            {
                return false;
            }
        }
        return true;
    }
}
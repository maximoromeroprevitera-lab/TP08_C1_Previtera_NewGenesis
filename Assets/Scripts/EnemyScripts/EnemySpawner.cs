using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;
    [Header("Spawn Settings")]
    [SerializeField] private float spawnY = -2.94f;
    [SerializeField] private float spawnTime = 5f;
    [SerializeField] private int maxEnemies = 2;
    [SerializeField] private float minDistanceFromPlayer = 6f;
    [SerializeField] private float maxDistanceFromPlayer = 13f;
    [SerializeField] private float minDistance = 7f;
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
        if (player == null || enemyPrefab == null)
        {
            return;
        }

        int currentEnemies = FindObjectsByType<EnemyLife>(
            FindObjectsSortMode.None
        ).Length;

        int free = maxEnemies - currentEnemies;

        if (free <= 0)
        {
            return;
        }

        int amount = Random.Range(1, free + 1);

        for (int i = 0; i < amount; i++)
        {
            SpawnEnemy();
        }
    }
    private void SpawnEnemy()
    {
        for (int i = 0; i < 10; i++)
        {
            float x = player.position.x + Random.Range(
                minDistanceFromPlayer,
                maxDistanceFromPlayer
            );

            if (IsFree(x))
            {
                Instantiate(
                    enemyPrefab,
                    new Vector2(x, spawnY),
                    Quaternion.identity
                );

                return;
            }
        }
    }
    private bool IsFree(float x)
    {
        foreach (EnemyLife enemy in FindObjectsByType<EnemyLife>(
            FindObjectsSortMode.None
        ))
        {
            if (Mathf.Abs(enemy.transform.position.x - x) < minDistance)
            {
                return false;
            }
        }
        return true;
    }
}
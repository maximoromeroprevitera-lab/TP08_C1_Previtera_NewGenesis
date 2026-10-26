using UnityEngine;
public class HealthPickableSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pickablePrefab;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnY = -3.5f;
    [SerializeField] private float spawnTime = 30f;
    [SerializeField] private int maxPickables = 1;
    [SerializeField] private float minDistanceFromPlayer = 4f;
    [SerializeField] private float maxDistanceFromPlayer = 10f;
    [SerializeField] private float clearance = 1.5f;
    [SerializeField] private float despawnDistance = 25f;
    private float timer;
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTime)
        {
            timer = 0f;
            SpawnPickable();
        }
    }
    private void SpawnPickable()
    {
        if (player == null) return;

        if (CountPickablesNearPlayer() >= maxPickables) return;

        for (int i = 0; i < 10; i++)
        {
            float x = player.position.x + Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);

            if (IsFree(x))
            {
                Instantiate(pickablePrefab, new Vector2(x, spawnY), Quaternion.identity);
                return;
            }
        }
    }
    private int CountPickablesNearPlayer()
    {
        int count = 0;

        foreach (HealthPickable pickable in FindObjectsByType<HealthPickable>(FindObjectsSortMode.None))
        {
            if (Mathf.Abs(pickable.transform.position.x - player.position.x) > despawnDistance)
            {
                Destroy(pickable.gameObject);
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
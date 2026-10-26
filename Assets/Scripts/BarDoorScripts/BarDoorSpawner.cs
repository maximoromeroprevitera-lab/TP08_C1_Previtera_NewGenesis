using UnityEngine;
public class BarDoorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private float timeToAppear = 100f;
    [SerializeField] private float distanceFromPlayer = 14f;
    [SerializeField] private float spawnY = -3f;
    private float timer;
    private bool doorSpawned;
    private void Update()
    {
        if (doorSpawned) return;

        timer += Time.deltaTime;

        if (timer >= timeToAppear && player != null)
        {
            doorSpawned = true;
            Instantiate(doorPrefab, new Vector2(player.position.x + distanceFromPlayer, spawnY), Quaternion.identity);
        }
    }
}
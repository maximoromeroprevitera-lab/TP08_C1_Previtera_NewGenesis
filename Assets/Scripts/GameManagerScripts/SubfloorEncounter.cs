using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
public class SubfloorEncounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LevelDataSO levelData;
    [Header("Enemy Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [Header("Doors")]
    [SerializeField] private TileDoor[] doorsToOpen;
    [Header("Final Door")]
    [SerializeField] private GameObject finalDoorPrefab;
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Vector3Int finalDoorCell = new Vector3Int(-19, -34, 0);
    private int remainingEnemies;
    private bool encounterStarted;
    private bool encounterCompleted;
    public void StartEncounter()
    {
        if (encounterStarted)
            return;

        encounterStarted = true;
        StartCoroutine(SpawnEnemies());
    }
    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(levelData.spawnDelay);

        int enemyCount = Random.Range(
            levelData.minimumEnemies,
            levelData.maximumEnemies + 1
        );

        remainingEnemies = enemyCount;

        for (int i = 0; i < enemyCount; i++)
        {
            if (enemyPrefab == null || spawnPoints.Length == 0)
                break;

            Transform spawnPoint = spawnPoints[i % spawnPoints.Length];

            GameObject enemy = Instantiate(
                enemyPrefab,
                spawnPoint.position,
                Quaternion.identity
            );

            EnemyLife enemyLife = enemy.GetComponent<EnemyLife>();

            if (enemyLife != null)
            {
                enemyLife.OnDied += EnemyDefeated;
            }
            else
            {
                Debug.LogError("The enemy prefab needs EnemyLife!", enemy);
                remainingEnemies--;
            }
        }

        CheckEnemiesRemaining();
    }
    private void EnemyDefeated()
    {
        remainingEnemies--;
        CheckEnemiesRemaining();
    }
    private void CheckEnemiesRemaining()
    {
        if (remainingEnemies > 0 || encounterCompleted)
            return;

        encounterCompleted = true;

        foreach (TileDoor door in doorsToOpen)
        {
            if (door != null)
                door.OpenDoor();
        }

        if (finalDoorPrefab != null && groundTilemap != null)
        {
            Vector3 doorPosition =
                groundTilemap.GetCellCenterWorld(finalDoorCell);

            Instantiate(
                finalDoorPrefab,
                doorPosition,
                Quaternion.identity
            );
        }
    }
}
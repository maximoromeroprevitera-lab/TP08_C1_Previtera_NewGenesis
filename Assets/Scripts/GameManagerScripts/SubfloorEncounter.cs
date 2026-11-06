using System.Collections;
using UnityEngine;
public class SubfloorEncounter : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int minimumEnemies = 4;
    [SerializeField] private int maximumEnemies = 5;
    [Header("Timing")]
    [SerializeField] private float spawnDelay = 3f;
    [Header("Doors")]
    [SerializeField] private TileDoor[] doorsToOpen;
    [SerializeField] private GameObject finalDoor;
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
        yield return new WaitForSeconds(spawnDelay);

        int enemyCount = Random.Range(
            minimumEnemies,
            maximumEnemies + 1
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

        if (finalDoor != null)
            finalDoor.SetActive(true);

        Debug.Log("Encounter completed!");
    }
}
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
public class LevelDataSO : ScriptableObject
{
    [Header("Enemy Settings")]
    public int minimumEnemies = 4;
    public int maximumEnemies = 5;

    [Header("Encounter Settings")]
    public float spawnDelay = 3f;
}
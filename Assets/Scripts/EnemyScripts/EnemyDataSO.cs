using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyDataSO : ScriptableObject
{
    [Header("Score")]
    public int scoreReward = 100;
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float stopDistance = 2.2f;
    [Header("Detection")]
    public float detectionDistance = 7f;
    [Header("Combat")]
    public int maxHealth = 6;
    public int attackDamage = 1;
    public float attackCooldown = 2f;
    public float knockbackForce = 7f;
}
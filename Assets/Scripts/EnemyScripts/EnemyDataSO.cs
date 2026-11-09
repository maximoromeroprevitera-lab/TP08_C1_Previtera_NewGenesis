using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyDataSO : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float stopDistance = 1.5f;
    [Header("Detection")]
    public float detectionDistance = 10f;
    [Header("Combat")]
    public int maxHealth = 3;
    public int attackDamage = 1;
    public float attackCooldown = 1.5f;
    [Header("Attack Timing")]
    public float firstHitDelay = 0.55f;
    public float firstAttackDuration = 1.1f;
    public float secondHitDelay = 0.55f;
    public float secondAttackDuration = 1.1f;
    [Header("Knockback")]
    public float knockbackForce = 7f;
    public float knockbackUpForce = 2f;
    [Header("Flash")]
    public float flashDuration = 0.1f;
    [Header("Score")]
    public int scoreReward = 100;
}
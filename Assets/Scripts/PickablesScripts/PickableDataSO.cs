using UnityEngine;

[CreateAssetMenu(fileName = "PickableData", menuName = "Game/Pickable Data")]
public class PickableDataSO : ScriptableObject
{
    [Header("Heal")]
    public int healAmount = 1;

    [Header("Damage")]
    public int bonusDamage = 1;

    [Header("Invulnerability")]
    public float invulnerabilityDuration = 5f;

    [Header("Effect Settings")]
    public float effectDuration = 5f;
    public float dropChance = 0.5f;
}
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    [Header("Combat")]
    public int maxHealth = 6;
    [Header("Attack Damage")]
    public int attack1Damage = 1;
    public int attack2Damage = 1;
    public int attack3Damage = 2;
    [Header("Attack Timing")]
    public float attack1Delay = 0.2f;
    public float attack2Delay = 0.2f;
    public float attack3Delay = 0.25f;
    public float comboResetTime = 1f;
    [Header("BackDash")]
    public float doubleTapTime = 2f;
    public float shortDashDistance = 1.5f;
    public float fullDashDistance = 3f;
    public float shortDashDuration = 0.25f;
    public float fullDashDuration = 0.4f;
    [Header("Roll")]
    public float rollSpeed = 7f;
    public float rollDuration = 0.5f;
    [Header("Defense")]
    public float invulnerabilityDuration = 1f;
    public float knockbackDuration = 0.2f;
}
using System.Collections;
using UnityEngine;
public class PlayerPickableEffects : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PickableDataSO pickableData;
    [SerializeField] private PlayerLife playerLife;
    [SerializeField] private PlayerInvulnerability playerInvulnerability;
    private bool isPickupLocked;
    private Coroutine damageCoroutine;
    public int BonusDamage { get; private set; }
    public bool CanCollect => !isPickupLocked;
    private void Awake()
    {
        if (playerLife == null)
            playerLife = GetComponent<PlayerLife>();

        if (playerInvulnerability == null)
            playerInvulnerability = GetComponent<PlayerInvulnerability>();
    }
    public bool TryCollectHeal()
    {
        if (!CanCollect || playerLife == null || pickableData == null)
            return false;

        if (playerLife.CurrentHealth >= playerLife.MaxHealth)
            return false;

        playerLife.Heal(pickableData.healAmount);

        StartPickupLock(pickableData.effectDuration);

        return true;
    }
    public bool TryCollectDamageBoost()
    {
        if (!CanCollect || pickableData == null)
            return false;

        BonusDamage = pickableData.bonusDamage;

        damageCoroutine = StartCoroutine(ResetDamageBoost());

        StartPickupLock(pickableData.effectDuration);

        return true;
    }
    public bool TryCollectInvincibility()
    {
        if (!CanCollect || pickableData == null ||
            playerInvulnerability == null)
            return false;

        playerInvulnerability.ActivateInvulnerability(
            pickableData.invulnerabilityDuration
        );

        float lockDuration = Mathf.Max(
            pickableData.effectDuration,
            pickableData.invulnerabilityDuration
        );

        StartPickupLock(lockDuration);

        return true;
    }
    private IEnumerator ResetDamageBoost()
    {
        yield return new WaitForSeconds(pickableData.effectDuration);

        BonusDamage = 0;
        damageCoroutine = null;
    }
    private void StartPickupLock(float duration)
    {
        isPickupLocked = true;
        StartCoroutine(UnlockPickups(duration));
    }
    private IEnumerator UnlockPickups(float duration)
    {
        yield return new WaitForSeconds(duration);

        isPickupLocked = false;
    }
}
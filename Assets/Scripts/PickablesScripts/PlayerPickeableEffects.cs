using System.Collections;
using UnityEngine;
using TMPro;
public class PlayerPickableEffects : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PickableDataSO pickableData;
    [SerializeField] private PlayerLife playerLife;
    [SerializeField] private PlayerInvulnerability playerInvulnerability;
    [Header("UI")]
    [SerializeField] private TMP_Text pickableText;
    [SerializeField] private float messageDuration = 5f;
    private Coroutine messageCoroutine;
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
        if (pickableText != null)
            pickableText.gameObject.SetActive(false);
    }
    private void ShowMessage(string message)
    {
        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);

        messageCoroutine = StartCoroutine(ShowMessageCoroutine(message));
    }
    private IEnumerator ShowMessageCoroutine(string message)
    {
        pickableText.gameObject.SetActive(true);
        pickableText.text = message;

        yield return new WaitForSeconds(messageDuration);

        pickableText.gameObject.SetActive(false);
        messageCoroutine = null;
    }
    private IEnumerator ShowTimerMessage(string effectName, float duration)
    {
        pickableText.gameObject.SetActive(true);

        float remainingTime = duration;

        while (remainingTime > 0)
        {
            pickableText.text = "Te quedan " + Mathf.CeilToInt(remainingTime) + " segundos " + effectName;

            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }

        pickableText.gameObject.SetActive(false);
        messageCoroutine = null;
    }
    public bool TryCollectHeal()
    {
        if (!CanCollect || playerLife == null || pickableData == null)
            return false;

        if (playerLife.CurrentHealth >= playerLife.MaxHealth)
            return false;

        playerLife.Heal(pickableData.healAmount);
        ShowMessage("¡Recuperaste vida!");
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
        messageCoroutine = StartCoroutine(
    ShowTimerMessage("de más daño", pickableData.effectDuration)
);  

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
        messageCoroutine = StartCoroutine(
    ShowTimerMessage("de invencibilidad", pickableData.invulnerabilityDuration)
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
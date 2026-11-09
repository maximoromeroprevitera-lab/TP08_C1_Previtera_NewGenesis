using System.Collections;
using UnityEngine;
public class EnemyFlash : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyDataSO enemyData;
    [Header("Flash Settings")]
    [SerializeField] private Color flashColor = Color.white;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Coroutine flashCoroutine;
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }
    public void Flash()
    {
        if (spriteRenderer == null)
            return;

        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(FlashRoutine());
    }
    private IEnumerator FlashRoutine()
    {
        spriteRenderer.color = flashColor;

        yield return new WaitForSeconds(enemyData.flashDuration);

        spriteRenderer.color = originalColor;
        flashCoroutine = null;
    }
}
using System.Collections;
using UnityEngine;
public class PlayerKnockback : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerData;
    private Rigidbody2D rb;
    private Coroutine knockbackCoroutine;
    public bool IsKnockedBack { get; private set; }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void ApplyKnockback(Vector2 force)
    {
        if (knockbackCoroutine != null)
        {
            StopCoroutine(knockbackCoroutine);
        }

        knockbackCoroutine = StartCoroutine(KnockbackRoutine(force));
    }
    private IEnumerator KnockbackRoutine(Vector2 force)
    {
        IsKnockedBack = true;

        rb.linearVelocity = Vector2.zero;
        rb.linearVelocity = force;

        yield return new WaitForSeconds(playerData.knockbackDuration);

        IsKnockedBack = false;
        knockbackCoroutine = null;
    }
}
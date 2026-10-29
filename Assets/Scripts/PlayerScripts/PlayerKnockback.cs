using UnityEngine;
using System.Collections;
public class PlayerKnockback : MonoBehaviour
{
    [SerializeField] private float knockbackDuration = 0.2f;
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

        yield return new WaitForSeconds(knockbackDuration);

        IsKnockedBack = false;
        knockbackCoroutine = null;
    }
}
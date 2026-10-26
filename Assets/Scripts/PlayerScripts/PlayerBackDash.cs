using System.Collections;
using UnityEngine;
public class PlayerBackDash : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [Header("Double Tap")]
    [SerializeField] private float doubleTapTime = 1f;
    [Header("Dash")]
    [SerializeField] private float shortDashDistance = 1.5f;
    [SerializeField] private float fullDashDistance = 3f;
    [SerializeField] private float shortDashDuration = 0.25f;
    [SerializeField] private float fullDashDuration = 0.4f;
    private PlayerAttack playerAttack;
    private float lastShiftTime;
    private bool firstShift;
    private bool isDashing;
    public bool IsDashing => isDashing;
    private void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        playerAttack = GetComponent<PlayerAttack>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) &&
            !isDashing && !playerAttack.IsAttacking)
        {
            if (firstShift && Time.time - lastShiftTime <= doubleTapTime)
            {
                firstShift = false;
                StartCoroutine(Dash(fullDashDistance, fullDashDuration));
            }
            else
            {
                firstShift = true;
                StartCoroutine(Dash(shortDashDistance, shortDashDuration));
            }

            lastShiftTime = Time.time;
        }

        if (firstShift && Time.time - lastShiftTime > doubleTapTime)
        {
            firstShift = false;
        }
    }
    private IEnumerator Dash(float distance, float duration)
    {
        isDashing = true;
        animator.Play("BackDash", 0, 0f);

        float direction = spriteRenderer.flipX ? 1f : -1f;
        float dashSpeed = distance / duration;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            rb.linearVelocity = new Vector2(
                direction * dashSpeed,
                rb.linearVelocity.y
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        isDashing = false;
    }
}
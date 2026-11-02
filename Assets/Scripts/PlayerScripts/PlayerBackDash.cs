using System.Collections;
using UnityEngine;
public class PlayerBackDash : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [Header("Double Tap")]
    [SerializeField] private float doubleTapTime = 1f;
    [Header("BackDash")]
    [SerializeField] private float shortDashDistance = 1.5f;
    [SerializeField] private float fullDashDistance = 3f;
    [SerializeField] private float shortDashDuration = 0.25f;
    [SerializeField] private float fullDashDuration = 0.4f;
    private PlayerAttack playerAttack;
    private PlayerRoll playerRoll;
    private PlayerStateMachine stateMachine;
    private float lastShiftTime;
    private bool firstShift;
    private bool isDashing;
    public bool IsDashing => isDashing || playerRoll.IsRolling;
    private void Start()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        playerAttack = GetComponent<PlayerAttack>();
        playerRoll = GetComponent<PlayerRoll>();
        stateMachine = GetComponent<PlayerStateMachine>();
    }
    private void Update()
    {
        if (IsDashing || playerAttack.IsAttacking)
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            float direction = Input.GetAxisRaw("Horizontal");

            if (direction != 0f)
            {
                firstShift = false;
                playerRoll.StartRoll(direction);
            }
            else if (firstShift && Time.time - lastShiftTime <= doubleTapTime)
            {
                firstShift = false;
                StartCoroutine(BackDash(fullDashDistance, fullDashDuration));
            }
            else
            {
                firstShift = true;
                StartCoroutine(BackDash(shortDashDistance, shortDashDuration));
            }

            lastShiftTime = Time.time;
        }

        if (firstShift && Time.time - lastShiftTime > doubleTapTime)
            firstShift = false;
    }
    private IEnumerator BackDash(float distance, float duration)
    {
        isDashing = true;

        stateMachine.ChangeState(PlayerState.BackDash);
        animator.Play("BackDash", 0, 0f);
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySlide();

        float direction = spriteRenderer.flipX ? 1f : -1f;
        float speed = distance / duration;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            rb.linearVelocity = new Vector2(
                direction * speed,
                rb.linearVelocity.y
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        isDashing = false;
        stateMachine.ChangeState(PlayerState.Idle);
    }
}
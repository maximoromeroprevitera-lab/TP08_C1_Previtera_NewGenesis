using System.Collections;
using UnityEngine;
public class PlayerBackDash : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerDataSO playerData;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
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

        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

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
            else if (firstShift &&
                     Time.time - lastShiftTime <= playerData.doubleTapTime)
            {
                firstShift = false;

                StartCoroutine(BackDash(
                    playerData.fullDashDistance,
                    playerData.fullDashDuration
                ));
            }
            else
            {
                firstShift = true;

                StartCoroutine(BackDash(
                    playerData.shortDashDistance,
                    playerData.shortDashDuration
                ));
            }

            lastShiftTime = Time.time;
        }

        if (firstShift &&
            Time.time - lastShiftTime > playerData.doubleTapTime)
        {
            firstShift = false;
        }
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
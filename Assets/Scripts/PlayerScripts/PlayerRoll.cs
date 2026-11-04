using System.Collections;
using UnityEngine;
public class PlayerRoll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D playerCollider;
    [Header("Roll")]
    [SerializeField] private float rollSpeed = 5f;
    [SerializeField] private float rollDuration = 0.65f;
    [SerializeField] private Vector2 rollColliderSize = new Vector2(0.6f, 0.8f);
    [SerializeField] private Vector2 rollColliderOffset = new Vector2(0f, -0.3f);
    private Vector2 originalSize;
    private Vector2 originalOffset;
    private PlayerStateMachine stateMachine;
    private PlayerInvulnerability playerInvulnerability;
    public bool IsRolling { get; private set; }
    private void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (playerCollider == null) playerCollider = GetComponent<BoxCollider2D>();

        stateMachine = GetComponent<PlayerStateMachine>();
        playerInvulnerability = GetComponent<PlayerInvulnerability>();

        originalSize = playerCollider.size;
        originalOffset = playerCollider.offset;
    }
    public void StartRoll(float direction)
    {
        if (!IsRolling)
            StartCoroutine(Roll(direction));
    }
    private IEnumerator Roll(float direction)
    {
        IsRolling = true;

        stateMachine.ChangeState(PlayerState.Roll);
        animator.Play("Roll", 0, 0f);
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySlide();

        playerCollider.size = rollColliderSize;
        playerCollider.offset = rollColliderOffset;

        if (playerInvulnerability != null)
            playerInvulnerability.ActivateInvulnerability();

        float elapsedTime = 0f;

        while (elapsedTime < rollDuration)
        {
            rb.linearVelocity = new Vector2(
                direction * rollSpeed,
                rb.linearVelocity.y
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        playerCollider.size = originalSize;
        playerCollider.offset = originalOffset;

        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Roll") &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }

        IsRolling = false;
    }
}
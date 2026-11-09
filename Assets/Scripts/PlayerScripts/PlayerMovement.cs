using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerData;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform shootPoint;
    private Rigidbody2D rb;
    private PlayerAttack playerAttack;
    private PlayerBackDash playerBackDash;
    private PlayerStateMachine stateMachine;
    private PlayerKnockback playerKnockback;
    private float horizontalInput;
    private Vector3 originalShootPointPosition;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<PlayerAttack>();
        playerBackDash = GetComponent<PlayerBackDash>();
        stateMachine = GetComponent<PlayerStateMachine>();
        playerKnockback = GetComponent<PlayerKnockback>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (shootPoint != null)
            originalShootPointPosition = shootPoint.localPosition;
    }
    private void Update()
    {
        if (playerAttack.IsAttacking || playerBackDash.IsDashing)
        {
            horizontalInput = 0f;
            return;
        }
        if (stateMachine.IsStunned() || playerAttack.IsAttacking || playerBackDash.IsDashing)
        {
            horizontalInput = 0f;
            return;
        }
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
            UpdateShootPoint();
        }
        else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
            UpdateShootPoint();
        }

        UpdateMovementState();
    }
    private void UpdateShootPoint()
    {
        if (shootPoint == null) return;

        float direction = spriteRenderer.flipX ? -1f : 1f;

        shootPoint.localPosition = new Vector3(
            Mathf.Abs(originalShootPointPosition.x) * direction,
            originalShootPointPosition.y,
            originalShootPointPosition.z
        );
    }
    private void FixedUpdate()
    {
        if (playerKnockback != null && playerKnockback.IsKnockedBack)
            return;

        if (playerBackDash.IsDashing)
            return;

        if (playerAttack.IsAttacking)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        Move();
    }
    private void Move()
    {
        rb.linearVelocity = new Vector2(
            horizontalInput * playerData.moveSpeed,
            rb.linearVelocity.y
        );
    }
    private void UpdateMovementState()
    {
        if (stateMachine == null) return;

        if (rb.linearVelocity.y < -0.1f || rb.linearVelocity.y > 0.1f)
            stateMachine.ChangeState(PlayerState.Jump);
        else if (Mathf.Abs(horizontalInput) > 0)
            stateMachine.ChangeState(PlayerState.Run);
        else
            stateMachine.ChangeState(PlayerState.Idle);
    }
}
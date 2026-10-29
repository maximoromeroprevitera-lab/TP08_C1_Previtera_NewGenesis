using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerData;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerAttack playerAttack;
    private PlayerBackDash playerBackDash;
    private PlayerStateMachine stateMachine;
    private float horizontalInput;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        playerBackDash = GetComponent<PlayerBackDash>();
        stateMachine = GetComponent<PlayerStateMachine>();

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }
    private void Update()
    {
        if (playerAttack.IsAttacking || playerBackDash.IsDashing)
        {
            horizontalInput = 0f;
            animator.SetFloat("Speed", 0f);
            return;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }

        UpdateMovementState();
    }
    private void FixedUpdate()
    {
        if (playerBackDash.IsDashing)
        {
            return;
        }

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
        if (stateMachine == null)
        {
            return;
        }

        if (rb.linearVelocity.y < -0.1f ||
            rb.linearVelocity.y > 0.1f)
        {
            stateMachine.ChangeState(PlayerState.Jump);
        }
        else if (Mathf.Abs(horizontalInput) > 0)
        {
            stateMachine.ChangeState(PlayerState.Run);
        }
        else
        {
            stateMachine.ChangeState(PlayerState.Idle);
        }
    }
}
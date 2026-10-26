using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerData;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerAttack playerAttack;
    private PlayerBackDash playerBackDash;
    private float horizontalInput;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        playerBackDash = GetComponent<PlayerBackDash>();

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
    }
    private void FixedUpdate()
    {
        if (playerBackDash.IsDashing)
        {
            // No sobrescribir la velocidad del backdash.
            return;
        }

        if (playerAttack.IsAttacking)
        {
            // Detener el movimiento durante el ataque.
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
}

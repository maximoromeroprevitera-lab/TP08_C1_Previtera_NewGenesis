using UnityEngine;
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerData;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerAttack playerAttack;
    private PlayerBackDash playerBackDash;
    private bool isGrounded;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        playerAttack = GetComponent<PlayerAttack>();
        playerBackDash = GetComponent<PlayerBackDash>();
    }
    private void Update()
    {
        if (playerAttack.IsAttacking || playerBackDash.IsDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            AudioManager.Instance.PlayJump();

            animator.SetBool("Grounded", false);

            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                playerData.jumpForce
            );

            isGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>() != null)
        {
            isGrounded = true;
            animator.SetBool("Grounded", true);
        }
    }
}

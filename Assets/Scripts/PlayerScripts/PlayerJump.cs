using UnityEngine;
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerData;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D rb;
    private PlayerAttack playerAttack;
    private PlayerBackDash playerBackDash;
    private bool isGrounded;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<PlayerAttack>();
        playerBackDash = GetComponent<PlayerBackDash>();
    }
    private void Update()
    {
        if (playerAttack.IsAttacking || playerBackDash.IsDashing)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayJump();

            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                playerData.jumpForce
            );

            isGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer.value) != 0)
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer.value) != 0)
        {
            isGrounded = false;
        }
    }
}
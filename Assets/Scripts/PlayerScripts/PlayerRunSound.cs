using UnityEngine;
public class PlayerRunSound : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool wasRunning;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        bool isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isRunning = isMoving && isGrounded;

        if (isRunning && !wasRunning)
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayRun();
        }

        if (!isRunning && wasRunning)
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.StopRun();
        }

        wasRunning = isRunning;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>() != null)
            isGrounded = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>() != null)
            isGrounded = false;
    }
    private void OnDisable()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.StopRun();
    }
}
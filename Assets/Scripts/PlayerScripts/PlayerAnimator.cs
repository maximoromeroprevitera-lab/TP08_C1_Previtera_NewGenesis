using UnityEngine;
public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerStateMachine stateMachine;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<PlayerStateMachine>();
    }
    private void OnEnable()
    {
        stateMachine.OnStateChanged += PlayState;
    }
    private void OnDisable()
    {
        stateMachine.OnStateChanged -= PlayState;
    }
    private void PlayState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Idle:
                animator.CrossFade("idle", 0.05f);
                break;
            case PlayerState.Run:
                animator.CrossFade("Run", 0.05f);
                break;
            case PlayerState.Jump:
                animator.CrossFade("Jump", 0.05f);
                break;
            case PlayerState.Hurt:
                animator.Play("TakeHit", 0, 0f);
                break;
            case PlayerState.Death:
                animator.Play("Death", 0, 0f);
                break;
        }
    }
}
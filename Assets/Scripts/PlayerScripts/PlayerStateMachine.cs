using UnityEngine;
public enum PlayerState
{
    Idle,
    Run,
    Jump,
    Attack,
    BackDash,
    Roll,
    Defend,
    Hurt,
    Death
}
public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; }
    public event System.Action<PlayerState> OnStateChanged;

    private void Start()
    {
        ChangeState(PlayerState.Idle);
    }
    public void ChangeState(PlayerState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        OnStateChanged?.Invoke(newState);
    }

    public bool CanMove()
    {
        return CurrentState == PlayerState.Idle ||
               CurrentState == PlayerState.Run ||
               CurrentState == PlayerState.Jump;
    }
    public bool CanJump()
    {
        return CurrentState == PlayerState.Idle ||
               CurrentState == PlayerState.Run;
    }
    public bool CanAttack()
    {
        return CurrentState == PlayerState.Idle ||
               CurrentState == PlayerState.Run ||
               CurrentState == PlayerState.Jump;
    }
    public bool IsStunned()
    {
        return CurrentState == PlayerState.Hurt ||
               CurrentState == PlayerState.Death;
    }
}
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
    private void Start()
    {
        ChangeState(PlayerState.Idle);
    }
    public void ChangeState(PlayerState newState)
    {
        CurrentState = newState;
        Debug.Log("Player State: " + CurrentState);
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
}

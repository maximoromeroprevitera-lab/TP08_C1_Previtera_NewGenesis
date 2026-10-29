using UnityEngine;
public enum EnemyState
{
    Idle,
    Chase,
    Attack1,
    Attack2,
    Death
}
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public EnemyState CurrentState { get; private set; } = EnemyState.Idle;
    public bool IsAttacking
    {
        get
        {
            return CurrentState == EnemyState.Attack1 ||
                   CurrentState == EnemyState.Attack2;
        }
    }
    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
    private void Start()
    {
        if (animator == null)
        {
            Debug.LogError("EnemyStateMachine: falta asignar el Animator.", this);
            return;
        }

        animator.SetBool("IsMoving", false);
        animator.SetBool("IsDead", false);

        Debug.Log("Estado inicial del enemigo: " + CurrentState, this);
    }
    public void SetMovement(bool isMoving)
    {
        if (CurrentState == EnemyState.Death || IsAttacking)
        {
            return;
        }

        animator.SetBool("IsMoving", isMoving);

        CurrentState = isMoving
            ? EnemyState.Chase
            : EnemyState.Idle;
    }
    public void PlayAttack1()
    {
        if (CurrentState == EnemyState.Death || IsAttacking)
        {
            return;
        }

        animator.SetBool("IsMoving", false);
        CurrentState = EnemyState.Attack1;
        animator.SetTrigger("Attack1");
    }
    public void PlayAttack2()
    {
        if (CurrentState != EnemyState.Attack1)
        {
            return;
        }

        CurrentState = EnemyState.Attack2;
        animator.SetTrigger("Attack2");
    }
    public void FinishAttack()
    {
        if (!IsAttacking || CurrentState == EnemyState.Death)
        {
            return;
        }

        CurrentState = EnemyState.Idle;
        animator.SetBool("IsMoving", false);
    }
    public void Die()
    {
        Debug.Log("SE EJECUTO EnemyStateMachine.Die()", this);
        if (CurrentState == EnemyState.Death)
        {
            return;
        }

        CurrentState = EnemyState.Death;
        animator.SetBool("IsMoving", false);
        animator.SetBool("IsDead", true);
    }
}